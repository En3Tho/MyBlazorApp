﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace MyBlazorApp.WebHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                 {
                     logging.ClearProviders();
                     logging.AddConsole(options =>
                         options.IncludeScopes = true);

                     logging.Configure(options =>
                     {
                         options.ActivityTrackingOptions = ActivityTrackingOptions.ParentId
                                                         | ActivityTrackingOptions.SpanId
                                                         | ActivityTrackingOptions.TraceId;
                     });
                 })
                .ConfigureWebHostDefaults(webBuilder =>
                 {
                     webBuilder.UseStartup<Startup>();
                 });
    }
}