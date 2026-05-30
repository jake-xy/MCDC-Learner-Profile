using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using LearnerProfile.app.Data;
using LearnerProfile.app.Models;
using LearnerProfile.app.ViewModels.Auth;
// using LearnerProfile.app.ViewModels.Home;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace LearnerProfile.app.Controllers
{
    public class AuthController : Controller
    {
        private readonly LearnerProfileContext _context;

        public AuthController(LearnerProfileContext context)
        {
            _context = context;
        }

        // --- TEACHER REGISTRATION ---
        [HttpGet]
        public IActionResult RegisterTeacher()
        {
            return View(new RegisterTeacherViewModel());
        }

        [HttpPost]
        public IActionResult RegisterTeacher(RegisterTeacherViewModel model) 
        {
            if (!ModelState.IsValid) return View(model);

            // check if email is already taken
            if (_context.Users.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "This email is already registered.");
                return View(model);
            }

            // create the base user
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Email = model.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.PasswordHash),
                Role = UserRole.Teacher,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // create teacher profile
            var teacherProfile = new Teacher
            {
                Id = Guid.NewGuid(),
                UserId = newUser.Id,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                ContactNumber = model.ContactNumber,
                IdNumber = model.IdNumber
            };

            // save to database
            _context.Users.Add(newUser);
            _context.Teachers.Add(teacherProfile);
            _context.SaveChanges();

            return RedirectToAction("Login", "Auth");
        }


        // --- PARENT REGISTRATION ---
        [HttpGet]
        public IActionResult RegisterParent()
        {
            return View(new RegisterParentViewModel());
        }

        [HttpPost]
        public IActionResult RegisterParent(RegisterParentViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (_context.Users.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "This email is already registered");
                return View(model);
            }

            // create the base user
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Email = model.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.PasswordHash),
                Role = UserRole.Parent,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // create teacher profile
            var parentProfile = new Parent
            {
                Id = Guid.NewGuid(),
                UserId = newUser.Id,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                ContactNumber = model.ContactNumber
            };

            // save to database
            _context.Users.Add(newUser);
            _context.Parents.Add(parentProfile);
            _context.SaveChanges();

            // send success message
            TempData["SuccessMessage"] = "Registration successful! You may now log in...";

            return RedirectToAction("Login", "Auth");
        }


        // --- LOGGING IN, LOG IN, LOG-IN, SIGN IN ---
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == model.Email);

            // check credentials
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                return View(model);
            }

            // pack the user's credentials into "claims"
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, user.Role.ToString())
            };

            // create the identity
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // set cookie options
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2)
            };

            // issue the cookie to the browswer
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );

            // redirect the user to their specific dashboard based on their role
            if (user.Role == UserRole.Teacher) return RedirectToAction("Index", "Teacher");
            if (user.Role == UserRole.Parent) return RedirectToAction("Index", "Parent");
            if (user.Role == UserRole.Admin) return RedirectToAction("Index", "Admin");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
