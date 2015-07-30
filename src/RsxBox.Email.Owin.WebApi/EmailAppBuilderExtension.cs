using System.Linq;
using Microsoft.AspNet.Builder;
using RsxBox.Email.Owin.WebApi.Configs;
using RsxBox.Email.Core.Interface;
using RsxBox.Email.Core.InMemory;
using RsxBox.Email.Core.Models;
using System.Net.Mail;
using Microsoft.Framework.DependencyInjection;

namespace RsxBox.Email.owin.WebApi
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public static class EmailAppBuiilderExtension
    {
        public static void UseRsxBoxEmailWebApiApp(this IApplicationBuilder app, RsxBoxEmailWebApiOption options)
        {
            var path = options.Path;

            path = string.IsNullOrEmpty(path) ? "api/email" : path;

            path = path.First().Equals('/') ? path.Substring(1, path.Length - 1) : path;
            path = path.Last().Equals('/') ? path : path + '/';
            var templatePath = path + "{action}";
            var defaultTemplate = "{controller}/{action}";
            //app.UseMvc(r => r.MapRoute(
            //    name: "emailcontroller",
            //    template: templatePath,
            //    defaults: new { controller = "RsxBoxEmail", action = "Index" },
            //    constraints: new object(),
            //    dataTokens: new { namespaces = new[] { "RsxBox.Email.Owin.WebApi.Controllers" } }));

            app.UseMvc();
        }

        public static void AddRsxBoxEmailWebApiAppService(this IServiceCollection services)
        {
            services.AddTransient<IEmailManager<EmailTemplate, int>, InMemoryEmailManager>();
            services.AddTransient<IEmailTemplateManager<EmailTemplate, int>, InMemoryEmailTemplateManager<EmailTemplate>>();
            services.AddTransient<IEmailSender<EmailTemplate>, InMemoryEmailSender<EmailTemplate>>();
            services.AddTransient<SmtpClient>();
        }
    }
}
