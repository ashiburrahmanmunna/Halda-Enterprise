using Microsoft.AspNetCore.Http;

namespace Halda.Core.DTO
{
    public class CompanyVM
    {



        public string? ComId { get; set; }
        public string? CompanySecretCode { get; set; }
        public string? AppKey { get; set; }
        //public ICollection<AppKeys> AppKeys { get; set; }   
        public string? CompanyCode { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyNameBangla { get; set; }
        public string? CompanyShortName { get; set; }
        public string? PrimaryAddress { get; set; }
        public string? CompanyAddressBangla { get; set; }
        public string? SecoundaryAddress { get; set; }
        public string? comPhone { get; set; }
        public string? comPhone2 { get; set; }
        public string? comFax { get; set; }
        public string? comEmail { get; set; }
        public string? comWeb { get; set; }
        public string? BaseComId { get; set; }
        public string? CountryId { get; set; }
        public int? DecimalField { get; set; }
        public string? ContPerson { get; set; }
        public string? ContDesig { get; set; }
        public bool? IsShowCurrencySymbol { get; set; }
        public bool? IsInActive { get; set; }
        public bool? IsGroup { get; set; }
        public bool? IsDoller { get; set; }
        public bool? isBarcode { get; set; }
        public bool? isProduct { get; set; }
        public bool? isCorporate { get; set; }
        public bool? isPOSprint { get; set; }
        public bool? IsService { get; set; }
        public bool? isOldDue { get; set; }
        public bool? isShortcutSale { get; set; }
        public bool? isRestaurantSale { get; set; }
        public bool? isTouch { get; set; }
        public bool? isShoeSale { get; set; }
        public bool? isIMEISale { get; set; }
        public bool? isMultipleWh { get; set; }
        public bool? isMultiCurrency { get; set; }
        public bool? isMultiDebitCredit { get; set; }
        public bool? isVoucherDistributionEntry { get; set; }
        public bool? isChequeDetails { get; set; }
        public byte[]? ComImageHeader { get; set; }
        public string? HeaderImagePath { get; set; }
        public string? HeaderFileExtension { get; set; }
        public byte[]? ComLogo { get; set; }
        public IFormFile? CompanyLogoFile { get; set; } // File data

        public string? LogoImagePath { get; set; }
        public string? LogoFileExtension { get; set; }
        public byte[]? ComSign { get; set; }
        public string? SignImagePath { get; set; }
        public string? SignFileExtension { get; set; }
        public string? Addvertise { get; set; }
        public bool? IsEPZ { get; set; }

    }
}

