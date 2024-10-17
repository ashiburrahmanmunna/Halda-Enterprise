using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models
{
    public class VariableData : SelfModel
    {
        public string Type { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string? LocalName { get; set; } = string.Empty;
        public int Order { get; set; } = 0;
    }
}
