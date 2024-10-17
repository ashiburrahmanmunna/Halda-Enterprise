using System.ComponentModel.DataAnnotations;

namespace Halda.Core.DTO;

public class RegisterRequest
{

    public string fullName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string userCompanyID { get; set; } = string.Empty;
    public bool ispartner { get; set; } = false;
    public bool isCompanyUser { get; set; } = false;
}
public class OtpRequest
{
    public string UserName { get; set; } = string.Empty;
    [Required]
    public string Otp { get; set; } = string.Empty;
}
