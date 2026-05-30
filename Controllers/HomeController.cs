using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LearnerProfile.app.Models;

namespace LearnerProfile.app.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult AboutUs()
    {
        return View();
    }

    public IActionResult ContactUs()
    {
        return View();
    }

    public IActionResult RegisterLanding()
    {
        return View();
    }

    public IActionResult LoginLanding()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult RegisterTeacher()
    {
        return View();
    }

    public IActionResult RegisterParent()
    {
        return View();
    }

    public IActionResult OurClasses()
    {
        return View();
    }

    public IActionResult OurTeachers()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
