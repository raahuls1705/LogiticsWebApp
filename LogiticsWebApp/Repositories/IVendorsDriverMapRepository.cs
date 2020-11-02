using LogiticsWebApp.Entities;
using LogiticsWebApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogiticsWebApp.Repositories
{
    public interface IVendorsDriverMapRepository 
    {
        VendorsDriverMap GetVendorDriverMap(string vendorId, string DriverId);
    }
}
