using Microsoft.EntityFrameworkCore;
using slightly_sober.Models;

namespace slightly_sober.Data
{
    public class SlightlySoberContext : DbContext
    {
        public SlightlySoberContext(DbContextOptions<SlightlySoberContext> options) : base(options) { }

        // Properties - Tables
        public DbSet<User> Users { get; set; }
        public DbSet<Login> Logins { get; set; }
    }

}
