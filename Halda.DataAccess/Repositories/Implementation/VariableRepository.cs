using Halda.Core.DTO;
using Halda.Core.DTO.PreOnboarding;
using Halda.Core.Models;
using Halda.Core.Models.Variable;
using Halda.DataAccess.Persistence;
using Halda.DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Halda.DataAccess.Repositories.Implementation
{
    public class VariableRepository : BaseRepository<VariableData, string>, IVariableRepository

    {
        public VariableRepository(HaldaDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IList<VariableData>> GetAllForDropDownAsync(CancellationToken token)
        {
            return await _dbContext.VariableData.ToListAsync(token);
        }

        public async Task<List<SelectListdto>> GetAllVariableData(string type, string searchTerm, CancellationToken token)
        {

            var query = _dbContext.VariableData.AsQueryable();

            query = query.Where(d => d.Type == type);

            // Check if searchTerm is null or empty, if so load the first 10 departments
            if (string.IsNullOrEmpty(searchTerm))
            {
                // Convert the result of GetAll() to IQueryable
                query = query.Take(10);
            }
            else
            {
                var lowerCaseSearchTerm = searchTerm.ToLower();
                query = query.Where(d => d.Type.ToLower().Contains(lowerCaseSearchTerm) ||
                                 d.Value.ToLower().Contains(lowerCaseSearchTerm));
            }

            var result = await query.Select(d => new SelectListdto
            {
                Id = d.Id,
                Text = d.Value
            }).ToListAsync(token);

            return result;
        }

        public async Task<List<SelectListdto>> GetApplicaitonFilter(string jobPostId, string type, CancellationToken token)
        {
            List<SelectListdto> selectListDtos;

            if (type == "Skills")
            {
                // Fetch the skills as a single string and split by commas
                var skills = await _dbContext.JobApplications
                                             .Where(p => p.JobPostId == jobPostId)
                                             .Select(p => p.Skills) // assuming "Skills" is the correct property name in your model
                                             .ToListAsync(token);

                // Split, trim, and distinct the skills
                var distinctSkills = skills
                    .Where(s => !string.IsNullOrEmpty(s)) // Filter out null/empty strings
                    .SelectMany(s => s.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    .Select(skill => skill.Trim()) // Trim spaces around skills
                    .Distinct()
                    .ToList();

                // Map to SelectListdto
                selectListDtos = distinctSkills
                    .Select(skill => new SelectListdto
                    {
                        Id = skill, // assuming skill can serve as an Id here, if not, adjust accordingly
                        Text = skill,
                        Selected = false // default as false, can be customized if needed
                    })
                    .ToList();
            }
            else
            {
                // Use reflection to dynamically access the property
                var param = Expression.Parameter(typeof(JobApplication), "p");
                var property = Expression.Property(param, type);

                if (property.Type != typeof(string))
                {
                    throw new ArgumentException($"Property '{type}' is not of type string.");
                }

                var lambda = Expression.Lambda<Func<JobApplication, string>>(property, param);

                var result = await _dbContext.JobApplications
                                             .Where(p => p.JobPostId == jobPostId)
                                             .Select(lambda)
                                             .Distinct()
                                             .ToListAsync(token);

                // Map to SelectListdto
                selectListDtos = result
                    .Where(r => !string.IsNullOrEmpty(r)) // Filter out null/empty strings
                    .Select(r => new SelectListdto
                    {
                        Id = r, // assuming the value can serve as an Id, adjust if necessary
                        Text = r,
                        Selected = false // default as false, can be customized if needed
                    })
                    .ToList();
            }

            // Add an empty field as the first entry
            selectListDtos.Insert(0, new SelectListdto
            {
                Id = string.Empty, // Empty Id for the blank option
                Text = "All", // Empty text for the blank option
                Selected = true // Set to true if you want the blank option selected by default
            });

            return selectListDtos;
        }


        public async Task<List<JobApplicationWithStatus>> GetApplicants([FromBody] GetApplicantsDTO request, CancellationToken token)
        {
            // Extract jobPostId and filters from the request object
            string jobPostId = request.JobPostId;
            Dictionary<string, string> filters = request.Filters;

            // Check if the milestone is provided
            if (string.IsNullOrEmpty(request.Milestone))
            {
                // If milestone is empty, return current results applying filters if any
                // Retrieve the job applications and include applicant details
                var query = _dbContext.JobApplications
                    .Where(p => p.JobPostId == jobPostId)
                    .Include(p => p.Applicant); // Assuming there is a navigation property for the applicant

                // Find the milestone with the minimum serial number for the given job post
                var milestoneId1 = await _dbContext.JobMileStones
                    .Where(p => p.JobPostId == request.JobPostId)
                    .OrderBy(p => p.Serial) // Order by Serial to get the minimum serial
                    .Select(p => p.Id)
                    .FirstOrDefaultAsync(token);

                // Retrieve applicants' statuses based on the milestoneId and jobPostId
                var applicants1 = await _dbContext.ApplicantApplicationStatus
                    .Where(s => s.MilestoneId == milestoneId1 && s.JobPostId == jobPostId)
                    .Select(s => new
                    {
                        s.ApplicantId,
                        s.Status
                    })
                    .ToListAsync(token);

                // Apply additional filters if provided
                if (filters != null && filters.Any())
                {
                    var param = Expression.Parameter(typeof(JobApplication), "p");
                    Expression finalExpression = null;

                    foreach (var filter in filters)
                    {
                        var type = filter.Key;
                        var searchTerm = filter.Value;

                        var typeProperty = Expression.Property(param, type);
                        Expression searchCondition = Expression.Equal(typeProperty, Expression.Constant(searchTerm));

                        if (finalExpression == null)
                        {
                            finalExpression = searchCondition;
                        }
                        else
                        {
                            finalExpression = Expression.AndAlso(finalExpression, searchCondition);
                        }
                    }

                    var lambda = Expression.Lambda<Func<JobApplication, bool>>(finalExpression, param);
                    var filteredApplicants = await query.Where(lambda).ToListAsync(token);

                    // Map filtered applicants to JobApplicationWithStatus
                    return filteredApplicants.Select(app => new JobApplicationWithStatus
                    {
                        JobPostId = app.JobPostId,
                        ApplicantId = app.ApplicantId,
                        CompanyId = app.CompanyId, // Assuming there is a CompanyId in Applicant
                        Name = app.Name,
                        University = app.University,
                        PhoneNumber = app.PhoneNumber,
                        Skills = app.Skills,
                        Email = app.Email,
                        CurrentLocation = app   .CurrentLocation,
                        ApplyingPosition = app.ApplyingPosition,
                        Experience = app.Experience,
                        LinkedinProfileLink = app.LinkedinProfileLink,
                        PreviousJobCompanyName = app.PreviousJobCompanyName,
                        CurrentSalary = app.CurrentSalary,
                        ExpectedSalary = app.ExpectedSalary,
                        CoverLetter = app   .CoverLetter,
                        HowDidYouKnow = app .HowDidYouKnow,
                        ResumeUrl = app.ResumeUrl,
                        GovtIdUrl = app .GovtIdUrl,
                        CertificateUrl = app.CertificateUrl,
                        TranscriptUrl = app.TranscriptUrl,
                        SSCCertificateUrl = app.SSCCertificateUrl,
                        HSCCertificateUrl = app.HSCCertificateUrl,
                        MScCertificateUrl = app .MScCertificateUrl,
                        BScCertificateUrl = app.BScCertificateUrl,
                        DateApplied = app.DateApplied,
                        Id = app.Id,
                        IsDelete = app.IsDelete,
                        DateAdded = app.DateAdded,
                        DateUpdated = app.DateUpdated,
                        Status = applicants1.FirstOrDefault(s => s.ApplicantId == app.ApplicantId)?.Status
                    }).ToList();
                }

                // Retrieve all applicants and append statuses
                var allApplicants = await query.ToListAsync(token);
                return allApplicants.Select(app => new JobApplicationWithStatus
                {
                    JobPostId = app.JobPostId,
                    ApplicantId = app.ApplicantId,
                    CompanyId = app.CompanyId,
                    Name = app.Name,
                    University = app.University,
                    PhoneNumber = app.PhoneNumber,
                    Skills = app.Skills,
                    Email = app.Email,
                    CurrentLocation = app.CurrentLocation,
                    ApplyingPosition = app.ApplyingPosition,
                    Experience = app.Experience,
                    LinkedinProfileLink = app.LinkedinProfileLink,
                    PreviousJobCompanyName = app.PreviousJobCompanyName,
                    CurrentSalary = app.CurrentSalary,
                    ExpectedSalary = app.ExpectedSalary,
                    CoverLetter = app.CoverLetter,
                    HowDidYouKnow = app.HowDidYouKnow,
                    ResumeUrl = app.ResumeUrl,
                    GovtIdUrl = app.GovtIdUrl,
                    CertificateUrl = app.CertificateUrl,
                    TranscriptUrl = app.TranscriptUrl,
                    SSCCertificateUrl = app.SSCCertificateUrl,
                    HSCCertificateUrl = app.HSCCertificateUrl,
                    MScCertificateUrl = app.MScCertificateUrl,
                    BScCertificateUrl = app.BScCertificateUrl,
                    DateApplied = app.DateApplied,
                    Id = app.Id,
                    IsDelete = app.IsDelete,
                    DateAdded = app.DateAdded,
                    DateUpdated = app.DateUpdated,
                    Status = applicants1.FirstOrDefault(s => s.ApplicantId == app.ApplicantId)?.Status
                }).ToList();
            }

            // If milestone is provided, retrieve the milestoneId
            //var milestoneId = await _dbContext.JobMileStones
            //    .Where(p => p.Name == request.Milestone && p.JobPostId == request.JobPostId)
            //    .Select(p => p.Id)
            //    .FirstOrDefaultAsync(token);

            // Retrieve applicants based on the milestoneId and jobPostId
            var applicants = await _dbContext.ApplicantApplicationStatus
                .Where(s => s.MilestoneId == request.MilestoneId && s.JobPostId == jobPostId)
                .Where(s => s.Status == Core.Enums.Status.Completed)
                .ToListAsync(token);

            // Build the base query for job applications
            var queryForApplicants = _dbContext.JobApplications
                .Where(p => p.JobPostId == jobPostId);

            // If milestoneId is found, filter the job applications using applicants
            if (request.MilestoneId != null)
            {
                var applicantIds = applicants.Select(a => a.ApplicantId).ToList(); // Assuming ApplicantId is the key

                var filteredQuery = queryForApplicants.Where(app => applicantIds.Contains(app.ApplicantId));

                // Apply additional filters if provided
                if (filters != null && filters.Any())
                {
                    var param = Expression.Parameter(typeof(JobApplication), "p");
                    Expression finalExpression = null;

                    foreach (var filter in filters)
                    {
                        var type = filter.Key;
                        var searchTerm = filter.Value;

                        var typeProperty = Expression.Property(param, type);
                        Expression searchCondition = Expression.Equal(typeProperty, Expression.Constant(searchTerm));

                        if (finalExpression == null)
                        {
                            finalExpression = searchCondition;
                        }
                        else
                        {
                            finalExpression = Expression.AndAlso(finalExpression, searchCondition);
                        }
                    }

                    var lambda = Expression.Lambda<Func<JobApplication, bool>>(finalExpression, param);
                    var filteredApplicantsWithStatus = await filteredQuery.Where(lambda).ToListAsync(token);

                    // Map the filtered applicants to JobApplicationWithStatus
                    return filteredApplicantsWithStatus.Select(app => new JobApplicationWithStatus
                    {
                        JobPostId = app.JobPostId,
                        ApplicantId = app.ApplicantId,
                        CompanyId = app.CompanyId,
                        Name = app.Name,
                        University = app.University,
                        PhoneNumber = app.PhoneNumber,
                        Skills = app.Skills,
                        Email = app.Email,
                        CurrentLocation = app.CurrentLocation,
                        ApplyingPosition = app.ApplyingPosition,
                        Experience = app.Experience,
                        LinkedinProfileLink = app.LinkedinProfileLink,
                        PreviousJobCompanyName = app.PreviousJobCompanyName,
                        CurrentSalary = app.CurrentSalary,
                        ExpectedSalary = app.ExpectedSalary,
                        CoverLetter = app.CoverLetter,
                        HowDidYouKnow = app.HowDidYouKnow,
                        ResumeUrl = app.ResumeUrl,
                        GovtIdUrl = app.GovtIdUrl,
                        CertificateUrl = app.CertificateUrl,
                        TranscriptUrl = app.TranscriptUrl,
                        SSCCertificateUrl = app.SSCCertificateUrl,
                        HSCCertificateUrl = app.HSCCertificateUrl,
                        MScCertificateUrl = app.MScCertificateUrl,
                        BScCertificateUrl = app.BScCertificateUrl,
                        DateApplied = app.DateApplied,
                        Id = app.Id,
                        IsDelete = app.IsDelete,
                        DateAdded = app.DateAdded,
                        DateUpdated = app.DateUpdated,
                        Status = applicants.FirstOrDefault(a => a.ApplicantId == app.ApplicantId)?.Status
                    }).ToList();
                }

                // Retrieve all applicants and append statuses
                var allFilteredApplicants = await filteredQuery.ToListAsync(token);
                return allFilteredApplicants.Select(app => new JobApplicationWithStatus
                {
                    JobPostId = app.JobPostId,
                    ApplicantId = app.ApplicantId,
                    CompanyId = app.CompanyId,
                    Name = app.Name,
                    University = app.University,
                    PhoneNumber = app.PhoneNumber,
                    Skills = app.Skills,
                    Email = app.Email,
                    CurrentLocation = app.CurrentLocation,
                    ApplyingPosition = app.ApplyingPosition,
                    Experience = app.Experience,
                    LinkedinProfileLink = app.LinkedinProfileLink,
                    PreviousJobCompanyName = app.PreviousJobCompanyName,
                    CurrentSalary = app.CurrentSalary,
                    ExpectedSalary = app.ExpectedSalary,
                    CoverLetter = app.CoverLetter,
                    HowDidYouKnow = app.HowDidYouKnow,
                    ResumeUrl = app.ResumeUrl,
                    GovtIdUrl = app.GovtIdUrl,
                    CertificateUrl = app.CertificateUrl,
                    TranscriptUrl = app.TranscriptUrl,
                    SSCCertificateUrl = app.SSCCertificateUrl,
                    HSCCertificateUrl = app.HSCCertificateUrl,
                    MScCertificateUrl = app.MScCertificateUrl,
                    BScCertificateUrl = app.BScCertificateUrl,
                    DateApplied = app.DateApplied,
                    Id = app.Id,
                    IsDelete = app.IsDelete,
                    DateAdded = app.DateAdded,
                    DateUpdated = app.DateUpdated,
                    Status = applicants.FirstOrDefault(a => a.ApplicantId == app.ApplicantId)?.Status
                }).ToList();
            }

            // Return empty list if no data found
            return new List<JobApplicationWithStatus>();
        }



    }
}
