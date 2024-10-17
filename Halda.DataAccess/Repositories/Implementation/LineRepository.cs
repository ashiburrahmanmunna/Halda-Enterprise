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
    public class LineRepository : BaseRepository<Line, string>, ILineRepository

    {
        public LineRepository(HaldaDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IList<Line>> GetAllForDropDownAsync(CancellationToken token)
        {
            return await _dbContext.Lines.ToListAsync(token);
        }

        public async Task<List<SelectListdto>> GetAllLine(string searchTerm, CancellationToken token)
        {

            var query = _dbContext.Lines.AsQueryable();

            // Check if searchTerm is null or empty, if so load the first 10 departments
            if (string.IsNullOrEmpty(searchTerm))
            {
                // Convert the result of GetAll() to IQueryable
                query = query.Take(10);
            }
            else
            {
                var lowerCaseSearchTerm = searchTerm.ToLower();
                query = query.Where(d => d.LineName.ToLower().Contains(lowerCaseSearchTerm) ||
                                 d.LineCode.ToLower().Contains(lowerCaseSearchTerm));
            }

            var result = await query.Select(d => new SelectListdto
            {
                Id = d.Id,
                Text = d.LineName
            }).ToListAsync(token);

            return result;
        }
    }
}
