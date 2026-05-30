using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnerProfile.app.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: AdminController
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Teachers()
        {
            return View();
        }

        public ActionResult Learners()
        {
            return View();
        }

        public ActionResult Parents()
        {
            return View();
        }

        public ActionResult Finance()
        {
            return View();
        }

    }
}
