using Halda.Core.Models.Onboarding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models.Attendance
{
    public class EmpLeaveAdjust : BaseModel
    {
        public string EmpID { get; set; } = string.Empty;
        public Employee Emp { get; set; }
        public string AdjustType { get; set; } = string.Empty;
        public string AdjustLeaveId { get; set; } = string.Empty;
        public Leave AdjustLeave { get; set; }
        public DateOnly AdjustDate { get; set; } 
        public DateOnly ReplaceDate { get; set; }
        public string Remarks { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        
    }
}
