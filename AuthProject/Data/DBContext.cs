using Microsoft.EntityFrameworkCore;
using AuthProject.Models;

namespace AuthProject.Data
{
    public class DBContext : DbContext
    {

        public DbSet<User> Users { get; set; }

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    userName = "admin@user.com",
                    password = "password1234",
                    role = "admin",
                    id = 1
                },
                new User
                {
                    userName = "regular@user.com",
                    password = "password1234",
                    role = "regular",
                    id = 2
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
