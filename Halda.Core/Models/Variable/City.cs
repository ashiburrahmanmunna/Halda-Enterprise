using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models.Variable
{
    public class City : SelfModel
    {
        public string CityId { get; set; }

        [Required]
        [Display(Name = "City Code")]
        public string CityCode { get; set; }

        [Required]
        [Display(Name = "State")]
        public string? StateId { get; set; }



        [Required]
        [Display(Name = "City Name")]
        public string CityName { get; set; }



        [Display(Name = "State")]
        public virtual State StateCity { get; set; }
        public virtual Country? Country { get; set; }


    }
}
