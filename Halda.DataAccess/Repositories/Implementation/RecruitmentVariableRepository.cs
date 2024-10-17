using Halda.Core.Models;
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
    public class RecruitmentVariableRepository : BaseRepository<RecruitmentVariable, string>, IRecruitmentVariableRepository
    {
        public RecruitmentVariableRepository(HaldaDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<JobMileStone>> PreOnBoardingVar(string jobPostId, CancellationToken token)
        {
            var result = await _dbContext.JobMileStones
                .Where(p => p.JobPostId == jobPostId).OrderBy(p => p.Serial)
                .Select(p => new JobMileStone
                {
                    Id = p.Id, // Assuming Id is a property in JobMileStones
                    Name = p.Name,
                    Status = p.Status,
                    
                    
                })
                .ToListAsync(token);

            return result;
        }

    }
}
