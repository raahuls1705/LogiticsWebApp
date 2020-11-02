using LogiticsWebApp.Areas.Vendor.Models;
using LogiticsWebApp.Data;
using LogiticsWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogiticsWebApp.Repositories
{
    public interface IUserRepository
    {
        List<AspNetUsers> GetAllDrivers();
        GridModel<DriverViewModel> GetDrivers(string vendorId, int currentPage, string search, int rows);
        void MapDriverToVendor(string driverId, string vendorId);
    }
}
