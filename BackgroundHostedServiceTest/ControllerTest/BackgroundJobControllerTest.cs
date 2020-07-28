using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using BackgroundHostedService.Infrastructure;
using Microsoft.Extensions.Logging;
using BackgroundHostedService.Controllers;
using BackgroundHostedService.Infrastructure;
using BackgroundHostedService.Model;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BackgroundHostedServiceTest.ControllerTest
{
    public class BackgroundJobControllerTest
    {
        private Mock<IBGHSDbContext> _mockbghsDbContext;
        private Mock<ILogger<BackgroundJobController>> _mockLogger;
        private BackgroundJobController backgroundJobController;
        public BackgroundJobControllerTest() {
            _mockLogger = new Mock<ILogger<BackgroundJobController>>();
            _mockbghsDbContext = new Mock<IBGHSDbContext>();
            backgroundJobController = new BackgroundJobController(_mockLogger.Object, _mockbghsDbContext.Object);
        }

        private void TestDataSetUp() {
            BackgroundJobs bgJob = new BackgroundJobs { JobID=1,JobDuration=10,JobTimeStramp=DateTime.Now,Status=JobConstants.Status_Inprogress};
            List<BackgroundJobs> backgroundJobsList = new List<BackgroundJobs>();
            backgroundJobsList.Add(bgJob);
        }

        [Fact]
        public async Task Get_Test()
        {
            BackgroundJobs bgJob = new BackgroundJobs { JobID = 1, JobDuration = 10, JobTimeStramp = DateTime.Now, Status = JobConstants.Status_Inprogress };
            List<BackgroundJobs> backgroundJobsList = new List<BackgroundJobs>();
            backgroundJobsList.Add(bgJob);
            _mockbghsDbContext.Setup(a => a.GetCurrentBackgroundJobs()).ReturnsAsync(backgroundJobsList);
            var res=(await backgroundJobController.Get()) as OkObjectResult;
            Assert.Equal(backgroundJobsList, res.Value);
        }

        [Fact]
        public async Task GetById_Test()
        {
            BackgroundJobs bgJob = new BackgroundJobs { JobID = 1, JobDuration = 10, JobTimeStramp = DateTime.Now, Status = JobConstants.Status_Inprogress };
            List<BackgroundJobs> backgroundJobsList = new List<BackgroundJobs>();
            backgroundJobsList.Add(bgJob);
            _mockbghsDbContext.Setup(a => a.GetCurrentBackgroundJobs()).ReturnsAsync(backgroundJobsList);
            var res = (await backgroundJobController.GetById(1)) as OkObjectResult;
            Assert.Equal(bgJob,res.Value);
        }

            [Fact]
            public async Task GetById_NegativeTest()
            {
                BackgroundJobs bgJob = new BackgroundJobs { JobID = 1, JobDuration = 10, JobTimeStramp = DateTime.Now, Status = JobConstants.Status_Inprogress };
                List<BackgroundJobs> backgroundJobsList = new List<BackgroundJobs>();
                backgroundJobsList.Add(bgJob);
                _mockbghsDbContext.Setup(a => a.GetCurrentBackgroundJobs()).Throws(new Exception("Internal Server Exception!!"));
            try
            {
                var res = await backgroundJobController.GetById(1);
            }
            catch (Exception ex) {
                Assert.NotNull(ex);
            }
            }


        }
}
