using Bjss.PriceBasket.Core.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Bjss.PriceBasket.Core.Tests.Validation
{
    [TestClass]
    public class AnyItemsValidatorTests
    {
        [TestMethod]
        public async Task ValidateAsync_WithProductsSupplied_ReturnsNoErrors()
        {
            //arrange
            var validator = new AnyItemsValidator();

            //act
            var errors = await validator.ValidateAsync(new string[] { "Milk", "Bead" });

            //assert
            Assert.IsFalse(errors.Any());
        }

        [TestMethod]
        public async Task ValidateAsync_WithNoProductsSupplied_ShouldReturnsError()
        {
            //arrange
            var validator = new AnyItemsValidator();

            //act
            var errors = await validator.ValidateAsync(new string[] {});

            //assert
            Assert.IsTrue(errors.Any());
            Assert.IsTrue(errors.First().Error == ValidationErrors.NoProductsSelected);
        }
    }
}
