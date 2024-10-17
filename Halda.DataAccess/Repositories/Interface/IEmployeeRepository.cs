using Halda.Core.DTO;
using Halda.Core.DTO.Onboarding;
using Halda.Core.Models;
using Halda.Core.Models.Onboarding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.DataAccess.Repositories.Interface
{
    public interface IEmployeeRepository : IRepository<Employee, string>
    {

        //Task<List<Employee>> GetEmployess(string searchterm, DateTime? Dob, CancellationToken token);
        Task<bool> UpdateEmployeeAsync(Employee employee, CancellationToken token);
        Task<bool> SaveEmployeeAsync(Employee model);
        Task<List<Employee>> GetEmployess(string searchterm, DateTime? Dob, CancellationToken token); 
        Task<List<Employee>> GetEmployeeListAsync(string searchterm, string companyId, CancellationToken token, int page = 1, int size = 5);
        Task<int> GetTotalRecordCountAsync(string searchTerm, string companyId, CancellationToken token);
        Task<EmpListResponseDTO> GetEmployeeDataAsync(string employeeId,CancellationToken token);
        Task<bool> IsEmployeeCodeExistsAsync(string employeeCode, string excludedEmployeeId = null);
        Task<List<SelectListdto>> GetAllEmployees(string searchTerm, CancellationToken token);

        //Task<List<Employee>> GetAllEmployeesByCompanyId(string companyId, CancellationToken token);
    }

}
