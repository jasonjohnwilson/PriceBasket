using System.Collections.Generic;
using System.Threading.Tasks;
using Bjss.PriceBasket.Core.Models;

namespace Bjss.PriceBasket.Core.Interfaces
{
    public interface IBasketDiscountProcessor
    {
        Task<IEnumerable<Discount>> GetAllDiscountsAsync(IBasket basket);
    }
}