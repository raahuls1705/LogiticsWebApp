using System;

namespace LogiticsWebApp.Entities
{
    public partial class VendorsDriverMap
    {
        public Guid Id { get; set; }
        public string VendorId { get; set; }
        public string DriverId { get; set; }
    }
}
