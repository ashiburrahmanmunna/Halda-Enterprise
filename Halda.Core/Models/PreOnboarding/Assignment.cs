using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models
{
    public class Assignment:BaseModel
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Notice { get; set; }
        public DateOnly? DueDate { get; set; }
        public string? AssignBy { get; set; }
        public string? Files { get; set; }

        // public string? ApplicantId { get; set; }
        // public virtual Applicant Applicant { get; set; }    
        //   public string? JobPostId { get; set; }
        //  public virtual JobPost JobPost { get; set; }       
        //  public virtual ICollection<ApplicantAssignment> ApplicantAssignments { get; set; } = new List<ApplicantAssignment>();
    }
}
