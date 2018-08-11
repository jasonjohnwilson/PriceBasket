using Bjss.PriceBasket.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bjss.PriceBasket.Application.Interfaces
{
    public interface IValidationErrorsMessageFormatter
    {
        string Format(IEnumerable<ValidationError> validationErrors);
    }
}
