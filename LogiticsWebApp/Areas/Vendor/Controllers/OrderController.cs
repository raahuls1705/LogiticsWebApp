using System;
using System.Threading.Tasks;
using CSharpVitamins;
using LogiticsWebApp.Areas.Vendor.Models;
using LogiticsWebApp.Data;
using LogiticsWebApp.Entities;
using LogiticsWebApp.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LogiticsWebApp.Areas.Vendor.Controllers
{
    public class OrderController : BaseController
    {
        private readonly OrderRepository _orderRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;
        public OrderController(OrderRepository orderRepository,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public IActionResult Index() => RedirectToAction(nameof(List));

        [HttpGet]
        public IActionResult List()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersList(int currentPageIndex,
            string searchLocation = "",
            int statusId = 0,
            int paymentDetailId = 0,
            int shippingDetailId = 0,
            string orderNumber = "",
            string trackingNumber = "",
            int rows = 10)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);

            var orders = _orderRepository.GetOrders(user.Id, currentPageIndex, searchLocation, statusId, paymentDetailId, shippingDetailId, orderNumber, trackingNumber, rows);

            return PartialView("_OrdersListView", orders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            OrderModel order = new OrderModel();
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(OrderModel ordermodel)
        {
            if (ModelState.IsValid)
            {
                var trackingNumber = new ShortGuid(Guid.NewGuid());
                var orderNumber = new ShortGuid(Guid.NewGuid());
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                var userId = user.Id;
                Orders order = new Orders();
                order.TrackingNumber = trackingNumber.ToString().ToUpper();
                order.OrderNumber = orderNumber.ToString().ToUpper();
                order.VendorId = userId;
                order.CreatedBy = userId;
                order.CreatedOn = DateTime.Now;
                order.Name = ordermodel.Name;
                order.Location = ordermodel.Location;
                order.Email = ordermodel.Email;
                order.Phone = ordermodel.Phone;
                order.StatusId = ordermodel.StatusId;
                order.ShippingDetailId = ordermodel.ShippingDetailId;
                order.PaymentDetailId = ordermodel.PaymentDetailId;
                order.Weight = ordermodel.Weight;
                order.Dimension = ordermodel.Dimension;
                _orderRepository.Insert(order);
                _orderRepository.Commit();
                return RedirectToAction(nameof(List));
            }
            return View(ordermodel);
        }
    }
}