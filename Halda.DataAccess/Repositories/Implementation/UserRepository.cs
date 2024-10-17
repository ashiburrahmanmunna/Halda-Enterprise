using Halda.Core.Models;
using Halda.Core.DTO;
using Halda.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Halda.DataAccess.Repositories.Interface;

namespace Halda.DataAccess.Repositories.Implementation
{
    public class UserRepository : BaseRepository<User, string>, IUserRepository
    {
        public UserRepository(HaldaDbContext dbContext) : base(dbContext)
        {
        }
    }
}
