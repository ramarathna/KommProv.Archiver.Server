using KommProv.Archiver.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KommProv.Archiver.Server.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Rule> Rules { get; set; }
        public DbSet<RuleArchive> Rule_Archive { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rule>().ToTable("Rule");
            modelBuilder.Entity<RuleArchive>().ToTable("Rule_Archive");
        }
    }
}
