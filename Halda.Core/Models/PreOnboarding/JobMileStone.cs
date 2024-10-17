using Halda.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models
{
    public class JobMileStone : BaseModel
    {
        public string Name { get; set; }

        [ForeignKey("JobPost")]
        public string? JobPostId { get; set; }
        public virtual JobPost JobPosts { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public DefaultType? DefaultType { get; set; }
        public bool? IsAssignment { get; set; }
        public Status? Status { get; set; }    
    }
}
