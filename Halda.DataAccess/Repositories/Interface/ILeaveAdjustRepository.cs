using Halda.Core.DTO.Attendance;
using Halda.Core.Models;
using Halda.Core.Models.Attendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.DataAccess.Repositories.Interface
{
    public interface ILeaveAdjustRepository : IRepository<EmpLeaveAdjust, string>
    {
        Task AddLeaveAdjustmentsAsync(LeaveAdjustmentDto model, string companyId, string userId, CancellationToken token);
        Task<EmpLeaveAdjust> GetLeaveAdjustmentByIdAsync(string empId, CancellationToken token);

        Task<List<EmpLeaveAdjust>> GetLeaveAdjustListAsync(string searchTerm, string startDate, string endDate, string companyId, CancellationToken token, int page = 1, int size = 10);

        Task<int> GetTotalRecordCountAsync(string searchTerm, string startDate, string endDate, string companyId, CancellationToken token);


    }
}
