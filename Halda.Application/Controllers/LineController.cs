using Halda.Core.Const;
using Halda.Core.Models.Variable;
using Halda.DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Halda.Application.Controllers
{
    public class LineController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public LineController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult AddLine()
        {
            ViewBag.CompanyId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
            ViewBag.UserId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SaveLine([FromBody] Line model, CancellationToken token)
        {
            try
            {
                var comid = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
                var userid = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

                model.CompanyId = "d5ba21d9-e99c-46a5-8ab0-191039dc4e06";
                model.UserId = "a605e7ca-e449-440f-816a-6016b9d52322";

                await _unitOfWork.lineRepository.AddAsync(model);
                await _unitOfWork.Save(token);
                TempData["Message"] = "Data Save Successfully";
                TempData["Status"] = "1";
                return Ok(new { error = false, message = "Saved successfully" });


            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLine(string id, [FromBody] Line model, CancellationToken token)
        {
            try
            {
                var comid = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
                var userid = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

                var update = await _unitOfWork.lineRepository.GetByIdAsync(model.Id, token);


                update.LineName = model.LineName;
                update.LineCode = model.LineCode;
                update.LocalName = model.LocalName;
                update.Order = model.Order;


                update.CompanyId = "d5ba21d9-e99c-46a5-8ab0-191039dc4e06";
                update.UserId = "a605e7ca-e449-440f-816a-6016b9d52322";


                await _unitOfWork.lineRepository.EditAsync(update);
                await _unitOfWork.Save(token);
              
                return Ok(new { error = false, message = "Saved successfully" });


            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLine(string id, [FromBody] Line model, CancellationToken token)
        {
            try
            {
                var delete = await _unitOfWork.lineRepository.GetByIdAsync(model.Id, token);

                await _unitOfWork.lineRepository.RemoveAsync(delete);
                await _unitOfWork.Save(token);

                return Ok(new { error = false, message = "Data Delete successfully" });


            }
            catch (Exception)
            {

                throw; 
            }
        }

        [HttpGet]
        public async Task<IActionResult> SearchLine(string searchTerm, CancellationToken token)
        {
            try
            {
                var lines = await _unitOfWork.lineRepository.GetAllLine(searchTerm, token);

                // Return the search result in the response
                return Ok(lines);
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return StatusCode(500, new { error = true, message = ex.Message });
            }
        }
    }
}
