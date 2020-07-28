using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire.Dashboard;

namespace BackgroundHostedService.Infrastructure.Authorization
{
    public class DashboardAuthentication : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return true;

        }
    }
}
