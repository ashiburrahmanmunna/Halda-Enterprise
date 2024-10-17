using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models.Variable
{
    public class Section : BaseModel
    {

        [StringLength(100)]
        public string SecName { get; set; } = string.Empty;
        public string? SecCode { get; set; }

        [StringLength(100)]
        public string? LocalName { get; set; }
        public int Order { get; set; } = 0;

    }
}
