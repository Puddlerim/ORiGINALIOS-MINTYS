
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZaidimuPortalasMVC.Models;

namespace ZaidimuPortalasMVC.Data
{
    public class ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options
    ) : IdentityDbContext(options)
    {
        public DbSet<PersonalGameEntry> PersonalGameEntries
            => Set<PersonalGameEntry>();
    }
}
