using Halda.Core.DTO.Onboarding;
using Halda.Core.Models.Onboarding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.DataAccess.Repositories.Interface
{
    public interface IBankDetailsRepository : IRepository<EmployeeBank, string>
    {
        Task<EmployeeBankDTO> GetBankDetailData(string employeeId, CancellationToken token);
    }
}
