using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models.Onboarding
{
    public class EmployeeDocument : BaseModel
    {
        public string? Type { get; set; }
        public string? DocPath { get; set; }
        public string? Declaration { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string? EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }
    }

}
