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

        // POST: /Login/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string userName, string userPassword)
        {
            if (ModelState.IsValid)
            {
                string message = _dbContext.LoginUser(userName, userPassword);
                if (message == "Login successful.")
                {
                    // Set the username in ViewBag
                    ViewBag.UserName = userName;

                    // Login successful
                    return RedirectToAction("Index", "Home", new { message });
                }
                else
                {
                    // Login failed
                    ViewBag.Message = message;
                    return View();
                }
            }

            return View();
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
