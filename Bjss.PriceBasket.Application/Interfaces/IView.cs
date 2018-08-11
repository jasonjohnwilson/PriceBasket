namespace Bjss.PriceBasket.Application.Interfaces
{
    public interface IView
    {
        void Present(string message);
        void ShowLoading();
        void HideLoading();

    }
}
