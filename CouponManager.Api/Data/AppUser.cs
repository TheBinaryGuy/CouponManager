using Microsoft.AspNetCore.Identity;
using System;

namespace CouponManager.Api.Data
{
    // Add profile data for application users by adding properties to the AppUser class
    public class AppUser : IdentityUser
    {
        [PersonalData]
        public string CompanyName { get; set; }
        [PersonalData]
        public Uri Url { get; set; }
        [PersonalData]
        public bool IsAdmin { get; set; }
    }
}
