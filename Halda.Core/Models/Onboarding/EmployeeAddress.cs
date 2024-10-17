using Halda.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models.Onboarding
{
    public class EmployeeAddress : BaseModel
    {
        public AddressType AddressTypes { get; set; } // Enum for Present/Permanent
        public string Apartment { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Postal { get; set; } = string.Empty;
        public string EmployeeId { get; set; } = string.Empty; // Foreign key to Employee

        public Employee? Employee { get; set; }
    }

}
