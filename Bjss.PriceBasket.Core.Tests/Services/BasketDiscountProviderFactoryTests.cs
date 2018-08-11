using Bjss.PriceBasket.Core.Interfaces;
using Bjss.PriceBasket.Core.Models;
using Bjss.PriceBasket.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bjss.PriceBasket.Core.Tests.Services
{
    [TestClass]
    public class BasketDiscountProviderFactoryTests
    {
        [TestMethod]
        public async Task GetAsync_WhenSpecialOffersAvailable_ShouldCreateDiscountsObjects()
        {
            //arrange
            var mockSpecialOfferRepository = new Mock<IGenericRepository<SpecialOffer>>();
            mockSpecialOfferRepository.Setup(m => m.GetAllAsync())
                .ReturnsAsync(
                    new List<SpecialOffer> {
                        new SpecialOffer
                        {
                            Name ="SpecialOffer",
                            DiscountAssemblyFullName = "Bjss.PriceBasket.Core.Services.ReduceProductPriceByPercentDiscounts, Bjss.PriceBasket.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
                        }
                    }
                );

            var discount = new ReduceProductPriceByPercentDiscounts(mockSpecialOfferRepository.Object);

            var mockObjectFactory = new Mock<IFactory>();
            mockObjectFactory.Setup(m => m.Get<IBasketDiscounts>(
                It.Is<string>(v => v == "Bjss.PriceBasket.Core.Services.ReduceProductPriceByPercentDiscounts, Bjss.PriceBasket.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"), 
                It.IsAny<object>()))
                .Returns(discount);

            var basketDiscountProviderFactory = new BasketDiscountProviderFactory(mockSpecialOfferRepository.Object, mockObjectFactory.Object);

            //act 
            var discountProviders = await basketDiscountProviderFactory.GetAsync();

            //assert
            Assert.AreEqual(1, discountProviders.Count());
            mockObjectFactory.Verify(m => m.Get<IBasketDiscounts>(
                It.Is<string>(v => v == "Bjss.PriceBasket.Core.Services.ReduceProductPriceByPercentDiscounts, Bjss.PriceBasket.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"),
                It.IsAny<object>()),
                Times.Once);
        }

        [TestMethod]
        public async Task GetAsync_WhenNoSpecialOffersAvailable_ShouldReturnEmptyEnumerable()
        {
            //arrange
            var mockSpecialOfferRepository = new Mock<IGenericRepository<SpecialOffer>>();
            mockSpecialOfferRepository.Setup(m => m.GetAllAsync())
                .ReturnsAsync(
                    Enumerable.Empty<SpecialOffer>()
                );

            var discount = new ReduceProductPriceByPercentDiscounts(mockSpecialOfferRepository.Object);
            var mockObjectFactory = new Mock<IFactory>();
            var basketDiscountProviderFactory = new BasketDiscountProviderFactory(mockSpecialOfferRepository.Object, mockObjectFactory.Object);

            //act 
            var discountProviders = await basketDiscountProviderFactory.GetAsync();

            //assert
            Assert.AreEqual(0, discountProviders.Count());
        }
    }
}
