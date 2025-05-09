using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace EMS.Blazor
{
    public class Program
    {
        public async static Task<int> Main(string[] args)
        {
            var DefaultCorsPolicyName = "Default";
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.File("Logs/logs.txt"))
                .WriteTo.Async(c => c.Console())
                .CreateLogger();

            try
            {
                Log.Information("Starting web host.");
                var builder = WebApplication.CreateBuilder(args);
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("Default", policy =>
                    {
                        policy
                            .WithOrigins(
                                "https://ems-fe-9eda0.web.app",
                                "http://localhost:4200",      // for Angular dev server
                                "https://localhost:4200"      // if testing over HTTPS
                            )
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials(); // needed for cookies or Authorization headers
                    });
                });


                // Add AppSettings and Autofac configuration
                builder.Host.AddAppSettingsSecretsJson()
                    .UseAutofac() // Set up Autofac for DI
                    .UseSerilog(); // Use Serilog for logging

                // Add Application for ABP module initialization
                await builder.AddApplicationAsync<EMSBlazorModule>();

                var app = builder.Build();

                // Initialize and run the application
                await app.InitializeApplicationAsync();
                await app.RunAsync();

                return 0;
            }
            catch (Exception ex)
            {
                if (ex is HostAbortedException)
                {
                    throw;
                }

                Log.Fatal(ex, "Host terminated unexpectedly!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
