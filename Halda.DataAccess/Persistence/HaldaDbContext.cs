using Microsoft.EntityFrameworkCore;
using Halda.Core.Models;
using Halda.Core.Models.Onboarding;
using Halda.Core.Models.Variable;
using Halda.Core.Models;
using Halda.Core.Models.Attendance;
using Halda.Core.Models.Budgeting;

namespace Halda.DataAccess.Persistence
{
    public class HaldaDbContext : DbContext
    {

        public HaldaDbContext(DbContextOptions options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<JobPost>()
                .HasMany(j => j.JobMileStones)
                .WithOne(m => m.JobPosts)
                .HasForeignKey(m => m.JobPostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<JobDescriptionTemplate>()
    .HasMany(j => j.Milestones)
    .WithOne(m => m.JobDescriptionTemplate)
    .HasForeignKey(m => m.JobDescriptionId)
    .OnDelete(DeleteBehavior.Cascade);


            //modelBuilder.Entity<Country>()
            //    .HasMany(e => e.States)
            //    .WithOne(e => e.StateCountry)
            //    .HasForeignKey(e => e.StateId)
            //    .IsRequired(false);

            //modelBuilder.Entity<State>()
            //    .HasMany(e=> e.Cities)
            //    .WithOne(e=> e.StateCity)
            //    .HasForeignKey(e=> e.CityId)
            //    .IsRequired(false);

            //modelBuilder.Entity<Country>()
            //    .HasMany(e => e.Companies)
            //    .WithOne(e => e.Country)
            //    .HasForeignKey(e => e.CountryId)
            //    .IsRequired(false);

            //modelBuilder.Entity<Cat_Type>()
            //    .HasMany(e=> e.Cat_Variables)
            //    .WithOne(e=> e.Cat_Type)
            //    .HasForeignKey(e=> e.Cat_TypeId)
            //    .IsRequired(false); 

            //modelBuilder.Entity<Company>()
            //    .HasMany(e=> e.Cat_Variable)
            //    .WithOne(e=>e.Company)
            //    .HasForeignKey(e=> e.CompanyId)
            //    .IsRequired(false);

            //modelBuilder.Entity<Company>()  
            //    .HasMany(e=> e.Milestones)
            //    .WithOne(e=> e.Company)
            //    .HasForeignKey(e=> e.CompanyId)
            //    .IsRequired(false);

            ////modelBuilder.Entity<Cat_Variable>()
            ////    .HasMany(e=> e.Milestones)
            ////    .WithOne(e=> e.Cat_Variable)
            ////    .HasForeignKey(e=>e.Variable_Id)
            ////    .IsRequired(false);

            //modelBuilder.Entity<Cat_Type>()
            //   .HasMany(e => e.Milestones)
            //   .WithOne(e => e.Cat_Type)
            //   .HasForeignKey(e => e.Type_Id)
            //   .IsRequired(false);
            //modelBuilder.Entity<JobApplication>()
            //.Property(j => j.Skills)
            //.HasColumnType("jsonb");
        }




        #region Company

        public DbSet<Country> Countries { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<City> Citys { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<User> Users { get; set; }

        #endregion


        #region Pre-onboarding Module

        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<ApplicantAssignment> ApplicantsAssignments { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<JobDescriptionTemplate> JobDescriptionTemplates { get; set; }
        public DbSet<ApplicantApplicationStatus> ApplicantApplicationStatus { get; set; }
        public DbSet<JobMileStone> JobMileStones { get; set; }
        public DbSet<JobPost> JobPosts { get; set; }
        public DbSet<Milestone> MileStones { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<RecruitmentVariable> RecruitmentVariables { get; set; }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Certification> Certifications { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<Education> Educations { get; set; }
       // public DbSet<ApplicantDoc> ApplicantDocs { get; set; }



        #endregion


        #region Onboarding Module

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmpEdu> EmpEdus { get; set; }
        public DbSet<EmployeeFamilyNomineeInfo> EmployeeFamilyNomineeInfos { get; set; }
        public DbSet<EmployeeAddress> EmployeeAddresses { get; set; }
        public DbSet<EmployeeEmergencyContact> EmployeeEmergencyContacts { get; set; }
        public DbSet<EmployeeBank> EmployeeBanks { get; set; }
        public DbSet<EmployeeTax> EmployeeTaxs { get; set; }
        public DbSet<EmpContractPeriod> EmpContractPeriod { get; set; }
        public DbSet<EmployeeTeam> EmployeeTeams { get; set; }
        public DbSet<EmployeeDocument> EmployeeDocuments { get; set; }

        //public DbSet<EmployeeBank> EmployeeBanks { get; set; }
        //public DbSet<EmployeeTax> EmployeeTaxes { get; set; }
        //public DbSet<EmployeeOnboardingChecklist> EmployeeOnboardingChecklists { get; set; }
        //public DbSet<EmployeeAsset> EmployeeAssets { get; set; }



        #endregion

        #region Attendance Module
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Holidays> Holidays { get; set; }
        public DbSet<EmpLeaveAdjust> EmpLeaveAdjusts { get; set; }



        #endregion

        public DbSet<VariableData> VariableData { get; set; }

        #region ManPower&Budgeting Module
        public DbSet<EmpSalary> EmpSalarys { get; set;}
        #endregion
    }
}
