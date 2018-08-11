using System.Collections.Generic;

namespace Bjss.PriceBasket.Core.Interfaces
{
    public interface IValidatorsProvider
    {
        IEnumerable<IBasketValidator> Get();
    }
}
