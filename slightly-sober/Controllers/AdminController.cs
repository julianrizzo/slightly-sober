using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using slightly_sober.Data;
using slightly_sober.Models;

namespace slightly_sober.Controllers
{
    public class AdminController : Controller
    {
        private readonly SlightlySoberContext _context;

        public AdminController(SlightlySoberContext context)
        {
            _context = context;
        }

        public IActionResult Console()
        {
            return View();
        }

        public IActionResult Users()
        {
            List<User> userList = _context.Users.Where(x => x.UserID != HttpContext.Session.GetInt32("UserID")).ToList();

            return View(userList);
        }

        public IActionResult Cocktails()
        {
            List<Cocktail> cocktailList = _context.Cocktails.ToList();

            return View(cocktailList);
        }

        [Route("Admin/DisableUser/{userID}")]
        public async Task<IActionResult> DisableUser(int userID, bool status)
        {
            var selectedUser = await _context.Users.Where(x => x.UserID == userID).Include(x => x.Login).FirstOrDefaultAsync();

            if (selectedUser != null)
            {
                selectedUser.Login.IsActive = status;
            }
            else
            {
                return NotFound();
            }

            _context.Users.Update(selectedUser);
            _context.SaveChanges();

            return RedirectToAction("Users");
        }

        [HttpGet]
        [Route("Admin/DeleteUser/{userID}")]
        public IActionResult DeleteUser(int userID)
        {
            var selectedUser = _context.Users.Where(x => x.UserID == userID).FirstOrDefault();
            return View(selectedUser);
        }

        [HttpGet]
        [Route("Admin/DeleteCocktail/{cocktailID}")]
        public IActionResult DeleteCocktail(int cocktailID)
        {
            var selectedCocktail = _context.Cocktails.Where(x => x.CocktailID == cocktailID).FirstOrDefault();
            return View(selectedCocktail);
        }


        [HttpPost]
        public IActionResult DeleteUserConfirmed(int userID)
        {
            var selectedUser = _context.Users.Where(x => x.UserID == userID).FirstOrDefault();
            var selectedLogin = selectedUser.Login;

            _context.Logins.Remove(selectedLogin);
            _context.Users.Remove(selectedUser);
            _context.SaveChanges();

            return RedirectToAction("Users");
        }

        [HttpPost]
        public IActionResult DeleteCocktailConfirmed(int cocktailID)
        {
            var selectedCocktail = _context.Cocktails.Where(x => x.CocktailID == cocktailID).FirstOrDefault();

            _context.Cocktails.Remove(selectedCocktail);
            _context.SaveChanges();

            return RedirectToAction("Cocktails");
        }
    }
}
