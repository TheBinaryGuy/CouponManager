using CouponManager.Api.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace CouponManager.Api.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, MinLength(4), MaxLength(12)]
        public string Code { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int DomainId { get; set; }
        [Required]
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public DateTimeOffset CreatedAt { get; } = DateTime.Now;
        public string CreatedBy { get; set; }
        public DateTimeOffset ModifiedAt { get; } = DateTime.Now;
        public string ModifiedBy { get; set; }
    }
}
