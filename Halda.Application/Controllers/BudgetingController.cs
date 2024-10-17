using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Halda.Application.Controllers
{
    public class BudgetingController : Controller
    {
        public IActionResult Index() { return View(); }

        [HttpGet]
        public JsonResult GetBudgetData()
        {
            var budgetData = new List<object>
            {
                new { department = "GTR", fiscalYear = "2024-2025", duration = "January-March", manpowerPrevious = 30, manpowerCurrent = 50, salaryPrevious = 50000, salaryMin = 50000, salaryMax = 100000, salaryForecasted = 60000, salaryRealCost = 55000},
                new { department = "Business Operation", fiscalYear = "", duration = "", manpowerPrevious = "", manpowerCurrent = "", salaryPrevious = 20000, salaryMax = "", salaryMin = "", salaryForecasted = "", salaryRealCost = "" },
                new { department = "Jr. UI/UX Designer", fiscalYear = "", duration = "", manpowerPrevious = "", manpowerCurrent = "", salaryPrevious = 5000, salaryMax = "", salaryMin = "", salaryForecasted = "", salaryRealCost = "" },
                new { department = "Graphics Designer", fiscalYear = "", duration = "", manpowerPrevious = "", manpowerCurrent = "", salaryPrevious = 10000, salaryMax = "", salaryMin = "", salaryForecasted = "", salaryRealCost = "" },
                new { department = "Software Implementation", fiscalYear = "", duration = "", manpowerPrevious = "", manpowerCurrent = "", salaryPrevious = 20000, salaryMax = "", salaryMin = "", salaryForecasted = "", salaryRealCost = "" },
                new { department = "Jr. Software Engineer", fiscalYear = "", duration = "", manpowerPrevious = "", manpowerCurrent = "", salaryPrevious = 5000, salaryMax = "", salaryMin = "", salaryForecasted = "", salaryRealCost = "" },
                new { department = "Sr. Software Engineer", fiscalYear = "", duration = "", manpowerPrevious = "", manpowerCurrent = "", salaryPrevious = 10000, salaryMax = "", salaryMin = "", salaryForecasted = "", salaryRealCost = "" }
            };

            return Json(budgetData);
        }
    }
}
