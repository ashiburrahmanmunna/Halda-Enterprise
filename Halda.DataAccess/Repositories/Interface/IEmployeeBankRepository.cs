using Halda.Core.Models.Onboarding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.DataAccess.Repositories.Interface
{
    public interface IEmployeeBankRepository : IRepository<EmployeeBank, string>
    {
        Task SaveOrUpdateAsync(EmployeeBank model, CancellationToken token);
    }
}
