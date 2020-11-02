using LogiticsWebApp.Data;
using LogiticsWebApp.Entities;
using LogiticsWebApp.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace LogiticsWebApp.Repositories
{
    public class IndustryTypeRepository : BaseRepository<IndustryType>, IIndustryTypeRepository
    {
        private readonly LogisticDbContext _context;
        public IndustryTypeRepository(LogisticDbContext context) : base(context)
        {
            _context = context;
        }

        public List<SelectListItem> GetIndustryTypeDropDownList(string selectedValue = "")
        {
            var list = GetAll();
            return (from c in list
                    select new SelectListItem()
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    }).ToList();
        }
    }
}
