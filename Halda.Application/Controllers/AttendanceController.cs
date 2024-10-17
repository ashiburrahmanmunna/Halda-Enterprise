using Halda.Core.Const;
using Halda.Core.DTO.Attendance;
using Halda.Core.Models.Attendance;
using Halda.Core.Models.Onboarding;
using Halda.DataAccess.Repositories;
using Halda.DataAccess.Repositories.Implementation;
using Halda.DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Halda.Application.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AttendanceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Action method for shift

        [HttpPost]
        public async Task<IActionResult> SaveShift([FromBody] Shift model, CancellationToken token)
        {
            if (model == null)
            {
                return BadRequest(new { error = true, message = "Invalid shift data" });
            }

            try
            {
                ViewBag.CompanyId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
                ViewBag.UserId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

                if (!string.IsNullOrEmpty(model.Id))
                {
                    // Update existing entry
                    await _unitOfWork.shiftRepository.EditAsync(model);
                    await _unitOfWork.Save(token);
                    return Ok(new { error = false, message = "Updated successfully" });
                }
                else
                {
                    // Create new entry
                    model.Id = Guid.NewGuid().ToString(); // Ensure a new Id is set
                    await _unitOfWork.shiftRepository.AddAsync(model);
                    await _unitOfWork.Save(token);
                    return Ok(new { error = false, message = "Saved successfully" });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteShift(string shiftId, CancellationToken token)
        {
            try
            {
                await _unitOfWork.shiftRepository.RemoveAsync(shiftId);
                await _unitOfWork.Save(token);
                return Ok(new { success = "1", msg = "Deleted Successfully" });

            }
            catch (Exception)
            {

                throw;
            }
        }


        [AllowAnonymous]
        public async Task<JsonResult> GetShiftList(string searchQuery = "", int page = 1, decimal size = 5)
        {
            try
            {
                var companyId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;

                if (string.IsNullOrEmpty(companyId))
                {
                    return Json(new
                    {
                        success = false,
                        message = "No Company ID found for the current user."
                    });
                }

                // If size is 0 or less, fetch all records without pagination
                if (size <= 0)
                {
                    size = await _unitOfWork.shiftRepository.GetTotalRecordCountAsync(searchQuery, companyId, CancellationToken.None);
                }

                // Retrieve shift list based on the company ID and search query
                var shiftList = await _unitOfWork.shiftRepository.GetShiftListAsync(searchQuery, companyId, CancellationToken.None, page, (int)size);

                if (shiftList == null || !shiftList.Any())
                {
                    return Json(new
                    {
                        success = false,
                        message = "No shifts found for the specified Company ID."
                    });
                }

                var totalRecordCount = await _unitOfWork.shiftRepository.GetTotalRecordCountAsync(searchQuery, companyId, CancellationToken.None);
                var pageCount = (int)Math.Ceiling((decimal)totalRecordCount / size);

                // Return the shift list and pagination info in JSON format
                return Json(new
                {
                    success = true,
                    data = shiftList,
                    totalRecordCount = totalRecordCount,
                    pageCount = pageCount
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }



        //Action method for leave

        [HttpPost]
        public async Task<IActionResult> SaveLeave([FromBody] Leave model, CancellationToken token)
        {
            if (model == null)
            {
                return BadRequest(new { error = true, message = "Invalid Leave data" });
            }

            try
            {
                ViewBag.CompanyId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
                ViewBag.UserId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

                if (!string.IsNullOrEmpty(model.Id))
                {
                    // Update existing entry
                    await _unitOfWork.leaveRepository.EditAsync(model);
                    await _unitOfWork.Save(token);
                    return Ok(new { error = false, message = "Updated successfully" });
                }
                else
                {
                    // Create new entry
                    model.Id = Guid.NewGuid().ToString(); // Ensure a new Id is set
                    await _unitOfWork.leaveRepository.AddAsync(model);
                    await _unitOfWork.Save(token);
                    return Ok(new { error = false, message = "Saved successfully" });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Deleteleave(string leaveId, CancellationToken token)
        {
            try
            {
                await _unitOfWork.leaveRepository.RemoveAsync(leaveId);
                await _unitOfWork.Save(token);
                return Ok(new { success = "1", msg = "Deleted Successfully" });

            }
            catch (Exception)
            {

                throw;
            }
        }

        [AllowAnonymous]
        public async Task<JsonResult> GetLeaveList(string searchQuery = "", int page = 1, decimal size = 5)
        {
            try
            {
                var companyId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;

                if (string.IsNullOrEmpty(companyId))
                {
                    return Json(new
                    {
                        success = false,
                        message = "No Company ID found for the current user."
                    });
                }

                // If size is 0 or less, fetch all records without pagination
                if (size <= 0)
                {
                    size = await _unitOfWork.leaveRepository.GetTotalRecordCountAsync(searchQuery, companyId, CancellationToken.None);
                }

                // Retrieve leave list based on the company ID and search query
                var leaveList = await _unitOfWork.leaveRepository.GetLeaveListAsync(searchQuery, companyId, CancellationToken.None, page, (int)size);

                if (leaveList == null || !leaveList.Any())
                {
                    return Json(new
                    {
                        success = false,
                        message = "No leaves found for the specified Company ID."
                    });
                }

                var totalRecordCount = await _unitOfWork.leaveRepository.GetTotalRecordCountAsync(searchQuery, companyId, CancellationToken.None);
                var pageCount = (int)Math.Ceiling((decimal)totalRecordCount / size);

                // Return the leave list and pagination info in JSON format
                return Json(new
                {
                    success = true,
                    data = leaveList,
                    totalRecordCount = totalRecordCount,
                    pageCount = pageCount
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> SearchLeave(string searchTerm, CancellationToken token)
        {
            try
            {
                var leaves = await _unitOfWork.leaveRepository.GetAllLeaves(searchTerm, token);

                // Return the search result in the response
                return Ok(leaves);
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return StatusCode(500, new { error = true, message = ex.Message });
            }
        }




        //  Leave action method end

        //Action method for holiday

        [HttpPost]
        public async Task<IActionResult> SaveHoliday([FromBody] List<Holidays> models, CancellationToken token)
        {
            if (models == null || !models.Any())
            {
                return BadRequest(new { error = true, message = "Invalid holiday data" });
            }

            try
            {
                ViewBag.CompanyId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
                ViewBag.UserId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

                foreach (var model in models)
                {
                    if (model == null)
                    {
                        return BadRequest(new { error = true, message = "Invalid holiday entry" });
                    }

                    if (!string.IsNullOrEmpty(model.Id))
                    {
                        // Update existing entry
                        await _unitOfWork.holidayRepository.EditAsync(model);
                    }
                    else
                    {
                        // Create new entry
                        model.Id = Guid.NewGuid().ToString(); // Ensure a new Id is set
                        await _unitOfWork.holidayRepository.AddAsync(model);
                    }
                }

                // Save all changes at once
                await _unitOfWork.Save(token);
                return Ok(new { error = false, message = "Saved successfully" });
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetPublicHolidays(string companyId, CancellationToken token)
        {
            if (string.IsNullOrEmpty(companyId))
            {
                return BadRequest(new { error = true, message = "Company ID is required." });
            }

            try
            {
                var holidays = await _unitOfWork.holidayRepository.GetPublicHolidaysByCompanyIdAsync(companyId, token);
                if (holidays == null || !holidays.Any())
                {
                    return NotFound(new { error = true, message = "No public holidays found for the given company ID." });
                }
                return Ok(holidays);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteHoliday(string holidayid, CancellationToken token)
        {
            try
            {
                await _unitOfWork.holidayRepository.RemoveAsync(holidayid);
                await _unitOfWork.Save(token);
                return Ok(new { success = "1", msg = "Deleted Successfully" });

            }
            catch (Exception)
            {

                throw;
            }
        }

        //Action method for holiday end

        // leave adjsut action method start

        [HttpPost]
        public async Task<IActionResult> SaveLeaveAdjust([FromBody] LeaveAdjustmentDto model, CancellationToken token)
        {
            if (model == null)
            {
                return BadRequest(new { error = true, message = "Invalid leave adjustment data" });
            }

            try
            {
                // Retrieve CompanyId and UserId from the user's claims
                var companyId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
                var userId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

                // Call the repository method to handle leave adjustments
                await _unitOfWork.leaveAdjustRepository.AddLeaveAdjustmentsAsync(model, companyId, userId, token);

                // Save changes to the database
                await _unitOfWork.Save(token);

                return Ok(new { error = false, message = "Leave adjustment saved successfully" });
            }
            catch (Exception ex)
            {
                // Return 500 on error
                return StatusCode(500, new { error = true, message = "An error occurred while saving the leave adjustment", details = ex.Message });
            }
        }

        public async Task<JsonResult> GetLeaveAdjustList(string searchQuery = "", string startDate = "", string endDate = "", int page = 1, int size = 10)
        {
            try
            {
                var companyId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
                if (string.IsNullOrEmpty(companyId))
                {
                    return Json(new { success = false, message = "No Company ID found for the current user." });
                }

                var leaveAdjustList = await _unitOfWork.leaveAdjustRepository.GetLeaveAdjustListAsync(searchQuery, startDate, endDate, companyId, CancellationToken.None, page, size);

                if (leaveAdjustList == null || !leaveAdjustList.Any())
                {
                    return Json(new { success = false, message = "No leave adjustments found for the specified criteria." });
                }

                var totalRecordCount = await _unitOfWork.leaveAdjustRepository.GetTotalRecordCountAsync(searchQuery, startDate, endDate, companyId, CancellationToken.None);
                var pageCount = (int)Math.Ceiling((decimal)totalRecordCount / size);

                return Json(new
                {
                    success = true,
                    data = leaveAdjustList,
                    totalRecordCount = totalRecordCount,
                    pageCount = pageCount
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LeaveAdjustDelete(string adjustleaveid, CancellationToken token)
        {
            try
            {
                await _unitOfWork.leaveAdjustRepository.RemoveAsync(adjustleaveid);
                await _unitOfWork.Save(token);
                return Ok(new { success = "1", msg = "Deleted Successfully" });

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteLeaveAdjustments([FromBody] List<string> leaveAdjustIds, CancellationToken cancellationToken)
        {
            if (leaveAdjustIds == null || leaveAdjustIds.Count == 0)
            {
                return BadRequest("No leave adjustment IDs provided.");
            }

            try
            {
                foreach (var id in leaveAdjustIds)
                {
                    var leaveAdjust = await _unitOfWork.leaveAdjustRepository.GetByIdAsync(id, cancellationToken);

                    if (leaveAdjust != null)
                    {
                        // Remove without cancellation token
                        await _unitOfWork.leaveAdjustRepository.RemoveAsync(leaveAdjust);
                    }
                }

                await _unitOfWork.Save(cancellationToken);

                return Ok(new { success = true, message = $"{leaveAdjustIds.Count} leave adjustments deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditLeaveAdjust([FromBody] LeaveAdjustmentDto model, CancellationToken token)
        {
            if (model == null || string.IsNullOrEmpty(model.EmpID) || model.AdjustDate == default)
            {
                return BadRequest(new { error = true, message = "Invalid leave adjustment data" });
            }

            try
            {
                // Retrieve CompanyId and UserId from the user's claims
                var companyId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
                var userId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

                // Call the repository method to handle leave adjustment editing
                var existingAdjustment = await _unitOfWork.leaveAdjustRepository.GetLeaveAdjustmentByIdAsync(model.EmpID, token); // Only using EmpID now

                if (existingAdjustment == null)
                {
                    return NotFound(new { error = true, message = "Leave adjustment not found" });
                }

                // Update the properties of the existing leave adjustment
                existingAdjustment.AdjustType = model.AdjustType;
                existingAdjustment.AdjustLeaveId = model.AdjustLeaveId;
                existingAdjustment.ReplaceDate = model.ReplaceDate;
                existingAdjustment.Remarks = model.Remarks;
                existingAdjustment.UserId = userId;
                existingAdjustment.AdjustDate = model.AdjustDate; // Update AdjustDate

                // Save changes to the database
                await _unitOfWork.Save(token);

                return Ok(new { error = false, message = "Leave adjustment updated successfully" });
            }
            catch (Exception ex)
            {
                // Return 500 on error
                return StatusCode(500, new { error = true, message = "An error occurred while updating the leave adjustment", details = ex.Message });
            }
        }



        //[HttpPost]
        //public async Task<IActionResult> EditLeaveAdjust(EmpLeaveAdjust leaveAdjust)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            // Update the leave adjustment
        //            await _unitOfWork.leaveAdjustRepository.EditAsync(leaveAdjust);
        //            await _unitOfWork.Save(CancellationToken.None); // Save changes

        //            // Return an Ok response
        //            return Ok(new { message = "Leave adjustment updated successfully." });
        //        }
        //        catch (Exception ex)
        //        {
        //            // Handle exceptions (log them, show error messages, etc.)
        //            ModelState.AddModelError("", $"Error updating leave adjustment: {ex.Message}");
        //        }
        //    }

        //    // If the model state is invalid or an error occurs, return a bad request response
        //    return BadRequest(ModelState); // Return validation errors
        //}

        //[HttpPost]
        //public async Task<IActionResult> EditLeaveAdjust(EmpLeaveAdjust leaveAdjust, CancellationToken cancellationToken)
        //{
        //    var employee = await _unitOfWork.employeeRepository.GetByIdAsync(leaveAdjust.EmpID, cancellationToken);

        //    if (employee == null)
        //    {
        //        return BadRequest("Employee not found.");
        //    }

        //    leaveAdjust.Emp = employee; // Populate the Employee object.

        //    // Continue with your update logic...
        //    await _unitOfWork.leaveAdjustRepository.EditAsync(leaveAdjust);
        //    await _unitOfWork.Save(CancellationToken.None); // Save changes

        //    return Ok("Leave adjustment updated successfully.");
        //}




        // leave adjsut action method end

        public ActionResult DailySummaryList()
        {
            ViewBag.CompanyId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
            ViewBag.UserId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

            return View();
        }

        public ActionResult TimeCard()
        {
            ViewBag.CompanyId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
            ViewBag.UserId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

            return View();
        }

        public ActionResult Leaves()
        {
            ViewBag.CompanyId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
            ViewBag.UserId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

            return View();
        }

        public ActionResult Attendance()
        {
            ViewBag.CompanyId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
            ViewBag.UserId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

            return View();
        }
        public ActionResult HolidaySetup()
        {
            ViewBag.CompanyId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
            ViewBag.UserId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;



            return View();
        }
        public ActionResult Shift()
        {
            ViewBag.CompanyId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
            ViewBag.UserId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

            return View();
        }
        public ActionResult DataImport()
        {
            ViewBag.CompanyId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
            ViewBag.UserId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

            return View();
        }

        public ActionResult TimeCardView()
        {
            ViewBag.CompanyId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
            ViewBag.UserId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

            return View();
        }
        public ActionResult LeaveSetup()
        {
            ViewBag.CompanyId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
            ViewBag.UserId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

            return View();
        }

        public ActionResult UserProfile()
        {
            ViewBag.CompanyId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
            ViewBag.UserId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

            return View();
        }

        
    }
}
