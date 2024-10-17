using Halda.Core.DTO.Onboarding;
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
    public class EmpOrganizationRepository : BaseRepository<EmpContractPeriod, string>, IEmpOrganizationRepository
    {
        public EmpOrganizationRepository(HaldaDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<EmpContractPeriod>> GetContractPeriodListAsync(string companyId, CancellationToken token)
        {

            var query = _dbContext.EmpContractPeriod
                .AsQueryable();


            query = query.Where(x => x.CompanyId == companyId);

            // Paginate the results
            var contractPeriods = await query
                .OrderByDescending(x => x.Id)
                .ToListAsync(token);

            // Return only the employee list as expected by the interface
            return contractPeriods;

        }

      

      

    }
}
