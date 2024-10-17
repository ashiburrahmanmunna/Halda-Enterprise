namespace Halda.Core.DTO;
public class TokenResult
{

    public string access_token { get; set; } = string.Empty;
    public string accessToken { get; set; } = string.Empty;

    public DateTime Expair { get; set; }

    public string Refresh_token { get; set; } = string.Empty;
    public string token_type { get; set; } = string.Empty;


}
