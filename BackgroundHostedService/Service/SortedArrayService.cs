using BackgroundHostedService.Infrastructure;
using BackgroundHostedService.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BackgroundHostedService.Service
{
    public class SortedArrayService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly ILogger _iLogger;
        public SortedArrayService(IServiceScopeFactory serviceScopeFactory, ILogger<SortedArrayService> iLogger)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            _iLogger = iLogger;

        }

        public async Task ExecuteSortArray(int[] inputArr)
        {
            _iLogger.LogInformation("New Array is added for sorting!!", inputArr);
            using (var scope = serviceScopeFactory.CreateScope())
            {
                BackgroundJobs backgroundJob = new BackgroundJobs();
                var dbContext = scope.ServiceProvider.GetService<BGHSDbContext>();
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                //sort the array from backgroundJob reference
                GetArraySorted(inputArr);

                stopWatch.Stop();
                long duration = stopWatch.ElapsedMilliseconds;
                backgroundJob.JobDuration = duration;
                backgroundJob.Status = JobConstants.Status_Completed;
                dbContext.BackgroundJobData.Add(backgroundJob);
                await dbContext.SaveChangesAsync();

                _iLogger.LogInformation("Added Array Sorting id completed",backgroundJob);
            }

        }
    
        private void GetArraySorted(int[] inputArr)
        {
            Array.Sort(inputArr);
            _iLogger.LogInformation("sorted array is ", inputArr);
        }
    }
    
}
