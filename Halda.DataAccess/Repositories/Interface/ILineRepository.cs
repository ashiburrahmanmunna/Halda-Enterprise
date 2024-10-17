using Halda.Core.DTO;
using Halda.Core.Models.Variable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.DataAccess.Repositories.Interface
{
    public interface ILineRepository : IRepository<Line, string>
    {
        Task<IList<Line>> GetAllForDropDownAsync(CancellationToken token);
        Task<List<SelectListdto>> GetAllLine(string searchTerm, CancellationToken token);
    }
}
