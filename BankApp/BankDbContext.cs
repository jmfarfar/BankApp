using BankApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankApp
{
    public class BankDbContext:DbContext
    {
        public BankDbContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
