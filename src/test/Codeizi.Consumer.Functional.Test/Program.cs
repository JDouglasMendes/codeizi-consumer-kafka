using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;

namespace Codeizi.Consumer.Functional.Test
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                      .MinimumLevel.Debug()
                      .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                      .Enrich.FromLogContext()
                      .WriteTo.Console()
                      .CreateLogger();

            try
            {
                var hostBuilder = CreateHostBuilder(args).Build();
                Log.Information("Iniciando Web Host");
                hostBuilder.Run();
                return;
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host encerrado inesperadamente");
                return;
            }
#pragma warning restore CA1031 // Do not catch general exception types
            finally
            {
               Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
                 .UseSerilog()
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                     webBuilder.UseStartup<Startup>();
                 });
    }
}