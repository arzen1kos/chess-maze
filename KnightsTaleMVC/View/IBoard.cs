namespace KnightsTaleMVC.View
{
    public interface IBoard
    {
        void PrintHeader();
        void PrintGridNumber(int count, string leading);
        void PrintHorizontalBoarder(char design, int lengthFactor);
        void PrintInnerGridBorder();
        void PrintCell(char piece);
    }
}
