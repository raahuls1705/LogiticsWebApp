using LogiticsWebApp.Data;
using LogiticsWebApp.Entities;
using LogiticsWebApp.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogiticsWebApp.Repositories
{
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        private readonly LogisticDbContext _context;
        public CountryRepository(LogisticDbContext context) : base(context)
        {
            _context = context;
        }
        public List<SelectListItem> GetCountryDropDownList(string selectedValue = "")
        {
            var allCountries = GetAll();
            return (from c in allCountries
                    select new SelectListItem()
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    }).ToList();
        }

    }
}
