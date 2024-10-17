using Halda.Core.DTO;
using Halda.Core.Models.Variable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.DataAccess.Repositories.Interface
{
    public interface ISectionRepository : IRepository<Section, string>
    {
        Task<IList<Section>> GetAllForDropDownAsync(CancellationToken token);
        Task<List<SelectListdto>> GetAllSection(string searchTerm, CancellationToken token);
    }
}
