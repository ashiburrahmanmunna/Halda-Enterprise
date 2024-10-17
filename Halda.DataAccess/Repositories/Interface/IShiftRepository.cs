
using Halda.Core.Models.Attendance;
using Halda.Core.Models.Onboarding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.DataAccess.Repositories.Interface
{
    public interface IShiftRepository : IRepository<Shift, string>
    {
        
        Task<List<Shift>> GetShiftListAsync(string searchTerm, string companyId, CancellationToken token, int page = 1, int size = 5);
        Task<int> GetTotalRecordCountAsync(string searchTerm, string companyId, CancellationToken token);

    }
}
