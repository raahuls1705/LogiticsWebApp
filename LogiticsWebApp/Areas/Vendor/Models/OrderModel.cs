using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LogiticsWebApp.Areas.Vendor.Models
{
    public partial class OrderModel
    {
        public int Id { get; set; }
        [DisplayName("Order Number")]
        public string OrderNumber { get; set; }
        [DisplayName("Tracking Number")]
        public string TrackingNumber { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        [DisplayName("Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter location")]
        [DisplayName("Location")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Please enter email")]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DisplayName("Phone")]
        [Required(ErrorMessage = "Please enter phone")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [DisplayName("Shipping Detail")]
        [Required(ErrorMessage = "Please select shipping detail")]
        public int ShippingDetailId { get; set; }
        [Required(ErrorMessage = "Please select payement detail")]
        [DisplayName("Payment Detail")]
        public int PaymentDetailId { get; set; }
        [Required(ErrorMessage = "Please select status")]
        [DisplayName("Status")]
        public int StatusId { get; set; }
        [DisplayName("Dimension")]
        public string Dimension { get; set; }
        [DisplayName("Weight")]
        public string Weight { get; set; }
        public string VendorId { get; set; }
    }
}
