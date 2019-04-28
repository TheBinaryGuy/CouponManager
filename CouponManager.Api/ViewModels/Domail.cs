using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CouponManager.Api.ViewModels
{
    public class Domail
    {
        [Required]
        public int Domail_ID { get; set; }

        [Required]
        public String Domail_Name { get; set; }

        [Required]
        public String Domain_Url { get; set; }

        [Required]
        public int Company_ID { get; set; }

    }
}
