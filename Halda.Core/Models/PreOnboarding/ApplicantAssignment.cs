using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models
{
    public class ApplicantAssignment : SelfModel
    { 
        public string? ApplicantId { get; set; }
        public virtual Applicant? Applicant { get; set; }

        public string? AssignmentId { get; set; }
        public virtual Assignment? Assignment { get; set; }
        public string? CompanyId { get; set; }
        public virtual Company? Company { get; set; }
        public string? JobPostId { get; set; }
        public virtual JobPost? JobPost { get; set; }

        public string? UploadedFilePath { get; set; }
        public bool IsSubmitted { get; set; } = false;
        public bool IsApproved { get; set; } = false;
        public int? Serial {  get; set; }
        public string? Type { get; set; }
    }
}
