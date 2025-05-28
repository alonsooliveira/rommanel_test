using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Rommanel.Test.API.Infra.EntityFramework
{
    public class RommanelContext : DbContext
    {
        public RommanelContext(DbContextOptions<RommanelContext> options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
