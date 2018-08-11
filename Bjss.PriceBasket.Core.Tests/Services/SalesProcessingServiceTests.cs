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
    public class SalesProcessingServiceTests
    {
        [TestMethod]
        public async Task TryCreateSaleAsync_WhenValidationFails_ShouldReturnSalesProcessingResult()
        {
            //arrange
            var mockBasketValidator = new Mock<IBasketValidator>();
            mockBasketValidator.Setup(m => m.ValidateAsync(It.IsAny<IEnumerable<string>>()))
                .Returns(Task.FromResult(new List<ValidationError> {
                    ValidationError.NoProductsSelected(),
                    ValidationError.OutOfStock("1234")
                }.AsEnumerable()));

            var mockBasketfactory = new Mock<IBasketFactory>();
            var mockSalesCalculator = new Mock<ISaleCalculator>();

            var salesProcessingService = new SalesProcessingService(
                mockBasketValidator.Object, 
                mockBasketfactory.Object, 
                mockSalesCalculator.Object);

            //act
            var result = await salesProcessingService.TryCreateSaleAsync(new string[] { });

            //assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(2, result.ValidationErrors.Count());
        }

        [TestMethod]
        public async Task TryCreateSaleAsync_WhenPassesValidation_ShouldReturnSuccessSalesProcessingResult()
        {
            //arrange
            var mockBasketValidator = new Mock<IBasketValidator>();
            mockBasketValidator.Setup(m => m.ValidateAsync(It.IsAny<IEnumerable<string>>()))
                .Returns(Task.FromResult(Enumerable.Empty<ValidationError>()));

            var mockBasketfactory = new Mock<IBasketFactory>();
            mockBasketfactory.Setup(m => m.Get(It.IsAny<string[]>()))
                .Returns(new Basket());

            var mockSalesCalculator = new Mock<ISaleCalculator>();
            mockSalesCalculator.Setup(m => m.CalculateAsync(It.IsAny<IBasket>()))
                .ReturnsAsync(new SaleDetails());

            var salesProcessingService = new SalesProcessingService(
                mockBasketValidator.Object,
                mockBasketfactory.Object,
                mockSalesCalculator.Object);

            //act
            var result = await salesProcessingService.TryCreateSaleAsync(new string[] { });

            //assert
            Assert.IsTrue(result.Success);
            mockBasketfactory.Verify(m => m.Get(It.IsAny<string[]>()), Times.Once);
            mockSalesCalculator.Verify(m => m.CalculateAsync(It.IsAny<IBasket>()), Times.Once);
        }
    }
}