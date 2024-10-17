using Halda.Core.DTO;

namespace Halda.DataAccess.Services.Interface.ICompany
{
    public interface ICompanyService
    {
        Task<bool> CreateCompany(CompanyDTO company, CancellationToken token);
        Task<bool> UpdateCompany(CompanyDTO company);
        Task<bool> DeleteCompany(string companyId);
        Task<CompanyDTO> GetCompanyById(string companyId, CancellationToken token);
        Task<bool> GetCompany(string companyId, CancellationToken token);
        Task<IEnumerable<CompanyDTO>> GetAllCompanies();
        // Task<IEnumerable<CompanyDTO>> FindCompaniesByCriteria(CompanySearchCriteriaDTO searchCriteria);
    }
}
