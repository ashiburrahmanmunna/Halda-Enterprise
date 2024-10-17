namespace Halda.Core.DTO
{
    public class JobDescriptionViewModel
    {
        public string? Id { get; set; }
        public string? DesignationId { get; set; }
        public string? Designation { get; set; }
        public string? Description { get; set; }
        public List<string>? Responsibilities { get; set; }
        public List<string>? Qualifications { get; set; }
        public List<string>? Benefits { get; set; }
        public List<string>? OtherInformation { get; set; }
        public string? CompanyId { get; set; }
        public string? UserId { get; set; }
        public string? UpdateByUserId { get; set; }
        public bool? IsDelete { get; set; }
        public List<MilestoneViewModel>? Milestones { get; set; }
        public List<JobListDTO>? JobLists { get; set; }
    }
}
