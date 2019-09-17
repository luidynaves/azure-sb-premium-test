using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using luidy_bus_test.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBusMessaging;

namespace luidy_bus_test
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            var serviceBusConnectionString = Configuration.GetConnectionString("ServiceBusConnectionString");

            services.AddScoped<ServiceBusTopicSender>();

            services.AddSingleton<TopicClient>(x => {
                return new TopicClient(
                    serviceBusConnectionString,
                    "transactiontest"
                );
            });

            services.AddSingleton<ServiceBusTopic>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseMvc();            
        }
    }
}
