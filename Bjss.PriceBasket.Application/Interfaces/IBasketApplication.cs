using System.Threading.Tasks;

namespace Bjss.PriceBasket.Application.Interfaces
{
    public interface IBasketApplication
    {
        Task ExecuteAsync(string[] args);
    }
}