using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogiticsWebApp.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogiticsWebApp.Areas.Company.Controllers
{
   
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}