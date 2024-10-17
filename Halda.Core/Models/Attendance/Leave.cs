using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models.Attendance
{
    public class Leave: BaseModel
    {
        public string LeaveName { get; set; } = string.Empty;
        public string EmployeeStatus { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Definition { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int LeavePerPeriod { get; set; } 
        public bool EmployeePreApply { get; set; } 
        public int MaximumDay { get; set; } 
        public bool FutureAdjustable { get; set; } 
        public float PercentageLeaveCarriedForward { get; set; } 
        public required string EligibleFor { get; set; } 
        public bool AdminAssignToEmployee { get; set; } 
        public string LeaveOptions { get; set; } = string.Empty;
        public bool PaidTimeOff { get; set; } 
        public int WithinDays { get; set; } 
        public int MaximumHours { get; set; } 
        public string Allowedfor { get; set; } = string.Empty;
    }
}
