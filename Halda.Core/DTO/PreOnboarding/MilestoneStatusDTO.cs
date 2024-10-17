using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.DTO.PreOnboarding
{
    public class MilestoneStatusDTO
    {
        public List<string>? ApplicantId { get; set; }
        public string? JobPostId { get; set; }
        public string? RecruitmentVariable { get; set; }
        public string? MilestoneId { get; set; }
    }
}
