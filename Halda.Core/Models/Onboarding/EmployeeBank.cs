using Halda.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models.Onboarding
{
    public class EmployeeBank : BaseModel
    {
        public AccountType? AccType { get; set; } 
        public string? AccHolderName { get; set; } 
        public string? AccNumber { get; set; } 
        public string? ReAccNumber { get; set; } 
        public string? BankName { get; set; } 
        public string? BankNumber { get; set; } 
        public string? RoutingNumber { get; set; } 
        public string? BranchName { get; set; }      
        public DateOnly? ExpiryDate { get; set; } 
        public string? CVV { get; set; } 
        public string? EmployeeId { get; set; } 
        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }
    }

}
