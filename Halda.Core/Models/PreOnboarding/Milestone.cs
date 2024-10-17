using Halda.Core.Enums;
using Halda.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models
{
    public class Milestone:BaseModel
    { 
        public string Name { get; set; }

        [ForeignKey("JobDescriptionTemplate")]
        public string? JobDescriptionId { get; set; }
        //public string? Type { get; set; }
        public virtual JobDescriptionTemplate JobDescriptionTemplate { get; set; }
        public DefaultType? DefaultType { get; set; }  
        public bool? IsAssignment { get; set; }
    }
}
