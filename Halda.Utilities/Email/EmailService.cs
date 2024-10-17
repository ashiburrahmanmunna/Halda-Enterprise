using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Halda.Utilities.Email;

public class EmailService : IEmailService
{
    private readonly SMTPConfigModel _smtpConfig;

    public EmailService(IOptions<SMTPConfigModel> smtpConfigOptions)
    {
        _smtpConfig = smtpConfigOptions.Value;
    }
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        MailMessage mailMessage = new MailMessage
        {
            Subject = subject,
            Body = htmlMessage,
            From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
            IsBodyHtml = _smtpConfig.IsBodyHTML,

        };
        mailMessage.To.Add(email);


        NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);

        SmtpClient smtpClient = new SmtpClient();

        smtpClient.Host = _smtpConfig.Host;
        smtpClient.Port = _smtpConfig.Port;
        smtpClient.EnableSsl = _smtpConfig.EnableSSL;
        smtpClient.UseDefaultCredentials = _smtpConfig.UseDefaultCredentials;
        smtpClient.Credentials = networkCredential;
        mailMessage.BodyEncoding = Encoding.Default;

        await smtpClient.SendMailAsync(mailMessage);
    }

    public async Task SendEmailAsync(List<string> email, string subject, string htmlMessage)
    {
        MailMessage mailMessage = new MailMessage
        {
            Subject = subject,
            Body = htmlMessage,
            From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
            IsBodyHtml = _smtpConfig.IsBodyHTML
        };

        foreach (var toEmail in email)
        {
            mailMessage.To.Add(toEmail);
        }

        NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);

        SmtpClient smtpClient = new SmtpClient();

        smtpClient.Host = _smtpConfig.Host;
        smtpClient.Port = _smtpConfig.Port;
        smtpClient.EnableSsl = _smtpConfig.EnableSSL;
        smtpClient.UseDefaultCredentials = _smtpConfig.UseDefaultCredentials;
        smtpClient.Credentials = networkCredential;
        mailMessage.BodyEncoding = Encoding.Default;

        await smtpClient.SendMailAsync(mailMessage);
    }

    //private string GetEmailBody(string templateName)
    //{
    //    var body = File.ReadAllText(string.Format("", templateName));
    //    return body;
    //}


}
