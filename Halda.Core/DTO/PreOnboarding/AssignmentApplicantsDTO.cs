using Halda.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.DTO.PreOnboarding
{
    public class AssignmentApplicantsDTO
    {
        public string? ApplicantId { get; set; }
        public string? JobPostId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public List<AssignmentStatusDTO>? Assignments { get; set; }
      //  public bool? isSelected { get; set; }
    }

    public class AssignmentStatusDTO
    {
        public string? AssignmentId { get; set; }
        public string? ApplicantId { get; set; }
        public bool? IsSubmitted { get; set; }
        public bool? IsApproved { get; set; }
        public int? Serial { get; set; }
        public List<Assignment1DTO>? Assignments { get; set; }
    }

    public class Assignment1DTO
    {
        public string? AssignmentId { get; set; }
        public bool IsSubmitted { get; set; }
        public bool IsApproved { get; set; }
        // Add other relevant properties from ApplicantsAssignments as needed
    }
}
