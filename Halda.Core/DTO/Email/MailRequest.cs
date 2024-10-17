using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.DTO
{
    public class MailRequest
    {
        public string ComId { get; set; }
        public string[] ToEmail { get; set; }
        [ValidateNever]
        public string Subject { get; set; }
        [ValidateNever]
        public string Body { get; set; }
        [ValidateNever]
        public List<IFormFile> Attachments { get; set; }
    }
}
