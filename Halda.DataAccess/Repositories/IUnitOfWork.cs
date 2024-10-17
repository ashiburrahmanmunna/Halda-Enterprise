using Halda.DataAccess.Repositories.Interface;
using Halda.Utilities.FileUpload;

namespace Halda.DataAccess.Repositories
{
    public interface IUnitOfWork : IAsyncDisposable
    {


        ICompanyRepository companyRepository { get; }
        IUserRepository userRepository { get; }
        IDesignationRepository designationRepository { get; }
        IRecruitmentVariableRepository recruitmentVariableRepository { get; }
        IJobDescriptionTemplateRepository jobDescriptionTemplateRepository { get; }
        IJobPostRepository jobPostRepository { get; }
        IApplicantRepository applicantRepository { get; }
        IEmployeeRepository employeeRepository { get; }
        IEmployeeEducationRepository employeeeduRepository { get; }
        IEmployeeFamNomiRepository employeefamnomiRepository {  get; }
        IEmployeeAddressRepository employeeAddRepo { get; }
        IEmployeeEmergencyContact employeeContactRepo {  get; }
        IDepartmentRepository departmentRepository { get; }
        IVariableRepository variableRepository { get; }
        ISectionRepository sectionRepository { get; }
        ILineRepository lineRepository { get; }
        IFloorRepository floorRepository { get; }
        IEmployeeBankRepository employeeBankRepository { get; }
        IEmployeeTaxRepository employeeTaxRepository { get; }
        ILeaveRepository leaveRepository { get; }
        IEmpOrganizationRepository empOrganizationRepository { get; }
        IShiftRepository shiftRepository { get; }
        IHolidayRepository holidayRepository { get; }
        ITeamMemberRepository teamMemberRepository { get; }
        IBankDetailsRepository bankDetailsRepository { get; }
        ITaxDetailsRepository taxDetailsRepository { get; }
        ILeaveAdjustRepository leaveAdjustRepository { get; }
        IDocumentRepository documentRepository { get; }
        IForecastingRepository forecastingRepository { get; }



        Task Save(CancellationToken token);
    }
}
