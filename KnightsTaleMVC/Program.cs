using KnightsTaleMVC.Controller;
using KnightsTaleMVC.Model;
using KnightsTaleMVC.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsTaleMVC
{
    internal class Program
    {
        static void Main()
        {
            var board = new Board(5);
            OutputConsoleView consoleView = new OutputConsoleView();

            var controller = new ChessController(consoleView, board);

            controller.Run();
        }
    }
}
