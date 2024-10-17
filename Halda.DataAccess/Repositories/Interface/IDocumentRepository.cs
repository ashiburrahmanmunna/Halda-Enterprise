using Halda.Core.DTO.Onboarding;
using Halda.Core.Models.Onboarding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.DataAccess.Repositories.Interface
{
    public interface IDocumentRepository : IRepository<EmployeeDocument, string>
    {
        Task<EmpDocumentDTO> GetDocumentsData(string employeeId, CancellationToken token);
        Task<List<EmployeeDocument>> GetEmpDocuments(string companyId, CancellationToken token);
    }
}
