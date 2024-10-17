using Halda.Core.Models.Onboarding;

namespace Halda.DataAccess.Repositories.Interface
{
    public interface IEmployeeFamNomiRepository : IRepository<EmployeeFamilyNomineeInfo, string>
    {
        Task SaveOrUpdateAsync(EmployeeFamilyNomineeInfo model, CancellationToken token);

    }

}
