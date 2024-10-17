using Halda.Core.Models.Onboarding;
using Halda.DataAccess.Persistence;
using Halda.DataAccess.Repositories;
using Halda.DataAccess.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

public class EmployeeEducationRepository : BaseRepository<EmpEdu, string>, IEmployeeEducationRepository
    {
        public EmployeeEducationRepository(HaldaDbContext dbContext) : base(dbContext)
        {

        }

        // Custom method to retrieve education records by EmployeeId
        public async Task<IEnumerable<EmpEdu>> GetEducationByEmployeeIdAsync(string employeeId, CancellationToken token)
        {
            return await _dbContext.EmpEdus
                                   .Where(e => e.EmployeeId == employeeId)
                                   .ToListAsync(token);
        }
        // Method to update education record
        public async Task<bool> UpdateEducationRecordAsync(EmpEdu educationRecord, CancellationToken token)
        {
            try
            {
                var existingRecord = await _dbContext.EmpEdus.FindAsync(new object[] { educationRecord.Id }, token);
                if (existingRecord == null)
                {
                    return false;
                }

                // Update properties
                existingRecord.Title = educationRecord.Title;
                existingRecord.SubjectName = educationRecord.SubjectName;
                existingRecord.Institute = educationRecord.Institute;
                existingRecord.StartYear = educationRecord.StartYear;
                existingRecord.EndYear = educationRecord.EndYear;
                existingRecord.Grade = educationRecord.Grade;
                existingRecord.CertificationType = educationRecord.CertificationType;

                _dbContext.EmpEdus.Update(existingRecord);
                await _dbContext.SaveChangesAsync(token);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }




   


