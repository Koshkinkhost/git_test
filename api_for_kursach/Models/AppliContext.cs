using Microsoft.EntityFrameworkCore;
namespace api_for_kursach.Models
{
    public class AppliContext(DbContextOptions<AppliContext> options):DbContext(options)
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Roles> Roles { get; set; }
    }
}
