using Halda.Core.Models.Onboarding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.DataAccess.Repositories.Interface
{
    public interface IEmployeeTaxRepository : IRepository<EmployeeTax, string>
    {
        Task SaveOrUpdateAsync(EmployeeTax model, CancellationToken token);
    }
}
