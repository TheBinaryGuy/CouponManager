using System;
using System.ComponentModel.DataAnnotations;

namespace CouponManager.Api.ViewModels
{
    public class RegisterViewModel
    {
        [Required, MaxLength(15)]
        public string UserName { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public Uri Url { get; set; }
        public bool IsAdmin { get; set; } = false;
    }
}