using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models.Onboarding
{
    public class EmployeeOnboardingChecklist : BaseModel
    {
        public string EmployeeId { get; set; }
        public string Task { get; set; } = string.Empty;
        public bool IsDone { get; set; }

        // Foreign key relationship
        public Employee Employee { get; set; }
    }

}
