using Halda.Core.DTO;
using Halda.Core.Enums;
using Halda.Core.Models;
using Halda.DataAccess.Persistence;
using Halda.DataAccess.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Halda.DataAccess.Repositories.Implementation
{
    public class JobPostRepository : BaseRepository<JobPost, string>, IJobPostRepository
    {
        public JobPostRepository(HaldaDbContext dbContext) : base(dbContext)
        {
        }

        public string CreateJobDescriptionWithMilestone(JobDescriptionViewModel jobDescriptionTemplate)
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
                        //DateAdded = DateTime.Now,
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
                    _dbContext.JobDescriptionTemplates.Add(jobDescription);
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
            var jobDescriptions = await _dbContext.JobDescriptionTemplates
                .Include(j => j.Milestones)
                .ToListAsync(token);

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
                        //Description = m.Description, // assuming Description is available in Milestone model
                        Serial = m.Serial,
                        Type = m.DefaultType.ToString()
                    }).ToList()
                };

                jobDescriptionViewModels.Add(jobDescriptionViewModel);
            }

            return jobDescriptionViewModels;
        }




        public async Task<JobDetailsDTO> GetJobDetailsById(string id, CancellationToken token)
        {
            try
            {
                // Pass the cancellation token to the database query
                var jobPost = await _dbContext.JobPosts
                                              .Where(x => x.Id == id)
                                              .Include(x => x.JobMileStones) // Correct lambda expression
                                              .FirstOrDefaultAsync(token);


                // Check if the token is cancelled
                token.ThrowIfCancellationRequested();

                if (jobPost != null)
                {
                    JobDetailsDTO jobDetailsDTO = new JobDetailsDTO
                    {
                        Id = jobPost.Id,
                        JobDescriptionId = jobPost.Id,
                        DesignationId = jobPost.DesignationId,
                        DesignationName = jobPost.Title,
                        JobTypes = jobPost.JobTypes?.ToList(),
                        JobTags = jobPost.JobTags?.ToList(),
                        Email = jobPost.Email,
                        LastDate = jobPost.LastDate?.ToString(),
                        SalaryMin = jobPost.SalaryMin,
                        SalaryMax = jobPost.SalaryMax,
                        Location = jobPost.Location,
                        Description = jobPost.Description,
                        Responsibilities = jobPost.Responsibilities?.ToList(),
                        Qualifications = jobPost.Qualifications?.ToList(),
                        Benefits = jobPost.Benefits?.ToList(),
                        OtherInformation = jobPost.OtherInformation?.ToList(),
                        Milestones = jobPost.JobMileStones.Select(m => new MilestoneViewModel
                        {
                            Id = m.Id,
                            Name = m.Name,
                            Description = m.Description, // assuming Description is available in Milestone model
                            Serial = m.Serial,
                            Type = m.DefaultType.ToString(),
                        }).OrderBy(p => p.Serial).ToList()
                    };

                    return jobDetailsDTO;
                }
                else
                {
                    throw new ArgumentException($"Job with id {id} not found.");
                }
            }
            catch (OperationCanceledException)
            {
                // Handle cancellation specifically
                throw new OperationCanceledException("The operation was canceled.");
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the job description with milestones.", ex);
            }
        }




        public async Task<List<DesignationViewModel>> GetMilestonesGroupedByDesignationAsync(string companyId, CancellationToken token)
        {
            // Fetch job description templates with milestones for the given company ID
            var jobDescriptionTemplates = await _dbContext.JobDescriptionTemplates
                //.Include(j => j.Designation)
                .Include(j => j.Milestones)
                .Where(j => j.CompanyId == companyId)
                .ToListAsync(token);

            // Map to DesignationViewModel
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


        public async Task<IList<JobDescriptionTemplate>> GetJobDescriptionsTemplateListByComId(string id, CancellationToken token)
        {
            return await _dbContext.JobDescriptionTemplates
                .Where(x => x.CompanyId == id)
                .ToListAsync(token);
        }


        public string CreateJobWithMilestone(JobDetailsDTO model)
        {
            try
            {



                var jobPost = new JobPost
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = model.DesignationName,
                    Description = model.Description,
                    JobTypes = model.JobTypes?.ToArray(),
                    JobTags = model.JobTags?.ToArray(),
                    Email = model.Email,
                    LastDate = model.LastDate != null ? DateOnly.Parse(model.LastDate) : null,
                    SalaryMin = model.SalaryMin,
                    SalaryMax = model.SalaryMax,
                    Location = model.Location,
                    Responsibilities = model.Responsibilities?.ToArray(),
                    Qualifications = model.Qualifications?.ToArray(),
                    Benefits = model.Benefits?.ToArray(),
                    OtherInformation = model.OtherInformation?.ToArray(),
                    StartDate = DateTime.UtcNow,
                    PublishDate = DateTime.UtcNow,
                    IsCompleted = "false",
                    DesignationId = model.DesignationId
                };


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

                _dbContext.JobPosts.Add(jobPost);
                _dbContext.JobMileStones.AddRange(jobMileStones);


                return "OK";
            }

            catch (Exception ex)
            {
                return ex.Message;
            }


        }

        public async Task<IList<JobListDTO>> GetJobListByComId(string companyId, CancellationToken token)
        {
            // Fetch job posts for the given company ID
            var jobPosts = await _dbContext.JobPosts
                .Where(x => x.CompanyId == companyId)
                .ToListAsync(token);

            // Map to JobListDTO
            var jobListDtos = jobPosts.Select(jobPost => new JobListDTO
            {
                Id = jobPost.Id,
                DesignationId = jobPost.DesignationId,
                JobTittel = jobPost.Title,
                JobTypes = jobPost.JobTypes?.ToList(),
                JobTags = jobPost.JobTags?.ToList(),
                Email = jobPost.Email,
                LastDate = jobPost.LastDate?.ToString("yyyy-MM-dd"),
                Location = jobPost.Location,
            }).ToList();

            return jobListDtos;
        }


    }
}
