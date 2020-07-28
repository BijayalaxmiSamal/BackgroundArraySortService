using BackgroundHostedService.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundHostedService.Infrastructure
{
    public class BGHSDbContext:DbContext,IBGHSDbContext
    {
        public BGHSDbContext(DbContextOptions options) : base(options) { }
        public BGHSDbContext() { }
        public DbSet<BackgroundJobs> BackgroundJobData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("BGHS_DB");
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();

            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var modified in modifiedEntries)
            {
                modified.Property("LastUpdated").CurrentValue = DateTime.UtcNow;
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<BackgroundJobs>> GetCurrentBackgroundJobs() {
            return BackgroundJobData.ToList();
        }
      
    }
}
