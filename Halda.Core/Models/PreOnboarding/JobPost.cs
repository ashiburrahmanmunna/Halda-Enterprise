using Halda.Core.Models.Variable;

namespace Halda.Core.Models
{
    public class JobPost : BaseModel
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string[]? JobTypes { get; set; }
        public string[]? JobTags { get; set; }
        public string? Email { get; set; }
        public DateOnly? LastDate { get; set; }
        public string? SalaryMin { get; set; }
        public string? SalaryMax { get; set; }
        public string? Location { get; set; }
        public string[]? Responsibilities { get; set; }
        public string[]? Qualifications { get; set; }
        public string[]? Benefits { get; set; }
        public string[]? OtherInformation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? PublishDate { get; set; }
        public string[]? Skills { get; set; }
        public string? IsCompleted { get; set; }
        public string? DesignationId { get; set; }
        public virtual Designation? Designation { get; set; }
        public string? DepartmentId { get; set; }
        public virtual Department? Department { get; }
        public virtual ICollection<Applicant>? Applicants { get; set; }
        public virtual ICollection<JobMileStone>? JobMileStones { get; set; }
    }
}
