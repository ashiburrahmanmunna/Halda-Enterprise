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
    public class BankDetailsRepository : BaseRepository<EmployeeBank, string>, IBankDetailsRepository
    {
        public BankDetailsRepository(HaldaDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<EmployeeBankDTO> GetBankDetailData(string employeeId, CancellationToken token)
        {
            // Fetch the employee data by employeeId
            var employeeData = await _dbContext.EmployeeBanks
                .Where(x => x.EmployeeId == employeeId)
                .Select(x => new EmployeeBankDTO
                {
                    Id = x.Id,
                    AccHolderName = x.AccHolderName,
                    AccType = x.AccType,
                    AccTypeText = x.AccType.HasValue ? x.AccType.ToString() : null,
                    AccNumber = x.AccNumber,
                    ReAccNumber = x.ReAccNumber,
                    BankName = x.BankName,
                    BankNumber = x.BankNumber,
                    RoutingNumber = x.RoutingNumber,
                    EmployeeId = x.EmployeeId
                })
                .FirstOrDefaultAsync(token);



            return employeeData;
        }

    }
}
