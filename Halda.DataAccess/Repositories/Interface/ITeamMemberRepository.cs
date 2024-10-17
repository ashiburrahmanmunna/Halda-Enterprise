using Halda.Core.Models.Onboarding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.DataAccess.Repositories.Interface
{ 
    public interface ITeamMemberRepository : IRepository<EmployeeTeam, string>
    {
        Task<List<Employee>> GetTeamMemberListAsync(string searchTerm, string companyId, CancellationToken token);
        Task<List<EmployeeTeam>> GetValidateTeamMember(string searchTerm, string companyId, CancellationToken token);

    }
}
