using LogiticsWebApp.Utilities.Enums;
using System;

namespace LogiticsWebApp.Areas.Vendor.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string TrackingNumber { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ShippingDetails ShippingDetail
        {
            get
            {
                return (ShippingDetails)Enum.Parse(typeof(ShippingDetails), ShippingDetailId.ToString());
            }
            set
            {
                this.ShippingDetail = value;
            }
        }
        public OrderStatus Status
        {
            get
            {
                return (OrderStatus)Enum.Parse(typeof(ShippingDetails), StatusId.ToString());
            }
            set
            {
                this.Status = value;
            }
        }
        public PaymentDetails PaymentDetail
        {
            get
            {
                return (PaymentDetails)Enum.Parse(typeof(PaymentDetails), PaymentDetailId.ToString());
            }
            set
            {
                this.PaymentDetail = value;
            }
        }

        public int ShippingDetailId { get; set; }
        public int PaymentDetailId { get; set; }
        public int StatusId { get; set; }
    }
}
