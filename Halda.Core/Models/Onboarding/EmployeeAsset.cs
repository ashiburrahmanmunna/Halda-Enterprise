using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models.Onboarding
{
    public class EmployeeAsset : BaseModel
    {
        public string EmployeeId { get; set; }
        public string Asset { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        // Foreign key relationship
        public Employee Employee { get; set; }
    }

}
