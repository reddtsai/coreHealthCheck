using beats.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace beats
{
    public static class HealthChecksRegister
    {
        public static IServiceCollection RegisterHealthChecks(this IServiceCollection services, IConfiguration configuration) 
        {
            var hc = services.AddHealthChecks();
            
            var healthCheckConfig= configuration.GetSection("HealthCheck").Get<HealthCheckConfig>();  

            // HealthChecks Uri
            foreach (var http in healthCheckConfig.Http)
            {
                hc.AddUrlGroup(http.Uri, http.Name, HealthStatus.Degraded);
            }
            
            // HealthChecks SqlServer  
            foreach(var sql in healthCheckConfig.SqlServer)
            {
                // Console.WriteLine(sql.ConnectionStrings);
                hc.AddSqlServer(connectionString: sql.ConnectionStrings, name: sql.Name);
            }

            // HealthChecks Redis
            foreach (var redis in healthCheckConfig.Redis)
            {
                hc.AddRedis(redis.ConnectionStrings, name: redis.Name);
            }

            if(healthCheckConfig.SignalR != null && string.IsNullOrEmpty(healthCheckConfig.SignalR.Url))
            {
                hc.AddSignalRHub(healthCheckConfig.SignalR.Url, name: healthCheckConfig.SignalR.Name);
            }
            
            
            return services;
        }
    }
}