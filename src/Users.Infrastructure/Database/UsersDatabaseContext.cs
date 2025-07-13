using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Users.Domain.Entities;

namespace Users.Infrastructure.Database
{
    public class UsersDatabaseContext : DbContext
    {
        public UsersDatabaseContext(DbContextOptions<UsersDatabaseContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
    }
}
