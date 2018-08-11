using Bjss.PriceBasket.Interfaces;

namespace Bjss.PriceBasket.View
{
    /// <summary>
    /// Abstraction to wrap the static Console class to enable it to be shielded from teh application and injected into lower level classes
    /// </summary>
    public class PriceBasketConsole : IConsole
    {
        public int CursorTop
        {
            get { return System.Console.CursorTop; }
        }

        public int CursorLeft
        {
            get { return System.Console.CursorLeft; }
        }

        public void Clear()
        {
            System.Console.Clear();
        }

        public void SetCursorPosition(int cursorLeft, int cursorTop)
        {
            System.Console.SetCursorPosition(cursorLeft, cursorTop);
        }

        public void Write(char value)
        {
            System.Console.Write(value);
        }

        public void WriteLine(string value)
        {
            System.Console.WriteLine(value);
        }
    }
}
