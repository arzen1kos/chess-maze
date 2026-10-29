using KnightsTaleMVC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsTaleMVC.View
{
    public interface IBoard
    {
        //void Start();

        void PrintGridNumber(int count, string leading);
        void PrintHorizontalBoarder(char design, int lengthFactor);
        void PrintInnerGridBorder();
        //void PrintBorder(string border, int count);
        //void DisplayCurrentMoveCount(int count);

        void PrintCell(char piece);
        //void Stop();
        //void GameComplete();
    }
}
