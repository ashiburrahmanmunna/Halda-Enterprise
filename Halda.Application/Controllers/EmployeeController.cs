using Halda.Core.Const;
using Halda.Core.Models.Onboarding;
using Halda.DataAccess.Repositories;
using Halda.DataAccess.Repositories.Implementation;
using Halda.DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Halda.Application.Handler;
using NuGet.Common;
using System.Linq.Dynamic.Core.Tokenizer;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Halda.Core.Enums;

namespace Halda.Application.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        //Employee save action method by borhan

        [HttpPost]
        public async Task<IActionResult> SaveEmployee([FromBody] Employee model, CancellationToken token)
        {
            try
            {
                // Retrieve the company ID from the authenticated user's claims
                var comid = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;

                // Set the company ID in the employee model (assuming there is a property for it)
                model.CompanyId = comid; // You need to have a CompanyId property in the Employee model

                // Save changes with the cancellation token, ensuring the operation is awaited
                await _unitOfWork.Save(token);

                return Ok(new { error = false, message = "Employee saved successfully" });
            }
            catch (OperationCanceledException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a meaningful response
                return StatusCode(500, new { error = true, message = "Internal Server Error" });
            }
        }


        //get employee action method by borhan

        [HttpGet]
        public async Task<IActionResult> GetEmployee(string searchTerm, DateTime? dob, CancellationToken token)
        {
            try
            {
                // Validate input parameters if necessary
                if (string.IsNullOrEmpty(searchTerm) && dob == null)
                {
                   // return BadRequest(new { success = false, message = "Either search term or date of birth must be provided." });
                    throw new CustomException("Either search term or date of birth must be provided.",400);
                }

                // Fetch employee list
                var employeeList = await _unitOfWork.employeeRepository.GetEmployess(searchTerm, dob, token);

                // Check if the employee list is empty
                if (employeeList == null || !employeeList.Any())
                {
                   // return NotFound(new { success = false, message = "No employees found." });

                    throw new CustomException("No employees found.", 404);
                }

                return Ok(new { success = true, data = employeeList });
            }

            catch (OperationCanceledException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                // _logger.LogError(ex, "Error occurred while fetching employee data.");

                // return StatusCode(500, new { success = false, message = "Internal Server Error." });
                throw;
            }
        }

        //Employee get action method by borhan

        [HttpGet]
        public async Task<IActionResult> GetEmployeeById(string id, CancellationToken token)
        {
            try
            {
                // Validate the input
                if (string.IsNullOrEmpty(id))
                {
                    // return BadRequest(new { success = false, message = "Employee ID is required." });
                    throw new CustomException("Employee ID is required.", 400);
                }

                // Fetch the employee by ID, passing the cancellation token
                var employee = await _unitOfWork.employeeRepository.GetByIdAsync(id, token);

                // Check if the employee was found
                if (employee == null)
                {
                 //   return NotFound(new { success = false, message = "Employee not found." });
                    throw new CustomException("Employee not found.", 404);
                }

                return Ok(new { success = true, data = employee });
            }
            catch (OperationCanceledException ex)
            {
                throw new Exception(ex.Message);
            }

            catch (Exception ex)
            {
                // Log the exception (optional)
                // _logger.LogError(ex, "Error occurred while fetching employee data by ID.");

              //  return StatusCode(500, new { success = false, message = "Internal Server Error." });
                throw;
            }
        }

        //Employee update action method by borhan

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(string id, [FromBody] Employee employee, CancellationToken token)
        {
            if (id != employee.Id)
            {
                return BadRequest("Employee ID mismatch");
            }


            var success = await _unitOfWork.employeeRepository.UpdateEmployeeAsync(employee, token);

            if (!success)
            {
                return NotFound($"Employee with ID {id} not found");
            }

            return Ok(employee);
        }

        


        public IActionResult UserInfo()
        {
            return View();
        }

        //SaveChangesCompletedEventData employee education by borhan

        [HttpPost]
        public async Task<IActionResult> SaveEmployeeEdu([FromBody] EmpEdu model, CancellationToken token)
        {
            try
            {
                // Add the new employee education record
                await _unitOfWork.employeeeduRepository.AddAsync(model);

                // Save changes with the cancellation token
                await _unitOfWork.Save(token);



                return Ok("Education saved successfully.");
            }

            catch (Exception ex)
            {
                // Handle general errors
               // return StatusCode(500, "Internal Server Error");
                throw;
            }
        }

        //Employee Education get by id by borhan

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpEdu>>> GetEducationByEmployeeId(string employeeId, CancellationToken token)
        {
            try
            {
                var educationRecords = await _unitOfWork.employeeeduRepository.GetEducationByEmployeeIdAsync(employeeId, token);

                if (educationRecords == null || !educationRecords.Any())
                {
                   // return NotFound($"No education records found for employee with ID: {employeeId}");
                    throw new CustomException($"No education records found for employee with ID: {employeeId}",400);
                }

                return Ok(educationRecords);
            }
            catch (OperationCanceledException ex)
            {
                // Handle cancellation explicitly
                // return StatusCode(StatusCodes.Status400BadRequest, "Operation was canceled.");
                throw new Exception(ex.Message);
            }
            catch (Exception)
            {
                // Log the exception
              //  return StatusCode(500, "An error occurred while retrieving the education records.");
                throw ;
            }
        }

        //Delete employee education by id by borhan

        [HttpDelete("education/{id}")]
        public async Task<IActionResult> DeleteEmployeeEdu(string id, CancellationToken token)
        {
            try
            {
                // Fetch the education record with the provided cancellation token
                var educationRecord = await _unitOfWork.employeeeduRepository.GetByIdAsync(id, token);

                if (educationRecord == null)
                {
                    return NotFound($"Education record with ID {id} not found.");
                }

                // Remove the education record asynchronously
                await _unitOfWork.employeeeduRepository.RemoveAsync(id);

                // Save changes with the cancellation token
                await _unitOfWork.Save(token);

                // Check if the operation was canceled before completing
                if (token.IsCancellationRequested)
                {
                    return StatusCode(499, "Request was canceled.");
                }

                return Ok($"Education record with ID {id} deleted successfully.");
            }
            catch (OperationCanceledException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception and return a generic error response
                throw;
            }
        }

        //Update employee education by id by borhan

        [HttpPut("education/{id}")]
        public async Task<IActionResult> EditEmployeeEdu(string id, [FromBody] EmpEdu model, CancellationToken token)
        {
            try
            {
                // Set the ID on the model (in case it's not set in the incoming request)
                model.Id = id;

                // Update the education record using the repository
                var success = await _unitOfWork.employeeeduRepository.UpdateEducationRecordAsync(model, token);

                if (!success)
                {
                    throw new CustomException($"Education record with ID {id} not found.", 404);
                }

                return Ok($"Education record with ID {id} updated successfully.");
            }
            catch (OperationCanceledException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception (e.g., using a logger)
                throw;
            }
        }

        // save or update family and nominee info action method by borhan

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdateFamilyNomineeInfo([FromBody] EmployeeFamilyNomineeInfo model, CancellationToken token)
        {
            try
            {
                await _unitOfWork.employeefamnomiRepository.SaveOrUpdateAsync(model, token);
                await _unitOfWork.Save(token);

                return Ok("Family and nominee info saved or updated successfully");
            }
            catch (OperationCanceledException ex)
            {
                return StatusCode(500, $"Operation canceled: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetFamilyNomineeInfo(string employeeId, CancellationToken token)
        {
            try
            {
                // Fetch the family nominee information based on employeeId
                var nomineeInfos = await _unitOfWork.employeefamnomiRepository.GetAsync(
                    filter: n => n.EmployeeId == employeeId,
                    include: null
                );

                if (nomineeInfos == null || !nomineeInfos.Any())
                {
                    return NotFound(new { message = "No family or nominee information found for the provided employee ID." });
                }

                // Return the fetched information
                return Ok(nomineeInfos);
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                return StatusCode(500, new { message = "An error occurred while retrieving family and nominee information.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdateEmployeeAddress([FromBody] EmployeeAddress model, CancellationToken token)
        {
            try
            {
                await _unitOfWork.employeeAddRepo.SaveOrUpdateAsync(model, token);
                await _unitOfWork.Save(token);

                return Ok("Employee address saved or updated successfully");
            }
            catch (OperationCanceledException ex)
            {
                return StatusCode(500, $"Operation canceled: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeAddress(string employeeId, CancellationToken token)
        {
            try
            {
                // Fetch the address information based on employeeId
                var addresses = await _unitOfWork.employeeAddRepo.GetAsync(
                    filter: a => a.EmployeeId == employeeId,
                    include: null
                );

                if (addresses == null || !addresses.Any())
                {
                    return NotFound(new { message = "No address information found for the provided employee ID." });
                }

                // Return the fetched information
                return Ok(addresses);
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                return StatusCode(500, new { message = "An error occurred while retrieving employee address information.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdateEmergencyContact([FromBody] EmployeeEmergencyContact model, CancellationToken token)
        {
            try
            {
                await _unitOfWork.employeeContactRepo.SaveOrUpdateAsync(model, token);
                await _unitOfWork.Save(token);

                return Ok("Emergency contact information saved or updated successfully");
            }
            catch (OperationCanceledException ex)
            {
                return StatusCode(500, $"Operation canceled: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetEmergencyContact(string employeeId, CancellationToken token)
        {
            try
            {
                // Fetch the emergency contact information based on employeeId
                var emergencyContacts = await _unitOfWork.employeeContactRepo.GetAsync(
                    filter: ec => ec.EmployeeId == employeeId,
                    include: null
                );

                if (emergencyContacts == null || !emergencyContacts.Any())
                {
                    return NotFound(new { message = "No emergency contact information found for the provided employee ID." });
                }

                // Return the fetched information
                return Ok(emergencyContacts);
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                return StatusCode(500, new { message = "An error occurred while retrieving emergency contact information.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdateBankDetails([FromBody] EmployeeBank model, CancellationToken token)
        {
            try
            {
                await _unitOfWork.employeeBankRepository.SaveOrUpdateAsync(model, token);
                await _unitOfWork.Save(token);

                return Ok("Emergency contact information saved or updated successfully");
            }
            catch (OperationCanceledException ex)
            {
                return StatusCode(500, $"Operation canceled: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetBankDetails(string employeeId, CancellationToken token)
        {
            try
            {
                // Fetch the emergency contact information based on employeeId
                var bankdetails = await _unitOfWork.employeeBankRepository.GetAsync(
                    filter: ec => ec.EmployeeId == employeeId,
                    include: null
                );

                if (bankdetails == null || !bankdetails.Any())
                {
                    return NotFound(new { message = "No emergency contact information found for the provided employee ID." });
                }

                // Return the fetched information
                return Ok(bankdetails);
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                return StatusCode(500, new { message = "An error occurred while retrieving emergency contact information.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdateTaxDetails([FromBody] EmployeeTax model, CancellationToken token)
        {
            try
            {
                await _unitOfWork.employeeTaxRepository.SaveOrUpdateAsync(model, token);
                await _unitOfWork.Save(token);

                return Ok("Emergency contact information saved or updated successfully");
            }
            catch (OperationCanceledException ex)
            {
                return StatusCode(500, $"Operation canceled: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTaxDetails(string employeeId, CancellationToken token)
        {
            try
            {
                // Fetch the emergency contact information based on employeeId
                var taxdetails = await _unitOfWork.employeeTaxRepository.GetAsync(
                    filter: ec => ec.EmployeeId == employeeId,
                    include: null
                );

                if (taxdetails == null || !taxdetails.Any())
                {
                    return NotFound(new { message = "No emergency contact information found for the provided employee ID." });
                }

                // Return the fetched information
                return Ok(taxdetails);
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                return StatusCode(500, new { message = "An error occurred while retrieving emergency contact information.", error = ex.Message });
            }
        }

    }
}
