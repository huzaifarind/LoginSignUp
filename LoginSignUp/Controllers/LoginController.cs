using LoginSignUp.Data;
using LoginSignUp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LoginSignUp.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly MyDbContext _dbContext;
        public LoginController(ILogger<LoginController> logger, MyDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        // GET: /Login/Login
        public IActionResult Login(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string userName, string userPassword)
        {
            if (ModelState.IsValid)
            {
                string message = _dbContext.LoginUser(userName, userPassword);
                if (message == "Login successful.")
                {
                    // Store username in session
                    HttpContext.Session.SetString("Username", userName);

                    // Redirect to Index action
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = message;
                    return View();
                }
            }

            return View();
        }

        // GET: /Login/Logout
        public IActionResult Logout()
        {
            // Clear session data
            HttpContext.Session.Clear();

            // Redirect to Login action
            return RedirectToAction("Login");
        }


        // GET: /Login/Register
        public IActionResult Register(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        // POST: /Login/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Users user)
        {
            if (ModelState.IsValid)
            {
                _dbContext.RegisterUser(user.UserName, user.UserEmail, user.UserPassword);

                // For simplicity, assuming registration always succeeds
                string message = "User registered successfully.";

                return RedirectToAction("Login", new { message });
            }

            return View(user);
        }
    }
}
