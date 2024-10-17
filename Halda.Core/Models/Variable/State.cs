using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models.Variable
{
    public class State : SelfModel
    {
        public string StateId { get; set; }

        [Required]
        [Display(Name = "State Code")]
        public string StateCode { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string? CountryId { get; set; }

        [Required]
        [Display(Name = "State Name")]
        public string StateName { get; set; }

        [Display(Name = "Country")]
        public virtual Country StateCountry { get; set; }


        [Display(Name = "City")]
        public virtual ICollection<City> Cities { get; set; }
    }
}
