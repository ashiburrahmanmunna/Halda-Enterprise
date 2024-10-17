using Halda.Core.DTO;

namespace Halda.Core.DTO
{
    public class DesignationViewModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public List<MilestoneViewModel> MilestoneViewModels { get; set; }
        public List<JobListDTO> JobLists { get; set; }
    }
}
