using Halda.Core.Enums;
using Halda.Core.Models.Variable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Halda.Core.Models
{
    public class JobDescriptionTemplate:BaseModel
    {    
        public string? DesignationId { get; set; }
        public virtual Designation? Designation { get; set; }   
        public string? Job_Title { get; set; }  
        public string[]? Skills { get; set; } 
        public string? Description { get; set; }    
        public string[]? Responsibility { get; set; } 
        public string[]? Qulifications { get; set; }  
        public string[]? Benefits { get; set; }
        public string[]? OtherInfo { get; set; }  
        public DefaultType? DefaultType { get; set; }
       public virtual ICollection<Milestone> Milestones { get; set; } = new List<Milestone>();

    }
}
