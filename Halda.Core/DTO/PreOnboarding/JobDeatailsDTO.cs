namespace Halda.Core.DTO
{

    public class JobDetailsDTO
    {
        public string? Id { get; set; }
        public string? JobDescriptionId { get; set; }
        public string? DesignationId { get; set; }
        public string? DesignationName { get; set; }
        public List<string>? JobTypes { get; set; }
        public List<string>? JobTags { get; set; }
        public string? Email { get; set; }
        public string? LastDate { get; set; }
        public string? SalaryMin { get; set; }
        public string? SalaryMax { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public string? CompanyId { get; set; }
        public string? UserId { get; set; }
        public List<string>? Responsibilities { get; set; }
        public List<string>? Qualifications { get; set; }
        public List<string>? Benefits { get; set; }
        public List<string>? OtherInformation { get; set; }

        public List<MilestoneViewModel>? Milestones { get; set; }
    }


}
