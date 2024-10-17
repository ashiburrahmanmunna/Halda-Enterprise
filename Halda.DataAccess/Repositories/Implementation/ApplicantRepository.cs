using Halda.Core.DTO;
using Halda.Core.DTO.PreOnboarding;
using Halda.Core.Models;
using Halda.Core.Models;
using Halda.DataAccess.Migrations;
using Halda.DataAccess.Persistence;
using Halda.DataAccess.Repositories.Interface;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
//using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;



namespace Halda.DataAccess.Repositories.Implementation
{
    public class ApplicantRepository : BaseRepository<Applicant, string>, IApplicantRepository
    {
        public ApplicantRepository(HaldaDbContext dbContext) : base(dbContext)
        {
        }


        public async Task<string> GetFilePath(string id, CancellationToken token)
        {
            return await _dbContext.Assignments
                .Where(a => a.Id == id)
                .Select(a => a.Files)
                .FirstOrDefaultAsync(token);
        }

        public async Task<JobApplication> GetJobApplication(string applicantId, string jobPostId, CancellationToken token)
        {
            return await _dbContext.JobApplications.Where(a => a.ApplicantId == applicantId && a.JobPostId == jobPostId).FirstOrDefaultAsync(token);
        }

        public bool CreateApplicant(ApplicantDTO applicant)
        {
            try
            {
                Applicant obj = new Applicant
                {
                    ApplicantId = applicant.ApplicantId,
                    PrimaryEmail = applicant.PrimaryEmail,
                    FirstName = applicant.FirstName,
                    LastName = applicant.LastName

                };

                _dbContext.Applicants.Add(obj);
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }

        }


