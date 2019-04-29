using System;
using System.ComponentModel.DataAnnotations;

namespace CouponManager.Api.Models
{
    public class Domain
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Uri Url { get; set; }

        [Required]
        public int CompanyId { get; set; }

    }
}
