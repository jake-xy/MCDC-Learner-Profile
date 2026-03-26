using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnerProfile.app.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class TeacherController : Controller
    {
        // GET: TeacherController
        public ActionResult Index()
        {
            return View();
        }

    }
}
