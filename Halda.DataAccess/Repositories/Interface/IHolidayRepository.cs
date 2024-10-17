using Halda.Core.Models.Attendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.DataAccess.Repositories.Interface
{
    public interface IHolidayRepository : IRepository<Holidays, string>
    {
        Task<IList<Holidays>> GetPublicHolidaysByCompanyIdAsync(string companyId, CancellationToken token);
    }
}
