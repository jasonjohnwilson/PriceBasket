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
    public class BasketDiscountProcessorTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task GetAllDiscountsAsync_WhenBasketNull_ShouldThrowException()
        {
            //arrange
            var mockBasketDiscountProviderFactory = new Mock<IBasketDiscountProviderFactory>();
            var basketDiscountProcessor = new BasketDiscountProcessor(mockBasketDiscountProviderFactory.Object);

            //act
            await basketDiscountProcessor.GetAllDiscountsAsync(null);
        }

        [TestMethod]
        public async Task GetAllDiscountsAsync_WhenDiscountsAvailable_ShouldReturnDiscount()
        {
            //arrange
            var discount = new Discount { };

            var mockBasketDiscount = new Mock<IBasketDiscounts>();
            mockBasketDiscount.Setup(m => m.GetDiscountsAsync(It.IsAny<IBasket>()))
                .ReturnsAsync(new List<Discount> { discount });

            var mockBasketDiscountProviderFactory = new Mock<IBasketDiscountProviderFactory>();
            mockBasketDiscountProviderFactory.Setup(m => m.GetAsync())
                .ReturnsAsync(new List<IBasketDiscounts>
                {
                    mockBasketDiscount.Object
                });

            var basketDiscountProcessor = new BasketDiscountProcessor(mockBasketDiscountProviderFactory.Object);

            //act
            var discounts = await basketDiscountProcessor.GetAllDiscountsAsync(new Basket());

            //assert
            mockBasketDiscount.Verify(m => m.GetDiscountsAsync(It.IsAny<IBasket>()), Times.Once);
            CollectionAssert.Contains(discounts.ToList(), discount);
        }
    }
}
