using Halda.Core.Enums;
using Halda.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.DTO.PreOnboarding
{
    public class GetApplicantsDTO
    {
        public string JobPostId { get; set; }
        public Dictionary<string, string> Filters { get; set; }
        public string Milestone { get; set; }
        public string MilestoneId { get; set; }
    }
    public class ApplicantsResponseDTO
    {
        public List<string>? ApplicantStatuses { get; set; }
        public List<JobApplication>? JobApplications { get; set; }
    }
    public class JobApplicationWithStatus
    {

        public string? JobPostId { get; set; }
        public string? ApplicantId { get; set; }
        public string? CompanyId { get; set; }

        public string? Name { get; set; }
        public string? University { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Skills { get; set; }
        public string? Email { get; set; }
        public string? CurrentLocation { get; set; }
        public string? ApplyingPosition { get; set; }
        public string? Experience { get; set; }
        public string? LinkedinProfileLink { get; set; }
        public string? PreviousJobCompanyName { get; set; }
        public string? CurrentSalary { get; set; }
        public string? ExpectedSalary { get; set; }
        public string? CoverLetter { get; set; }
        public string? HowDidYouKnow { get; set; }
        public string? ResumeUrl { get; set; }
        public string? GovtIdUrl { get; set; }
        public string? CertificateUrl { get; set; }
        public string? TranscriptUrl { get; set; }
        public string? SSCCertificateUrl { get; set; }
        public string? HSCCertificateUrl { get; set; }
        public string? MScCertificateUrl { get; set; }
        public string? BScCertificateUrl { get; set; }
        public DateTime? DateApplied { get; set; }
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public bool? IsDelete { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }
        public Status? Status { get; set; } // Adjust the type based on your enum
    }



}

