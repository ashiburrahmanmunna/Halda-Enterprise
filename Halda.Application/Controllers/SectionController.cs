using Halda.Core.Const;
using Halda.Core.Models.Variable;
using Halda.DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Halda.Application.Controllers
{
    public class SectionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public SectionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult AddSection()
        {
            ViewBag.CompanyId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
            ViewBag.UserId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SaveSection([FromBody] Section model, CancellationToken token)
        {
            try
            {
                var comid = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
                var userid = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

                model.CompanyId = "d5ba21d9-e99c-46a5-8ab0-191039dc4e06";
                model.UserId = "a605e7ca-e449-440f-816a-6016b9d52322";

                await _unitOfWork.sectionRepository.AddAsync(model);
                await _unitOfWork.Save(token);
                TempData["Message"] = "Data Save Successfully";
                TempData["Status"] = "1";
                return Ok(new { error = false, message = "Saved successfully" });


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSection(string id, [FromBody] Section model, CancellationToken token)
        {
            try
            {
                var comid = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
                var userid = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

                var update = await _unitOfWork.sectionRepository.GetByIdAsync(model.Id, token);


                update.SecName = model.SecName;
                update.SecCode = model.SecCode;
                update.LocalName = model.LocalName;
                update.Order = model.Order;


                update.CompanyId = "d5ba21d9-e99c-46a5-8ab0-191039dc4e06";
                update.UserId = "a605e7ca-e449-440f-816a-6016b9d52322";


                await _unitOfWork.sectionRepository.EditAsync(update);
                await _unitOfWork.Save(token);
                TempData["Message"] = "Data Save Successfully";
                TempData["Status"] = "1";
                return Ok(new { error = false, message = "Saved successfully" });


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSection(string id, [FromBody] Designation model, CancellationToken token)
        {
            try
            {
                var delete = await _unitOfWork.sectionRepository.GetByIdAsync(model.Id, token);

                await _unitOfWork.sectionRepository.RemoveAsync(delete);
                await _unitOfWork.Save(token);

                return Ok(new { error = false, message = "Data Delete successfully" });


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> SearchSection(string searchTerm, CancellationToken token)
        {
            try
            {
                var sections = await _unitOfWork.sectionRepository.GetAllSection(searchTerm, token);

                // Return the search result in the response
                return Ok(sections);
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return StatusCode(500, new { error = true, message = ex.Message });
            }
        }

    }
}
