using Bjss.PriceBasket.Core.Interfaces;
using Bjss.PriceBasket.Core.Models;
using Bjss.PriceBasket.Core.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bjss.PriceBasket.Core.Tests.Validation
{
    [TestClass]
    public class ValidProductValidatorTests
    {
        [TestMethod]
        public async Task ValidateAsync_WithValidProductsSupplied_ReturnsNoErrors()
        {
            //arrange
            var mockProductRepository = new Mock<IGenericRepository<Product>>();
            mockProductRepository.Setup(m => m.GetAllAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .Returns(
                Task.FromResult(
                    new List<Product> {
                        new Product{ Name = "Milk" },
                        new Product{ Name = "Bread" }
                    }.AsEnumerable()));

            var validator = new ValidProductValidator(mockProductRepository.Object);

            //act
            var errors = await validator.ValidateAsync(new string[] { "Milk", "Bread" });

            //assert
            Assert.IsFalse(errors.Any());
        }

        [TestMethod]
        public async Task ValidateAsync_WithInvalidProductsSupplied_ShouldReturnsError()
        {
            //arrange
            var mockProductRepository = new Mock<IGenericRepository<Product>>();
            mockProductRepository.Setup(m => m.GetAllAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .Returns(Task.FromResult(new List<Product>().AsEnumerable()));

            var validator = new ValidProductValidator(mockProductRepository.Object);

            //act
            var errors = await validator.ValidateAsync(new string[] { "Milk", "Bread" });

            //assert
            Assert.IsTrue(errors.Any());
        }
    }
}
