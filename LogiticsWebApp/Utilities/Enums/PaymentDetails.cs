using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LogiticsWebApp.Utilities.Enums
{
    public enum PaymentDetails
    {
        [Display(Name = "Online/Prepaid")]
        [Description("Online/Prepaid")]
        Online = 1,

        [Display(Name = "Cash on Delivery")]
        [Description("Cash on Delivery")]
        CashOnDelivery = 2

    }
}
