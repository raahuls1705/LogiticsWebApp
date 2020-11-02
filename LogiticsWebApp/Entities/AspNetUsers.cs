using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogiticsWebApp.Entities
{
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            AspNetUserRoles = new HashSet<AspNetUserRoles>();
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public int? CompanySizeId { get; set; }
        public string CurrentProcessDescription { get; set; }
        public string CurrentProcessChallenges { get; set; }
        public int? IndustryTypeId { get; set; }
        public string Capacity { get; set; }
        public string CountriesCoverByVendor { get; set; }
        public string SecondaryContact { get; set; }
        public ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
    }
}
