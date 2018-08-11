namespace Bjss.PriceBasket.Interfaces
{
    public interface IConsole
    {
        int CursorTop { get; }
        int CursorLeft { get; }
        void Write(char value);
        void WriteLine(string value);
        void SetCursorPosition(int cursorLeft, int cursorTop);
        void Clear();
    }
}
