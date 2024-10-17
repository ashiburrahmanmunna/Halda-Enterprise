using Halda.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Halda.Core.Models
{
    public class BaseModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? CompanyId { get; set; }
        public virtual Company? Company { get; set; }
        public int? Serial { get; set; }
        public string? UserId { get; set; }
        public string? UpdateByUserId { get; set; }
        public bool? IsDelete { get; set; }

       
        public DateTime? DateAdded { get; set; }=DateTime.UtcNow;
       
        public DateTime? DateUpdated { get; set; }
    }
}
