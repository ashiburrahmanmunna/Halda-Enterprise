using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.DTO.Auth
{
    public class UserCompanyDto
    {
        public UserDto? UserInfo { get; set; }
        public CompanyInfoDto? CompanyInfo { get; set; }
        public IList<CompanyInfoDto>? MyCompany { get; set; }
        public IList<CompanyInfoDto>? OtherCompany { get; set; }
    }
    public class UserDto
    {
        public string Id { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
        public string? ProfileImage { get; set; }

        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

    }
    public class CompanyInfoDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? AddresLineOne { get; set; }
        public string? AddresLinetwo { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipOrPostalCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? TeachnicalContactEmail { get; set; }
        public string? TradeLicenseNo { get; set; }
        public bool IsOwner { get; set; }
        public bool IsActive { get; set; }
    }
}
