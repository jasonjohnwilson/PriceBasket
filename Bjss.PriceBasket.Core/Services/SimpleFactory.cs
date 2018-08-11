using Bjss.PriceBasket.Core.Interfaces;
using System;

namespace Bjss.PriceBasket.Core.Services
{
    /// <summary>
    /// Factory used to create objects via simple reflection
    /// </summary>
    public class SimpleFactory : IFactory
    {
        public TType Get<TType>(string assemblyFullName, params object[] obj)
        {
            return (TType)Activator.CreateInstance(Type.GetType(assemblyFullName), obj);
        }
    }
}
