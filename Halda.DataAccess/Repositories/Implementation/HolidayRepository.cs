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
    public class HolidayRepository : BaseRepository<Holidays, string>, IHolidayRepository
    {

        public HolidayRepository(HaldaDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IList<Holidays>> GetPublicHolidaysByCompanyIdAsync(string companyId, CancellationToken token)
        {
            return await _dbContext.Holidays
                .Where(h => h.CompanyId == companyId && h.HolidayType == "Public")
                .ToListAsync(token);
        }

    }
}
