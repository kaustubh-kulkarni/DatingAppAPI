using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{

    public class DataContext : DbContext
    {
        // Constructor
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        // DbSet needs the entity that we are using in this case AppUser and the class Users
        public DbSet<AppUser> Users { get; set; }
    }
}