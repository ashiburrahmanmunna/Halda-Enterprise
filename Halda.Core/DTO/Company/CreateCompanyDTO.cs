using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.DTO
{
    public class CreateCompanyDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public bool? isActive { get; set; }
        public string? UserId { get; set; }

    }
}
