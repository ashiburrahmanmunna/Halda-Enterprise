using Halda.Core.DTO.Onboarding;
using Halda.Core.Models.Onboarding;
using Halda.DataAccess.Persistence;
using Halda.DataAccess.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Halda.DataAccess.Repositories.Implementation
{
    public class TaxDetailsRepository : BaseRepository<EmployeeTax, string>, ITaxDetailsRepository
    {
        public TaxDetailsRepository(HaldaDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<EmployeeTaxsDTO> GetTaxDetailData(string employeeId, CancellationToken token)
        {
            // Fetch the employee data by employeeId
            var employeeData = await _dbContext.EmployeeTaxs
                .Where(x => x.EmployeeId == employeeId)
                .Select(x => new EmployeeTaxsDTO
                {
                    Id = x.Id,
                    TaxYear = x.TaxYear,
                    Name = x.Name,
                    ReturnSubmit = x.ReturnSubmit,
                    ReturnSubmitDate = x.ReturnSubmitDate.Value.ToString("dd-MMM-yyyy"),
                    AcknowledgmentSlipRecDate = x.AcknowledgmentSlipRecDate.Value.ToString("dd-MMM-yyyy"),
                    TaxCertificateReceive = x.TaxCertificateReceive,
                    TaxExtension = x.TaxExtension,
                    EmployeeId = x.EmployeeId
                })
                .FirstOrDefaultAsync(token);



            return employeeData;
        }

    }
}
