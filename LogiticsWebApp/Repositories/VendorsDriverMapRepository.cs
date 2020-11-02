using LogiticsWebApp.Data;
using LogiticsWebApp.Entities;
using LogiticsWebApp.Infrastructure;
using System.Linq;

namespace LogiticsWebApp.Repositories
{
    public class VendorsDriverMapRepository : BaseRepository<VendorsDriverMap>, IVendorsDriverMapRepository
    {
        private readonly LogisticDbContext _context;
        public VendorsDriverMapRepository(LogisticDbContext context) : base(context)
        {
            _context = context;
        }

        public VendorsDriverMap GetVendorDriverMap(string vendorId, string DriverId)
        {
            return _context.VendorsDriverMap.Where(a => a.VendorId == vendorId && a.DriverId == DriverId).FirstOrDefault();
        }
    }
}
