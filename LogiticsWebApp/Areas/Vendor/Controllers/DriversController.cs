using LogiticsWebApp.Areas.Vendor.Models;
using LogiticsWebApp.Data;
using LogiticsWebApp.Repositories;
using LogiticsWebApp.Utilities;
using LogiticsWebApp.Utilities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LogiticsWebApp.Areas.Vendor.Controllers
{
    public class DriversController : BaseController
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly INotificationService _notifictionService;
        private readonly VendorsDriverMapRepository _vendorDriverMapRepository;
        private readonly LogisticDbContext _db;
        public DriversController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUserRepository userRepository,
            INotificationService notificationService,
            VendorsDriverMapRepository vendorsDriverRepository,
            LogisticDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepository = userRepository;
            _notifictionService = notificationService;
            _vendorDriverMapRepository = vendorsDriverRepository;
            _db = db;
        }

        public IActionResult Index() => RedirectToAction(nameof(List));

        [HttpGet]
        public IActionResult List()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetDriversListAsync(int currentPageIndex,
            string search = "",
            int rows = 10)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            return PartialView("_DriversListView", _userRepository.GetDrivers(user.Id, currentPageIndex, search, rows));
        }

        [HttpGet]
        public IActionResult Create()
        {
            DriverViewModel driverViewModel = new DriverViewModel();
            return View(driverViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DriverViewModel driverViewModel)
        {
            if (ModelState.IsValid)
            {
                var driver = await _userManager.FindByEmailAsync(driverViewModel.Email);
                if (driver != null)
                {
                    ModelState.AddModelError(string.Empty, "Email already taken. Please try with another email.");
                    return View(driverViewModel);
                }
                var user = new ApplicationUser
                {
                    UserName = driverViewModel.Email,
                    Email = driverViewModel.Email,
                    EmailConfirmed = true,
                    FirstName = driverViewModel.FirstName,
                    LastName = driverViewModel.LastName,
                    PhoneNumber = driverViewModel.PhoneNumber,
                    Phone = driverViewModel.PhoneNumber
                };
                var result = await _userManager.CreateAsync(user, driverViewModel.Password);
                await _userManager.AddToRoleAsync(user, UserRoles.Driver.ToString());
                var vendor = await _userManager.FindByEmailAsync(User.Identity.Name);
                _userRepository.MapDriverToVendor(user.Id, vendor.Id);

                if (result.Succeeded)
                {
                    _notifictionService.SuccessNotification(CommonConstants.ItemAddedSuccessfully);
                    return RedirectToAction(nameof(List));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(driverViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            var driverViewModel = new DriverViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.Phone,
                Email = user.Email,
                Password = user.PasswordHash
            };
            return View(driverViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DriverViewModel driverViewModel)
        {
            ModelState.Remove(nameof(driverViewModel.Password));
            ModelState.Remove(nameof(driverViewModel.Email));
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(driverViewModel.Id);
                user.Phone = driverViewModel.PhoneNumber;
                user.PhoneNumber = driverViewModel.PhoneNumber;
                user.FirstName = driverViewModel.FirstName;
                user.LastName = driverViewModel.LastName;
                await _userManager.UpdateAsync(user);
                _notifictionService.SuccessNotification(CommonConstants.ItemUpdatedSuccessfully);
                return RedirectToAction(nameof(List));
            }
            return View(driverViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            var driverViewModel = new DriverViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.Phone,
                Email = user.Email,
                Password = user.PasswordHash,
                Id = id
            };
            return View(driverViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            var vendor = await _userManager.FindByEmailAsync(User.Identity.Name);
            var vendorDriverMap = _vendorDriverMapRepository.GetVendorDriverMap(vendor.Id, id);
            _vendorDriverMapRepository.Delete(vendorDriverMap.Id);
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return Json(new
                {
                    Message = CommonConstants.ItemDeletedSuccessfully,
                    Status = NotificationStatus.Success.ToString()
                });
            }
            else
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += error.Description + @", ";
                }
                errors = errors.TrimEnd(',');
                return Json(new
                {
                    Message = errors,
                    Status = NotificationStatus.Errors.ToString()
                });
            }
        }

        [HttpGet]
        public IActionResult ChangePassword(string username)
        {
            ResetPasswordModel resetPasswordModel = new ResetPasswordModel();
            resetPasswordModel.Email = username;
            return View(resetPasswordModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ResetPasswordModel resetPasswordModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);

                var result = await _userManager.ResetPasswordAsync(user, code, resetPasswordModel.Password);

                if (result.Succeeded)
                {
                    _notifictionService.SuccessNotification(CommonConstants.PasswordUpdatedSuccessfully);
                    return RedirectToAction(nameof(List));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(resetPasswordModel);
        }



    }
}
