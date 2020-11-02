using LogiticsWebApp.Areas.Vendor.Models;
using LogiticsWebApp.Data;
using LogiticsWebApp.Entities;
using LogiticsWebApp.Infrastructure;
using LogiticsWebApp.Utilities.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogiticsWebApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly LogisticDbContext _context;
        private readonly VendorsDriverMapRepository _vendorDriverMapRepository;

        public UserRepository(LogisticDbContext context,
            VendorsDriverMapRepository repository,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _vendorDriverMapRepository = repository;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public List<AspNetUsers> GetAllDrivers()
        {
            var query = (from ur in _context.AspNetUserRoles
                         join r in _context.AspNetRoles on ur.RoleId equals r.Id
                         join u in _context.AspNetUsers on ur.UserId equals u.Id
                         where r.Name == UserRoles.Driver.ToString()
                         select u);

            var list = query.ToList();
            return list;
        }

        public GridModel<DriverViewModel> GetDrivers(string vendorId, int currentPage, string search, int rows)
        {
            int maxRows = rows;
            var driverGridModel = new GridModel<DriverViewModel>();

            var driverIds = _context.VendorsDriverMap.Where(a => a.VendorId == vendorId).Select(a => a.DriverId).AsQueryable();

            var query = (from ur in _context.AspNetUserRoles
                         join r in _context.AspNetRoles on ur.RoleId equals r.Id
                         join u in _context.AspNetUsers on ur.UserId equals u.Id
                         where r.Name == UserRoles.Driver.ToString() &&
                         driverIds.Contains(u.Id)
                         select u);


            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                search = search.ToLower();
                query = (from u in query
                         where search.Contains(u.FirstName) ||
                         search.Contains(u.LastName) ||
                         search.Contains(u.Email) ||
                         search.Contains(u.PhoneNumber)
                         select u);
            }

            var data = query
                        .OrderBy(customer => customer.Email)
                        .Skip((currentPage - 1) * maxRows)
                        .Take(maxRows).Select(a => new DriverViewModel
                        {
                            FirstName = a.FirstName,
                            LastName = a.LastName,
                            Email = a.Email,
                            PhoneNumber = a.PhoneNumber,
                            Id = a.Id
                        }).ToList();

            driverGridModel.TotalCount = query.Count();

            double pageCount = (double)((decimal)query.Count() / Convert.ToDecimal(maxRows));
            driverGridModel.PageCount = (int)Math.Ceiling(pageCount);

            driverGridModel.CurrentPageIndex = currentPage;
            driverGridModel.SelectedRow = rows.ToString();
            driverGridModel.List = data;
            return driverGridModel;
        }

        public void MapDriverToVendor(string driverId, string vendorId)
        {
            VendorsDriverMap vendorsDriverMap = new VendorsDriverMap()
            {
                DriverId = driverId,
                VendorId = vendorId
            };
            _vendorDriverMapRepository.Insert(vendorsDriverMap);
            _vendorDriverMapRepository.Commit();
        }
    }
}
