using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CouponManager.Api.ViewModels
{
    public class Company
    {
        [Required]
        public int Company_ID { get; set; }

        [Required]
        public String Company_Name { get; set; }

        [Required]
        public String Company_Url { get; set; }

        [Required]
        public String CompanyEmail { get; set; }

        [Required]
        public String UserMame { get; set; }



    }
}
