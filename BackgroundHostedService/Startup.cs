using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackgroundHostedService.Infrastructure;
using BackgroundHostedService.Infrastructure.Authorization;
using BackgroundHostedService.Service;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BackgroundHostedService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IBGHSDbContext, BGHSDbContext>();
            services.AddDbContext<BGHSDbContext>();
            
            //services.AddSingleton<IHostedService, SortedArrayService>();

            services.AddHangfire(config =>
            {
                config.UseMemoryStorage();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHangfireServer();
            app.UseHangfireDashboard(options: new DashboardOptions()
            {
                Authorization = new[] { new DashboardAuthentication() }
            });

           
            
        }
    }
}
