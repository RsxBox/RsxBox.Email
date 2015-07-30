using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using RsxBox.Email.owin.WebApi;

namespace RsxBox.Email.DevWebHost
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
            
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddRsxBoxEmailWebApiAppService();
        }
    }
}
