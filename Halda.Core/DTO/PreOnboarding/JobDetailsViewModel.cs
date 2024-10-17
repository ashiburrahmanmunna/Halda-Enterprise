using Halda.Core.Enums;
using Halda.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.Core.DTO.PreOnboarding
{
    public class JobDetailsViewModel
    {
        public string? JobId { get; set; }
        public string? JobName { get; set; }
        public string? CompanyName { get; set; }
        public JobInfo? JobInfo { get; set; }
    }

    public class JobInfo
    {
        public List<GroupedAssignmentViewModel>? GroupedAssignments { get; set; }
        public List<MilestonesViewModel>? Milestones { get; set; }
    }

    public class GroupedAssignmentViewModel
    {
        public int? Serial { get; set; }
        public DateOnly? DateAdded { get; set; }
        public List<AssignmentViewModel>? Assignments { get; set; }
    }



    public class AssignmentViewModel
    {
        public string? Id { get; set; }
        public int? Serial { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public string? Notice { get; set; }
        public DateOnly? DueDate { get; set; }
        public DateOnly? AssignedDate { get; set; }
        public string? AssignBy { get; set; }
        public List<FilesInfo>? Files { get; set; }
    }

    public class MilestonesViewModel
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Status? Status { get; set; }
        public DateTime? CompletionDate { get; set; }
    }

    public class FilesInfo
    {
        public byte[] FileContent { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}