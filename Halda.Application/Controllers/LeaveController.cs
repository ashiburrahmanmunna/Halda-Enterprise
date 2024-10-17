using Halda.Core.Models.Attendance;
using Halda.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Halda.Application.Controllers
{
    public class LeaveController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public LeaveController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        [HttpGet]

        public IActionResult Index()
        {
            return Ok();
        }


        //[HttpPost]
        //public async Task<IActionResult> Create(Leave obj)
        //{
        //    unitOfWork.Leave.Add(obj);
        //    await unitOfWork.CompleteAsync();
        //    return Ok("Index");

        //}

        //[HttpGet("GetById")] // same tai ai khaney route bole disi
        //public IActionResult Get(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Leave leaveFromDb = unitOfWork.Leave.Get(u => u.Id == id);
        //    if (leaveFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(leaveFromDb);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Edit(int id, Leave model)
        //{
        //    if (id != model.Id)
        //    {
        //        return BadRequest("Id mismatach");
        //    }
        //    try
        //    {
        //        var leave = unitOfWork.Leave.Get(u => u.Id == model.Id);
        //        if (leave == null)
        //        {
        //            return BadRequest("LeaveForm is not found");
        //        }
        //        leave.LeaveName = model.LeaveName;
        //        unitOfWork.Leave.Update(leave);
        //        await unitOfWork.CompleteAsync();
        //        return Ok("Update successfully");
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Update Failed");
        //    }

        //}
        //// Delete


        //[HttpDelete("{id}")] // learn query path and query string??
        //public async Task<IActionResult> DeletePOST(int id)
        //{
        //    try
        //    {
        //        var leave = unitOfWork.Leave.Get(u => u.Id == id);

        //        if (leave == null)
        //        {
        //            return BadRequest("leaveform not found");
        //        }
        //        unitOfWork.Leave.Remove(leave);
        //        await unitOfWork.CompleteAsync();
        //        return Ok("Deleted successfully");
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Delete Failed");
        //    }

        //}
    }
}
