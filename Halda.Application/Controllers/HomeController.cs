using Halda.Core.Const;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Halda.Application.Controllers
{
    public class HomeController : Controller
    {
        // GET: HomeController
        public ActionResult Index()
        {
            return View();
        }

        //this is non confrim view start

        public ActionResult personal_information()
        {
            return View();
        }

        public ActionResult EmployeeList()
        {
            return View();
        }

        public ActionResult Add_information()
        {           
            return View();
        }

        public ActionResult DailySummaryList()
        {
            return View();
        }

        public ActionResult LeaveSetup()
        {
            return View();
        }

        public ActionResult ShiftSetup()
        {
            return View();
        }

        public ActionResult HolidaySetup()
        {
            return View();
        }

        public ActionResult ViewTimeCard()
        {
            return View();
        }
        public ActionResult Leaves()
        {
            return View();
        }
        public ActionResult Attendance()
        {
            return View();
        }


        //this is non confrim view end

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
