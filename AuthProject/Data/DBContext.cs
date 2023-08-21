using Microsoft.EntityFrameworkCore;
using AuthProject.Models;

namespace AuthProject.Data
{
    public class DBContext : DbContext
    {

        public DbSet<User> Users { get; set; }

        public DBContext(DbContextOptions<DBContext> options) 
        {

        }
    }
}
