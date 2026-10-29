using KnightsTaleMVC;
using KnightsTaleMVC.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static KnightsTaleMVC.Model.Cell;

namespace KnightsTaleMVC.Model
{
    public class Board
    {
        public readonly int Size;
        public readonly Cell[,] TheGrid;
        private readonly Piece knightPiece;
        internal int Boundary;

        public List<Cell> LegalMoves { get; private set; }

        // Initialise recent cell value
        public Cell ActiveCell = new Cell(0, 0);
        public Cell PreviousCell = new Cell(0, 0);

        // Initialise Move count
        public int MoveCount = 0;

        public Board(int size)
        {
            Size = size;
            TheGrid = new Cell[Size, Size];
            LegalMoves = new List<Cell>();
            Boundary = Size - 1;
            knightPiece = new Piece("Knight");
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    TheGrid[i, j] = new Cell(i, j);
                }
            }
        }
        internal bool IsTargetCell(int row, int col)
        {
            return row == Boundary && col == Boundary;
        }
        public void WaitToRestart(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        //public void EvaluateSelectCell(Cell selectedCell)
        public string EvaluateSelectCell(int row, int col)
        {
            PreviousCell = TheGrid[ActiveCell.RowNumber, ActiveCell.ColumnNumber];

            // Checks if cell is occupied with X (selected) or the selection is one of the next legal moves
            string prompt;

            if (IsInBound(row, col))
            {
                Cell nextMove = TheGrid[row, col];
                if (IsLegalMove(nextMove))
                {
                    // Save the last selected cell
                    MarkNextLegalMove();
                    MoveCount++;
                    UpdateLegalMoves(nextMove);

                    SetPreviousMove(nextMove);
                    prompt = null;
                }
                else
                {
                    prompt = "Illegal Move";
                }
            }
            else
            {
                // Selected cell (row num or column num) is greater than the board size
                prompt = "Out of Range";
            }
            return prompt;
        }
        public void SetPreviousMove(Cell currentCell)
        {
            ActiveCell = TheGrid[currentCell.RowNumber, currentCell.ColumnNumber];

            TheGrid[currentCell.RowNumber, currentCell.ColumnNumber].CurrentlyOccupied = true;
            //if (MoveCount != 0)
            //{
            //    Console.WriteLine($"OLD: {PreviousCell.RowNumber}, {PreviousCell.ColumnNumber}");
            //    WaitToRestart(1000);

            //    Console.WriteLine($"NEW: {ActiveCell.RowNumber}, {ActiveCell.ColumnNumber}");
            //    WaitToRestart(1000);
            //};
        }
        internal bool IsLegalMove(Cell nextMove)
        {
            return nextMove.CurrentlyOccupied || LegalMoves.Contains(nextMove);
        }
        public void MarkNextLegalMove()
        {
            //Cell currentCell
            // clear all previous legal moves
            LegalMoves.Clear();

            // find all legal moves and mark as legal
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    TheGrid[i, j].LegalNextMove = false;
                    TheGrid[i, j].CurrentlyOccupied = false;
                }
            }
        }
        internal void UpdateLegalMoves(Cell currentCell)
        {
            foreach (var (offsetRow, offsetColumn) in knightPiece.Movement)
            {
                //Start
                int newRow = currentCell.RowNumber + offsetRow;
                int newColumn = currentCell.ColumnNumber + offsetColumn;

                if (IsInBound(newRow, newColumn))
                {
                    AddLegalMove(newRow, newColumn);
                }
            }
        }
        internal bool IsInBound(int x, int y)
        {
            return x >= 0 && x < Size && y >= 0 && y < Size; ;
        }
        //public void AddLegalMove(Cell selectedCell)
        public void AddLegalMove(int newRow, int newColumn)
        {
            var selectedCell = TheGrid[newRow, newColumn];
            selectedCell.LegalNextMove = true;
            LegalMoves.Add(selectedCell);
        }

        public enum CellState
        {
            StartMark,
            TargetMark,
            TargetReached,
            Selected,
            LegalMove,
            Default,
        }

        public bool IsInRange(Cell targetNextMove)
        {
            // Checks if the target cell is within scope of the active cell
            return IsTargetCell(targetNextMove.RowNumber, targetNextMove.ColumnNumber) && LegalMoves.Contains(targetNextMove);
        }
        public CellState SetCellState(int rowNum, int colNum)
        {
            Cell c = TheGrid[rowNum, colNum];
            if (rowNum == 0 && colNum == 0 && MoveCount == 0)
            {
                // Starting position cell
                return CellState.StartMark;
            }
            else if (c.LegalNextMove)
            {
                return CellState.LegalMove;
            }

            else if (IsTargetCell(rowNum, colNum))
            {
                if (c.CurrentlyOccupied)
                {
                    // Indicating that Target cell is reached. Game complete.
                    return CellState.TargetReached;
                }
                else
                {
                    return CellState.TargetMark;
                }
            }
            else if (c.CurrentlyOccupied)
            {
                // Cell is selected
                return CellState.Selected;
            }
            else
            {
                return CellState.Default;
            }
        }

        public string SwitchToPreviousMove(string pressed)
        {
            string output = "";
            switch (pressed)
            {
                case "Undo":
                    //Console.Write("UndoMove");
                    //RecentCell = TheGrid[currentCell.RowNumber, currentCell.ColumnNumber]

                    output = $"{PreviousCell.RowNumber} {PreviousCell.ColumnNumber}";
                    //output = "UndoMove";
                    break;
                case "Redo":
                    //Console.Write("RedoMove");
                    output = $"{ActiveCell.RowNumber} {ActiveCell.ColumnNumber}";
                    break;
            }
            return output;
        }
        public string ResetBoard()
        {
            return "ResetBoard";
        }
    }
}
