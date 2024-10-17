using Halda.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Halda.Core.DTO;

public class AUserDTO
{
    public string Id { get; set; }
    //public string UserId { get; set; } // based on care panel
    public string UserName { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string? Phone { get; set; }
    public Role Role { get; set; }
    public string? Image { get; set; }

    public string? DeptId { get; set; }
    //public Department Department { get; set; }
    public string? ComId { get; set; }
    //public Company Company { get; set; }
}
