using System;
using System.Collections.Generic;

namespace beats.Models
{
    public class HealthCheckConfig
    {
        public List<HealthCheckSqlServer> SqlServer { get; set; } 

        public List<HealthCheckRedis> Redis { get; set; } 

        public List<HealthCheckHttp> Http { get; set; } 

        public HealthCheckSignalR SignalR { get; set; } 
    }

    public class HealthCheckSqlServer
    {
        public string ConnectionStrings { get; set; }  

        public string Name { get; set; } 

    }
    
    public class HealthCheckRedis
    {
        public string ConnectionStrings { get; set; }  

        public string Name { get; set; } 

    }

    public class HealthCheckHttp
    {
        public string Url { get; set; }  

        public Uri Uri 
        { 
            get
            {
                return new Uri(Url);
            } 
        }  

        public string Name { get; set; } 

    }

    public class HealthCheckSignalR
    {
        public string Url { get; set; }  

        public string Name { get; set; } 

    }
}