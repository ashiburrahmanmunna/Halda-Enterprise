using Halda.Core.DTO;
using Halda.Core.DTO.PreOnboarding;
using Halda.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Halda.DataAccess.Repositories.Interface
{
    public interface IApplicantRepository : IRepository<Applicant, string>
    {
        Task<string> GetFilePath(string id, CancellationToken token);
        Task<JobApplication> GetJobApplication(string applicantId, string jobPostId, CancellationToken token);
        bool CreateApplicant(ApplicantDTO applicant);

        Task<string> CreateApplicantAsync(JobApplicationDTO jobApplicationDto, CancellationToken token);
        Task<string> UpdateMilestoneStatus(MilestoneStatusDTO milestoneStatusDTO, CancellationToken token);

        Task<string> SaveAssignment(AssignmentDTO assignmentDto, string filesJson, CancellationToken token);

        Task<IList<Assignment>> GetAssignment(string comId, CancellationToken token);
        Task<bool> AssignAssignment(ApplicantAssignmentDTO applicantAssignmentDTO);

        Task<List<AssignmentApplicantsDTO>> GetAssignmentApplicants(string jobPostId, string milestoneId, CancellationToken token);
        Task AssignmentApprove(string assignmentId, string applicantId, CancellationToken token);
        Task<List<JobDashboardDTO>> GetApplicantJobApplicationsAsync(string applicantId);

        Task<JobDetailsViewModel> GetJobDetailsById (string jobPostId, string applicantId, CancellationToken token);
        
    }
}
