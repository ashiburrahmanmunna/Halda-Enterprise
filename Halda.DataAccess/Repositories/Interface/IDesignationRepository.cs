using Halda.Core.DTO;
using Halda.Core.Models.Variable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.DataAccess.Repositories.Interface
{
    public interface IDesignationRepository : IRepository<Designation, string>
    {
        Task<IList<Designation>> GetAllForDropDownAsync(CancellationToken token);
        Task<List<SelectListdto>> GetAllDesignation(string searchTerm,string selectedvalue, CancellationToken token);
    }
}
