using Halda.Core.Models.Onboarding;

namespace Halda.DataAccess.Repositories.Interface
{
    //public interface IEmployeeAddressRepository : IRepository<EmployeeAddress, string>
    //{


    //}

    public interface IEmployeeEmergencyContact : IRepository<EmployeeEmergencyContact, string> 
    {
        Task SaveOrUpdateAsync(EmployeeEmergencyContact model, CancellationToken token);
    }

}
