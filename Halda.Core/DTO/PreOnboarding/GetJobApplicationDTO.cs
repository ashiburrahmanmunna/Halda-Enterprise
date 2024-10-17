using Halda.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.DTO.PreOnboarding
{
    public class GetJobApplicationDTO
    {
       // public JobApplication? JobApplication { get; set; }

        public FileStream? Resume { get; set; }
        public IFormFile? GovtId { get; set; }
        public IFormFile? Certificate { get; set; }
        public IFormFile? Transcript { get; set; }
        public IFormFile? SSCCertificate { get; set; }
        public IFormFile? HSCCertificate { get; set; }
        public IFormFile? MScCertificate { get; set; }
        public IFormFile? BScCertificate { get; set; }
    }
}
