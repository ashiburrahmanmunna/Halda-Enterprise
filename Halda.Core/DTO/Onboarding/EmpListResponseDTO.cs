using Halda.Core.Enums;
using Halda.Core.Models.Onboarding;
using Halda.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.DTO.Onboarding
{
    public class EmpListResponseDTO
    {
      
            public string Id { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? FatherName { get; set; }
            public string? MotherName { get; set; }
            public string? EmployeeCode { get; set; }
            public string? EmployeeType { get; set; }
            public string? FingerId { get; set; }
            public string? OldFingerId { get; set; }
            public string? JoiningDate { get; set; }
            public string? Nationality { get; set; }
            public string? Gender { get; set; }
            public string? Religion { get; set; }
            public string? MeritalStatus { get; set; }
            public string? PrimaryEmail { get; set; }
            public string? DOB { get; set; } 
            public string? NID { get; set; }
            public string? PassportNumber { get; set; }
            public string? PassportIssueDate { get; set; } 
            public string? PrimaryMNo { get; set; }
            public string? SecMNo { get; set; }
            public string? EmergencyContact { get; set; }
            public string? DepartmentId { get; set; }
            public string? DesignationId { get; set; }
            public string? DesigName { get; set; }
            public string? DeptName { get; set; }
            public string? SecName { get; set; }
            public string? SectionId { get; set; } 
            public string? LineId { get; set; } 
            public string? LineName { get; set; }
            public string? FloorId { get; set; } 
            public string? FloorName { get; set; }
            public bool? IsActive { get; set; }
            public string? Location { get; set; }


    }

    public class EmployeeJobInfoDTO
    {
        public string? CompanyId { get; set; }
        public string? UserId { get; set; }
        public string? EmployeeCode { get; set; }
        public string? EmployeeType { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string? Location { get; set; }
        public string? DesignationId { get; set; }
        public string? DepartmentId { get; set; }
        public string? Gender { get; set; }
    }


    public class EmployeeBankDTO : BaseModel
    {
        public AccountType? AccType { get; set; }
        public string? AccTypeText { get; set; }
        public string? AccHolderName { get; set; }
        public string? AccNumber { get; set; }
        public string? ReAccNumber { get; set; }
        public string? BankName { get; set; }
        public string? BankNumber { get; set; }
        public string? RoutingNumber { get; set; }
        public string? BranchName { get; set; }
        public string? ExpiryDate { get; set; }
        public string? CVV { get; set; }
        public string? EmployeeId { get; set; }
        
    }

    public class EmployeeTaxsDTO : BaseModel
    {       
        public string? TaxYear { get; set; }
        public string? Name { get; set; }
        public bool? ReturnSubmit { get; set; }
        public string? ReturnSubmitDate { get; set; }
        public string? AcknowledgmentSlipRecDate { get; set; }
        public bool? TaxCertificateReceive { get; set; }
        public bool? TaxExtension { get; set; }
        public string? EmployeeId { get; set; }
        

    }

    public class EmpDocumentDTO : BaseModel
    {
        public string? Type { get; set; }
        public string? DocPath { get; set; }     
        public string? EmployeeId { get; set; }


    }

}
