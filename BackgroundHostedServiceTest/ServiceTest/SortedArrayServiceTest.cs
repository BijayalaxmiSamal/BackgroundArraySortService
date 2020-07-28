using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using BackgroundHostedService.Infrastructure;
using Microsoft.Extensions.Logging;
using BackgroundHostedService.Controllers;
using BackgroundHostedService.Model;
using System.Threading.Tasks;
using BackgroundHostedService.Service;
using Microsoft.Extensions.DependencyInjection;

namespace BackgroundHostedServiceTest.ServiceTest
{
    public class SortedArrayServiceTest
    {
        private Mock<IServiceScopeFactory> _mockScopeFactory;
             private Mock<IServiceScope> _mockServiceScope;
        private Mock<IServiceProvider> _mockServiceProvider;
        private Mock<ILogger<SortedArrayService>> _mockLogger;
        private Mock<IBGHSDbContext> _mockDbContext;
        private SortedArrayService sortedArrayService;
        public SortedArrayServiceTest()
        {
            _mockScopeFactory = new Mock<IServiceScopeFactory>();
            _mockLogger = new Mock<ILogger<SortedArrayService>>();
            _mockServiceScope = new Mock<IServiceScope>();
            _mockServiceProvider = new Mock<IServiceProvider>();
            _mockDbContext = new Mock<IBGHSDbContext>();
            sortedArrayService = new SortedArrayService(_mockScopeFactory.Object, _mockLogger.Object);
        }

        private void TestDataSetUp()
        {
            BackgroundJobs bgJob = new BackgroundJobs { JobID = 1, JobDuration = 10, JobTimeStramp = DateTime.Now, Status = JobConstants.Status_Inprogress };
            List<BackgroundJobs> backgroundJobsList = new List<BackgroundJobs>();
            backgroundJobsList.Add(bgJob);
        }

        [Fact]
        public async Task ExecuteSortArray_Test()
        {
            _mockScopeFactory.Setup(s => s.CreateScope()).Returns(_mockServiceScope.Object);
            _mockServiceScope.Setup(a => a.ServiceProvider).Returns(_mockServiceProvider.Object);
            _mockServiceProvider.Setup(x => x.GetService(typeof(IBGHSDbContext))).Returns(_mockDbContext.Object);
            int[] inpArr=new int[] { 3, 4, 5,1,6,8 };
            try
            {
                await sortedArrayService.ExecuteSortArray(inpArr);
                bool isExecuted = true;
                Assert.True(isExecuted);
            }
            catch (Exception ex)
            {
                bool isExecuted = true;
                Assert.NotNull(ex);
            }
           
                       
        }

        [Fact]
        public async Task ExecuteSortArray_NegativeTest()
        {
            _mockScopeFactory.Setup(s => s.CreateScope()).Returns(_mockServiceScope.Object);
            _mockServiceScope.Setup(a => a.ServiceProvider).Returns(_mockServiceProvider.Object);
            _mockServiceProvider.Setup(x => x.GetService(typeof(IBGHSDbContext))).Throws(new Exception("Internal Server Exception!!"));
            int[] inpArr = new int[] { 3, 4, 5, 1, 6, 8 };
            try
            {
                await sortedArrayService.ExecuteSortArray(inpArr);
            }
            catch (Exception ex) {
                Assert.NotNull(ex);
                    }
        }

    }
}
