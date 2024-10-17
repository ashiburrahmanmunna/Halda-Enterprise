using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models.Variable
{
    public class Designation : BaseModel
    {

        [StringLength(100)]
        public string DesigName { get; set; } = string.Empty;

        [Display(Name = "Designation Code")]
        public string? DesigCode { get; set; }

        [StringLength(100)]
        public string? LocalName { get; set; }
        public int Order { get; set; } = 0;


    }
}
