using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleHashing.Net;
using slightly_sober.Data;
using slightly_sober.Models;

namespace slightly_sober.Controllers
{
    [Route("Login")]
    public class LoginController : Controller
    {
        private readonly SlightlySoberContext _context;

        public LoginController(SlightlySoberContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        private const string SessionKey_Login = $"{nameof(LoginController)}_Login";
        private const string SessionKey_Password = $"{nameof(LoginController)}_Password";

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            SimpleHash hash = new SimpleHash();
            var selectedUser = await _context.Users.Where(x => x.UserName == username).Include(x => x.Login).FirstOrDefaultAsync();

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
            HttpContext.Session.SetInt32("LoginID", selectedUser.UserID);
            HttpContext.Session.SetString("UserName", selectedUser.UserName);

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
