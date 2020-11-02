using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using LogiticsWebApp.Data;
using LogiticsWebApp.Repositories;
using LogiticsWebApp.Utilities.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace LogiticsWebApp.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;

        //private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            this.Input = new InputModel();
        }



        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }
        public string Role { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            public InputModel()
            {
                this.CompanySizeList = new List<SelectListItem>();
                this.IndustryTypeList = new List<SelectListItem>();
                this.Countries = new List<SelectListItem>();
            }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Company Name")]
            public string CompanyName { get; set; }
            [DataType(DataType.Text)]
            [Display(Name = "Address")]
            public string Address { get; set; }
            [DataType(DataType.Text)]
            [Display(Name = "City")]
            public string City { get; set; }
            [DataType(DataType.PostalCode)]
            [Display(Name = "Zip/Postal Code")]
            public string PostalCode { get; set; }
            [DataType(DataType.Text)]
            [Display(Name = "Country")]
            public string Country { get; set; }
            [DataType(DataType.PhoneNumber)]
            [Display(Name = "Phone")]
            public string Phone { get; set; }
            [DataType(DataType.Text)]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }
            [DataType(DataType.Text)]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }
            [DataType(DataType.Text)]
            [Display(Name = "Job Title")]
            public string JobTitle { get; set; }
            [DataType(DataType.Text)]
            [Display(Name = "Industry Type")]
            public List<SelectListItem> IndustryTypeList { get; set; }
            public int IndustryTypeId { get; set; }
            [DataType(DataType.Text)]
            [Display(Name = "Company Size")]
            public List<SelectListItem> CompanySizeList { get; set; }
            public int CompanySizeId { get; set; }
            [DataType(DataType.Text)]
            [Display(Name = "Some information about your current process")]
            public string CurrentProcessDescription { get; set; }
            [DataType(DataType.Text)]
            [Display(Name = "Challenges in your current process")]
            public string CurrentProcessChallenges { get; set; }
            [DataType(DataType.PhoneNumber)]
            [Display(Name = "Secondary Contact")]
            public string SecondaryContact { get; set; }
            [DataType(DataType.Text)]
            [Display(Name = "Countries vendor can cover")]
            public string CountriesCoverByVendor { get; set; }
            [DataType(DataType.Text)]
            [Display(Name = "Capacity")]
            public string Capacity { get; set; }
            public List<SelectListItem> Countries { get; set; }
        }

        public async Task OnGetAsync(string role, string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            Role = role ?? UserRoles.CompanyOwner.ToString();
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null, string role = "")
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    EmailConfirmed = true,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    SecondaryContact = Input.SecondaryContact,
                    Phone = Input.Phone,
                    Capacity = Input.Capacity,
                    CountriesCoverByVendor = Input.CountriesCoverByVendor,
                    Address = Input.Address,
                    City = Input.City,
                    CompanyName = Input.CompanyName,
                    CompanySizeId = Input.CompanySizeId,
                    CurrentProcessChallenges = Input.CurrentProcessChallenges,
                    CurrentProcessDescription = Input.CurrentProcessDescription,
                    JobTitle = Input.JobTitle,
                    PostalCode = Input.PostalCode,
                    IndustryTypeId = Input.IndustryTypeId,
                    PhoneNumber = Input.Phone
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                await _userManager.AddToRoleAsync(user, role);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            Role = role;
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
