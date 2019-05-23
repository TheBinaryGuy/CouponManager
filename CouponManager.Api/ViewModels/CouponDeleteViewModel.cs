﻿using System.ComponentModel.DataAnnotations;

namespace CouponManager.Api.ViewModels
{
    public class CouponDeleteViewModel
    {
        [Required, MinLength(4), MaxLength(12)]
        public string Code { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string DomainName { get; set; }
    }
}
