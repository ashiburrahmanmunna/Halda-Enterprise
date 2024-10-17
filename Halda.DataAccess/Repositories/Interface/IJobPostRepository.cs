using Halda.Core.DTO;
using Halda.Core.Models;

namespace Halda.DataAccess.Repositories.Interface
{
    public interface IJobPostRepository : IRepository<JobPost, string>
    {
        string CreateJobDescriptionWithMilestone(JobDescriptionViewModel jobDescriptionTemplate);
        string CreateJobWithMilestone(JobDetailsDTO model);
        Task<List<JobDescriptionViewModel>> GetAllJobDescriptionsAsync(CancellationToken token);
        Task<JobDetailsDTO> GetJobDetailsById(string id, CancellationToken token);
        Task<IList<JobListDTO>> GetJobListByComId(string id, CancellationToken token);
        Task<List<DesignationViewModel>> GetMilestonesGroupedByDesignationAsync(string companyId, CancellationToken token);
        Task<bool> DeleteMilestoneById(string Id);

    }
}
