using Bjss.PriceBasket.Core.Interfaces;
using Bjss.PriceBasket.Core.Models;
using Bjss.PriceBasket.Core.Validation;
using System.Collections.Generic;

namespace Bjss.PriceBasket.Core.Services
{
    /// <summary>
    /// Provides all the validators that should be used against all selected products
    /// </summary>
    public class ValidatorsProvider : IValidatorsProvider
    {
        private readonly IGenericRepository<Product> _productRepository;

        public ValidatorsProvider(IGenericRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Gets all teh validators
        /// </summary>
        /// <returns>Enumeration of basket validators</returns>
        public IEnumerable<IBasketValidator> Get()
        {
            //To Do: configure and load validators dynamically

            return new List<IBasketValidator> {
                    new AnyItemsValidator(),
                    new OutOfStockValidator(_productRepository),
                    new ValidProductValidator(_productRepository)
            };
        }

        
    }
}
