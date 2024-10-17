using Halda.Core.DTO;
using Halda.Core.Models.Attendance;
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
    public class LeaveRepository : BaseRepository<Leave, string>, ILeaveRepository
    {

        public LeaveRepository(HaldaDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Leave>> GetLeaveListAsync(string searchTerm, string companyId, CancellationToken token, int page = 1, int size = 5)
        {
            var query = _dbContext.Leaves.AsQueryable();

            // Apply search filter
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(x => x.LeaveName.ToLower().Contains(searchTerm) ||
                                         x.Allowedfor.ToLower().Contains(searchTerm));
            }

            // Filter by Company ID
            query = query.Where(x => x.CompanyId == companyId);

            // Get total record count
            int totalRecordCount = await query.CountAsync(token);

            // Paginate the results
            var leaves = await query
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(token);

            return leaves;
        }

        public async Task<int> GetTotalRecordCountAsync(string searchTerm, string companyId, CancellationToken token)
        {
            var query = _dbContext.Leaves.AsQueryable();

            // Apply search filter
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(x => x.LeaveName.ToLower().Contains(searchTerm) ||
                                         x.Allowedfor.ToLower().Contains(searchTerm));
            }

            // Filter by Company ID
            return await query.CountAsync(token);
        }

        public async Task<List<SelectListdto>> GetAllLeaves(string searchTerm, CancellationToken token)
        {
            var query = _dbContext.Leaves.AsQueryable();

            // Check if searchTerm is null or empty, if so load the first 10 leaves
            if (string.IsNullOrEmpty(searchTerm))
            {
                query = query.Take(10);
            }
            else
            {
                var lowerCaseSearchTerm = searchTerm.ToLower();
                query = query.Where(l => l.DisplayName.ToLower().Contains(lowerCaseSearchTerm));
            }

            var result = await query.Select(l => new SelectListdto
            {
                Id = l.Id,
                Text = l.DisplayName // DisplayName for the search list
            }).ToListAsync(token);

            return result;
        }
    }
}
