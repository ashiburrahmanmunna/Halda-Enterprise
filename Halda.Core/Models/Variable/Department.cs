using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models.Variable
{
    public class Department : BaseModel
    {

        [StringLength(100)]
        public string DeptName { get; set; } = string.Empty;

        [Display(Name = "Department Code")]
        public string? DeptCode { get; set; }

        [StringLength(100)]
        public string? LocalName { get; set; }
        public int Order { get; set; } = 0;

    }

}
