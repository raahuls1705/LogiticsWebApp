using LogiticsWebApp.Data;
using LogiticsWebApp.Entities;
using LogiticsWebApp.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace LogiticsWebApp.Repositories
{
    public class CompanySizeRepository : BaseRepository<CompanySize>, ICompanySizeRepository
    {
        private readonly LogisticDbContext _context;
        public CompanySizeRepository(LogisticDbContext context) : base(context)
        {
            _context = context;
        }

        public List<SelectListItem> GetCompanySizeDropDownList(string selectedValue = "")
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
