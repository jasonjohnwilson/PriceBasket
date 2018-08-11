using Bjss.PriceBasket.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bjss.PriceBasket.Core.Interfaces
{
    public interface IBasketDiscounts
    {
        Task<IEnumerable<Discount>> GetDiscountsAsync(IBasket basket);
    }
}
