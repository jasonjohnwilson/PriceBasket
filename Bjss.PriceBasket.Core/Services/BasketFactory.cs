using Bjss.PriceBasket.Core.Interfaces;
using Bjss.PriceBasket.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bjss.PriceBasket.Core.Services
{
    /// <summary>
    /// Creates the factory using product repository
    /// </summary>
    public class BasketFactory : IBasketFactory
    {
        private readonly IGenericRepository<Product> _product;

        public BasketFactory(IGenericRepository<Product> product)
        {
            _product = product;
        }

        /// <summary>
        /// Creates a basket based on selected products
        /// </summary>
        /// <param name="products">All products to add to basket</param>
        /// <returns>A basket abstraction</returns>
        public IBasket Get(string[] products)
        {
            if(products == null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            var basketItems = new List<BasketItem>();

            foreach(var product in products)
            {
                var productModel = _product.GetAll().Where(p => p.Name.ToLower() == product.ToLower()).FirstOrDefault();

                if(productModel == null)
                {
                    throw new Exception($"Product {product} cannot be found");
                }

                basketItems.Add(new BasketItem
                {
                    Id = productModel.Id,
                    Name = productModel.Name,
                    Price = productModel.Price
                });
            }

            return new Basket { Items = basketItems };
        }
    }
}
