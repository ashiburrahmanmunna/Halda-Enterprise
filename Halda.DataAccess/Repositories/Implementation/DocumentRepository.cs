using Halda.Core.DTO.Onboarding;
using Halda.Core.Models.Onboarding;
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
    public class DocumentRepository : BaseRepository<EmployeeDocument, string>, IDocumentRepository
    {
        public DocumentRepository(HaldaDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<EmpDocumentDTO> GetDocumentsData(string employeeId, CancellationToken token)
        {
            // Fetch the employee data by employeeId
            var employeeData = await _dbContext.EmployeeDocuments
                .Where(x => x.EmployeeId == employeeId)
                .Select(x => new EmpDocumentDTO
                {
                    Id = x.Id,
                    Type = x.Type,
                    DocPath = x.DocPath,                    
                    EmployeeId = x.EmployeeId
                })
                .FirstOrDefaultAsync(token);



            return employeeData;
        }

        public async Task<List<EmployeeDocument>> GetEmpDocuments(string companyId, CancellationToken token)
        {

            var query = _dbContext.EmployeeDocuments
                .AsQueryable();
            
            query = query.Where(x => x.CompanyId == companyId);


            // Paginate the results
            var employees = await query
                .OrderByDescending(x => x.Id)
                .ToListAsync(token);

            // Return only the employee list as expected by the interface
            return employees;

        }

    }
}
