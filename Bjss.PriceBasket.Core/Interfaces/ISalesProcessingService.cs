using System.Threading.Tasks;
using Bjss.PriceBasket.Core.Models;

namespace Bjss.PriceBasket.Core.Interfaces
{
    public interface ISalesProcessingService
    {
        Task<SalesProcessingResult> TryCreateSaleAsync(string[] selectedProducts);
    }
}