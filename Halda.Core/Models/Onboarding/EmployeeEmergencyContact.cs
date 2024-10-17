using Halda.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models.Onboarding
{
    public class EmployeeEmergencyContact : BaseModel
    {
        public EmergencyContactType ContactType { get; set; } // Enum for EmergencyOne/EmergencyTwo
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Relationship { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Occupation { get; set; } = string.Empty;
        public string OfficeAddress { get; set; } = string.Empty;
        public string EmployeeId { get; set; } = string.Empty; // Foreign key to Employee

        public Employee? Employee { get; set; }
    }

}
