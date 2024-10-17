using Halda.Application.Handler;
using Halda.Core.DTO;
using Halda.Core.DTO.PreOnboarding;
using Halda.Core.Models;
using Halda.DataAccess.Repositories;
using Halda.Utilities.FileUpload;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IO.Compression;


namespace Halda.Application.Controllers
{
    public class JobApplicationController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IFileService fileService;

        public JobApplicationController(IUnitOfWork _unitOfWork, IFileService fileService)
        {
            unitOfWork = _unitOfWork;
            this.fileService = fileService;
        }


        public IActionResult JobDetails(string jobPostId)
        {
            ViewBag.JobPostId = jobPostId;
            return View();
        }

        public IActionResult JobDashboard()
        {
            return View();
        }
        public async Task<List<JobDashboardDTO>> GetAppliedJobsByUserId(CancellationToken token)
        {
            //var userId = Request.Cookies["UserId"];
            var userId = "838c9aea-ee7c-4dc9-9a18-fc4800d66c4d";
           // var userId = "70252bac-10c3-4514-b1a9-433b4a8b8760";

            var result = await unitOfWork.applicantRepository.GetApplicantJobApplicationsAsync(userId);

            return result;
        }

        [HttpPost]
        public async Task<IActionResult> AddApplicant([FromBody] JobApplicationDTO model, CancellationToken token)
        {
            var userId = Request.Cookies["UserId"];
            model.ApplicantId = userId;
            try
            {
                token.ThrowIfCancellationRequested();
                // Process and save files
                var fileTypes = new Dictionary<string, string>
        {
            { nameof(JobApplicationDTO.ResumeUrl), "Resume" },
            { nameof(JobApplicationDTO.GovtIdUrl), "Govt ID" },
            { nameof(JobApplicationDTO.CertificateUrl), "Certificate" },
            { nameof(JobApplicationDTO.TranscriptUrl), "Transcript" },
            { nameof(JobApplicationDTO.SSCCertificateUrl), "SSC Certificate" },
            { nameof(JobApplicationDTO.HSCCertificateUrl), "HSC Certificate" },
            { nameof(JobApplicationDTO.BScCertificateUrl), "BSc Certificate" },
            { nameof(JobApplicationDTO.MScCertificateUrl), "MSc Certificate" }
        };

                foreach (var fileType in fileTypes)
                {
                    var property = typeof(JobApplicationDTO).GetProperty(fileType.Key);
                    if (property != null)
                    {
                        var combinedValue = (string)property.GetValue(model);
                        if (!string.IsNullOrEmpty(combinedValue))
                        {
                            var parts = combinedValue.Split('|');
                            if (parts.Length == 2)
                            {
                                string fileName = fileType.Value + "_" + parts[0];

                                string extension = Path.GetExtension(fileName);

                                string newFileName = model.JobPostId + "_" + model.ApplicantId + "_" + Guid.NewGuid().ToString() + "." + extension;

                                string fileContent = parts[1];

                                string savedFilePath = fileService.FileUploadProcessing(fileContent, newFileName, "Media");
                                // Update the model with the saved file path
                                property.SetValue(model, savedFilePath);
                            }
                        }
                    }
                }

                // Set the DateApplied if it's not already set
                if (model.DateApplied == null)
                {
                    model.DateApplied = DateTime.UtcNow;
                }

                // Pass the processed data to the repository
                var response = await unitOfWork.applicantRepository.CreateApplicantAsync(model, token);
                await unitOfWork.Save(token);
                return Ok(new { success = true, message = "Application submitted successfully!" });
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<JobDetailsViewModel> GetJobDetailsById (string jobPostId, CancellationToken token)
        {
            //var userId = Request.Cookies["UserId"];
            var userId = "838c9aea-ee7c-4dc9-9a18-fc4800d66c4d";
            var result = await unitOfWork.applicantRepository.GetJobDetailsById(jobPostId, userId, token);

            return result;

        }

        public async Task<IActionResult> DownloadAssignment(string assignmentId, CancellationToken token)
        {
            try
            {
                // Get the filepath from the database using the assignmentId
                var assignment = await unitOfWork.applicantRepository.GetFilePath(assignmentId, token);
                if (assignment == null)
                {
                    throw new CustomException("Assignment not found.", 404);
                }

                // Deserialize the filepath
                var files = JsonConvert.DeserializeObject<List<FilesInfo>>(assignment);

                // Create a memory stream to hold the zip file
                using var memoryStream = new MemoryStream();
                using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var file in files)
                    {
                        try
                        {
                            // Prepare the file stream for the specified file path
                            using var fileStream = fileService.PrepareFileDownload(file.FilePath);

                            // Create a new zip entry for the file
                            var zipEntry = zipArchive.CreateEntry(file.FileName);


                            using (var entryStream = zipEntry.Open())
                            {
                                fileStream.CopyToAsync(entryStream, token);
                            }

                            fileStream.Close();
                        }
                        catch (Exception ex)
                        {
                            // You might want to log the error or handle it in a specific way
                            throw new CustomException($"Error processing file {file.FileName}: {ex.Message}", 500);
                        }
                    }
                }

                // Reset the memory stream position to the beginning
                memoryStream.Position = 0;

                // Return the zip file as a FileContentResult
                return File(memoryStream.ToArray(), "application/zip", $"Assignment_{assignmentId}.zip");

            }
            catch (CustomException ex)
            {
                // Handle custom exceptions and return appropriate status codes
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                // Log the error and return a generic error response
                // You might want to log the exception details here
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadAssignment([FromForm] AssignmentDTO assignmentDto, CancellationToken token)
        {
            try
            {
              //  var userId = Request.Cookies["UserId"];
                var userId = "838c9aea-ee7c-4dc9-9a18-fc4800d66c4d";
                assignmentDto.UserId = userId;
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
                            string filePath = fileService.FileUploadProcessing(file, "SubAssignment");

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
                var result = await unitOfWork.applicantRepository.SaveAssignment(assignmentDto, filesJson, token);
                await unitOfWork.Save(token);

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

    }
}
