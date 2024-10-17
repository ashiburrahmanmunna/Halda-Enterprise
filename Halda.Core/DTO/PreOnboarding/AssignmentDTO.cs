using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.DTO.PreOnboarding
{
    public class AssignmentDTO
    {
        public string? AssignmentName { get; set; }
        public string? AssignmentId { get; set; }
        public string? AssignBy { get; set; }
        public DateOnly? Deadline { get; set; }
        public IFormFile? Files { get; set; }
        public string? CompanyId { get; set; }
        public string? UserId { get; set; }
        public string? jobPostId { get; set; }
        public int? Serial { get; set; }
        
    }
}
