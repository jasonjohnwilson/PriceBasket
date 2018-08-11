using Bjss.PriceBasket.Core.Models;
using System.Threading.Tasks;

namespace Bjss.PriceBasket.Core.Interfaces
{
    public interface ISaleCalculator
    {
        Task<SaleDetails> CalculateAsync(IBasket basket);
    }
}