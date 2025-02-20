using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Reflection;

namespace Infra.Data.Core
{
    public partial class SqlServerDbContext : DbContext
    {

        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            OnModelCreatingPartial(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {                
                optionsBuilder.LogTo(message => Debug.WriteLine(message), LogLevel.Debug, DbContextLoggerOptions.UtcTime | DbContextLoggerOptions.SingleLine);
            }
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}