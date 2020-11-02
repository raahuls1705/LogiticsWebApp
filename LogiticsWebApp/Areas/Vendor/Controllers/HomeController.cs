using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogiticsWebApp.Areas.Company.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LogiticsWebApp.Areas.Vendor.Controllers;

namespace LogiticsWebApp.Areas.Vendor.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}