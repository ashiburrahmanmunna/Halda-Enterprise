using Halda.Core.DTO.Budgeting;
using Halda.Core.DTO.Onboarding;
using Halda.Core.Models.Budgeting;
using Halda.Core.Models.Onboarding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.DataAccess.Repositories.Interface
{
    public interface IForecastingRepository : IBaseRepository<EmpSalary, string>
    {
        Task<IEnumerable<EmpSalaryViewModel>> GetEmpSalaryViewModelsAsync(CancellationToken token);
    }
}
