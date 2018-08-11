using Bjss.PriceBasket.Core.Validation;
using System;

namespace Bjss.PriceBasket.Core.Models
{
    public sealed class ValidationError
    {
        private ValidationError() { }
        public ValidationErrors Error { get; private set; }
        public string Product { get; private set; } 

        public static ValidationError NoProductsSelected()
        {
            return new ValidationError
            {
                Error = ValidationErrors.NoProductsSelected
            };
        }

        public static ValidationError OutOfStock(string product)
        {
            if(string.IsNullOrEmpty(product))
            {
                throw new ArgumentException($"{nameof(product)} cannot not be null or empty");
            }

            return new ValidationError
            {
                Error = ValidationErrors.OutOfStock,
                Product = product
            };
        }

        public static ValidationError ProductNotFound(string product)
        {
            if (string.IsNullOrEmpty(product))
            {
                throw new ArgumentException($"{nameof(product)} cannot not be null or empty");
            }

            return new ValidationError
            {
                Error = ValidationErrors.ProductNotFound,
                Product = product
            };
        }
    }
}
