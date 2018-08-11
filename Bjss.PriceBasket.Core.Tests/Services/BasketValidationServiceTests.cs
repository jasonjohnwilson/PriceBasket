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
    public class BasketValidationServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ValidateAsync_WithNullProducts_ShouldThrowException()
        {
            //arrange
            var basketValidationService = new BasketValidationService(null);

            //act
            await basketValidationService.ValidateAsync(null);

            //assert
        }

        [TestMethod]
        public async Task ValidateAsync_WithTwoValidators_ShouldValidateEachProductTwice()
        {
            //arrange
            var mockValidatorA = new Mock<IBasketValidator>();
            mockValidatorA.Setup(m => m.ValidateAsync(It.IsAny<IEnumerable<string>>()))
                .Returns(Task.FromResult<IEnumerable<ValidationError>>(new List<ValidationError>()));

            var mockValidatorB = new Mock<IBasketValidator>();
            mockValidatorB.Setup(m => m.ValidateAsync(It.IsAny<IEnumerable<string>>()))
                .Returns(Task.FromResult<IEnumerable<ValidationError>>(new List<ValidationError>()));

            var mockValidatorProvider = new Mock<IValidatorsProvider>();
            mockValidatorProvider.Setup(m => m.Get())
                .Returns(new List<IBasketValidator>
                {
                    mockValidatorA.Object,
                    mockValidatorB.Object
                });
        

            var basketValidationService = new BasketValidationService(mockValidatorProvider.Object);

            //act
            await basketValidationService.ValidateAsync(new string[] {
                "Milk", "Soup"
            });

            //assert
            mockValidatorProvider.Verify(m => m.Get(), Times.Once);
            mockValidatorA.Verify(m => m.ValidateAsync(It.IsAny<IEnumerable<string>>()), Times.Exactly(1));
            mockValidatorB.Verify(m => m.ValidateAsync(It.IsAny<IEnumerable<string>>()), Times.Exactly(1));
        }
    }
}
