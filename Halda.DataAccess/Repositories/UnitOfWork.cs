using Halda.Core.Models.Attendance;
using Halda.DataAccess.Persistence;
using Halda.DataAccess.Repositories.Implementation;
using Halda.DataAccess.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
namespace Halda.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly DbContext _DbContext;

        #region Repositories
        public ICompanyRepository companyRepository { get; private set; }

        public IUserRepository userRepository { get; private set; }
        public IDesignationRepository designationRepository { get; private set; }
        public IRecruitmentVariableRepository recruitmentVariableRepository { get; private set; }
        public IJobDescriptionTemplateRepository jobDescriptionTemplateRepository { get; private set; }
        public IJobPostRepository jobPostRepository { get; private set; }
        public IApplicantRepository applicantRepository { get; private set; }
        public IEmployeeRepository employeeRepository { get; private set; }
        public IEmployeeEducationRepository employeeeduRepository { get; private set; }
        public IEmployeeAddressRepository employeeAddRepo { get; private set; }
        public IEmployeeFamNomiRepository employeefamnomiRepository {  get; private set; }
        public IEmployeeEmergencyContact employeeContactRepo {  get; private set; }
        public IDepartmentRepository departmentRepository { get; private set; }        
        public ISectionRepository sectionRepository { get; private set; }
        public ILineRepository lineRepository { get; private set; }
        public IFloorRepository floorRepository { get; private set; }
        public IVariableRepository variableRepository { get; private set; }
        public IEmployeeBankRepository employeeBankRepository { get; private set; }
        public IEmployeeTaxRepository employeeTaxRepository { get; private set; }
        public ILeaveRepository leaveRepository { get; private set; }
        public IEmpOrganizationRepository empOrganizationRepository { get; private set; }
        public IShiftRepository shiftRepository { get; private set; }
        public IHolidayRepository holidayRepository { get; private set; }
        public ITeamMemberRepository teamMemberRepository { get; private set; }
        public IBankDetailsRepository bankDetailsRepository { get; private set; }
        public ITaxDetailsRepository taxDetailsRepository { get; private set; }
        public ILeaveAdjustRepository leaveAdjustRepository { get; private set; }   
        public IDocumentRepository documentRepository { get; private set; }   
        public IForecastingRepository forecastingRepository { get; private set; }



        #endregion

        public UnitOfWork(HaldaDbContext context)
        {
            _DbContext = context;
            companyRepository = new CompanyRepository(context);
            userRepository = new UserRepository(context);
            designationRepository = new DesignationRepository(context);
            recruitmentVariableRepository = new RecruitmentVariableRepository(context);
            jobDescriptionTemplateRepository = new JobDescriptionTemplateRepository(context);
            jobPostRepository = new JobPostRepository(context);
            applicantRepository = new ApplicantRepository(context);
            employeeRepository = new EmployeeRepository(context);
            employeeeduRepository = new EmployeeEducationRepository(context);
            employeefamnomiRepository = new EmployeeFamNomiRepository(context);
            employeeAddRepo = new EmployeeAddressRepository(context);
            employeeContactRepo = new EmployeeEmergencyContactRepository(context);         
            departmentRepository = new DepartmentRepository(context);
            sectionRepository = new SectionRepository(context);
            lineRepository = new LineRepository(context);
            floorRepository = new FloorRepository(context);
            variableRepository = new VariableRepository(context);
            employeeBankRepository = new EmployeeBankRepository(context);
            employeeTaxRepository = new EmployeeTaxRepository(context);
            leaveRepository = new LeaveRepository(context);
            empOrganizationRepository = new EmpOrganizationRepository(context);
            teamMemberRepository = new TeamMemberRepository(context);
            shiftRepository = new ShiftRepository(context);
            holidayRepository = new HolidayRepository(context);
            bankDetailsRepository = new BankDetailsRepository(context);
            taxDetailsRepository = new TaxDetailsRepository(context);
            leaveAdjustRepository = new LeaveAdjustRepository(context);
            documentRepository = new DocumentRepository(context);
            forecastingRepository = new ForecastingRepository(context);

        }
        public async ValueTask DisposeAsync()
        {
            if (_DbContext != null)
            {
                await _DbContext.DisposeAsync();
            }
        }

        public virtual async Task Save(CancellationToken token)
        {
            await _DbContext.SaveChangesAsync(token);
        }


    }
}
