using Bjss.PriceBasket.Core.Interfaces;
using Bjss.PriceBasket.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bjss.PriceBasket.Core.Services
{
    /// <summary>
    /// Class to aggregate all validators and apply them to the products entered on the command line
    /// </summary>
    public class BasketValidationService : IBasketValidator
    {
        private readonly IValidatorsProvider _validatorsProvider;

        public BasketValidationService(IValidatorsProvider validatorsProvider)
        {
            _validatorsProvider = validatorsProvider;
        }

        /// <summary>
        /// Validates all the products 
        /// </summary>
        /// <param name="products">Array of product names</param>
        /// <returns>Enumeration of validation errors</returns>
        public async Task<IEnumerable<ValidationError>> ValidateAsync(IEnumerable<string> products)
        {
            if(products == null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            var validationErrors = new List<ValidationError>();

            foreach (var validator in _validatorsProvider.Get())
            {
                validationErrors.AddRange(await validator.ValidateAsync(products));
            }

            return validationErrors;
        }
    }
}
