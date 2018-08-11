using Bjss.PriceBasket.Core.Interfaces;
using Bjss.PriceBasket.Core.Models;
using Bjss.PriceBasket.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bjss.PriceBasket.Core.Tests.Services
{
    [TestClass]
    public class SaleCalculatorTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task CalculateAsync_WhenNullBasket_ShouldThrowException()
        {
            //arrange
            var salesCalculator = new SaleCalculator(null);

            //act
            await salesCalculator.CalculateAsync(null);

            //assert
        }

        [TestMethod]
        public async Task CalculateAsync_WhenNoDiscounts_TotalAndSubTotalShouldBeEqual()
        {
            //arrange
            var mockBasketDiscountProcessor = new Mock<IBasketDiscountProcessor>();
            mockBasketDiscountProcessor.Setup(m => m.GetAllDiscountsAsync(It.IsAny<IBasket>()))
                .Returns(Task.FromResult(
                    Enumerable.Empty<Discount>()
                    ));

            var salesCalculator = new SaleCalculator(mockBasketDiscountProcessor.Object);

            var basket = new Basket
            {
                Items = new List<BasketItem> {
                    new BasketItem { Id = Guid.NewGuid(), Price = 0.30m},
                    new BasketItem { Id = Guid.NewGuid(), Price = 0.30m},
                    new BasketItem { Id = Guid.NewGuid(), Price = 1.30m},
                    new BasketItem { Id = Guid.NewGuid(), Price = 2.30m},
                }
            };

            //act
            var saleDetails = await salesCalculator.CalculateAsync(basket);

            //assert
            Assert.AreEqual(saleDetails.SubTotal, saleDetails.Total);
            Assert.AreEqual(saleDetails.Total, basket.Items.Sum(i => i.Price));
        }

        [TestMethod]
        public async Task CalculateAsync_WithDiscounts_ShouldBeSubtractedFromTheTotal()
        {
            //arrange
            var discounts = new List<Discount>
                    {
                         new Discount {  DiscountAmount = 1.20m},
                         new Discount{  DiscountAmount = 0.80m}
                    };

            var mockBasketDiscountProcessor = new Mock<IBasketDiscountProcessor>();
            mockBasketDiscountProcessor.Setup(m => m.GetAllDiscountsAsync(It.IsAny<IBasket>()))
                .Returns(Task.FromResult(discounts.AsEnumerable()));

            var salesCalculator = new SaleCalculator(mockBasketDiscountProcessor.Object);

            var basket = new Basket
            {
                Items = new List<BasketItem> {
                    new BasketItem { Id = Guid.NewGuid(), Price = 0.30m},
                    new BasketItem { Id = Guid.NewGuid(), Price = 0.30m},
                    new BasketItem { Id = Guid.NewGuid(), Price = 1.30m},
                    new BasketItem { Id = Guid.NewGuid(), Price = 2.30m},
                }
            };

            //act
            var saleDetails = await salesCalculator.CalculateAsync(basket);

            //assert
            Assert.AreEqual(saleDetails.Total, basket.Items.Sum(i => i.Price) - discounts.Sum(d=>d.DiscountAmount));
        }
    }
}
