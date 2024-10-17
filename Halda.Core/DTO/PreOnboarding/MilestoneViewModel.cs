using Halda.Core.Enums;

namespace Halda.Core.DTO
{
    public class MilestoneViewModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? DesignationId { get; set; }
        public string? JobDescriptionId { get; set; }
        public int? Serial { get; set; }
        public string? Type { get; set; }
        public Status? Status { get; set; }
    }
}
