using Halda.Core.DTO;
using Halda.Core.Models;

namespace Halda.DataAccess.Repositories.Interface
{
    public interface IJobDescriptionTemplateRepository : IRepository<JobDescriptionTemplate, string>
    {
        Task<string> CreateJobDescriptionWithMilestone(JobDescriptionViewModel jobDescriptionTemplate);
        string CreateJobWithMilestone(JobDetailsDTO model);
        Task<List<JobDescriptionViewModel>> GetAllJobDescriptionsAsync(CancellationToken token);
        Task<JobDescriptionViewModel> GetJobDescriptionsWithMilestoneById(string id, CancellationToken token);
        Task<IList<JobDescriptionTemplate>> GetJobDescriptionsTemplateListByComId(string id, CancellationToken token);
        Task<List<DesignationViewModel>> GetMilestonesGroupedByDesignationAsync(string companyId, CancellationToken token);
        Task<List<DesignationViewModel>> GetMilestonesFilteredByDesignation(string companyId, string desigId, CancellationToken token);
        Task<bool> DeleteMilestoneById(string Id);
        string UpdateJob(JobDetailsDTO model);

    }
}
