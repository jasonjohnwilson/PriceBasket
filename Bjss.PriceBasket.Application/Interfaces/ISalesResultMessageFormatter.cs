using Bjss.PriceBasket.Core.Models;

namespace Bjss.PriceBasket.Application.Interfaces
{
    public interface ISalesResultMessageFormatter
    {
        string Format(SaleDetails saleDetails);
    }
}
