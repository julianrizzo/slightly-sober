using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleHashing.Net;
using slightly_sober.Data;
using slightly_sober.Models;

namespace slightly_sober.Controllers
{
    public class LoginController : Controller
    {
        private readonly SlightlySoberContext _context;

        public LoginController(SlightlySoberContext context)
        {
            _context = context;
        }

        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        private const string SessionKey_Login = $"{nameof(LoginController)}_Login";
        private const string SessionKey_Password = $"{nameof(LoginController)}_Password";

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            SimpleHash hash = new SimpleHash();
            var selectedUser = await _context.Users.Where(x => x.Username == username).Include(x => x.Login).FirstOrDefaultAsync();

            if (!selectedUser.Login.IsActive)
            {
                ModelState.AddModelError("LoginFailed", "This account has been locked. Please contact an Admin to unlock.");
                return View();
            }
            if (selectedUser.Login == null || string.IsNullOrEmpty(password) || !hash.Verify(password, selectedUser.Login.PasswordHash))
            {
                ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
                return View();
            }

            // Login customer.
            HttpContext.Session.SetInt32("UserID", selectedUser.UserID);
            HttpContext.Session.SetString("Username", selectedUser.Username);

            return RedirectToAction("Index", "Home");
        }

        [Route("Signup")]
        public IActionResult Signup()
        {
            return View();
        }

        [Route("Signup")]
        [HttpPost]
        public IActionResult Signup(string username, string firstName, string lastName, string email, string password, string passwordConfirmation)
        {
            bool checkUsername = _context.Users.Any(x => x.Username == username);

            if (checkUsername)
            {
                ModelState.AddModelError("SignupFailed", "User with this username already exists");
                return View();
            }

            if (!(password == passwordConfirmation))
            {
                ModelState.AddModelError("SignupFailed", "Passwords do not match");
                return View();
            }

            var hash = new SimpleHash();
            var pwHash = hash.Compute(password);

            User newUser = new(username, firstName, lastName, email, false, new Login(pwHash));

            _context.Users.Add(newUser);
            _context.SaveChanges();

            HttpContext.Session.SetInt32("LoginID", newUser.UserID);
            HttpContext.Session.SetString("Username", newUser.Username);

            return RedirectToAction("Index", "Home");
        }

        [Route("Logout")]
        public IActionResult Logout()
        {
            // Logout customer.
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}
