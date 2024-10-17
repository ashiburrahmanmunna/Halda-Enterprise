using System.ComponentModel.DataAnnotations;

namespace Halda.Core.DTO
{
    public class RegisterModel
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? PostId { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string userCompanyID { get; set; } = string.Empty;
    }
}
