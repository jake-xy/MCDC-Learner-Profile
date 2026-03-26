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

    }
}
