using Bjss.PriceBasket.Core.Models;
using Bjss.PriceBasket.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Bjss.PriceBasket.Infrastructure.DataAccess.Contexts
{
    public static class PriceBasketContextDataSeeder
    {
        public static IApplicationBuilder SeedDatabase(this IApplicationBuilder builder)
        {
            using (var serviceScope = builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<PriceBasketContext>();

                context.Database.Migrate();

                var soupId = Guid.NewGuid();
                var breadId = Guid.NewGuid();
                var milkId = Guid.NewGuid();
                var appleId = Guid.NewGuid();

                if (context.Products.Any() == false)
                {
                    context.Products.AddRange(new Product[] {
                           new Product{Id = soupId, Name = "Soup", Price = 0.65m, QuantityAvailable = 100 },
                           new Product{ Id  = breadId, Name = "Bread", Price = 0.80m, QuantityAvailable = 100 },
                           new Product{ Id = milkId, Name = "Milk", Price = 1.30m, QuantityAvailable = 100 },
                           new Product{ Id = appleId, Name= "Apples", Price = 1.00m, QuantityAvailable = 100 }
                    });
                
                    var tenPercentOfApplesId = Guid.NewGuid();
                    var buy2Get1HalfPriceId = Guid.NewGuid();

                    context.SpecialOffers.AddRange(new SpecialOffer[]
                    {
                        new SpecialOffer
                        {
                            Id = tenPercentOfApplesId,
                            Name = "Apples10PercentOff",
                            Description = "Apples 10% off",
                            DiscountAssemblyFullName = typeof(ReduceProductPriceByPercentDiscounts).AssemblyQualifiedName
                        },
                        new SpecialOffer
                        {
                            Id  = buy2Get1HalfPriceId,
                            Name = "BuyTwoSoupGetOneLoafHalfBrpice",
                            Description = "Buy 2 tins of soup and get a loaf of bread for half price",
                            DiscountAssemblyFullName = typeof(BuyTwoGetOneHalfPriceDiscounts).AssemblyQualifiedName
                        }
                    });

                    context.BuyTwoGetOneHalfPriceSpecialOffers.Add(
                    new BuyTwoGetOneHalfPriceSpecialOffer
                    {
                        Id = Guid.NewGuid(),
                        SpecialOfferId = buy2Get1HalfPriceId,
                        BuyTwoOfProductId = soupId,
                        DiscountedProductId = breadId,
                        DiscountPercent = 50
                    });

                    context.DiscountedProductsSpecialOffers.Add(new DiscountedProductsSpecialOffer
                    {
                        Id = Guid.NewGuid(),
                        DiscountedProductId = appleId,
                        SpecialOfferId = tenPercentOfApplesId,
                        DiscountPercent = 10
                    });
                }

                context.SaveChanges();
            }

            return builder;
        }
    }
}
