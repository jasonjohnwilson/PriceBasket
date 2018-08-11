using Bjss.PriceBasket.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Bjss.PriceBasket
{
    /// <summary>
    /// Main entry point for the console application
    /// </summary>
    class Program
    {
        private static IBasketApplication _basketApplication;

        static void Main(string[] args)
        {
            SetCulture();

            var host = new WebHostBuilder()
           .UseContentRoot(Directory.GetCurrentDirectory())
           .UseStartup<Startup>()
           .UseServer(new NoopServer())
           .Build();

            _basketApplication = host.Services.GetRequiredService<IBasketApplication>();

            MainAsync(args).Wait();
        }

        private async static Task MainAsync(string[] args)
        {
            try
            {
                await _basketApplication.ExecuteAsync(args);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(FormatErrorMessage(e));
            }

            System.Console.ReadKey();
        }
        
        private static string FormatErrorMessage(Exception e)
        {
            return $"Error occured while processing basket [Message: {e.Message}, StackTrace: {e.StackTrace}]";
        }

        private static void SetCulture()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-GB");
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-GB");
        }
    }
}
