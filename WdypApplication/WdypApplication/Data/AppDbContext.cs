using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WdypApplication.Models;

namespace WdypApplication.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)

        {
        }
        public DbSet<WdypApplication.Models.User> User { get; set; } = default!;

        public DbSet<User> Users { get; set; }
    }
}
