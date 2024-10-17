using Halda.Core.Enums;
using Halda.Core.Models.Variable;
using System.ComponentModel.DataAnnotations;

namespace Halda.Core.Models
{
    public class User : BaseModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string? Phone { get; set; }
        public CompanyRole Role { get; set; }
        public string? Image { get; set; }
        public bool? IsDelete { get; set; } = false;
        public string? DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public string? DesignationId { get; set; }
        public virtual Designation Designation { get; set; }
    }
}
