using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using Halda.Core.DTO;
using Halda.DataAccess.Services.IServices;


namespace Halda.Application.Services
{
    internal class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;
        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }






        public async Task ProjectInvitation(ProjectInviteMail mailRequest)
        {




            foreach (string toEmail in mailRequest.ToEmail)
            {
                var message = new MailMessage();
                message.From = new MailAddress(_mailSettings.Mail, "OKR");
                message.To.Add(new MailAddress(toEmail));
                message.Subject = "OKR Invition";
                message.Body = mailRequest.Body;
                string projectInvitation = "https://gtrbd.net/OKR/Account/Project?ComId=" + mailRequest.ComId + "&Pid=" + mailRequest.Pid;

                message.Body = $"{mailRequest.Body}<br><br>Project Link: <a href='{projectInvitation}'>{projectInvitation}</a>";
                message.IsBodyHtml = _mailSettings.IsBodyHTML;

                using (var client = new System.Net.Mail.SmtpClient())
                {
                    client.Host = _mailSettings.Host;
                    client.Port = _mailSettings.Port;
                    client.EnableSsl = _mailSettings.EnableSSL;

                    client.Credentials = new NetworkCredential(_mailSettings.UserName, _mailSettings.Password);
                    client.Send(message);
                }
            }

        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {




            foreach (string toEmail in mailRequest.ToEmail)
            {
                var message = new MailMessage();
                message.From = new MailAddress(_mailSettings.Mail, "OKR");
                message.To.Add(new MailAddress(toEmail));

                //if (emailCC != null)
                //{

                //    foreach (var mailCC in emailCC)
                //    {
                //        message.CC.Add(new MailAddress(mailCC));
                //    }
                //}
                message.Subject = "OKR Invition";
                message.Body = mailRequest.Body;
                //message.Attachments.Add(new System.Net.Mail.Attachment(attchment));
                string registrationLink = "https://gtrbd.net/OKR/Account/Register?ComId=" + mailRequest.ComId;
                message.Body = $"{mailRequest.Body}<br><br>Registration Link: <a href='{registrationLink}'>{registrationLink}</a>";
                message.IsBodyHtml = _mailSettings.IsBodyHTML; //true;

                using (var client = new System.Net.Mail.SmtpClient())
                {
                    client.Host = _mailSettings.Host; //"smtp.gmail.com";
                    client.Port = _mailSettings.Port;//587;
                    client.EnableSsl = _mailSettings.EnableSSL;// true;
                                                               //client.Credentials = new NetworkCredential(config.GetSection("CredentialMail").Value, config.GetSection("CredentialPassword").Value);
                    client.Credentials = new NetworkCredential(_mailSettings.UserName, _mailSettings.Password);
                    client.Send(message);
                }
            }




            //var email = new MimeMessage();
            //email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            //email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            //email.Subject = mailRequest.Subject;

            //var builder = new BodyBuilder();
            //if (mailRequest.Attachments != null)
            //{
            //    byte[] fileBytes;
            //    foreach (var file in mailRequest.Attachments)
            //    {
            //        if (file.Length > 0)
            //        {
            //            using (var ms = new MemoryStream())
            //            {
            //                file.CopyTo(ms);
            //                fileBytes = ms.ToArray();
            //            }
            //            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
            //        }
            //    }
            //}
            ////mailRequest.Body = "This is test";
            //builder.HtmlBody = mailRequest.Body;
            //email.Body = builder.ToMessageBody();
            //using var smtp = new MailKit.Net.Smtp.SmtpClient();
            //await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            ////smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.SslOnConnect);
            //smtp.Authenticate(_mailSettings.UserName, _mailSettings.Password);
            //var response = await smtp.SendAsync(email);
            //await smtp.DisconnectAsync(true);
        }


        public async Task AssignSendEmailAsync(MailRequest mailRequest)
        {



            foreach (string toEmail in mailRequest.ToEmail)
            {
                var message = new MailMessage();
                message.From = new MailAddress(_mailSettings.Mail, "OKR");

                message.To.Add(new MailAddress(toEmail));

                //if (emailCC != null)
                //{

                //    foreach (var mailCC in emailCC)
                //    {
                //        message.CC.Add(new MailAddress(mailCC));
                //    }
                //}
                message.Subject = mailRequest.Subject;
                message.Body = mailRequest.Body;
                //message.Attachments.Add(new System.Net.Mail.Attachment(attchment));
                // string registrationLink = "https://gtrbd.net/OKR/Account/RegisterUser?ComId=" + mailRequest.ComId;
                //message.Body = $"{mailRequest.Body}<br><br>Registration Link: <a href='{registrationLink}'>{registrationLink}</a>";
                message.IsBodyHtml = _mailSettings.IsBodyHTML; //true;

                using (var client = new System.Net.Mail.SmtpClient())
                {
                    client.Host = _mailSettings.Host; //"smtp.gmail.com";
                    client.Port = _mailSettings.Port;//587;
                    client.EnableSsl = _mailSettings.EnableSSL;// true;
                                                               //client.Credentials = new NetworkCredential(config.GetSection("CredentialMail").Value, config.GetSection("CredentialPassword").Value);
                    client.Credentials = new NetworkCredential(_mailSettings.UserName, _mailSettings.Password);
                    client.Send(message);
                }
            }
        }

    }
}
