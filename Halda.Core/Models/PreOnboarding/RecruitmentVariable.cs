using Halda.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models
{
    public class RecruitmentVariable : BaseModel
    {
        public string? Name { get; set; }   
        public string? Description { get; set; }
        public string? Type { get; set; }
        public DefaultType? DefaultType { get; set; }    

    }
}
