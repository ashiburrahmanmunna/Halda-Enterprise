using Halda.Core.DTO.Onboarding;
using Halda.Core.Models.Onboarding;
using Halda.DataAccess.Persistence;
using Halda.DataAccess.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Halda.Core.DTO.Budgeting;
using Halda.Core.Models.Budgeting;

namespace Halda.DataAccess.Repositories.Implementation
{
    public class ForecastingRepository : BaseRepository<EmpSalary, string>, IForecastingRepository
    {
        public ForecastingRepository(HaldaDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<EmpSalaryViewModel>> GetEmpSalaryViewModelsAsync(CancellationToken token)
        {
            return await _dbContext.EmpSalarys
                .Include(e => e.Employee)
                .Include(e => e.Department)
                .Include(e => e.Designation)
                .Include(e => e.Company)
                .Select(e => new EmpSalaryViewModel
                {
                    CompanyName = e.Company.CompanyName,
                    EmployeeName = e.Employee.FirstName + " " + e.Employee.LastName,
                    DepartmentName = e.Department.DeptName,
                    DesignationName = e.Designation.DesigName,
                    GrossSalary = e.GrossSalary,
                    MinSalary = e.MinSalary,
                    MaxSalary = e.MaxSalary
                })
                .ToListAsync(token);
        }
    }
}
