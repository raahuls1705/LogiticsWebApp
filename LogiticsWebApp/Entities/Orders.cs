using System;

namespace LogiticsWebApp.Entities
{
    public partial class Orders
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public int StatusId { get; set; }
        public string TrackingNumber { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int ShippingDetailId { get; set; }
        public int PaymentDetailId { get; set; }
        public string Dimension { get; set; }
        public string Weight { get; set; }
        public string VendorId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
