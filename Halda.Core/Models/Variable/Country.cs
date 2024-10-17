using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models.Variable
{
    public class Country : SelfModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CountryId { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 2)]
        [Display(Name = "Country Code")]
        public string CountryCode { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 2)]
        [Display(Name = "Dial Code")]
        public string DialCode { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Country Name")]
        public string CountryName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [Display(Name = "Country Short Name")]
        public string CountryShortName { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 4)]
        [Display(Name = "Culture Info")]
        public string CultureInfo { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "Currency Name")]
        public string CurrencyName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        [Display(Name = "Currency Symbol")]
        public string CurrencySymbol { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        [Display(Name = "Currency Short Name")]
        public string CurrencyShortName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 6)]
        [Display(Name = "Flag Class")]
        public string FlagClass { get; set; }

        public bool IsActive { get; set; }

        [Display(Name = "States")]
        public virtual ICollection<State> States { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public ICollection<Company> Companies { get; set; }

    }
}
