using Halda.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.DataAccess.Services.IServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
        Task ProjectInvitation(ProjectInviteMail mailRequest);
        Task AssignSendEmailAsync(MailRequest mailRequest);
    }
}
