using Halda.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models
{
    public class ApplicantApplicationStatus
    {
        public string Id { get; set; } = Guid.NewGuid().ToString(); 

        [ForeignKey(nameof(Applicant))]
        public string? ApplicantId { get; set; } 
        public virtual Applicant Applicant { get; set; }

        [ForeignKey(nameof(JobMileStone))]
        public string MilestoneId { get; set; }  

        public virtual JobMileStone JobMileStone { get; set;}

        [ForeignKey (nameof(JobPost))]
        public string? JobPostId { get; set; }   
        public virtual JobPost JobPost { get; set; }    
        public  Status? Status { get; set; } 

    }
}
