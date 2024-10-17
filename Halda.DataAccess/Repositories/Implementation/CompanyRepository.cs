using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Halda.Core.Models;
using Halda.DataAccess.Persistence;
using Halda.DataAccess.Repositories.Interface;
using Microsoft.EntityFrameworkCore;


namespace Halda.DataAccess.Repositories.Implementation
{
    public class CompanyRepository : BaseRepository<Company, string>, ICompanyRepository
    {
        public CompanyRepository(HaldaDbContext dbContext) : base(dbContext)
        {
        }
    }
}
