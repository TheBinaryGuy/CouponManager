using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CouponManager.Api.ViewModels
{
    public class Coupons
    {
        [Required]
        public int Coupon_ID { get; set; }

        [Required]
        public string Coupon_Name { get; set; }

        [Required ]
        public string Coupon_Code { get; set; }

        [Required]
        public int Coupon_Category_ID { get; set; }

        [Required]
        public int Domail_ID { get; set; }

        [Required]

        public int Company_ID { get; set; }
    }
}
