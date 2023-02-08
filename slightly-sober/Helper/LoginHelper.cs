using slightly_sober.Data;
using slightly_sober.Models;

namespace slightly_sober.Helper
{
    public class LoginHelper
    {

        private readonly SlightlySoberContext _context;

        public LoginHelper(SlightlySoberContext context)
        {
            _context = context;
        }

/*        public static User FetchLoginObject(string userName)
        {

        }
*/
        public static bool IsAccountLocked(string loginID)
        {
            return false;
        }

        public static bool VerifyLoginCreds(string userName, string password)
        {
            return false;
        }

/*        public static bool IsLoggedIn()
        {
            var result = HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).HasValue;
        }*/
    }
}
