using System.ComponentModel.DataAnnotations;

namespace Halda.Core.DTO
{
    public class OtpVm
    {
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }
    }
}
