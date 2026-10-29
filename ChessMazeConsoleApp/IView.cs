using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMazeConsoleApp
{
    interface IView
    {
        void Start();
        int Read(string prompt);
        void Display<T>(T board_grid);
        void End();
    }
}
