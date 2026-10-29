using KnightsTaleMVC.Model;
using KnightsTaleMVC.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static KnightsTaleMVC.Model.Board;

namespace KnightsTaleMVC.Controller
{
    class ChessController
    {
        private readonly ConsoleView _view;
        private readonly Board _board;
        public short currentRow = 0;
        public short currentColumn = 0;
        //internal Cell currentCell;
        public ChessController(ConsoleView view, Board board)
        {
            _view = view;
            _board = board;
        }

        public void Run()
        {
            if (_board.MoveCount <= 0)
            {
                Cell initCell = _board.TheGrid[currentRow, currentColumn];
                _board.SetPreviousMove(initCell);
            }
            // Ask user to continue
            // Clear previous console output
            _view.Start();
            // Initialise board display
            DisplayBoard();
            //if (gameComplete)
            DisplayOpeningPrompt();
        }

        internal void DisplayOpeningPrompt()
        {
            // Start prompt
            ConsoleKeyInfo input = _view.GetOption("Press 'ENTER/SPACEBAR' key to continue. Press 'ESCAPE' to  end.");

            switch (input.Key)
            {
                // Exit game
                case ConsoleKey.Escape:
                    _view.Stop();
                    break;

                // Keep playing
                case ConsoleKey.Enter:
                case ConsoleKey.Spacebar:
                    GetUserCellSelection();
                    Run();
                    break;

                // Undo
                //case ConsoleKey.Backspace:
                //case ConsoleKey.LeftArrow:
                //    Console.Write("Move undo");
                //    string undo = _board.SwitchToPreviousMove("Undo");
                //    _view.DisplayWarning(undo);

                //    break;
                //// Redo
                //case ConsoleKey.Tab:
                //case ConsoleKey.RightArrow:
                //    Console.Write("Move redo");
                //    string redo = _board.SwitchToPreviousMove("Redo");
                //    _view.DisplayWarning(redo);
                //    break;

                // Reset
                case ConsoleKey.End:
                    string reset = _board.ResetBoard();
                    _view.DisplayWarning(reset);
                    break;

                default:
                    string prompt = HandleInvalidCommand();
                    _view.DisplayWarning(prompt);
                    _board.WaitToRestart(2000);
                    Run();
                    break;
            }
        }
        internal void DisplayBoard()
        {
            // ==========
            char borderDesign = '=';
            char cellChar = ' ';

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
                    CellState state = _board.SetCellState(i, j);
                    switch (state)
                    {
                        case CellState.Default:
                            cellChar = ' ';
                            break;
                        case CellState.StartMark:
                            cellChar = 'S';
                            break;
                        case CellState.TargetMark:
                            cellChar = 'E';
                            break;

                        case CellState.TargetReached:
                            cellChar = '\u263a';
                            break;
                        case CellState.Selected:
                            cellChar = 'H';
                            break;
                        case CellState.LegalMove:
                            //cellChar = '#';
                            cellChar = _board.IsTargetCell(currentRow, currentColumn) ? ' ' : '#';
                            break;
                        default:
                            cellChar = ' ';
                            break;
                    };
                    _view.PrintCell(cellChar);
                }
                // Print the vertical row counter (DOWN / RIGHT SIDE)
                _view.PrintGridNumber(i, "|");
                Console.WriteLine();
            }
            _view.PrintHorizontalBoarder(borderDesign, boardSize);
            //Cell c = _board.TheGrid[currentRow, currentColumn];
            // It is the end cell that is reached, and it is also a legal move?
            if (cellChar == '\u263a')
            {
                // Display text that game is finished
                _view.GameComplete();
                // End game
                _view.Stop();
            }
            // Display the number of moves made
            _view.DisplayCurrentMoveCount(_board.MoveCount);

        }
        internal void GetUserCellSelection()
        {
            bool validRow = false;
            bool validColumn = false;
            while (!validRow || !validColumn)
            {
                if (!validRow)
                {
                    string rowInput = _view.GetCoordinate("Enter the current row number (down)", "right");
                    if (Int16.TryParse(rowInput, out _))
                    {
                        //currentRow = parsedRow;
                        currentRow = Int16.Parse(rowInput);
                        validRow = true;
                    }
                    else
                    {
                        Console.WriteLine(_board.SwitchToPreviousMove(rowInput));
                        _board.WaitToRestart(1000);

                        break;
                    }
                }
                if (validRow && !validColumn)
                {
                    string columnInput = _view.GetCoordinate("Enter the current column number (across)", "top");
                    if (Int16.TryParse(columnInput, out _))
                    {
                        currentColumn = Int16.Parse(columnInput);
                        validColumn = true;
                    }
                    else
                    {
                        Console.WriteLine(_board.SwitchToPreviousMove(columnInput));
                        _board.WaitToRestart(1000);

                        break;
                    }
                    HandleInput(currentRow, currentColumn);
                }
            }
            //Console.WriteLine($"{currentRow}, {currentColumn}");
        }

        internal string HandleInvalidCommand(string type = "default")
        {
            string prompt;

            switch (type)
            {
                case "Out of Range":
                    prompt = $"Selected cell is out of board range. Please enter row/col number SMALLER than {_board.Size}.";
                    break;

                case "Illegal Move":
                    prompt = "Selected cell is not a possible next move for the knight's current position.";
                    break;
                default:
                    prompt = "Command not found.";
                    break;
            }
            return prompt;
        }
        internal void HandleInput(int row, int col)
        {
            string prompt = _board.EvaluateSelectCell(row, col);
            // Clear previous console output
            _view.Start();

            // Initialise board display
            DisplayBoard();

            if (prompt != null)
            {
                prompt = HandleInvalidCommand(prompt);
                _view.DisplayWarning(prompt);
            }
            _board.WaitToRestart(1000);
            GetUserCellSelection();
        }
    }
}
