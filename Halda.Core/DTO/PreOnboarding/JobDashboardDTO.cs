using Halda.Core.Enums;
using Halda.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.DTO.PreOnboarding
{
    public class JobDashboardDTO
    {
        public string JobApplicationId { get; set; }
        public string ApplicantId { get; set; }
        public string JobPostId { get; set; }
        public string ApplyingPosition { get; set; }
        public string CompanyName { get; set; }
        public List<JobStatusDTO> JobStatuses { get; set; } = new List<JobStatusDTO>();
    }

    // DTO for returning milestone and application status
    public class JobStatusDTO
    {
        public string MilestoneName { get; set; }
        public Status? MilestoneStatus { get; set; }
        public Status? ApplicationStatus { get; set; }
        public int? Serial { get; set; }
    }
}
