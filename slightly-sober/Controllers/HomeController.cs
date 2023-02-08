using Microsoft.AspNetCore.Mvc;
using slightly_sober.Data;
using slightly_sober.Models;
using System.Diagnostics;

namespace slightly_sober.Controllers
{
    public class HomeController : Controller
    {
  /*      private readonly ILogger<HomeController> _logger;
        ILogger<HomeController> logger,*/

        private readonly SlightlySoberContext _context;

        public HomeController(SlightlySoberContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            User me = _context.Users.Where(x => x.Username == "admin").FirstOrDefault();
            HttpContext.Session.SetInt32("UserID", me.UserID);
            HttpContext.Session.SetString("Username", me.Username);
            HttpContext.Session.SetString("IsAdmin", me.IsAdmin.ToString());
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}