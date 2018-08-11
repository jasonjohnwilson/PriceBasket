using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bjss.PriceBasket.Core.Interfaces;
using Bjss.PriceBasket.Core.Models;

namespace Bjss.PriceBasket.Core.Validation
{
    /// <summary>
    /// Validates that the selected product names correspond to valid products
    /// </summary>
    public class ValidProductValidator : IBasketValidator
    {
        private readonly IGenericRepository<Product> _productRepository;

        public ValidProductValidator(IGenericRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ValidationError>> ValidateAsync(IEnumerable<string> products)
        {
            var validationErrors = new List<ValidationError>();

            foreach (var product in products)
            {
                var exists = (await _productRepository
                    .GetAllAsync(p => p.Name.Equals(product, StringComparison.InvariantCultureIgnoreCase)))
                    .Any();

                if(!exists)
                {
                    validationErrors.Add(ValidationError.ProductNotFound(product));
                }
            }

            return validationErrors;
        }
    }
}
