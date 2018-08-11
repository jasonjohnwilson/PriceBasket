using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bjss.PriceBasket.Core.Interfaces
{
    public interface IBasketDiscountProviderFactory
    {
        Task<IEnumerable<IBasketDiscounts>> GetAsync();
    }
}