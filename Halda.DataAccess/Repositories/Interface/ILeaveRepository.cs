using Halda.Core.DTO;
using Halda.Core.Models.Attendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.DataAccess.Repositories.Interface
{
    public  interface ILeaveRepository : IRepository<Leave, string>
    {
        Task<List<Leave>> GetLeaveListAsync(string searchTerm, string companyId, CancellationToken token, int page = 1, int size = 5);
        Task<int> GetTotalRecordCountAsync(string searchTerm, string companyId, CancellationToken token);

        Task<List<SelectListdto>> GetAllLeaves(string searchTerm, CancellationToken token);
    }
}