        public async Task<string> CreateApplicantAsync(JobApplicationDTO jobApplicationDto, CancellationToken token)
        {
            try
            {
                // Retrieve CompanyId for the job post asynchronously
                var comId = await _dbContext.JobPosts
                                           .Where(p => p.Id == jobApplicationDto.JobPostId)
                                           .Select(p => p.CompanyId)
                                           .FirstOrDefaultAsync(token);

                if (comId == string.Empty) // Check for invalid company Id
                {
                    throw new ArgumentException("Invalid JobPostId. CompanyId not found.");
                }

                jobApplicationDto.CompanyId = comId;

                // Map DTO to JobApplication entity
                var jobApplication = jobApplicationDto.Adapt<JobApplication>();

                // Add job application to the context
                await _dbContext.JobApplications.AddAsync(jobApplication, token);

                // Retrieve related milestones for the job post
                var milestones = await _dbContext.JobMileStones
                                                 .Where(p => p.JobPostId == jobApplication.JobPostId)
                                                 .ToListAsync(token);

                // Prepare and add ApplicantApplicationStatus for each milestone
                var applicationStatuses = milestones.Select(milestone => new ApplicantApplicationStatus
                {
                    ApplicantId = jobApplication.ApplicantId,
                    MilestoneId = milestone.Id,
                    JobPostId = jobApplication.JobPostId,
                    Status = Core.Enums.Status.NotStarted
                }).ToList();

                await _dbContext.ApplicantApplicationStatus.AddRangeAsync(applicationStatuses, token);


                return "Success";
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<string> UpdateMilestoneStatus(MilestoneStatusDTO milestoneStatusDTO, CancellationToken token)
        {
            try
            {

                // Retrieve the milestone by its Name
                var milestone = await _dbContext.JobMileStones
                    .FirstOrDefaultAsync(p => p.Id == milestoneStatusDTO.MilestoneId && p.JobPostId == milestoneStatusDTO.JobPostId, token);

                // Check if the milestone exists
                if (milestoneStatusDTO.MilestoneId == null)
                {
                    return "Milestone not found";
                }

                // Update the status of the milestone
                milestone.Status = Core.Enums.Status.Completed;

                // Update the status for each applicant
                var allApplicants = await _dbContext.ApplicantApplicationStatus
             .Where(p => p.MilestoneId == milestone.Id && p.JobPostId == milestoneStatusDTO.JobPostId)
             .ToListAsync(token);

                // Update the status of applicants in the milestoneStatusDTO.ApplicantId list
                foreach (var applicant in allApplicants)
                {
                    if (milestoneStatusDTO.ApplicantId.Contains(applicant.ApplicantId))
                    {
                        // If applicant is in the DTO, mark them as completed
                        applicant.Status = Core.Enums.Status.Completed;
                    }
                    else
                    {
                        // If applicant is not in the DTO, mark them as rejected
                        applicant.Status = Core.Enums.Status.Rejected;
                    }
                }

                return "Success";
            }
            catch (Exception ex)
            {
                // Log exception (optional)
                throw; // Consider adding logging here
            }
        }

        public async Task<string> SaveAssignment(AssignmentDTO assignmentDto, string filesJson, CancellationToken token)
        {
            try
            {
                if (string.IsNullOrEmpty(assignmentDto.UserId))
                {
                    // Create the assignment object
                    var assignment = new Assignment
                    {
                        Title = assignmentDto.AssignmentName,
                        AssignBy = assignmentDto.AssignBy,
                        DueDate = assignmentDto.Deadline,
                        Files = filesJson,
                        CompanyId = assignmentDto.CompanyId,
                    };

                    // Add the assignment to the database
                    await _dbContext.Assignments.AddAsync(assignment);
                }
                else
                {
                    // If UserId is present, update the existing UserAssignment entry
                    var existingUserAssignment = await _dbContext.ApplicantsAssignments
                        .Where(ua => ua.ApplicantId == assignmentDto.UserId && ua.JobPostId == assignmentDto.jobPostId
                        && ua.Serial == assignmentDto.Serial && ua.AssignmentId == assignmentDto.AssignmentId).FirstOrDefaultAsync(token);

                    if (existingUserAssignment != null)
                    {
                        // Update the existing UserAssignment entry
                        existingUserAssignment.UploadedFilePath = filesJson;
                        existingUserAssignment.IsSubmitted = true;

                        // Mark the entity as modified
                        _dbContext.ApplicantsAssignments.Update(existingUserAssignment);
                    }
                    else
                    {
                        // Handle the case where the UserAssignment does not exist
                        return "User assignment not found.";
                    }
                }

                // Save changes to the database
                await _dbContext.SaveChangesAsync();

                return "Success";
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                throw;
            }
        }



        public async Task<IList<Assignment>> GetAssignment(string comId, CancellationToken token)
        {
            // Validate the input (e.g., comId should not be null or empty)
            if (string.IsNullOrEmpty(comId))
            {
                throw new ArgumentException("Company ID is required.", nameof(comId));
            }

            try
            {
                // Assuming you have a repository or DbContext for accessing the database
                var assignments = await _dbContext.Assignments
                                                  .Where(a => a.CompanyId == comId)
                                                  .ToListAsync(token);


                return assignments;
            }
            catch (Exception ex)
            {
                // Handle exceptions, log the error if necessary
                throw new Exception("An error occurred while fetching assignments.", ex);
            }
        }



        public async Task<bool> AssignAssignment(ApplicantAssignmentDTO applicantAssignmentDTO)
        {
            // Ensure that applicantAssignmentDTO contains valid data
            if (applicantAssignmentDTO == null ||
                applicantAssignmentDTO.ApplicantId == null || !applicantAssignmentDTO.ApplicantId.Any() ||
                applicantAssignmentDTO.AssignmentId == null || !applicantAssignmentDTO.AssignmentId.Any() ||
                string.IsNullOrEmpty(applicantAssignmentDTO.CompanyId) ||
                string.IsNullOrEmpty(applicantAssignmentDTO.JobPostId))
            {
                throw new ArgumentException("Invalid ApplicantAssignmentDTO data.");
            }

            var newAssignments = new List<ApplicantAssignment>();

            foreach (var applicantId in applicantAssignmentDTO.ApplicantId)
            {
                // Get the current maximum serial for the individual applicant
                var currentMaxSerial = await _dbContext.ApplicantsAssignments
                    .Where(a => a.CompanyId == applicantAssignmentDTO.CompanyId
                                && a.JobPostId == applicantAssignmentDTO.JobPostId
                                && a.ApplicantId == applicantId)
                    .MaxAsync(a => (int?)a.Serial) ?? 0;

                // Increment the serial for this applicant
                currentMaxSerial++;

                foreach (var assignmentId in applicantAssignmentDTO.AssignmentId)
                {
                    var applicantAssignment = new ApplicantAssignment
                    {
                        ApplicantId = applicantId,
                        AssignmentId = assignmentId,
                        CompanyId = applicantAssignmentDTO.CompanyId,
                        DateAdded = DateTime.UtcNow,
                        JobPostId = applicantAssignmentDTO.JobPostId,
                        Serial = currentMaxSerial,
                        IsSubmitted = applicantAssignmentDTO.IsSubmitted,
                        UploadedFilePath = applicantAssignmentDTO.UploadedFilePath,
                        Type = applicantAssignmentDTO.Type
                    };

                    newAssignments.Add(applicantAssignment);
                }
            }

            // Add all new assignments in a single operation
            await _dbContext.ApplicantsAssignments.AddRangeAsync(newAssignments);

            return true;
        }


        public async Task<List<AssignmentApplicantsDTO>> GetAssignmentApplicants(string jobPostId, string milestoneId, CancellationToken token)
        {
            var applicants = await _dbContext.ApplicantApplicationStatus
                .Where(a => a.MilestoneId == milestoneId && a.Status == Core.Enums.Status.Completed)
                
                .ToListAsync();

            var result = new List<AssignmentApplicantsDTO>();

            foreach (var applicant in applicants)
            {
                // Retrieve the list of assignments for the current applicant
                var assignments = await _dbContext.ApplicantsAssignments
                    .Where(a => a.ApplicantId == applicant.ApplicantId && a.JobPostId == jobPostId)
                    .ToListAsync();

                // Group assignments by Serial
                var groupedAssignments = assignments
                    .GroupBy(a => a.Serial)
                    .Select(g => new AssignmentStatusDTO
                    {
                        AssignmentId = g.First().AssignmentId,
                        ApplicantId = applicant.ApplicantId,
                        IsSubmitted = g.Any(a => a.IsSubmitted),
                        IsApproved = g.Any(a => a.IsApproved),
                        Serial = g.Key,
                        Assignments = g.Select(a => new Assignment1DTO
                        {
                            AssignmentId = a.AssignmentId,
                            IsSubmitted = a.IsSubmitted,
                            IsApproved = a.IsApproved
                            // Add other properties as needed
                        }).ToList()
                    })
                    .ToList();

                // Create the applicant DTO
                var applicantInfo = await _dbContext.JobApplications
                    .Where(a => a.ApplicantId == applicant.ApplicantId)
                    .Select(b => new { b.Name, b.Email })
                    .FirstOrDefaultAsync();

                var stat = new AssignmentApplicantsDTO
                {
                    Name = applicantInfo?.Name,
                    Email = applicantInfo?.Email,
                    ApplicantId = applicant.ApplicantId,
                    JobPostId = jobPostId,
                    Assignments = groupedAssignments,
                  //  isSelected = applicant.Status == Core.Enums.Status.Completed ? true : false,


                };

                result.Add(stat);
            }

            return result;
        }


        public async Task AssignmentApprove(string assignmentId, string applicantId, CancellationToken token)
        {
            var assignment = await _dbContext.ApplicantsAssignments
                .FirstOrDefaultAsync(a => a.ApplicantId == applicantId && a.AssignmentId == assignmentId, token);

            if (assignment != null)
            {
                assignment.IsApproved = true;
            }
        }

        public async Task<List<JobDashboardDTO>> GetApplicantJobApplicationsAsync(string applicantId)
        {
            // Ensure applicantId is not null or empty
            if (string.IsNullOrEmpty(applicantId))
            {
                throw new ArgumentException("Applicant ID cannot be null or empty.", nameof(applicantId));
            }

            // Query the database to get job applications, milestones, and statuses for the given applicant, ordered by Serial
            var jobApplications = await _dbContext.JobApplications
                .Where(ja => ja.ApplicantId == applicantId)
                .Select(ja => new JobDashboardDTO
                {
                    JobApplicationId = ja.Id,
                    JobPostId = ja.JobPostId,
                    ApplyingPosition = ja.ApplyingPosition,
                    CompanyName = ja.Company.CompanyName, // Assuming Company has a 'Name' property
                    JobStatuses = _dbContext.ApplicantApplicationStatus
                        .Where(aas => aas.ApplicantId == applicantId && aas.JobPostId == ja.JobPostId)
                        .OrderBy(aas => aas.JobMileStone.Serial) // Ordering by Serial (assumed field)
                        .Select(aas => new JobStatusDTO
                        {
                            MilestoneName = aas.JobMileStone.Name,
                            MilestoneStatus = aas.JobMileStone.Status,
                            ApplicationStatus = aas.Status,
                            Serial = aas.JobMileStone.Serial // Assuming Serial exists in JobMileStone
                        }).ToList()
                })
                .OrderBy(ja => ja.JobStatuses.FirstOrDefault().Serial) // Ordering by the first serial in the statuses
                .ToListAsync();

            return jobApplications;
        }

        public async Task<JobDetailsViewModel> GetJobDetailsById(string jobPostId, string applicantId, CancellationToken token)
        {
            var jobPost = await _dbContext.JobPosts
                .Include(j => j.Company)
                .FirstOrDefaultAsync(a => a.Id == jobPostId);

            // Fetch assignments
            var assignments = await _dbContext.ApplicantsAssignments
                .Where(a => a.JobPostId == jobPostId && a.ApplicantId == applicantId)
                .Include(a => a.Assignment)
                .ToListAsync(token);

            // Fetch milestones
            var milestones = await _dbContext.ApplicantApplicationStatus
                .Where(a => a.JobPostId == jobPostId && a.ApplicantId == applicantId)
                .Include(a => a.JobMileStone)
                    .ThenInclude(m => m.Company)
                    .ThenInclude(c => c.JobPosts)
                .OrderBy(a => a.JobMileStone.Serial)
                .ToListAsync(token);

            // Group assignments by serial, with a default value for null Serial
            var groupedAssignments = assignments
                .GroupBy(a => a.Serial ?? 0) // Use default value (e.g., 0) for null Serial
                .OrderBy(g => g.Key)
                .ToDictionary(g => g.Key, g => g.ToList());



            // Initialize job details view model
            var jobDetails = new JobDetailsViewModel
            {
                JobId = jobPostId,
                JobName = jobPost?.Title ?? "Unknown Title",
                CompanyName = jobPost?.Company?.CompanyName ?? "Unknown Company",
                JobInfo = new JobInfo
                {
                    GroupedAssignments = groupedAssignments.Select(g => new GroupedAssignmentViewModel
                    {
                        Serial = g.Key,

                        DateAdded = g.Value.FirstOrDefault()?.DateAdded != null
    ? DateOnly.FromDateTime(g.Value.FirstOrDefault().DateAdded.Value)
    : (DateOnly?)null,


                        Assignments = g.Value.Select(a => new AssignmentViewModel
                        {
                            Id = a.Assignment?.Id ?? string.Empty,
                            Title = a.Assignment?.Title ?? "Unknown Title",
                            Type = "Task",
                            Serial = g.Key,
                            Description = a.Assignment?.Description ?? "No Description",
                            Notice = a.Assignment?.Notice,
                            AssignedDate = DateOnly.FromDateTime(a.Assignment?.DateAdded ?? DateTime.MinValue),
                            DueDate = a.Assignment?.DueDate,
                            AssignBy = a.Assignment?.AssignBy,
                            Files = string.IsNullOrEmpty(a.Assignment?.Files)
                                ? new List<FilesInfo>()
                                : JsonSerializer.Deserialize<List<FilesInfo>>(a.Assignment.Files) ?? new List<FilesInfo>()
                        }).ToList(),
                       
                    }).ToList(),
                    Milestones = milestones.Select(m => new MilestonesViewModel
                    {
                        Id = m.JobMileStone?.Id ?? string.Empty,
                        Title = m.JobMileStone?.Name ?? "Unknown Milestone",
                        Description = m.JobMileStone?.Description ?? "No Description",
                        Status = m.Status,
                       // CompletionDate = m.CompletionDate
                    }).ToList()
                }
            };

            return jobDetails;
        }



    }

}