using Halda.Core.DTO;
using Halda.DataAccess.Repositories;
using Halda.DataAccess.Services.Interface.ICompany;
using Mapster;
using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Http.Internal;
using System.Linq.Dynamic.Core.Tokenizer;


namespace Halda.DataAccess.Services.Repository.Company
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;


        public CompanyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<bool> CreateCompany(CompanyDTO companyDto, CancellationToken token)
        {
            var company = companyDto.Adapt<Core.Models.Company>();

            try
            {
                string rootPath = "root";
                string companyFolderPath = Path.Combine(rootPath, companyDto.ComId.ToString());

                if (!Directory.Exists(companyFolderPath))
                {
                    Directory.CreateDirectory(companyFolderPath);
                }

                if (companyDto.CompanyLogoFile != null)
                {
                    string fileName = companyDto.CompanyLogoFile.FileName;
                    string filePath = Path.Combine(companyFolderPath, fileName);

                    if (!File.Exists(filePath))
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
                        {
                            // Pass the token to the CopyToAsync method
                            await companyDto.CompanyLogoFile.CopyToAsync(stream, token);
                        }

                        company.LogoImagePath = filePath;
                        company.LogoFileExtension = Path.GetExtension(fileName);
                    }
                }

                // Assuming _unitOfWork.Save supports cancellation
                await _unitOfWork.Save(token);

                return true;
            }
            catch (OperationCanceledException)
            {
                // Handle the case when the operation is canceled
                return false;
            }
            catch (Exception)
            {
                // Handle other exceptions
                return false;
            }
        }

        public Task<bool> DeleteCompany(string companyId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CompanyDTO>> GetAllCompanies()
        {
            throw new NotImplementedException();
        }

        public async Task<CompanyDTO> GetCompanyById(string companyId, CancellationToken token)
        {
            try
            {
                var company = await _unitOfWork.companyRepository.GetByIdAsync(companyId, token);

                if (company == null)
                {
                    return null;
                }

                var companyDto = company.Adapt<CompanyDTO>();

                if (!string.IsNullOrEmpty(company.LogoImagePath))
                {
                    var filePath = company.LogoImagePath;

                    if (File.Exists(filePath))
                    {
                        companyDto.ComLogo = await File.ReadAllBytesAsync(filePath, token);

                        // If you need to convert to IFormFile, uncomment the following line:
                        // companyDto.CompanyLogoFile = ConvertToIFormFile(filePath);
                    }
                }

                return companyDto;
            }
            catch (OperationCanceledException)
            {
                // Handle cancellation specifically
                throw new OperationCanceledException("The operation was canceled.", token);
            }
            catch (Exception ex)
            {
                // Log the exception as needed
                // _logger.LogError(ex, "Error fetching company by ID.");

                throw new Exception("Error fetching company by ID", ex);
            }
        }

        public Task<bool> UpdateCompany(CompanyDTO company)
        {
            throw new NotImplementedException();
        }

        public static IFormFile ConvertToIFormFile(string filePath)
        {
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var fileName = Path.GetFileName(filePath);
            return new FormFile(fileStream, 0, fileStream.Length, fileName, fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/octet-stream"
            };
        }

        public async Task<bool> GetCompany(string companyId, CancellationToken token)
        {
            try
            {
                var company = await _unitOfWork.companyRepository.GetByIdAsync(companyId, token);
                return company != null;
            }
            catch (OperationCanceledException)
            {
                // Handle the operation being canceled
                throw new OperationCanceledException("The operation was canceled.", token);
            }
            catch (Exception ex)
            {
                // Log the exception (optional, but recommended)
                // _logger.LogError(ex, "Error checking company existence by ID.");

                throw new Exception("Error checking company existence by ID", ex);
            }
        }

    }
}
