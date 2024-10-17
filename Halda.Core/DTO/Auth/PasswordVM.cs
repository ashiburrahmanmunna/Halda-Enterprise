using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Halda.Core.DTO
{
    public class PasswordVM
    {
        public string userName { get; set; }

        [Required(ErrorMessage = "New Password is required")]
        [PasswordPropertyText]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).*$",
            ErrorMessage = "\nAt least 8 characters\n" +
                           "\nAt least one uppercase letter\n" +
                           "\nAt least one lowercase letter\n" +
                           "\nAt least one numeric digit\n" +
                           "\nAt least one special character")]

        public string newPassword { get; set; }
    }
}
