using Halda.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Halda.Application.Controllers
{
    public class PayrollController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PayrollController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Increment()
        {
            return View();
        }
        public IActionResult User()
        {
           return View(); 
        }
    }
}
