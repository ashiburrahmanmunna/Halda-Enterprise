using Halda.Core.Models.Onboarding;
using Halda.Core.Models.Variable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models.Budgeting
{
    public class EmpSalary:BaseModel
    {
        public string? EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }
        public double GrossSalary { get; set; }
        public double MinSalary { get; set; }
        public double MaxSalary { get; set; }
        public string? DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }
        public string? DesignationId { get; set; }
        [ForeignKey("DesignationId")]
        public Designation? Designation { get; set; }

    }


}
