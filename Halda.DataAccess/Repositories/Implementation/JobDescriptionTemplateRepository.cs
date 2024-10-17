using Halda.Core.DTO;
using Halda.Core.Enums;
using Halda.Core.Models;
using Halda.DataAccess.Persistence;
using Halda.DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Halda.DataAccess.Repositories.Implementation
{
    public class JobDescriptionTemplateRepository : BaseRepository<JobDescriptionTemplate, string>, IJobDescriptionTemplateRepository
    {
        public JobDescriptionTemplateRepository(HaldaDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<string> CreateJobDescriptionWithMilestone(JobDescriptionViewModel jobDescriptionTemplate)
        {
            try
            {
                if (jobDescriptionTemplate.Id != null)
                {
                    // var existingJobDescription = _dbContext.JobDescriptionTemplates.FirstOrDefault(j => j.Id == jobDescriptionTemplate.Id);
                    var existingJobDescription = _dbContext.JobDescriptionTemplates
                                                .Include(j => j.Milestones)
                                                .FirstOrDefault(j => j.Id == jobDescriptionTemplate.Id);
                    if (existingJobDescription != null)
                    {
                        //existingJobDescription.Designation_Id = jobDescriptionTemplate.DesignationId;
                        // existingJobDescription.Job_Title = jobDescriptionTemplate.Designation; // Assuming Designation is stored in Job_Title field
                        existingJobDescription.Description = jobDescriptionTemplate.Description;

                        existingJobDescription.Responsibility = jobDescriptionTemplate.Responsibilities != null ? jobDescriptionTemplate.Responsibilities.ToArray() : null;
                        existingJobDescription.Qulifications = jobDescriptionTemplate.Qualifications != null ? jobDescriptionTemplate.Qualifications.ToArray() : null;
                        existingJobDescription.Benefits = jobDescriptionTemplate.Benefits != null ? jobDescriptionTemplate.Benefits.ToArray() : null;
                        existingJobDescription.OtherInfo = jobDescriptionTemplate.OtherInformation != null ? jobDescriptionTemplate.OtherInformation.ToArray() : null;

                        // Update milestones
                        foreach (var milestone in jobDescriptionTemplate.Milestones)
                        {
                            var existingMilestone = existingJobDescription.Milestones.FirstOrDefault(m => m.Id == milestone.Id);
                            if (existingMilestone != null)
                            {
                                existingMilestone.Name = milestone.Name;
                                //existingMilestone.Name = milestone.Description;
                                //existingMilestone.IsAssignment = milestone.Type;
                                existingMilestone.IsAssignment = milestone.Type == "Assignment";
                                // Update other properties as needed
                            }
                            else
                            {
                                existingJobDescription.Milestones.Add(new Milestone
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    Name = milestone.Name,
                                    Serial = milestone.Serial,
                                    IsAssignment = milestone.Type == "Assignment",
                                    JobDescriptionId = existingJobDescription.Id,
                                   
                                });
                            }
                        }

                        // Update other properties like CompanyId, UserId, etc.
                        // existingJobDescription.CompanyId = existingMilestone.CompanyId;
                        // existingJobDescription.UserId = model.UserId;
                        existingJobDescription.UpdateByUserId = jobDescriptionTemplate.UpdateByUserId;
                        existingJobDescription.DateUpdated = DateTime.UtcNow;
                        // existingJobDescription.IsDelete = model.IsDelete;
                        _dbContext.JobDescriptionTemplates.Update(existingJobDescription);

                    }
                }

                else
                {
                    var jobDescription = new JobDescriptionTemplate
                    {
                        Id = Guid.NewGuid().ToString(),
                        DesignationId = jobDescriptionTemplate.DesignationId,
                        Description = jobDescriptionTemplate.Description,
                        Responsibility = jobDescriptionTemplate.Responsibilities?.ToArray(),
                        Qulifications = jobDescriptionTemplate.Qualifications?.ToArray(),
                        Benefits = jobDescriptionTemplate.Benefits?.ToArray(),
                        OtherInfo = jobDescriptionTemplate.OtherInformation?.ToArray(),
                        Job_Title = jobDescriptionTemplate.Designation,
                        CompanyId = jobDescriptionTemplate.CompanyId,
                        UserId = jobDescriptionTemplate.UserId,
                        UpdateByUserId = jobDescriptionTemplate.UpdateByUserId,
                        DateAdded = DateTime.UtcNow,
                        Milestones = new List<Milestone>()
                    };

                    if (jobDescriptionTemplate.Milestones != null)
                    {
                        foreach (var milestoneViewModel in jobDescriptionTemplate.Milestones)
                        {
                            var milestone = new Milestone
                            {
                                Id = milestoneViewModel.Id ?? Guid.NewGuid().ToString(),
                                Name = milestoneViewModel.Description,
                                Serial = milestoneViewModel.Serial,
                                // JobDescription_Id = jobDescription.Id,                         
                            };
                            if (milestoneViewModel.Type == "Assignment")
                            {
                                milestone.IsAssignment = true;
                            }
                            else
                            {
                                milestone.IsAssignment = false;
                            }
                            jobDescription.Milestones.Add(milestone);
                        }
                    }
                   await _dbContext.JobDescriptionTemplates.AddAsync(jobDescription);
                }



                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<bool> DeleteMilestoneById(string Id)
        {
            try
            {
                var data = await _dbContext.MileStones.FirstOrDefaultAsync(x => x.Id == Id);
                if (data != null)
                {
                    _dbContext.MileStones.Remove(data);

                    return true;
                }
            }
            catch (Exception ex)
            {
                //return false;
            }

            return false;
        }

        public async Task<List<JobDescriptionViewModel>> GetAllJobDescriptionsAsync(CancellationToken token)
        {
            // Fetch job descriptions asynchronously with cancellation token
            var jobDescriptions = await _dbContext.JobDescriptionTemplates
                .Include(j => j.Milestones)
                .ToListAsync(token); // Pass the CancellationToken to ToListAsync

            var jobDescriptionViewModels = new List<JobDescriptionViewModel>();

            foreach (var jobDescription in jobDescriptions)
            {
                var jobDescriptionViewModel = new JobDescriptionViewModel
                {
                    DesignationId = jobDescription.DesignationId,
                    Designation = jobDescription.Job_Title,
                    Description = jobDescription.Description,
                    Responsibilities = jobDescription.Responsibility?.ToList(),
                    Qualifications = jobDescription.Qulifications?.ToList(),
                    Benefits = jobDescription.Benefits?.ToList(),
                    OtherInformation = jobDescription.OtherInfo?.ToList(),
                    Milestones = jobDescription.Milestones.Select(m => new MilestoneViewModel
                    {
                        Id = m.Id,
                        Name = m.Name,
                        // Description = m.Description, // Uncomment if needed
                        Serial = m.Serial,
                        Type = m.DefaultType.ToString()
                    }).ToList()
                };

                jobDescriptionViewModels.Add(jobDescriptionViewModel);
            }

            return jobDescriptionViewModels;
        }





        public async Task<JobDescriptionViewModel> GetJobDescriptionsWithMilestoneById(string id, CancellationToken token)
        {
            try
            {
                var jobDescription = await _dbContext.JobDescriptionTemplates
                    .Include(j => j.Milestones)
                    .FirstOrDefaultAsync(j => j.Id == id, cancellationToken: token);

                if (jobDescription == null)
                {
                    return null;
                }

                var jobDescriptionViewModel = new JobDescriptionViewModel
                {
                    Id = jobDescription.Id,
                    DesignationId = jobDescription.DesignationId,
                    Description = jobDescription.Description,
                    Responsibilities = jobDescription.Responsibility?.ToList(),
                    Qualifications = jobDescription.Qulifications?.ToList(),
                    Benefits = jobDescription.Benefits?.ToList(),
                    OtherInformation = jobDescription.OtherInfo?.ToList(),
                    Designation = jobDescription.Job_Title,
                    CompanyId = jobDescription.CompanyId,
                    UserId = jobDescription.UserId,
                    UpdateByUserId = jobDescription.UpdateByUserId,
                    Milestones = jobDescription.Milestones?.Select(m => new MilestoneViewModel
                    {
                        Id = m.Id,
                        Description = m.Name,
                        Serial = m.Serial,
                        Type = m.IsAssignment.HasValue && m.IsAssignment.Value ? "Assignment" : "Other",
                    }).OrderBy(p => p.Serial).ToList()
                };

                return jobDescriptionViewModel;
            }
            catch (OperationCanceledException)
            {
                // Handle the operation cancellation
                throw new OperationCanceledException("The operation was canceled.");
            }
            catch (Exception ex)
            {
                // Log the exception as needed
                // _logger.LogError(ex, "An error occurred while retrieving job description with milestones.");

                throw new Exception("An error occurred while retrieving the job description with milestones.", ex);
            }
        }



        public async Task<List<DesignationViewModel>> GetMilestonesGroupedByDesignationAsync(string companyId, CancellationToken token)
        {
            var jobDescriptionTemplates = await _dbContext.JobDescriptionTemplates
                //.Include(j => j.Designation)
                .Include(j => j.Milestones)
                .Where(j => j.CompanyId == companyId)
                .ToListAsync(token);

            var designationViewModels = jobDescriptionTemplates.Select(j => new DesignationViewModel
            {
                Id = j?.Id,
                Name = j?.Job_Title,
                MilestoneViewModels = j.Milestones?
                    .Select(m => new MilestoneViewModel
                    {
                        Id = m.Id,
                        Name = m.Name,
                        Serial = m.Serial,
                        Type = m.DefaultType.ToString()
                    })
                    .OrderBy(m => m.Serial)
                    .ToList() ?? new List<MilestoneViewModel>()
            }).ToList();

            return designationViewModels;
        }


        public async Task<List<DesignationViewModel>> GetMilestonesFilteredByDesignation(string companyId, string desigId, CancellationToken token)
        {
            // Define a variable to hold the fetched job description templates
            List<JobDescriptionTemplate> jobDescriptionTemplates;

            // Fetch job descriptions based on the designation filter
            if (string.IsNullOrEmpty(desigId) || desigId == "0")
            {
                // Fetch all job descriptions if designation filter is not applied
                jobDescriptionTemplates = await _dbContext.JobDescriptionTemplates
                    .Include(j => j.Milestones)
                    .Where(j => j.CompanyId == companyId)
                    .ToListAsync(token);
            }
            else
            {
                // Fetch job descriptions filtered by the designation ID
                jobDescriptionTemplates = await _dbContext.JobDescriptionTemplates
                    .Include(j => j.Milestones)
                    .Where(j => j.CompanyId == companyId && j.DesignationId == desigId)
                    .ToListAsync(token);
            }

            // Transform job descriptions to view models
            var designationViewModels = jobDescriptionTemplates.Select(j => new DesignationViewModel
            {
                Id = j?.Id,
                Name = j?.Job_Title,
                MilestoneViewModels = j.Milestones?
                    .Select(m => new MilestoneViewModel
                    {
                        Id = m.Id,
                        Name = m.Name,
                        Serial = m.Serial,
                        Type = m.DefaultType.ToString()
                    })
                    .OrderBy(m => m.Serial)
                    .ToList() ?? new List<MilestoneViewModel>()
            }).ToList();

            return designationViewModels;
        }




        public async Task<IList<JobDescriptionTemplate>> GetJobDescriptionsTemplateListByComId(string Id, CancellationToken token)
        {
            return await _dbContext.JobDescriptionTemplates
                .Where(x => x.CompanyId == Id)
                .ToListAsync(token);
        }

        public string CreateJobWithMilestone(JobDetailsDTO model)
        {
            try
            {
                JobPost jobPost;
                bool isNew = false;

                if (!string.IsNullOrEmpty(model.Id))
                {
                    jobPost = _dbContext.JobPosts.FirstOrDefault(j => j.Id == model.Id);
                    if (jobPost == null)
                    {
                        return "Job post not found.";
                    }
                }
                else
                {
                    // Create a new job post
                    jobPost = new JobPost
                    {
                        Id = Guid.NewGuid().ToString(),
                        StartDate = DateTime.UtcNow,
                        PublishDate = DateTime.UtcNow,
                        IsCompleted = "false",
                        DateAdded = DateTime.UtcNow,
                    };
                    isNew = true;
                }

                // Update job post fields
                jobPost.Title = model.DesignationName;
                jobPost.Description = model.Description;
                jobPost.JobTypes = model.JobTypes?.ToArray();
                jobPost.JobTags = model.JobTags?.ToArray();
                jobPost.Email = model.Email;
                jobPost.LastDate = model.LastDate != null ? DateOnly.Parse(model.LastDate) : null;
                jobPost.SalaryMin = model.SalaryMin;
                jobPost.SalaryMax = model.SalaryMax;
                jobPost.Location = model.Location;
                jobPost.Responsibilities = model.Responsibilities?.ToArray();
                jobPost.Qualifications = model.Qualifications?.ToArray();
                jobPost.Benefits = model.Benefits?.ToArray();
                jobPost.OtherInformation = model.OtherInformation?.ToArray();
                jobPost.DesignationId = model.DesignationId;
                jobPost.CompanyId = model.CompanyId;
                jobPost.UserId = model.UserId;
                jobPost.UpdateByUserId = model.UserId;
                jobPost.DateUpdated = DateTime.UtcNow;
                if (isNew)
                {
                    _dbContext.JobPosts.Add(jobPost);

                    if(model.Milestones != null && model.Milestones.Any())
                    {
                        var jobMileStones = new List<JobMileStone>();
                        foreach(var milestone in model.Milestones)
                        {
                            var jobMileStone = new JobMileStone
                            {
                                Id = Guid.NewGuid().ToString(),
                                Name = milestone.Description,
                                JobPostId = jobPost.Id,
                                Description = milestone.Description,
                                Type = milestone.Type,
                                Status = Status.NotStarted,
                                Serial = milestone.Serial
                            };
                            jobMileStones.Add(jobMileStone);
                        }
                        _dbContext.JobMileStones.AddRange(jobMileStones);
                    }
                    else
                    {
                        var milestones = _dbContext.MileStones.Where(x => x.JobDescriptionId == model.DesignationId).ToList();

                        var jobMileStones = new List<JobMileStone>();

                        foreach (var milestone in milestones)
                        {
                            var jobMileStone = new JobMileStone
                            {
                                Id = Guid.NewGuid().ToString(),
                                Name = milestone.Name,
                                JobPostId = jobPost.Id,
                                Description = milestone.Name,
                                Type = milestone.DefaultType.HasValue ? milestone.DefaultType.Value.ToString() : null,
                                DefaultType = milestone.DefaultType,
                                IsAssignment = milestone.IsAssignment,
                                Status = Status.NotStarted
                            };
                            jobMileStones.Add(jobMileStone);
                        }

                        _dbContext.JobMileStones.AddRange(jobMileStones);
                    }
                    
                }
                else
                {
                    _dbContext.JobPosts.Update(jobPost);
                }

                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string UpdateJob(JobDetailsDTO model)
        {
            try
            {
                // Check if the job post exists
                var jobPost = _dbContext.JobPosts.Include(x => x.JobMileStones).FirstOrDefault(j => j.Id == model.Id);
                if (jobPost == null)
                {
                    return "Job post not found.";
                }

                // Update job post fields
                jobPost.Title = model.DesignationName;
                jobPost.Description = model.Description;
                jobPost.JobTypes = model.JobTypes?.ToArray();
                jobPost.JobTags = model.JobTags?.ToArray();
                jobPost.Email = model.Email;
                jobPost.LastDate = model.LastDate != null ? DateOnly.Parse(model.LastDate) : null;
                jobPost.SalaryMin = model.SalaryMin;
                jobPost.SalaryMax = model.SalaryMax;
                jobPost.Location = model.Location;
                jobPost.Responsibilities = model.Responsibilities?.ToArray();
                jobPost.Qualifications = model.Qualifications?.ToArray();
                jobPost.Benefits = model.Benefits?.ToArray();
                jobPost.OtherInformation = model.OtherInformation?.ToArray();
                jobPost.UpdateByUserId = model.UserId;
                jobPost.DateUpdated = DateTime.UtcNow;

                // Update milestones
                if (model.Milestones != null)
                {
                    // Clear existing milestones if necessary (optional based on your logic)
                    jobPost.JobMileStones.Clear();

                    foreach (var milestoneDto in model.Milestones)
                    {
                        // Find the existing milestone or create a new one
                        var existingMilestone = jobPost.JobMileStones.FirstOrDefault(m => m.Id == milestoneDto.Id);

                        if (existingMilestone != null)
                        {
                            // Update the existing milestone
                            existingMilestone.Name = milestoneDto.Description;
                            existingMilestone.Description = milestoneDto.Description;
                            existingMilestone.Serial = milestoneDto.Serial;
                            existingMilestone.Type = milestoneDto.Type;
                        }
                        else
                        {
                            // Create a new milestone
                            var newMilestone = new JobMileStone
                            {
                                Id = Guid.NewGuid().ToString(),
                                Name = milestoneDto.Description,
                                Description = milestoneDto.Description,
                                Serial = milestoneDto.Serial,
                                Type = milestoneDto.Type,
                                JobPostId = jobPost.Id // Link to the JobPost
                            };
                            jobPost.JobMileStones.Add(newMilestone);
                        }
                    }
                }

                // Save changes to job post and milestones
                _dbContext.JobPosts.Update(jobPost);

                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }




    }
}
