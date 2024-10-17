using Halda.Core.DTO;
using Halda.Core.Models.Variable;
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
    public class DesignationRepository : BaseRepository<Designation, string>, IDesignationRepository

    {
        public DesignationRepository(HaldaDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IList<Designation>> GetAllForDropDownAsync(CancellationToken token)
        {
            return await _dbContext.Designations.ToListAsync(token);
        }

        public async Task<List<SelectListdto>> GetAllDesignation(string searchTerm,string selectedvalue, CancellationToken token)
        {

            var query = _dbContext.Designations.AsQueryable();

            // Check if searchTerm is null or empty, if so load the first 10 departments
            if (string.IsNullOrEmpty(searchTerm))
            {
                // Convert the result of GetAll() to IQueryable
                query = query.Take(10);
            }
            else
            {
                var lowerCaseSearchTerm = searchTerm.ToLower();
                query = query.Where(d => d.DesigName.ToLower().Contains(lowerCaseSearchTerm) ||
                                 d.DesigCode.ToLower().Contains(lowerCaseSearchTerm));
            }

            var result = await query.Select(d => new SelectListdto
            {
                Id = d.Id,
                Text = d.DesigName                
            }).ToListAsync(token);            
           
            return result;
        }

    }
}
