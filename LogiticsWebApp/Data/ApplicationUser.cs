using LogiticsWebApp.Entities;
using Microsoft.AspNetCore.Identity;

namespace LogiticsWebApp.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public int? IndustryTypeId { get; set; }
        public int? CompanySizeId { get; set; }
        public string CurrentProcessDescription { get; set; }
        public string CurrentProcessChallenges { get; set; }
        public string SecondaryContact { get; set; }
        public string CountriesCoverByVendor { get; set; }
        public string Capacity { get; set; }
       
    }
}
