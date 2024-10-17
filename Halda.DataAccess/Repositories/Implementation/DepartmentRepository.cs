using Halda.Core.DTO;
using Halda.Core.Models.Variable;
using Halda.DataAccess.Persistence;
using Halda.DataAccess.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Text;
using System.Threading.Tasks;

namespace Halda.DataAccess.Repositories.Implementation
{
    public class DepartmentRepository : BaseRepository<Department, string>, IDepartmentRepository

    {
        public DepartmentRepository(HaldaDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IList<Department>> GetAllForDropDownAsync(CancellationToken token)
        {
            return await _dbContext.Departments.ToListAsync(token);
        }

        public async Task<List<SelectListdto>> GetAllDepartment(string searchTerm,string selectedvalue, CancellationToken token)
        {
           
            var query = _dbContext.Departments.AsQueryable();

            // Check if searchTerm is null or empty, if so load the first 10 departments
            if (string.IsNullOrEmpty(searchTerm))
            {
                // Convert the result of GetAll() to IQueryable
                query = query.Take(10);
            }
            else
            {
                var lowerCaseSearchTerm = searchTerm.ToLower();
                query = query.Where(d => d.DeptName.ToLower().Contains(lowerCaseSearchTerm) ||
                                 d.DeptCode.ToLower().Contains(lowerCaseSearchTerm));
            }

            var result = await query.Select(d => new SelectListdto
            {
                Id = d.Id,
                Text = d.DeptName
            }).ToListAsync(token);

            return result;
        }

    }
}
