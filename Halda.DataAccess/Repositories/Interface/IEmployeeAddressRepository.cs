using Halda.Core.Models.Onboarding;

namespace Halda.DataAccess.Repositories.Interface
{
    public interface IEmployeeAddressRepository : IRepository<EmployeeAddress, string>
    {
         Task SaveOrUpdateAsync(EmployeeAddress model, CancellationToken token);
    }

}
