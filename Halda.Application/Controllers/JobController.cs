using Halda.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Halda.Application.Handler;

namespace Halda.Application.Controllers
{
    public class JobController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public JobController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> JobDetails(string id)
        {
            ViewBag.id = id;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetJobDetails(string id, CancellationToken token)
        {
            try
            {
                // Check if the ID is valid
                if (string.IsNullOrEmpty(id))
                {
                    //return BadRequest(new { success = false, message = "Job ID is required." });
                    throw new CustomException("Job ID is required.", 400);
                }


                var jobDetails = await _unitOfWork.jobPostRepository.GetJobDetailsById(id, token);

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
                // Log the exception for internal purposes (if logging is set up)
                // _logger.LogError(ex, "An error occurred while processing job details.");

                //  return StatusCode(500, new { success = false, message = "An error occurred while processing your request." });
                
                throw;
            }
        }



        [HttpGet]
        public async Task<IActionResult> JobApplication(string id, CancellationToken token)
        {
            try
            {
                var jobPost = await _unitOfWork.jobPostRepository.GetByIdAsync(id, token);

                if (jobPost == null)
                {
                    // Handle case where the job post is not found
                    return NotFound(); // You can also return a custom error view if needed
                }

                ViewBag.Id = id;
                ViewBag.JobName = jobPost.Title;

                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpDelete]
        public async Task<bool> DeleteJobPost(string id, CancellationToken token)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new CustomException("JobPostId is required", 400);
            }

            try
            {
                // Call the repository method to delete the job post
                await _unitOfWork.jobPostRepository.RemoveAsync(id);

                // Save the changes with the cancellation token
                await _unitOfWork.Save(token);

                return true;
            }
            catch (OperationCanceledException ex)
            {
                // Handle cancellation explicitly
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                // Handle the general exception with logging or additional context if needed
                throw;
            }
        }

    }
}
