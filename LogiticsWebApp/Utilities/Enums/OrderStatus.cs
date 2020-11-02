using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LogiticsWebApp.Utilities.Enums
{
    public enum OrderStatus
    {
        [Display(Name = "Completed")]
        [Description("Completed")]
        Completed = 1,

        [Display(Name = "Pending")]
        [Description("Pending")]
        Pending = 2,

        [Display(Name = "Unassigned")]
        [Description("Unassigned")]
        Unassigned = 3,

        [Display(Name = "Cancelled")]
        [Description("Cancelled")]
        Cancelled = 4
    }
}
