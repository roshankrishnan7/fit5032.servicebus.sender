using fit5032.order_sender.ConfigOptions;
using fit5032.order_sender.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fit5032.order_sender
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
            services.AddControllersWithViews();

            // Read ServiceBus section from appsettings and binds to ServiceBusConfig model
            // This helps us to easily group related configuration, which makes it easier for a developer to understand
            var serviceBusConfig = Configuration.GetSection("ServiceBus");
            services.Configure<ServiceBusConfig>(serviceBusConfig);

            var serviceBusConnectionString = serviceBusConfig.Get<ServiceBusConfig>().ConnectionString;

            // Registers ServiceBusClient using the provided connection string
            // Once ServiceBusClient is registered, we can use Dependency Injection to use instance of ServiceBusClient
            // in other services/controllers
            services.AddAzureClients(builder =>
            {
                builder.AddServiceBusClient(serviceBusConnectionString);
            });

            // Registers the IServiceBusSenderService that we created, with the implementation 
            // specified in ServiceBusSenderService
            services.AddTransient<IServiceBusSenderService, ServiceBusSenderService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
