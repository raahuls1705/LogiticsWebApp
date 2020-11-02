using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogiticsWebApp.Areas.Company.Controllers
{
    [Area("Company")]
    [Authorize(Roles = "CompanyOwner")]
    public class BaseController : Controller
    {
    }
}