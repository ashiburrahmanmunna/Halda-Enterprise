using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models.Variable
{
    public class Line : BaseModel
    {

        [StringLength(100)]
        public string LineName { get; set; } = string.Empty;
        public string? LineCode { get; set; }

        [StringLength(100)]
        public string? LocalName { get; set; }
        public int Order { get; set; } = 0;

    }
}
