using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Z.Dapper.Plus;
using Microsoft.AspNetCore.Builder;
using ProtoType.Core.Entities;

namespace ProtoType.Api
{
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(new RenderedCompactJsonFormatter(), "logs/log.json", rollingInterval: RollingInterval.Hour)
            .CreateLogger();

            try
            {
                Log.Information("Starting up");
                SetupDapper();
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        static void SetupDapper()
        {
            DapperPlusManager.Entity<Fixture>().Table("dbo.Fixture").Identity(x => x.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration((ctx, configBuilder) =>
                {
                    IHostEnvironment env = ctx.HostingEnvironment;

                    configBuilder.AddEnvironmentVariables();

                    if (env.IsDevelopment())
                    {
                        configBuilder.AddUserSecrets<Program>();
                    }
                });
    }
}