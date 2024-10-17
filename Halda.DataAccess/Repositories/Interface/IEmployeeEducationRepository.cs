using Halda.Core.Models.Onboarding;

namespace Halda.DataAccess.Repositories.Interface
{
    public interface IEmployeeEducationRepository : IRepository<EmpEdu, string>
    {
        Task<IEnumerable<EmpEdu>> GetEducationByEmployeeIdAsync(string employeeId, CancellationToken token);
        Task<bool> UpdateEducationRecordAsync(EmpEdu educationRecord, CancellationToken token);


    }

}
