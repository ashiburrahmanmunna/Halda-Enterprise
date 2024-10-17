
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Halda.Core.DTO
{
    public class CompanyDTO
    {
        public string Id { get; set; }
        public string ComId { get; set; }
        public string Name { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public bool? isActive { get; set; }
        public UserDTO? User { get; set; }
        public IFormFile? CompanyLogoFile { get; set; }
        public byte[] ComLogo { get; set; }
    }
}
