using BackgroundHostedService.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundHostedService.Infrastructure
{
    public interface IBGHSDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
        Task<IList<BackgroundJobs>> GetCurrentBackgroundJobs();
    }
}