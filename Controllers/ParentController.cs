using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnerProfile.app.Controllers
{
    [Authorize(Roles = "Parent")]
    public class ParentController : Controller
    {
        // GET: ParentController
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult Teachers()
        {
            return View();
        }

        public IActionResult Enrollment()
        {
            return View();
        }

        public IActionResult Attendance()
        {
            return View();
        }

        public IActionResult Grades()
        {
            return View();
        }

        public IActionResult Payments()
        {
            return View();
        }

    }
}
