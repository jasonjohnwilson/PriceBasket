using Bjss.PriceBasket.Core.Interfaces;
using Bjss.PriceBasket.Core.Models;
using Bjss.PriceBasket.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bjss.PriceBasket.Core.Tests.Services
{
    [TestClass]
    public class BasketFactoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Get_WithNullProducts_ShouldThrowArgumentNullException()
        {
            //arrange
            var basketFactory = new BasketFactory(null);

            //act
            basketFactory.Get(null);

            //assert
        }

        [TestMethod]
        public void Get_CalledWith2Products_ShouldReturnBasketWith2Items()
        {
            //arrange
            var milk = new Product { Id = Guid.NewGuid(), Name = "Milk", Price = 0.40m, QuantityAvailable = 100 };
            var soup = new Product { Id = Guid.NewGuid(), Name = "Soup", Price = 0.40m, QuantityAvailable = 100 };
            var bread = new Product { Id = Guid.NewGuid(), Name = "Bread", Price = 0.40m, QuantityAvailable = 100 };
            var apples = new Product { Id = Guid.NewGuid(), Name = "Apples", Price = 0.40m, QuantityAvailable = 100 };

            var mockProductRepository = new Mock<IGenericRepository<Product>>();
            mockProductRepository.Setup(m => m.GetAll())
                .Returns(
                    new List<Product>
                        {
                            milk, soup, bread, apples
                        }.AsQueryable()
                );


            var basketFactory = new BasketFactory(mockProductRepository.Object);
            
            //act
            var basket = basketFactory.Get(new string[] { "Milk", "Soup" });

            //assert
            Assert.AreEqual(2, basket.Items.Count());
            Assert.IsNotNull(basket.Items.FirstOrDefault(i => i.Name == "Soup"));
            Assert.IsNotNull(basket.Items.FirstOrDefault(i => i.Name == "Milk"));
        }

    }
}
