using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace beats
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterHealthChecks(_config);
                  
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHealthChecks("/liveness", new HealthCheckOptions
            {
                Predicate = _ => true
            });

            app.UseHealthChecks("/hc", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }

            // app.Run(async (context) =>
            // {
            //     await context.Response.WriteAsync("Hello World!");
            // });
        }
    }
}
