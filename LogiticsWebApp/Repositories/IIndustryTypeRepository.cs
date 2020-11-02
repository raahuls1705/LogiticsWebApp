using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogiticsWebApp.Repositories
{
    public interface IIndustryTypeRepository 
    {
        List<SelectListItem> GetIndustryTypeDropDownList(string selectedValue = "");
    }
}
