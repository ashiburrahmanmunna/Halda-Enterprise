using Halda.Application.Handler;
using Halda.Application.Models;
using Halda.Core.Const;
using Halda.Core.DTO;
using Halda.Core.Models;
using Halda.DataAccess.Services.Interface.ICompany;
using Microsoft.AspNetCore.Mvc;

namespace Halda.Application.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CompanyDTO? data, CancellationToken token)
        {
            try
            {
                if (data == null)
                {
                    // Handle the case where data is null (e.g., return a bad request or an error view)
                    // return BadRequest("Data cannot be null");
                    throw new CustomException("Data cannot be null",400);
                }

                data.ComId = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;

                if (string.IsNullOrEmpty(data.ComId))
                {
                    // Handle the case where the company ID is not found or is invalid
                    // return BadRequest("Invalid Company ID");    
                    throw new CustomException("Invalid Company ID", 400);
                }

                _companyService.CreateCompany(data, token);

                return RedirectToAction(nameof(Index));
            }
            catch (OperationCanceledException ex)
            {
                throw new Exception(ex.Message);
            }

            catch (Exception ex)
            {
                // If ErrorViewModel does not have a Message property, just use RequestId or another property
                return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
                throw;
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View();
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View();
                throw new Exception(ex.Message);
            }
        }
    }
}
