using System;
using System.Collections.Generic;
using System.Text;

namespace Bjss.PriceBasket.Core.Interfaces
{
    public interface IBasketFactory
    {
        IBasket Get(string[] products);
    }
}
