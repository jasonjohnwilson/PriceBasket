namespace Bjss.PriceBasket.Core.Interfaces
{
    public interface IFactory
    {
        TType Get<TType>(string assemblyFullName, params object[] obj);
    }
}
