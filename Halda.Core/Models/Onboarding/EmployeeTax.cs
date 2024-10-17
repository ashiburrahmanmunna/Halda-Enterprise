using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models.Onboarding
{
    public class EmployeeTax : BaseModel
    {
        public string? TaxYear { get; set; }
        public string? Name { get; set; }
        public bool? ReturnSubmit { get; set; }
        public DateTime? ReturnSubmitDate { get; set; }
        public DateTime? AcknowledgmentSlipRecDate { get; set; } 
        public bool? TaxCertificateReceive { get; set; }
        public bool? TaxExtension { get; set; }
        public string? EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }
    }

}
