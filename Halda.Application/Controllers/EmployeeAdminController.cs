using Halda.Core.Const;
using Halda.Core.DTO.Onboarding;
using Halda.Core.Enums;
using Halda.Core.Models.Onboarding;
using Halda.DataAccess.Repositories;
using Halda.DataAccess.Repositories.Implementation;
using Halda.DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text.Json;

namespace Halda.Application.Controllers
{
    public class EmployeeAdminController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public EmployeeAdminController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public ActionResult AddEmployeeInformation(string? employeeId)
        {
            ViewBag.CompanyId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
            ViewBag.UserId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

            if (employeeId == null)
            {
                ViewBag.ActionType = "Create";
            }
            else
            {
                ViewBag.ActionType = "Edit";
            }

            ViewBag.employeeId = employeeId;
            return View();
        }

        public ActionResult AdminEmployeeList()
        {
            ViewBag.CompanyId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
            ViewBag.UserId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

            return View();
        }

        [AllowAnonymous]
        public async Task<JsonResult> GetEmployeeList(string fromDate = "", string toDate = "", int page = 1, decimal size = 5, string searchquery = "")
        {
            try
            {
                var companyId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
                ViewBag.CompanyId = companyId;
                ViewBag.UserId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

                if (string.IsNullOrEmpty(companyId))
                {
                    return Json(new
                    {
                        success = false,message = "No Company ID found for the current user."
                    });
                }

                var employeeList = await _unitOfWork.employeeRepository.GetEmployeeListAsync(searchquery, companyId, CancellationToken.None, page, (int)size);

                if (employeeList == null || !employeeList.Any())
                {                   
                    return Json(new
                    {
                        success = false,message = "No employees found for the specified Company ID."
                    });
                }

                var totalRecordCount = await _unitOfWork.employeeRepository.GetTotalRecordCountAsync(searchquery, companyId, CancellationToken.None);
                var pageCount = (int)Math.Ceiling((decimal)totalRecordCount / size);

                // Return the employee list and pagination info in JSON format
                return Json(new
                {
                    success = true,
                    data = employeeList,
                    totalRecordCount = totalRecordCount,
                    pageCount = pageCount
                });
                
                //return Json(new { success = true, data = employeeList });
            }
            catch (Exception)
            {
                throw;
            }
        }
       

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SaveEmployee([FromBody] Employee model, string updateId, CancellationToken token)
        {
            try
            {               
                var comid = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
                var userid = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

                var isEmployeeCodeExists = await _unitOfWork.employeeRepository.IsEmployeeCodeExistsAsync(model.EmployeeCode, model.Id);

                if (isEmployeeCodeExists)
                {
                    return Ok(new { error = true, message = "Employee Code must be unique." });
                }

                if (model.Id == updateId)
                {
                    await _unitOfWork.employeeRepository.EditAsync(model);
                    await _unitOfWork.Save(token);
                    return Ok(new { error = false, message = "Updated successfully" });

                }
                else 
                {
                    await _unitOfWork.employeeRepository.AddAsync(model);
                    await _unitOfWork.Save(token);   
                    return Ok(new { error = false, message = "Saved successfully" });
                }
                               

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [AllowAnonymous]      
        public async Task<IActionResult> DeleteAdminEmp(string employeeId, CancellationToken token)
        {
            try
            {
                await _unitOfWork.employeeRepository.RemoveAsync(employeeId);
                await _unitOfWork.Save(token);
                return Ok(new { success = "1", msg = "Deleted Successfully" });

            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetEmployeeData(string employeeId,CancellationToken token)
        {
            try
            {
                var employeeData = await _unitOfWork.employeeRepository.GetEmployeeDataAsync(employeeId,token);


                return Ok(employeeData);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public ActionResult EmployeeOrganization(string? employeeId)
        {
            ViewBag.CompanyId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
            ViewBag.UserId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

            //if (employeeId == null)
            //{
            //    ViewBag.ActionType = "Create";
            //}
            //else
            //{
            //    ViewBag.ActionType = "Edit";
            //}

            ViewBag.employeeId = employeeId;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SaveEmpOrganization([FromBody] EmpContractPeriod model, string updateId, CancellationToken token)
        {
            try
            {
                var comid = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
                var userid = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

                if (model.Id == updateId)
                {
                    await _unitOfWork.empOrganizationRepository.EditAsync(model);
                    await _unitOfWork.Save(token);
                    return Ok(new { error = false, message = "Updated successfully" });

                }
                else
                {
                    await _unitOfWork.empOrganizationRepository.AddAsync(model);
                    await _unitOfWork.Save(token);
                    return Ok(new { error = false, message = "Saved successfully" });
                }


            }
            catch (Exception)
            {

                throw;
            }
        }

        [AllowAnonymous]
        public async Task<JsonResult> GetContractList()
        {
            try
            {
                var companyId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
                ViewBag.CompanyId = companyId;
                ViewBag.UserId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

                if (string.IsNullOrEmpty(companyId))
                {
                    return Json(new
                    {
                        success = false,message = "No Company ID found for the current user."
                    });
                }

                var employeeList = await _unitOfWork.empOrganizationRepository.GetContractPeriodListAsync(companyId, CancellationToken.None);

                if (employeeList == null || !employeeList.Any())
                {
                    return Json(new
                    {
                        success = false, message = "No employees found for the specified Company ID."
                    });
                }

                // Return the employee list and pagination info in JSON format
                return Json(new
                {
                    success = true,
                    data = employeeList.ToList()
                });

            }
            catch (Exception)
            {
                throw;
            }
        }



        [AllowAnonymous]
        public async Task<JsonResult> GetTeamMemberList(string searchquery = "")
        {
            try
            {
                var companyId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
                ViewBag.CompanyId = companyId;
                ViewBag.UserId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

                if (string.IsNullOrEmpty(companyId))
                {
                    return Json(new
                    {
                        success = false,
                        message = "No Company ID found for the current user."
                    });
                }

                var employeeList = await _unitOfWork.teamMemberRepository.GetTeamMemberListAsync(searchquery, companyId, CancellationToken.None);

                if (employeeList == null || !employeeList.Any())
                {
                    return Json(new
                    {
                        success = false,
                        message = "No employees found for the specified Company ID."
                    });
                }               

                // Return the employee list and pagination info in JSON format
                return Json(new
                {
                    success = true,
                    data = employeeList.ToList()
                    
                });

                //return Json(new { success = true, data = employeeList });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SaveTeamMember([FromBody] EmployeeTeam model, string updateId, CancellationToken token)
        {
            try
            {
                var comid = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
                var userid = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

                if (model.Id == updateId)
                {
                    await _unitOfWork.teamMemberRepository.EditAsync(model);
                    await _unitOfWork.Save(token);
                    return Ok(new { error = false, message = "Updated successfully" });

                }
                else
                {
                    await _unitOfWork.teamMemberRepository.AddAsync(model);
                    await _unitOfWork.Save(token);
                    return Ok(new { error = false, message = "Saved successfully" });
                }


            }
            catch (Exception)
            {

                throw;
            }
        }

        [AllowAnonymous]
        public async Task<JsonResult> ValidateTeamMemberList(string searchquery = "")
        {
            try
            {
                var companyId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
                ViewBag.CompanyId = companyId;
                ViewBag.UserId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

                if (string.IsNullOrEmpty(companyId))
                {
                    return Json(new
                    {
                        success = false,
                        message = "No Company ID found for the current user."
                    });
                }

                var employeeList = await _unitOfWork.teamMemberRepository.GetValidateTeamMember(searchquery, companyId, CancellationToken.None);

                if (employeeList == null || !employeeList.Any())
                {
                    return Json(new
                    {
                        success = false,
                        message = "No employees found for the specified Company ID."
                    });
                }

                // Return the employee list and pagination info in JSON format
                return Json(new
                {
                    success = true,
                    data = employeeList.ToList()

                });

                //return Json(new { success = true, data = employeeList });
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SaveBankDetails([FromBody] EmployeeBank model, string updateId, CancellationToken token)
        {
            try
            {
                var comid = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
                var userid = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

                var existingBank = await _unitOfWork.bankDetailsRepository.Single(x => x.EmployeeId == updateId);

                existingBank.CompanyId = model.CompanyId;
                existingBank.UserId = model.UserId;
                existingBank.AccHolderName = model.AccHolderName;
                existingBank.AccNumber = model.AccNumber;
                existingBank.AccType = model.AccType;
                existingBank.ReAccNumber = model.ReAccNumber;
                existingBank.BankNumber = model.BankNumber;
                existingBank.BankName = model.BankName;
                existingBank.RoutingNumber = model.RoutingNumber;

                await _unitOfWork.Save(token);
                return Ok(new { error = false, message = "Updated successfully" });

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult BankAccountType()
        {
            var accountTypes = Enum.GetValues(typeof(AccountType))
                                   .Cast<AccountType>()
                                   .Select(at => new SelectListItem
                                   {
                                       Value = at.ToString(),
                                       Text = at.ToString()
                                   }).ToList();

            ViewBag.AccountTypes = accountTypes;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SaveTaxDetails([FromBody] EmployeeTax model, string updateId, CancellationToken token)
        {
            try
            {
                var comid = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
                var userid = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

                var existingTax = await _unitOfWork.taxDetailsRepository.Single(x => x.EmployeeId == updateId);

                existingTax.CompanyId = model.CompanyId;
                existingTax.UserId = model.UserId;
                existingTax.TaxYear = model.TaxYear;
                existingTax.Name = model.Name;
                existingTax.TaxCertificateReceive = model.TaxCertificateReceive;
                existingTax.TaxExtension = model.TaxExtension;
                existingTax.ReturnSubmit = model.ReturnSubmit;
                existingTax.ReturnSubmitDate = model.ReturnSubmitDate;
                existingTax.AcknowledgmentSlipRecDate = model.AcknowledgmentSlipRecDate;                

                await _unitOfWork.Save(token);
                return Ok(new { error = false, message = "Updated successfully" });

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SaveJobInformation([FromBody] EmployeeJobInfoDTO model, string updateId, CancellationToken token)
        {
            try
            {
                var comid = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
                var userid = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

                var existingEmployee = await _unitOfWork.employeeRepository.Single(x => x.Id == updateId);

                // Update only the fields from the DTO
                existingEmployee.CompanyId = model.CompanyId;
                existingEmployee.UserId = model.UserId;
                existingEmployee.EmployeeCode = model.EmployeeCode;
                existingEmployee.EmployeeType = model.EmployeeType;
                existingEmployee.JoiningDate = model.JoiningDate;
                existingEmployee.Location = model.Location;
                existingEmployee.DesignationId = model.DesignationId;
                existingEmployee.DepartmentId = model.DepartmentId;
                existingEmployee.Gender = model.Gender;

                //await _unitOfWork.employeeRepository.EditAsync(model);
                await _unitOfWork.Save(token);
                return Ok(new { error = false, message = "Updated successfully" });

                


            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetBankDetailData(string employeeId, CancellationToken token)
        {
            try
            {
                var employeeData = await _unitOfWork.bankDetailsRepository.GetBankDetailData(employeeId, token);


                return Ok(employeeData);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetTaxDetailData(string employeeId, CancellationToken token)
        {
            try
            {
                var employeeData = await _unitOfWork.taxDetailsRepository.GetTaxDetailData(employeeId, token);


                return Ok(employeeData);
            }
            catch (Exception)
            {

                throw;
            }

        }
       

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SaveDocuments(string updateId, CancellationToken token)
        {
            try
            {
                var comid = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
                var userid = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

                // Check if files were uploaded
                var files = Request.Form.Files;
                if (files.Count == 0)
                {
                    return BadRequest(new { error = true, message = "No files were uploaded." });
                }

                // Get the file types array from the form data
                var types = Request.Form["types"].ToArray();
                if (files.Count != types.Length)
                {
                    return BadRequest(new { error = true, message = "Number of files and types do not match." });
                }

                // Define the folder path where the files will be stored
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Media", "EmployeeDocFile");

                // Ensure the directory exists, if not, create it
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Save each file with its corresponding type
                List<EmployeeDocument> documentList = new List<EmployeeDocument>();
                for (int i = 0; i < files.Count; i++)
                {
                    var file = files[i];
                    var docType = types[i]; 

                    // Generate a unique file name to avoid overwriting
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string extension = Path.GetExtension(file.FileName);
                    string uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";

                    string filePath = Path.Combine(folderPath, uniqueFileName);

                    // Save the file to the specified path
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var relativeFilePath = Path.Combine("Media", "EmployeeDocFile", Path.GetFileName(filePath));
                    
                    var document = new EmployeeDocument
                    {
                        CompanyId = comid,
                        UserId = userid,
                        EmployeeId = updateId,
                        Type = docType, 
                        DocPath = relativeFilePath 
                    };

                    documentList.Add(document);
                }
               
                foreach (var document in documentList)
                {
                    if (document.Id != null) 
                    {
                        await _unitOfWork.documentRepository.AddAsync(document);
                    }
                    else 
                    {
                        var existingDocument = await _unitOfWork.documentRepository.Single(x => x.EmployeeId == updateId);
                        existingDocument.CompanyId = document.CompanyId;
                        existingDocument.UserId = document.UserId;
                        existingDocument.Type = document.Type;
                        existingDocument.DocPath = document.DocPath;
                        existingDocument.EmployeeId = document.EmployeeId;
                    }
                }
               
                await _unitOfWork.Save(token);

                return Ok(new { error = false, message = "Documents saved successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = true, message = "An error occurred: " + ex.Message });
            }
        }



        [HttpGet]
        public async Task<IActionResult> SearchEmployee(string searchTerm, CancellationToken token)
        {
            try
            {
                var employees = await _unitOfWork.employeeRepository.GetAllEmployees(searchTerm, token);

                // Return the search result in the response
                return Ok(employees);
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return StatusCode(500, new { error = true, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetEmpDocumentData(string employeeId, CancellationToken token)
        {
            try
            {
                var employeeData = await _unitOfWork.documentRepository.GetDocumentsData(employeeId, token);


                return Ok(employeeData);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [AllowAnonymous]
        public async Task<JsonResult> GetEmpDocumentList()
        {
            try
            {
                var companyId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
                ViewBag.CompanyId = companyId;
                ViewBag.UserId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.UserId)?.Value;

                if (string.IsNullOrEmpty(companyId))
                {
                    return Json(new
                    {
                        success = false,
                        message = "No Company ID found for the current user."
                    });
                }

                var employeeList = await _unitOfWork.documentRepository.GetEmpDocuments(companyId, CancellationToken.None);

                if (employeeList == null || !employeeList.Any())
                {
                    return Json(new
                    {
                        success = false,
                        message = "No employees found for the specified Company ID."
                    });
                }

                // Return the employee list and pagination info in JSON format
                return Json(new
                {
                    success = true,
                    data = employeeList.ToList()

                });

                //return Json(new { success = true, data = employeeList });
            }
            catch (Exception)
            {
                throw;
            }
        }


        [AllowAnonymous]
        public async Task<IActionResult> GetDocDownload(string docPath)
        {
            try
            {
                if (string.IsNullOrEmpty(docPath))
                {
                    return BadRequest(new { error = true, message = "Document path is required." });
                }

                // Construct the full file path on the server
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), docPath);

                if (!System.IO.File.Exists(fullPath))
                {
                    return NotFound(new { error = true, message = "File not found." });
                }

                // Get the file's MIME type based on its extension
                var fileExtension = Path.GetExtension(fullPath);
                var mimeType = GetMimeType(fileExtension);

                // Return the file as a download
                var fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
                return File(fileBytes, mimeType, Path.GetFileName(fullPath));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = true, message = "An error occurred: " + ex.Message });
            }
        }

        // Helper method to get the MIME type
        private string GetMimeType(string fileExtension)
        {
            return fileExtension.ToLower() switch
            {
                ".pdf" => "application/pdf",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                _ => "application/octet-stream", // Default MIME type for unknown file types
            };
        }


    }
}
