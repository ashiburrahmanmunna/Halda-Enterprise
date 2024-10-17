using Halda.Core.Models.Onboarding;
using Halda.DataAccess.Persistence;
using Halda.DataAccess.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.DataAccess.Repositories.Implementation
{
    public class TeamMemberRepository : BaseRepository<EmployeeTeam, string>, ITeamMemberRepository
    {
        public TeamMemberRepository(HaldaDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Employee>> GetTeamMemberListAsync(string searchTerm, string companyId, CancellationToken token)
        {

            var query = _dbContext.Employees
                .AsQueryable();

            // Apply search filter
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(x => x.FirstName.ToLower().Contains(searchTerm)
                );
            }

            query = query.Where(x => x.CompanyId == companyId);


            // Paginate the results
            var employees = await query
                .OrderByDescending(x => x.Id)
                .Take(5)
                .ToListAsync(token);

            // Return only the employee list as expected by the interface
            return employees;

        }

        public async Task<List<EmployeeTeam>> GetValidateTeamMember(string searchTerm, string companyId, CancellationToken token)
        {

            var query = _dbContext.EmployeeTeams
                .Include(x => x.Member)
                .AsQueryable();

            // Apply search filter
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(x => x.IsTeamHead.ToString().ToLower().Contains(searchTerm)
                );
            }

            query = query.Where(x => x.CompanyId == companyId);


            // Paginate the results
            var employees = await query
                .OrderByDescending(x => x.Id)
                .Take(5)
                .ToListAsync(token);

            // Return only the employee list as expected by the interface
            return employees;

        }

    }

}
