using Halda.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.DTO.PreOnboarding
{
    public class ApplicantAssignmentDTO
    {
        public List<string>? ApplicantId { get; set; }

        public List<string>? AssignmentId { get; set; }
        public string? CompanyId { get; set; }
        public string? JobPostId { get; set; }
        public string? UploadedFilePath { get; set; }
        public bool IsSubmitted { get; set; } = false;
        public int? Serial { get; set; }
        public string? Type { get; set; }
    }
}
