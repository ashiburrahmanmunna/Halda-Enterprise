using Halda.Core.Models;
using Halda.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.DataAccess.Repositories.Interface
{
    public interface IRecruitmentVariableRepository : IRepository<RecruitmentVariable, string>
    {
         Task<List<JobMileStone>> PreOnBoardingVar(string jobPostId, CancellationToken token);

    }
}
