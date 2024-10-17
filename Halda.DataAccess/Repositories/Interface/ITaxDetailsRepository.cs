using Halda.Core.DTO.Onboarding;
using Halda.Core.Models.Onboarding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.DataAccess.Repositories.Interface
{
    public interface ITaxDetailsRepository : IRepository<EmployeeTax, string>
    {
        Task<EmployeeTaxsDTO> GetTaxDetailData(string employeeId, CancellationToken token);
    }
}
