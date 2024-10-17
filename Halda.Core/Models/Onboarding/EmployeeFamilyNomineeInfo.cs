using Halda.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models.Onboarding
{
    public class EmployeeFamilyNomineeInfo : BaseModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Occupation { get; set; } = string.Empty;
        public DateOnly DoB { get; set; }
        public string Relationship { get; set; } = string.Empty;
        public bool Alive { get; set; } 
        public int Age { get; set; } 
        public string? PhotoPath { get; set; }
        public string? AttachmentPath { get; set; }
        public InfoType InfoType { get; set; }
        public string EmployeeId { get; set; } = string.Empty; // Foreign key to Employee

        public Employee? Employee { get; set; }
    }

}
