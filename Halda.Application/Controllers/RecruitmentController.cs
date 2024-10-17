using Halda.Core.DTO;
using Halda.Core.Models;
using Halda.Core.Models.Onboarding;
using Halda.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Xml;
using Halda.Application.Handler;
using Halda.Core.Models.Variable;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using Halda.Core.DTO.PreOnboarding;
using Halda.Utilities.FileUpload;
using Newtonsoft.Json;
using System.IO.Compression;

namespace Halda.Application.Controllers
{

    public class RecruitmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService fileService;

        public RecruitmentController(IUnitOfWork unitOfWork, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            this.fileService = fileService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ApplicationProcess(string Id)
        {
            var companyId = Request.Cookies["ComId"];
            ViewBag.id = Id;
            ViewBag.companyId = companyId;
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> JobTemplate(CancellationToken token) 
        {
            // Retrieve company ID from cookies
              var companyId = Request.Cookies["ComId"];
            //var companyId = "d5ba21d9-e99c-46a5-8ab0-191039dc4e06";
            //// Ensure companyId is valid
            //if (string.IsNullOrEmpty(companyId))
            //{
            //    //  return BadRequest("Company ID is required.");
            //    throw new CustomException("Company ID is required.", 400);
            //}


            try
            {

                // Fetch job list based on company ID with cancellation token
                var jobListDtos = await _unitOfWork.jobPostRepository.GetJobListByComId(companyId, token);

                // Fetch milestone groups by designation for the company with cancellation token
                //var designationViewModels = await _unitOfWork.jobDescriptionTemplateRepository.GetMilestonesGroupedByDesignationAsync(companyId, token);

                //// Assign job lists to corresponding designations
                //foreach (var designation in designationViewModels)
                //{
                //    designation.JobLists = jobListDtos.Where(j => j.DesignationId == designation.Id).ToList();
                //}

                // Return the populated view models to the view
                return View(jobListDtos);
            }
            catch (OperationCanceledException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
               
                // Log the error (optional)
                // _logger.LogError(ex, "Error occurred in JobTemplate.");

              //  return StatusCode(500, "Internal Server Error.");
                throw;
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllTemplate(string designation, CancellationToken token)
        {
            // Retrieve company ID from cookies
            var companyId = Request.Cookies["ComId"];

            // Ensure companyId is valid
            //if (string.IsNullOrEmpty(companyId))
            //{
            //   // return BadRequest("Company ID is required.");
            //     throw new CustomException("Company ID is required.",400);
            //}

            try
            {
                // Fetch designations filtered by designation with cancellation token
                var designations = await _unitOfWork.jobDescriptionTemplateRepository.GetMilestonesFilteredByDesignation(companyId, designation, token);

                // Return the data as JSON
                return Json(designations);
            }
            catch (OperationCanceledException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the error (optional)
                // Log the error (optional)
                // _logger.LogError(ex, "Error occurred in GetAllTemplate.");

                // return StatusCode(500, "Internal Server Error.");
                throw;
            }
        }




        [HttpGet]
        public async Task<IActionResult> GetJobDetails(string Id, CancellationToken token)
        {
            try
            {
                // Validate the input
                if (string.IsNullOrEmpty(Id))
                {
                  //  return BadRequest(new { success = false, message = "Job ID is required." });
                    throw new CustomException("Job ID is required.", 400);
                   
                }

                
                // Fetch job details
                var jobDetails = await _unitOfWork.jobPostRepository.GetJobDetailsById(Id, token);
                if (jobDetails != null)
                {
                    return Ok(new { success = true, data = jobDetails });
                }
                else
                {
                   // return NotFound(new { success = false, message = "Job details not found." });
                    throw new CustomException("Job details not found.", 404);
                }
            }
            catch (OperationCanceledException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                // _logger.LogError(ex, "Error occurred while getting job details.");
                throw;
              //  return StatusCode(500, new { success = false, message = "An error occurred while processing your request." });
            }
        }






        [HttpGet]
        public ActionResult CreateJobDescription()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateScratchJob()
        {
            return View();
        }


        [HttpGet]
        public ActionResult ViewJobDescription(string id)
        {
            ViewBag.id = id;
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> CreateJob(string? id)
        {
            ViewBag.id = id;

            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetDetailsById(string id, CancellationToken token)
        {
            try
            {
                // Fetch job description with milestones by ID, using the cancellation token
                var jobDescriptionViewModel = await _unitOfWork.jobDescriptionTemplateRepository.GetJobDescriptionsWithMilestoneById(id, token);

                if (jobDescriptionViewModel != null)
                {
                    return Json(new { success = true, data = jobDescriptionViewModel });
                }
                else
                {
                    return Json(new { success = false, message = "Job description not found." });
                }
            }
            catch (OperationCanceledException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                // _logger.LogError(ex, "Error occurred while fetching job details.");

                //return Json(new { success = false, message = "An error occurred: " + ex.Message });
                throw;
            }
        }



        [HttpPost]
        public async Task<IActionResult> CreateJobDescription([FromBody] JobDescriptionViewModel model, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException();
            }

            try
            {
                var companyId = Request.Cookies["ComId"];
                var userId = Request.Cookies["UserId"];

                //if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(userId))
                //{
                //   // return Unauthorized("Company ID or User ID is missing");
                //   throw new UnauthorizedAccessException("Company ID or User ID is missing");
                //}

                model.CompanyId = companyId;
                model.UserId = userId;

                // Create job description with milestone
                var result = await _unitOfWork.jobDescriptionTemplateRepository.CreateJobDescriptionWithMilestone(model);

                if (result != "Success")
                {
                   // return BadRequest(result);
                   throw new BadHttpRequestException(result);
                }

                // Save changes with cancellation token
                await _unitOfWork.Save(token);

                return Ok("Job description created successfully");
            }
            catch (OperationCanceledException ex)
            {
                // return StatusCode(499, "Request was canceled");
                throw new OperationCanceledException(ex.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }


        public ActionResult Delete(string id)
        {
            return View();
        }


        public async Task<IList<Designation>> GetDesignations(CancellationToken token)
        {
            try
            {
                var desig = await _unitOfWork.designationRepository.GetAllAsync(token);
                return desig;
            }
            catch (OperationCanceledException ex)
            {
                // Handle specific operation cancellation exception
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw;
            }
        }



        public async Task<IActionResult> GetDesignationDropdown(CancellationToken token)
        {
            try
            {
                var desig = await _unitOfWork.designationRepository.GetAllForDropDownAsync(token);
                return Ok(desig);
            }
            catch (OperationCanceledException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception as needed
                // _logger.LogError(ex, "An error occurred while retrieving designations.");

               // return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
                throw;
            }
        }


        [HttpGet]
        public async Task<IActionResult> CreateJobPost(string id)
        {
            return View();
        }
        public async Task<IList<JobDescriptionTemplate>> GetJobDescriptionTemplatesList(CancellationToken token)
        {
            try
            {
                
                var companyId = Request.Cookies["ComId"];

                if (string.IsNullOrEmpty(companyId))
                {
                    throw new CustomException("Company ID is missing from the cookies.",400);

                }
               // token.ThrowIfCancellationRequested();
                var templatesList = await _unitOfWork.jobDescriptionTemplateRepository.GetJobDescriptionsTemplateListByComId(companyId, token);

                return templatesList;
            }
            catch (OperationCanceledException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception (optional, but recommended)
                // _logger.LogError(ex, "Error fetching job description templates list.");

               // throw new Exception("Error fetching job description templates list.", ex);
                throw;
            }
        }

        public IList<RecruitmentVariable> GetRecruitmentVariables(CancellationToken token)
        {
            Expression<Func<RecruitmentVariable, bool>> filter = rv => rv.Name == "Milestone" && rv.DefaultType == 0;
            string orderBy = "Serial";

            try
            {
                // Call the asynchronous method with the cancellation token
                var result =  _unitOfWork.recruitmentVariableRepository.GetDynamicAsync(token, filter, orderBy, null, false);
                return result.Result;
            }
            catch (OperationCanceledException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> ViewJobDetails(string id)
        {
            ViewBag.id = id;
            return View();
        }

        public async Task<bool> DeleteMilestone(string? id, CancellationToken token)
        {
            if (string.IsNullOrEmpty(id))
            {
                return true;
            }

            try
            {
                // Call the repository method to delete the milestone
                bool deletionResult = await _unitOfWork.jobDescriptionTemplateRepository.DeleteMilestoneById(id);

                if (deletionResult)
                {
                    // Save changes with the cancellation token
                    await _unitOfWork.Save(token);


                    return true;
                }

                return false;
            }
            catch (OperationCanceledException ex)
            {
                throw new Exception(ex.Message);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateJob([FromBody] JobDetailsDTO model, CancellationToken token)
        {
            var companyId = Request.Cookies["ComId"];
            var userId = Request.Cookies["UserId"];
            model.CompanyId = companyId;
            model.UserId = userId;

            if (ModelState.IsValid)
            {
                try
                {
                    // Pass the cancellation token to your repository method
                    var response = _unitOfWork.jobDescriptionTemplateRepository.CreateJobWithMilestone(model);

                    // Save changes with the cancellation token
                    await _unitOfWork.Save(token);

                    return Ok(new { success = true, message = "Job post created successfully!" });
                }
                catch (OperationCanceledException ex)
                {
                    throw new Exception(ex.Message);
                }
                catch (Exception ex)
                {
                    // Handle any other exceptions
                   // return StatusCode(500, new { success = false, message = ex.Message });
                    throw;
                }
            }

            // return BadRequest(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
            throw new CustomException("Error", 400);
            
        }

        [HttpPut]
        public async Task<IActionResult> UpdateJob([FromBody] JobDetailsDTO model, CancellationToken token)
        {
            //var companyId = Request.Cookies["ComId"];
            var userId = Request.Cookies["UserId"];
            //model.CompanyId = companyId;
            model.UserId = userId;

            if (ModelState.IsValid)
            {
                try
                {
                    // Pass the cancellation token to your repository method
                    var response = _unitOfWork.jobDescriptionTemplateRepository.UpdateJob(model);

                    // Save changes with the cancellation token
                    await _unitOfWork.Save(token);

                    return Ok(new { success = true, message = "Job post updated successfully!" });
                }
                catch (OperationCanceledException ex)
                {
                    throw new Exception(ex.Message);
                }
                catch (Exception ex)
                {
                    // Handle any other exceptions
                    // return StatusCode(500, new { success = false, message = ex.Message });
                    throw;
                }
            }

            // return BadRequest(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
            throw new CustomException("Error", 400);

        }

        [HttpPost]
        public IActionResult Assignment([FromBody]List<string> applicantIds)
        {
            var companyId = Request.Cookies["ComId"];
            ViewBag.CompanyId = companyId;
            return View();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMilestoneStatus([FromBody] MilestoneStatusDTO model, CancellationToken token)
        {
            try
            {
                // Check for cancellation request
                token.ThrowIfCancellationRequested();

                // Pass the processed data to the repository
                var response = await _unitOfWork.applicantRepository.UpdateMilestoneStatus(model, token);

                // Ensure the response indicates success
                if (response != "Success") // You can customize this check based on your repository response
                {
                    return BadRequest(new { success = false, message = response });
                }

                // Save changes
                await _unitOfWork.Save(token);

                return Ok(new { success = true, message = "Application submitted successfully!" });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveAssignment([FromForm] AssignmentDTO assignmentDto, CancellationToken token)
        {
            try
            {
                var companyId = Request.Cookies["ComId"];
                assignmentDto.CompanyId = companyId;
                if (assignmentDto == null)
                {
                    throw new Microsoft.AspNetCore.Http.BadHttpRequestException("Assignment data is required.");
                }


                // Initialize list to store file information
                var filesList = new List<Dictionary<string, string>>();

                // Save uploaded files and populate the files list
                var files = Request.Form.Files; // Get the uploaded files from the request
                if (files != null && files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        // Check for cancellation request before processing each file
                        token.ThrowIfCancellationRequested();

                        if (file.Length > 0)
                        {
                            // Process each file and get its path
                            string filePath = fileService.FileUploadProcessing(file, "Assignment");

                            // Add file details to the list as a dictionary
                            filesList.Add(new Dictionary<string, string>
                    {
                        { "FileName", file.FileName },
                        { "FilePath", filePath }
                    });
                        }
                    }
                }

                // Serialize the files list to JSON
                var filesJson = JsonConvert.SerializeObject(filesList);

                // Check for cancellation request before saving to the database
                token.ThrowIfCancellationRequested();

                // Save assignment to the database, including the serialized files
                var result = await _unitOfWork.applicantRepository.SaveAssignment(assignmentDto, filesJson, token);
                await _unitOfWork.Save(token);

                if (result != "Success")
                {
                    throw new Exception();
                }

                // Return success response
                return Ok(new { message = "Assignment saved successfully!" });
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IActionResult> LoadAssignment(string comId, CancellationToken token)
        
        {
            try
            {
                // Get assignments asynchronously and await the result
                var assignments = await _unitOfWork.applicantRepository.GetAssignment(comId, token);

                // Return the assignments as an Ok response
                return Ok(assignments);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpPost]
        public async Task<IActionResult> AssignAssignment([FromBody] ApplicantAssignmentDTO applicantAssignmentDTO, CancellationToken token)
        {
            try
            {
                // Retrieve the company ID from the cookie
                var companyId = Request.Cookies["ComId"];
                if (string.IsNullOrEmpty(companyId))
                {
                    return BadRequest("Company ID is missing.");
                }

                // Set the CompanyId in the DTO
                applicantAssignmentDTO.CompanyId = companyId;

                // Check for cancellation request
                token.ThrowIfCancellationRequested();

                // Ensure the DTO has valid data before processing
                if (applicantAssignmentDTO.ApplicantId == null || !applicantAssignmentDTO.ApplicantId.Any())
                {
                    throw new Microsoft.AspNetCore.Http.BadHttpRequestException("ApplicantId is required.");
                }

                if (applicantAssignmentDTO.AssignmentId == null || !applicantAssignmentDTO.AssignmentId.Any())
                {
                    throw new Microsoft.AspNetCore.Http.BadHttpRequestException("AssignmentId is required.");
                }

                // Call the repository to assign the applicant assignments
                var result = await _unitOfWork.applicantRepository.AssignAssignment(applicantAssignmentDTO);

                // Save changes using the _unitOfWork pattern
                await _unitOfWork.Save(token);

                // Return a success response
                return Ok(new { Success = true, Message = "Assignment successfully saved." });
            }

            catch (Exception ex)
            {
                throw (ex);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAssignmentApplicants(string jobpostId, string milestoneId, CancellationToken token)
        {
            try
            {

                var result = await _unitOfWork.applicantRepository.GetAssignmentApplicants(jobpostId, milestoneId, token);

                // Return the search result in the response
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut]
        public async Task<IActionResult> AssignmentApprove(string assignmentId, string applicantId, CancellationToken token)
        {
            // Await the repository method call
            await _unitOfWork.applicantRepository.AssignmentApprove(assignmentId, applicantId, token);
            await _unitOfWork.Save(token);
            // Return a success response
            return Ok(new { message = "Assignment approved successfully." });
        }




        [HttpPost]
        public async Task<IActionResult> GetApplicantDataWithFiles([FromBody] List<DocumentModel> documents, CancellationToken token)
        {
            // Use a memory stream to create the zip file in memory
            using (var memoryStream = new MemoryStream())
            {
                // Create a zip archive in the memory stream
                using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, leaveOpen: true))
                {
                    foreach (var document in documents)
                    {
                        // Skip entries where the Type or FilePath is null
                        if (string.IsNullOrEmpty(document?.Type) || string.IsNullOrEmpty(document?.FilePath))
                        {
                            continue;
                        }

                        // Prepare the file for download and get the FileStream
                        using (var fileStream = fileService.PrepareFileDownload(document.FilePath))
                        {
                            // Use the type as the file name in the zip archive
                            var zipEntry = zipArchive.CreateEntry($"{document.Type}{Path.GetExtension(document.FilePath)}");

                            // Open the zip entry stream to write the file
                            using (var entryStream = zipEntry.Open())
                            {
                                // Copy the file content to the zip entry
                                await fileStream.CopyToAsync(entryStream, token);
                            }
                        }
                    }
                }

                // Reset the memory stream position to the beginning
                memoryStream.Position = 0;

                // Return the zip file as a download
                return File(memoryStream.ToArray(), "application/zip", "files.zip");
            }
        }


    }
}
