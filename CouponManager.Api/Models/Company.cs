using System;
using System.ComponentModel.DataAnnotations;

namespace CouponManager.Api.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Uri Url { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}
