using Halda.Core.DTO.Onboarding;
using Halda.Core.Models.Onboarding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.DataAccess.Repositories.Interface
{
    public interface IEmpOrganizationRepository : IRepository<EmpContractPeriod, string>
    {
        Task<List<EmpContractPeriod>> GetContractPeriodListAsync(string companyId, CancellationToken token);
        
        
    }
}
