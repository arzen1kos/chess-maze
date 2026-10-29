using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMazeConsoleApp
{
    class ChessMazeController
    {
        private readonly IView _view;
        private readonly Board board_model;
        public ChessMazeController(IView view, Board model)
        {
            _view = view;
            board_model = model;
        }

        public void Run()
        {
            _view.Start();
            //string prompt = "Enter an integer for the chessboard's dimension: ";
            //int dimension = _view.Read(prompt);

            string board = board_model.GenerateBoard(); //dimension
            _view.Display(board);
            _view.End();
        }
    }
}
