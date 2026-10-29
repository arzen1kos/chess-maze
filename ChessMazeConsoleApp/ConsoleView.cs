using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMazeConsoleApp
{
    class ConsoleView : IView
    {
        public void Start()
        {
            Console.Clear();
        }
        public int Read(string prompt)
        {
            Console.WriteLine(prompt);
            int dimension = Convert.ToInt32(Console.ReadLine());
            return dimension;
        }

        public void Display<T>(T board_grid)
        {
            Console.WriteLine(board_grid);
        }
        public void End()
        {
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
