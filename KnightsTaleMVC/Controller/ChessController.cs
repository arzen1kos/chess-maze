using KnightsTaleMVC.Model;
using KnightsTaleMVC.View;
using System;
using static KnightsTaleMVC.Model.Board;

namespace KnightsTaleMVC.Controller
{
    class ChessController
    {
        private readonly OutputConsoleView _view;
        private readonly Board _board;
        public short selectedRow = 0;
        public short selectedColumn = 0;
        //internal Cell currentCell;

        public ChessController(OutputConsoleView view, Board board)
        {
            _view = view;
            _board = board;
        }

        public void Run()
        {
            _board.StoreInitCell();
            _board.AltSelectedCellState(selectedRow, selectedColumn);
            while (true)
            {
                ResetBoard();
                ConsoleKey command = _view.GetOption("Press ENTER/SPACE to move, END to reset, or ESC to exit.").Key;
                if (command == ConsoleKey.Escape)
                {
                    _view.Stop();
                    return;
                }

                if (command == ConsoleKey.End)
                {
                    _view.DisplayWarning(_board.ResetBoard());
                    continue;
                }

                if (command == ConsoleKey.Enter || command == ConsoleKey.Spacebar)
                {
                    GetUserCellSelection();
                    if (_board.IsTargetCell(_board.ActiveCell.RowNumber, _board.ActiveCell.ColumnNumber))
                    {
                        return;
                    }
                    continue;
                }

                HandleInvalidCommand();
            }
        }
        internal void ResetBoard()
        {
            // Clear previous console output
            _view.Start();
            // Initialise board display
            DisplayBoard();
        }
        internal void DisplayBoard()
        {
            // ==========
            char borderDesign = '=';
            char character = ' ';

            _view.PrintHeader();
            int boardSize = _board.Size;
            _view.PrintHorizontalBoarder(borderDesign, boardSize);

            for (int a = 0; a < boardSize; a++)
            {
                // Print the horizontal column counter (ACROSS / TOP SIDE)
                _view.PrintGridNumber(a, "   ");
            }
            Console.WriteLine();
            for (int i = 0; i < boardSize; i++)
            {
                for (int h = 0; h < boardSize; h++)
                {
                    // Print inner grid: + ---
                    _view.PrintInnerGridBorder();
                }
                Console.WriteLine();

                for (int j = 0; j < boardSize; j++)
                {
                    character = _board.MarkCell(i, j);
                    _view.PrintCell(character);
                }
                // Print the vertical row counter (DOWN / RIGHT SIDE)
                _view.PrintGridNumber(i, "|");
                Console.WriteLine();
            }
            _view.PrintHorizontalBoarder(borderDesign, boardSize);
            // It is the end cell that is reached, and it is also a legal move?
            _view.Print(_board.ListMoves());

            if (character == (char)CellState.TargetReached)
            {
                // Display text that game is finished
                _view.GameComplete(_board.MoveCount);
                // End game
                _view.Stop();
            }
            // Display the number of moves made
            _view.DisplayCurrentMoveCount(_board.MoveCount);

        }
        //internal void ListMoves()
        //{
        //    string s = "{";
        //    foreach (Cell c in _board.MoveSequence)
        //    {
        //        s += $"[{c.RowNumber},{c.ColumnNumber}]  ";
        //        //Console.WriteLine($"{c.ColumnNumber},{c.RowNumber}");
        //    }
        //    s += "}";
        //    Console.WriteLine(s);
        //}
        internal void GetUserCellSelection()
        {
            string rowInput = _view.GetCoordinate("row", "down", "right");
            if (rowInput == "Undo" || rowInput == "Redo")
            {
                _view.DisplayWarning(_board.SwitchToPreviousMove(rowInput));
                return;
            }

            if (!short.TryParse(rowInput, out selectedRow))
            {
                _view.DisplayWarning("Enter a whole-number row.");
                return;
            }

            string columnInput = _view.GetCoordinate("column", "across", "top");
            if (!short.TryParse(columnInput, out selectedColumn))
            {
                _view.DisplayWarning("Enter a whole-number column.");
                return;
            }

            HandleInput(selectedRow, selectedColumn);
        }
        internal void HandleInvalidCommand(string type = "default")
        {
            string prompt;

            switch (type)
            {
                case "Out of Range":
                    prompt = $"Selected cell num is larger than board's size range.";
                    break;

                case "Illegal Move":
                    prompt = "Impossible move, out of scope of knight's current movement radar.";
                    break;
                case "Repeated Move":
                    prompt = "Same cell is selected.";
                    break;
                default:
                    prompt = "Command not found.";
                    break;
            }
            _view.DisplayWarning(prompt);
        }
        internal void HandleInput(int row, int col)
        {
            string prompt = _board.EvaluateSelectCell(row, col);
            ResetBoard();

            if (prompt != null)
            {
                HandleInvalidCommand(prompt);
            }
        }
    }
}
