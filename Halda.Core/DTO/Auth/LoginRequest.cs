
namespace Halda.Core.DTO;

public class LoginRequest
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? PostId { get; set; } = string.Empty;
}
