namespace Halda.Utilities.Email;

public interface IEmailService
{
    Task SendEmailAsync(List<string> emailaddress, string subject, string htmlMessage);

    Task SendEmailAsync(string emailaddress, string subject, string htmlMessage);

}
