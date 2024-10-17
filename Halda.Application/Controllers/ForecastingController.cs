using Halda.Core.DTO.Budgeting;
using Halda.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Halda.Application.Controllers
{
    public class ForecastingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ForecastingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Simulation()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetForecastData()
        {
            var empSalaries = await _unitOfWork.forecastingRepository.GetEmpSalaryViewModelsAsync(HttpContext.RequestAborted);
            var forecastData = CalculateForecast(empSalaries);
            return Json(forecastData);
        }

        private object CalculateForecast(IEnumerable<EmpSalaryViewModel> empSalaries)
        {
            var companyGroups = empSalaries.GroupBy(e => e.CompanyName);
            var forecastData = new List<object>();
            foreach (var company in companyGroups)
            {
                var companyData = new
                {
                    Company = company.Key,
                    Previous = company.Count(),
                    PreviousGrossSalary = company.Average(c => c.GrossSalary),
                    MaxSalary = company.Max(c => c.MaxSalary),
                    MinSalary = company.Min(c => c.MinSalary),
                    Departments = company.GroupBy(e => e.DepartmentName).Select(dept =>
                    {
                        var deptData = new Dictionary<string, object>
                        {
                            ["Department"] = dept.Key,
                            ["Previous"] = dept.Count(),
                            ["PreviousGrossSalary"] = dept.Average(e => e.GrossSalary),
                            ["MaxSalary"] = dept.Max(e => e.MaxSalary),
                            ["MinSalary"] = dept.Min(e => e.MinSalary)
                        };
                        deptData["Designations"] = dept.GroupBy(e => e.DesignationName).Select(g =>
                        {
                            return new Dictionary<string, object>
                            {
                                ["Designation"] = g.Key,
                                ["Previous"] = g.Count(),
                                ["PreviousGrossSalary"] = g.Average(e => e.GrossSalary),
                                ["MaxSalary"] = g.Max(e => e.MaxSalary),
                                ["MinSalary"] = g.Min(e => e.MinSalary)
                            };
                        });
                        return deptData;
                    })
                };
                forecastData.Add(companyData);
            }
            return forecastData;
        }
    }
}
