using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;
using LogiticsWebApp.Entities;
using LogiticsWebApp.Infrastructure;
using LogiticsWebApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace LogiticsWebApp.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<ErrorLog> _errorLogRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, 
            ILogger<HomeController> logger,
            IRepository<ErrorLog> errorLogRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _errorLogRepository = errorLogRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>();
            ViewData["statusCode"] = HttpContext.Response.StatusCode;
            ViewData["message"] = exception.Error.Message;
            ViewData["stackTrace"] = exception.Error.StackTrace;
            ErrorLog errorLog = new ErrorLog();
            errorLog.AddedDate = DateTime.Now;
            errorLog.FullMessage = exception.Error.Message;
            errorLog.ShortMessage = exception.Error.InnerException != null ? exception.Error.InnerException.Message : string.Empty;
            errorLog.IpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            errorLog.LogLevelId = 0;
            errorLog.PageUrl = "";
            errorLog.LoggedUserId = string.Empty;
            _errorLogRepository.Insert(errorLog);
            _errorLogRepository.Commit();
            return View();
        }



        

    }
}
