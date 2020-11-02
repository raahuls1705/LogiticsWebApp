using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LogiticsWebApp.Utilities.Enums
{
    public enum ShippingDetails
    {
        [Display(Name = "Same Day")]
        [Description("Same Day")]
        SameDay = 1,

        [Display(Name = "Next Day")]
        [Description("Next Day")]
        NextDay = 2,

        [Display(Name = "Standard 3-5 days")]
        [Description("Standard 3-5 days")]
        Standard = 3
    }
}
