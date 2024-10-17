using Halda.Core.Enums;
using Halda.Core.Models.Variable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models.Onboarding
{
    public class Employee : BaseModel
    {
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? FatherName { get; set; } = string.Empty;
        public string? MotherName { get; set; } = string.Empty;
        public string? EmployeeCode { get; set; } 
        public string? EmployeeType { get; set; } 
        public string? FingerId { get; set; } 
        public string? OldFingerId { get; set; }
        public DateTime? JoiningDate { get; set; }
        public DateTime DOB { get; set; }
        public string? Gender { get; set; } = string.Empty;
        public string? Religion { get; set; } = string.Empty;
        public string? MeritalStatus { get; set; } = string.Empty;       
        public string? Nationality { get; set; } = string.Empty;
        public string? NID { get; set; } = string.Empty; // National ID
        public string? PassportNumber { get; set; } = string.Empty;
        public DateTime? PassportIssueDate { get; set; } // Nullable in case the passport is not issued
        public string? PrimaryMNo { get; set; } = string.Empty;
        public string? SecMNo { get; set; } = string.Empty;
        public string? EmergencyContact { get; set; } = string.Empty;
        public string? PrimaryEmail { get; set; } = string.Empty;
        public int? SalaryProfileId { get; set; }

        public string? DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public  Department? Department { get; set; }
        public string? DesignationId { get; set; }
        [ForeignKey("DesignationId")]
        public  Designation? Designation { get; set; }
        public string? SectionId { get; set; }
        [ForeignKey("SectionId")]
        public  Section? Section { get; set; }
        public string? LineId { get; set; }
        [ForeignKey("LineId")]
        public  Line? Line { get; set; }
        public string? FloorId { get; set; }
        [ForeignKey("FloorId")]
        public  Floor? Floor { get; set; }
        public bool? IsActive { get; set; }
        [StringLength(40)]
        public string? EmpUserId { get; set; }
        public string? JobTitle { get; set; }
        public string? Location { get; set; }
    }

    public class EmpContractPeriod : BaseModel
    {
        public string? ContractPeriod { get; set; }
        public string? Status { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public string? Attachments { get; set; }
    }

    
}
