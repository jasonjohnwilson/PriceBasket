using Bjss.PriceBasket.Application.Interfaces;
using Bjss.PriceBasket.Application.Services;
using Bjss.PriceBasket.Console.View;
using Bjss.PriceBasket.Core.Interfaces;
using Bjss.PriceBasket.Core.Models;
using Bjss.PriceBasket.Core.Services;
using Bjss.PriceBasket.Infrastructure.DataAccess.Contexts;
using Bjss.PriceBasket.Infrastructure.DataAccess.Repositories;
using Bjss.PriceBasket.Interfaces;
using Bjss.PriceBasket.View;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Reflection;

namespace Bjss.PriceBasket
{
    /// <summary>
    /// Bootsraps the aplications IOC and configuration
    /// </summary>
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            Configuration = builder.Build();
            Environment = env;
        }

        public IConfigurationRoot Configuration { get; private set; }
        public IHostingEnvironment Environment { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IBasketApplication, BasketApplication>();
            services.AddSingleton<ISalesProcessingService, SalesProcessingService>();
            services.AddSingleton<ISalesResultMessageFormatter, SalesDisplayMessageFormatter>();
            services.AddSingleton<IValidationErrorsMessageFormatter, ValidationErrorMessageFormatter>();
            services.AddSingleton<IView, PriceBasketView>();
            services.AddSingleton<IConsole, PriceBasketConsole>();
            services.AddSingleton<ILoadingSpinner, LoadingSpinner>();
            services.AddSingleton<IBasketDiscountProcessor, BasketDiscountProcessor>();
            services.AddSingleton<IBasketDiscountProviderFactory, BasketDiscountProviderFactory>();
            services.AddSingleton<IGenericRepository<SpecialOffer>, GenericRepository<SpecialOffer>>();
            services.AddSingleton<IGenericRepository<Product>, GenericRepository<Product>>();
            services.AddSingleton<IGenericRepository<BuyTwoGetOneHalfPriceSpecialOffer>, GenericRepository<BuyTwoGetOneHalfPriceSpecialOffer>>();
            services.AddSingleton<IGenericRepository<DiscountedProductsSpecialOffer>, GenericRepository<DiscountedProductsSpecialOffer>>();
            services.AddSingleton<IFactory, SimpleFactory>();
            services.AddSingleton<IValidatorsProvider, ValidatorsProvider>();
            services.AddSingleton<ISaleCalculator, SaleCalculator>();
            services.AddSingleton<IBasketValidator, BasketValidationService>();
            services.AddSingleton<IBasketFactory, BasketFactory>();
            services.AddDbContext<PriceBasketContext>(
                options => options.UseSqlServer(Configuration["ConnectionStrings:PriceBasketDb"], 
                action => action.MigrationsAssembly(Assembly.GetAssembly(typeof(PriceBasketContext)).GetName().Name)));
        }

        public void Configure(IApplicationBuilder builder)
        {
            if(Environment.IsDevelopment())
            {
                builder.SeedDatabase();
            }
        }
    }
}
