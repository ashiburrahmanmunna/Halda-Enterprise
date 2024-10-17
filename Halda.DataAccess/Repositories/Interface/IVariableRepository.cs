using Halda.Core.DTO;
using Halda.Core.DTO.PreOnboarding;
using Halda.Core.Models;
using Halda.Core.Models.Variable;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.DataAccess.Repositories.Interface
{
    public interface IVariableRepository : IRepository<VariableData, string>
    {
        Task<IList<VariableData>> GetAllForDropDownAsync(CancellationToken token);
        Task<List<SelectListdto>> GetAllVariableData(string type, string searchTerm, CancellationToken token);

        Task<List<SelectListdto>> GetApplicaitonFilter(string jobPostId, string type, CancellationToken token);
        Task<List<JobApplicationWithStatus>> GetApplicants([FromBody] GetApplicantsDTO request, CancellationToken token);
    }
}
