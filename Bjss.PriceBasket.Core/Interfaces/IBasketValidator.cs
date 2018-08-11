using System.Collections.Generic;
using System.Threading.Tasks;
using Bjss.PriceBasket.Core.Models;

namespace Bjss.PriceBasket.Core.Interfaces  
{
    public interface IBasketValidator
    {
        Task<IEnumerable<ValidationError>> ValidateAsync(IEnumerable<string> products);
    }
}