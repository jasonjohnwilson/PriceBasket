using Bjss.PriceBasket.Core.Interfaces;
using Bjss.PriceBasket.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bjss.PriceBasket.Core.Validation
{
    /// <summary>
    /// Validates that products have been selected
    /// </summary>
    public class AnyItemsValidator : IBasketValidator
    {
        public Task<IEnumerable<ValidationError>> ValidateAsync(IEnumerable<string> products)
        {
            var validationErrors = new List<ValidationError>();

            if (products.Any() == false)
            {
                validationErrors.Add(ValidationError.NoProductsSelected());
            }

            return Task.FromResult(validationErrors.AsEnumerable());
        }
    }
}
