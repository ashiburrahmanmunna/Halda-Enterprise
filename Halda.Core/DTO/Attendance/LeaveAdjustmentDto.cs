using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.DTO.Attendance
{
    public class LeaveAdjustmentDto
    {
        public string? EmpID { get; set; } // Employee ID or null if "Select All"
        public string AdjustType { get; set; }
        public string AdjustLeaveId { get; set; } // Leave ID to adjust
        public DateOnly AdjustDate { get; set; } // Adjustment Date
        public DateOnly ReplaceDate { get; set; } // Replace Date
        public string Remarks { get; set; }
        public string? DepartmentId { get; set; } // Selected Department ID
        public string? DesignationId { get; set; } // Selected Designation ID
    }

}
