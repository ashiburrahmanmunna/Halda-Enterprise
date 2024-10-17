using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.Models
{
    public class SelfModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public bool? IsDelete { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
