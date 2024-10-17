using Halda.Core.DTO;
using Halda.Core.Models.Variable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.DataAccess.Repositories.Interface
{
    public interface IFloorRepository : IRepository<Floor, string>
    {
        Task<IList<Floor>> GetAllForDropDownAsync(CancellationToken token);
        Task<List<SelectListdto>> GetAllFloor(string searchTerm, CancellationToken token);
    }
}
