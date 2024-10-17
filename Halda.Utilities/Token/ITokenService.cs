namespace Halda.Application.Services
{
    public interface ITokenService
    {
        void ReadToken(string idtoken);

        string ValidateToken(string token);
    }
}
