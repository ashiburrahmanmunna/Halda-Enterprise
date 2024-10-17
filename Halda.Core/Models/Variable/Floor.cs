using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models.Variable
{
    public class Floor : BaseModel
    {

        [StringLength(100)]
        public string FloorName { get; set; } = string.Empty;
        public string? FloorCode { get; set; }

        [StringLength(100)]
        public string? LocalName { get; set; }
        public int Order { get; set; } = 0;

    }
}
