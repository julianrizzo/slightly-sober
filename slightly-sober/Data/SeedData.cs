using SimpleHashing.Net;
using slightly_sober.Data;
using slightly_sober.Models;

namespace slightly_sober.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<SlightlySoberContext>();

            // Checks if DB is already populated with User data
            if (!context.Users.Any())
            {

                var pw = new SimpleHash().Compute("admin");
                User defaultUser = new("admin", "Julian", "Rizzo", "julian.rizzo.17@hotmail.com", true, new Login(pw));

                // Add and save
                context.Users.AddRange(defaultUser);
                context.SaveChanges();
                Cocktail demoCocktail = new("Margarita", "A little mess of fun.", "Coupe", "Tequila", "Some other stuff.", defaultUser.UserID);
                context.Cocktails.AddRange(demoCocktail);
                context.SaveChanges();
            }
        }

    }
}
