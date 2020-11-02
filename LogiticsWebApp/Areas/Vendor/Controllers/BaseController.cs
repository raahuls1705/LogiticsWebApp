using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogiticsWebApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogiticsWebApp.Areas.Vendor.Controllers
{
    [Area("Vendor")]
    [Authorize(Roles = "Vendor")]
    public class BaseController : Controller
    {
       
    }
}