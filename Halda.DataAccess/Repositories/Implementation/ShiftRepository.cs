
using Halda.Core.DTO.Onboarding;
using Halda.Core.Models.Attendance;
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
    public class ShiftRepository : BaseRepository<Shift, string>, IShiftRepository
    {
        public ShiftRepository(HaldaDbContext dbContext) : base(dbContext)
        {

        }

        

        public async Task<List<Shift>> GetShiftListAsync(string searchTerm, string companyId, CancellationToken token, int page = 1, int size = 5)
        {
            var query = _dbContext.Shifts.AsQueryable();

            // Apply search filter
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(x => x.ShiftName.ToLower().Contains(searchTerm) ||
                                         x.ShiftType.ToLower().Contains(searchTerm));
            }

            // Filter by Company ID
            query = query.Where(x => x.CompanyId == companyId);

            // Get total record count
            int totalRecordCount = await query.CountAsync(token);

            // Paginate the results
            var shifts = await query
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(token);

            return shifts;
        }

        public async Task<int> GetTotalRecordCountAsync(string searchTerm, string companyId, CancellationToken token)
        {
            var query = _dbContext.Shifts.AsQueryable();

            // Apply search filter
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(x => x.ShiftName.ToLower().Contains(searchTerm) ||
                                         x.ShiftType.ToLower().Contains(searchTerm));
            }

            // Filter by Company ID
            return await query.CountAsync(token);
        }

    }
}
