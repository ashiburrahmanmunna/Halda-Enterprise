using Halda.Core.Models.Onboarding;
using Halda.DataAccess.Persistence;
using Halda.DataAccess.Repositories;
using Halda.DataAccess.Repositories.Interface;
public class EmployeeEmergencyContactRepository : BaseRepository<EmployeeEmergencyContact, string>, IEmployeeEmergencyContact
    {
        public EmployeeEmergencyContactRepository(HaldaDbContext dbContext): base(dbContext) 
        {

        }
        public async Task SaveOrUpdateAsync(EmployeeEmergencyContact model, CancellationToken token)
        {
            if (string.IsNullOrEmpty(model.Id))
            {
                // Generate a new ID for the model
                model.Id = Guid.NewGuid().ToString();
                await AddAsync(model);
            }
            else
            {
                var existingRecord = await GetByIdAsync(model.Id, token);
                if (existingRecord != null)
                {
                    // Update existing record
                    _dbContext.Entry(existingRecord).CurrentValues.SetValues(model);
                }
                else
                {
                    // Handle the case where the ID doesn't exist
                    throw new InvalidOperationException("Attempted to update a non-existent record");
                }
            }

            // Note: We don't call SaveAsync() here because that will be handled by the Unit of Work
        }
    }




   


