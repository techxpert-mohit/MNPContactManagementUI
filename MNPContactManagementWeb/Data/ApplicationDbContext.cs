using Microsoft.EntityFrameworkCore;
using MNPContactManagementWeb.Models;

namespace MNPContactManagementWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ContactDetails> ContactDetails { get; set; }
    }
}
