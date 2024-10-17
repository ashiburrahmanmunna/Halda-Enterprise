using Halda.Core.DTO;
using Halda.Core.Models.Variable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.DataAccess.Repositories.Interface
{
    public interface IDepartmentRepository : IRepository<Department, string>
    {
        Task<IList<Department>> GetAllForDropDownAsync(CancellationToken token);
        Task<List<SelectListdto>> GetAllDepartment(string searchTerm,string selectedvalue, CancellationToken token);
    }
}
