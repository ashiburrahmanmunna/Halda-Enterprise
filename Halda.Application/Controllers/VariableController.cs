using Halda.Core.DTO.PreOnboarding;
using Halda.Core.Models;
using Halda.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Halda.Application.Controllers
{
    public class VariableController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public VariableController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> SearchVariable(string type, string searchTerm, CancellationToken token)
        {
            try
            {
                var VariableData = await _unitOfWork.variableRepository.GetAllVariableData(type, searchTerm, token);

                // Return the search result in the response
                return Ok(VariableData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetApplicaitonFilter(string jobPostId,string type, CancellationToken token)
        {
            try
            {
                var result = await _unitOfWork.variableRepository.GetApplicaitonFilter(jobPostId, type, token);

                // Return the search result in the response
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetApplicants([FromBody]GetApplicantsDTO request , CancellationToken token)
        {
            try
            {
               
                var result = await _unitOfWork.variableRepository.GetApplicants(request, token);

                // Return the search result in the response
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> PreOnboardingVar (string jobPostId, CancellationToken token)
        {
            try
            {
                var result = await _unitOfWork.recruitmentVariableRepository.PreOnBoardingVar(jobPostId, token);

                // Return the search result in the response
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
