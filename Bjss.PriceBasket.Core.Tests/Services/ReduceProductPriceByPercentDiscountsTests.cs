using Bjss.PriceBasket.Core.Interfaces;
using Bjss.PriceBasket.Core.Models;
using Bjss.PriceBasket.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bjss.PriceBasket.Core.Tests.Services
{
    [TestClass]
    public class ReduceProductPriceByPercentDiscountsTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetDiscountsAsync_WithNullBasket_ShouldThrowException()
        {
            //arrange
            var reduceProductPriceByPercentDiscounts = new ReduceProductPriceByPercentDiscounts(null);

            //act
            await reduceProductPriceByPercentDiscounts.GetDiscountsAsync(null);

            //assert
        }

        [TestMethod]
        public async Task GetDiscountsAsync_WithProductsMeetingRequirement_ShouldReturnCorrectDiscount()
        {
            //arrange
            var reducedProductId = Guid.NewGuid();
            var reducedProductPrice = 1.00m;
            var discountPercent = 10;

            var mockSpecialOfferRepository = new Mock<IGenericRepository<SpecialOffer>>();
            mockSpecialOfferRepository.Setup(m => m.GetAllAsync(It.IsAny<Expression<Func<SpecialOffer, DiscountedProductsSpecialOffer>>>()))
                .ReturnsAsync(
                    new List<SpecialOffer> {
                        new SpecialOffer
                        {
                            Name ="SpecialOffer",
                            Description = "10 % off",
                            DiscountAssemblyFullName = "Bjss.PriceBasket.Core.Services.ReduceProductPriceByPercentDiscounts, Bjss.PriceBasket.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                              DiscountedProductsSpecialOffer = new DiscountedProductsSpecialOffer
                             {
                                   DiscountedProductId = reducedProductId,
                                  DiscountPercent = discountPercent,
                                  SpecialOffer = new SpecialOffer{ Name ="SpecialOffer",
                                  Description = "10 % off"}

                            }
                        }
                    }
                );

            var basket = new Basket
            {
                Items = new List<BasketItem>
                {
                 new BasketItem{ Id = reducedProductId, Name = "Apples", Price = reducedProductPrice},
                 new BasketItem{ Id = reducedProductId, Name = "Apples", Price = reducedProductPrice},
                }
            };

            var reduceProductPriceByPercentDiscounts = new ReduceProductPriceByPercentDiscounts(mockSpecialOfferRepository.Object);

            //act
            var discounts = await reduceProductPriceByPercentDiscounts.GetDiscountsAsync(basket);

            //assert
            Assert.AreEqual(2, discounts.Count());
            Assert.AreEqual(discounts.Sum(d=>d.DiscountAmount), 0.20m);
        }

        [TestMethod]
        public async Task GetDiscountsAsync_WithSpecialOffers_ShouldReturnNoDiscounts()
        {
            //arrange
            var reducedProductId = Guid.NewGuid();
            var reducedProductPrice = 1.00m;

            var mockSpecialOfferRepository = new Mock<IGenericRepository<SpecialOffer>>();
            mockSpecialOfferRepository.Setup(m => m.GetAllAsync(It.IsAny<Expression<Func<SpecialOffer, DiscountedProductsSpecialOffer>>>()))
                .ReturnsAsync(
                    new List<SpecialOffer> ()
                );

            var basket = new Basket
            {
                Items = new List<BasketItem>
                {
                 new BasketItem{ Id = reducedProductId, Name = "Apples", Price = reducedProductPrice},
                 new BasketItem{ Id = reducedProductId, Name = "Apples", Price = reducedProductPrice},
                }
            };

            var reduceProductPriceByPercentDiscounts = new ReduceProductPriceByPercentDiscounts(mockSpecialOfferRepository.Object);

            //act
            var discounts = await reduceProductPriceByPercentDiscounts.GetDiscountsAsync(basket);

            //assert
            Assert.AreEqual(0, discounts.Count());
        }
    }
}
