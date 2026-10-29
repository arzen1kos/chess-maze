using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMazeConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            new ChessMazeController(new ConsoleView(), new Board()).Run();
        }
    }
}
