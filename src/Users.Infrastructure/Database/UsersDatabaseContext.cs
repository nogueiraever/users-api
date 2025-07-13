using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Users.Infrastructure.Database
{
    public class UsersDatabaseContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
