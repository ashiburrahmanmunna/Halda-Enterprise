using System.ComponentModel.DataAnnotations;

namespace Halda.Core.DTO
{
    public class LoginDTO
    {
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
