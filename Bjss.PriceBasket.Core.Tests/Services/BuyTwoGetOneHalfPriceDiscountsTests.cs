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
    public class BuyTwoGetOneHalfPriceDiscountsTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetDiscountsAsync_WithNullBasket_ShouldThrowException()
        {
            //arrange
            var buyTwoGetOneHalfPriceDiscounts = new BuyTwoGetOneHalfPriceDiscounts(null);

            //act
            await buyTwoGetOneHalfPriceDiscounts.GetDiscountsAsync(null);
            //assert
        }

        [TestMethod]
        public async Task GetDiscountsAsync_WithProductsMeetingRequirement_ShouldReturnCorrectDiscount()
        {
            //arrange
            var buy2ProductId = Guid.NewGuid();
            var buy2ProductPrice = 0.65m;
            var discountProductId = Guid.NewGuid();
            var discountProductPrice = 0.80m;
            var discountPercent = 50m;

            var mockSpecialOfferRepository = new Mock<IGenericRepository<SpecialOffer>>();
            mockSpecialOfferRepository.Setup(m => m.GetAllAsync(It.IsAny<Expression<Func<SpecialOffer, BuyTwoGetOneHalfPriceSpecialOffer>>>()))
                .ReturnsAsync(
                    new List<SpecialOffer> {
                        new SpecialOffer
                        {
                            Name ="SpecialOffer",
                            Description = "Buy 2 Get One free",
                            DiscountAssemblyFullName = "Bjss.PriceBasket.Core.Services.BuyTwoGetOneHalfPriceDiscounts, Bjss.PriceBasket.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                             BuyTwoGetOneHalfPriceSpecialOffer = new BuyTwoGetOneHalfPriceSpecialOffer
                             {
                                  BuyTwoOfProductId = buy2ProductId,
                                  DiscountedProductId = discountProductId,
                                  DiscountPercent = discountPercent,
                                  SpecialOffer = new SpecialOffer{ Name ="SpecialOffer",
                                  Description = "Buy 2 Get One free"}

                            }
                        }
                    }
                );

            var basket = new Basket
            {
                Items = new List<BasketItem>
                {
                 new BasketItem{ Id = buy2ProductId, Name = "Soup", Price = buy2ProductPrice},
                 new BasketItem{  Id = buy2ProductId, Name = "Soup", Price =  buy2ProductPrice },
                 new BasketItem{ Id = discountProductId,  Name = "Bread", Price = discountProductPrice },
                }
            };

            var buyTwoGetOneHalfPriceDiscounts = new BuyTwoGetOneHalfPriceDiscounts(mockSpecialOfferRepository.Object);

            //act
            var discounts = await buyTwoGetOneHalfPriceDiscounts.GetDiscountsAsync(basket);

            //assert
            Assert.AreEqual(1, discounts.Count());
            Assert.AreEqual(discounts.Single().DiscountAmount, discountProductPrice / 100 * 50);
        }

        [TestMethod]
        public async Task GetDiscountsAsync_WithNoSpecialOffers_ShouldReturnNoDiscounts()
        {
            //arrange
            var buy2ProductId = Guid.NewGuid();
            var buy2ProductPrice = 0.65m;
            var discountProductId = Guid.NewGuid();
            var discountProductPrice = 0.80m;
            
            var mockSpecialOfferRepository = new Mock<IGenericRepository<SpecialOffer>>();
            mockSpecialOfferRepository.Setup(m => m.GetAllAsync(It.IsAny<Expression<Func<SpecialOffer, BuyTwoGetOneHalfPriceSpecialOffer>>>()))
                .ReturnsAsync(
                    new List<SpecialOffer>()
                );

            var basket = new Basket
            {
                Items = new List<BasketItem>
                {
                 new BasketItem{ Id = buy2ProductId, Name = "Soup", Price = buy2ProductPrice},
                 new BasketItem{  Id = buy2ProductId, Name = "Soup", Price =  buy2ProductPrice },
                 new BasketItem{ Id = discountProductId,  Name = "Bread", Price = discountProductPrice },
                }
            };

            var buyTwoGetOneHalfPriceDiscounts = new BuyTwoGetOneHalfPriceDiscounts(mockSpecialOfferRepository.Object);

            //act
            var discounts = await buyTwoGetOneHalfPriceDiscounts.GetDiscountsAsync(basket);

            //assert
            Assert.AreEqual(0, discounts.Count());
        }
    }
}
