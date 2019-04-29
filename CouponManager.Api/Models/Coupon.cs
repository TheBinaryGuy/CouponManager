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

        public int CompanyId { get; set; }
    }
}
