using Halda.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models.Onboarding
{
    public class EmpEdu : BaseModel
    {
        public string Title { get; set; } = string.Empty;

        public string SubjectName { get; set; } = string.Empty;

        public string Institute { get; set; } = string.Empty;

        public DateOnly StartYear { get; set; }

        public DateOnly EndYear { get; set; }

        public string Grade { get; set; } = string.Empty;

        public CertificationType CertificationType { get; set; } // Enum for Academic/Professional

        public string EmployeeId { get; set; } = string.Empty; // Foreign key to Employee

        public Employee? Employee { get; set; }
    }
}
