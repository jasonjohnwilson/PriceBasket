using Bjss.PriceBasket.Infrastructure.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace Eps.IdentityServer.Infrastructure.Database.Design
{
    public class PriceBasketContextDesignTimeConfiguration : IDesignTimeDbContextFactory<PriceBasketContext>
	{
		public PriceBasketContext CreateDbContext(string[] args)
		{
			var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENV");

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", true);

			var configuration = configurationBuilder.Build();

			var dbContextOptionsBuilder = new DbContextOptionsBuilder<PriceBasketContext>();
			
			dbContextOptionsBuilder.UseSqlServer(configuration["ConnectionStrings:PriceBasketDb"],
				options => 
				options.MigrationsAssembly(Assembly.GetAssembly(typeof(PriceBasketContextDesignTimeConfiguration)).GetName().Name));
			
			var context = new PriceBasketContext(dbContextOptionsBuilder.Options);
			
			return context;
		}
	}
}
