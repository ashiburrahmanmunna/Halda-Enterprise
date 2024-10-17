using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.DTO.Budgeting
{
    public class EmpSalaryViewModel
    {
        public string? CompanyName { get; set; }
        public string? EmployeeName { get; set; }
        public string? DepartmentName { get; set; }
        public string? DesignationName { get; set; }
        public double GrossSalary { get; set; }
        public double MinSalary { get; set; }
        public double MaxSalary { get; set; }
    }
}
