using System;
using System.ComponentModel.DataAnnotations;

namespace CouponManager.Api.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTimeOffset CreatedAt { get; } = DateTime.Now;
        public string CreatedBy { get; set; }
        public DateTimeOffset ModifiedAt { get; } = DateTime.Now;
        public string ModifiedBy { get; set; }
    }
}
