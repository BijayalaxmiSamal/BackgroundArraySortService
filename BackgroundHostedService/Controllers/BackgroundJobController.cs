using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackgroundHostedService.Infrastructure;
using BackgroundHostedService.Model;
using BackgroundHostedService.Service;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BackgroundHostedService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BackgroundJobController : ControllerBase
    {
        private readonly IBGHSDbContext _bghsDbContext;

        private readonly ILogger<BackgroundJobController> _logger;

        public BackgroundJobController(ILogger<BackgroundJobController> logger, IBGHSDbContext bghsDbContext)
        {
            _logger = logger;
            _bghsDbContext = bghsDbContext;
        }

        [HttpGet]
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            var BgJobData = await _bghsDbContext.GetCurrentBackgroundJobs();
            return Ok(BgJobData);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(long JobID)
        {
            var allBgJobsData = await _bghsDbContext.GetCurrentBackgroundJobs();
            var bgJobSingleData = allBgJobsData.Where(s => s.JobID == JobID).FirstOrDefault();
            return Ok(bgJobSingleData);
        }

        [HttpPost]
        [HttpGet("AddArrayToSort")]
        public IActionResult Post(int[] inputArr)
        {            
            RecurringJob.AddOrUpdate<SortedArrayService>(w => w.ExecuteSortArray(inputArr), Cron.Never);           
            return Ok("Job Enqueued!!");
        }
    }
}
