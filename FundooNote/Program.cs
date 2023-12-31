using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNote
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logPath = Path.Combine(Directory.GetCurrentDirectory(),"Logs");
            NLog.GlobalDiagnosticsContext.Set("FundooLogs", logPath);
            var logger=NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            logger.Info("Application started.......");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging(options =>
                {
                    options.ClearProviders();
                    options.SetMinimumLevel(LogLevel.Trace);
                }).UseNLog();
    }
}
