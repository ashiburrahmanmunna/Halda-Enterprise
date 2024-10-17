using Halda.Core.Enums;
using Halda.Core.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Halda.Core.Models
{
    public class JobApplication : SelfModel
    {    

        [ForeignKey("JobPost")]
        public string? JobPostId { get; set; }
        public virtual JobPost? JobPost { get; set; }

        [ForeignKey("Applicant")]
        public string? ApplicantId { get; set; }
        public virtual Applicant? Applicant { get; set; }

        [ForeignKey("Company")]
        public string? CompanyId { get; set; }
        public virtual Company? Company { get; set; }
      
        //public Status? ApplyingStatus { get; set; }


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
      //  public virtual List<Applicant>? Applicants { get; set; } = new List<Applicant>();    
    }
}
