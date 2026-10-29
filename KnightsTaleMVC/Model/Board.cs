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
        internal List<Cell> MoveSequence { get; private set; }

        // Initialise recent cell value
        public Cell ActiveCell = new Cell(0, 0);
        public Cell PreviousCell = new Cell(0, 0);
        
        // Initialise Move count
        public int MoveCount = 0;
        internal int CurrentCellIndex = -1;

        public Board(int size)
        {
            Size = size;
            TheGrid = new Cell[Size, Size];
            LegalMoves = new List<Cell>();
            MoveSequence = new List<Cell>();
            Boundary = Size - 1;
            knightPiece = new Piece("Knight");
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    TheGrid[i, j] = new Cell(i, j);
                }
            }
            ActiveCell = TheGrid[0, 0];
            UpdateLegalMoves(ActiveCell);
        }
        internal bool IsTargetCell(int row, int col)
        {
            /// Validates if selected cell is the goal(i.e. last cell on the board)
            return row == Boundary && col == Boundary;
        }
        public void WaitToRestart(int milliseconds = 1000)
        { 
            Thread.Sleep(milliseconds);
        }
        public void StoreInitCell()
        {
            // Preserve the initial cell before it will be overwritten by new cell (input by usr)
            PreviousCell = TheGrid[ActiveCell.RowNumber, ActiveCell.ColumnNumber];
        }
        public string EvaluateSelectCell(int selectedRow, int selectedColumn)
        {
            if (!IsInBound(selectedRow, selectedColumn))
            {
                return "Out of Range";
            }

            var selectedCell = TheGrid[selectedRow, selectedColumn];
            if (selectedCell == ActiveCell)
            {
                return "Repeated Move";
            }

            if (!IsLegalMove(selectedCell))
            {
                return "Illegal Move";
            }

            PreviousCell = ActiveCell;
            if (MoveSequence.Count > MoveCount)
            {
                MoveSequence.RemoveRange(MoveCount, MoveSequence.Count - MoveCount);
            }

            ResetCellState();
            AltSelectedCellState(selectedRow, selectedColumn);
            MoveSequence.Add(ActiveCell);
            CurrentCellIndex = MoveSequence.Count - 1;
            MoveCount = MoveSequence.Count;
            LegalMoves.Clear();
            UpdateLegalMoves(ActiveCell);
            return null;

        }
        public void AltSelectedCellState(int selectedRow, int selectedColumn)
        {
            ActiveCell = TheGrid[selectedRow, selectedColumn];
            TheGrid[selectedRow, selectedColumn].CurrentlyOccupied = true;
        }
        public void ResetCellState()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    // Clearing any cell's previous legal moves
                    TheGrid[i, j].LegalNextMove = false;
                    // Marking all cells as blank
                    TheGrid[i, j].CurrentlyOccupied = false;
                }
            }
        }
        internal void UpdateLegalMoves(Cell currentCell)
        {
            ///<summary>
            /// Generate all moves within range of the selected cell and add it to the temporary list if legal.
            ///</summary>
            foreach (var (offsetRow, offsetColumn) in knightPiece.movement)
            {
                // Getting all the offset cells from KnightPiece property: Movement
                var newRow = currentCell.RowNumber + offsetRow;
                int newColumn = currentCell.ColumnNumber + offsetColumn;

                if (IsInBound(newRow, newColumn))
                {
                    // Mark as legal
                    MarkNextLegalMove(newRow, newColumn);
                }
            }
        }
        internal bool IsInBound(int x, int y)
        {
            //Validates if next move is within board scope
            return x >= 0 && x < Size && y >= 0 && y < Size; ;
        }
        internal bool IsLegalMove(Cell nextMove)
        {
            return LegalMoves.Contains(nextMove);
        }
        //public void AddLegalMove(Cell selectedCell)
        public void MarkNextLegalMove(int newRow, int newColumn)
        {
            /// <summary>
            /// Makes the selected cell to be the current/active cell
            /// </summary>
            var selectedCell = TheGrid[newRow, newColumn];
            selectedCell.LegalNextMove = true;
            LegalMoves.Add(selectedCell);
        }
        public enum CellState
        {
            TargetReached = '\u263a',
            Selected = 'H',
        }

        public enum CellType
        {
            StartMark = 'S',
            TargetMark = 'E',
            LegalMove = '#',
            Default = ' ',
        }

        public char MarkCell(int rowNum, int colNum)
        {
            /// <summary>
            /// Defines what will be the character inside the cell
            /// </summary>
            char output;
            Cell c = TheGrid[rowNum, colNum];
            if (rowNum == 0 && colNum == 0 && MoveCount == 0)
            {
                // Starting position cell
                output = (char)CellType.StartMark;
            }
            else if (c.LegalNextMove)
            {
                if (IsTargetCell(ActiveCell.RowNumber, ActiveCell.ColumnNumber))
                {
                    // If the target cell is reached, no need to display legal moves: ' '
                    output = (char)CellType.Default;
                }
                else
                {
                    // #
                    output = (char)CellType.LegalMove;
                }
            }

            else if (IsTargetCell(rowNum, colNum))
            {
                if (c.CurrentlyOccupied)
                {
                    // Indicating that Target cell is reached. Game complete.
                    output = (char)CellState.TargetReached;
                }
                else
                {
                    output = (char)CellType.TargetMark;
                }
            }
            else if (c.CurrentlyOccupied)
            {
                // Cell is selected
                output = (char)CellState.Selected;
            }
            else
            {
                output = (char)CellType.Default;
            }
            return output;
        }

        internal int GetLastMoveIndex()
        {
            return CurrentCellIndex;
        }

        public string ListMoves()
        {
            string s = "{";
            foreach (Cell c in MoveSequence)
            {
                s += $"[{c.RowNumber},{c.ColumnNumber}]  ";
            }
            s += "}";
            return s;
        }
        public string SwitchToPreviousMove(string pressed)
        {
            if (pressed == "Undo")
            {
                if (CurrentCellIndex < 0)
                {
                    return "There are no moves to undo.";
                }

                CurrentCellIndex--;
                MoveCount = CurrentCellIndex + 1;
                RestoreCurrentPosition();
                return "Move undone.";
            }

            if (pressed == "Redo")
            {
                if (CurrentCellIndex + 1 >= MoveSequence.Count)
                {
                    return "There are no moves to redo.";
                }

                CurrentCellIndex++;
                MoveCount = CurrentCellIndex + 1;
                RestoreCurrentPosition();
                return "Move redone.";
            }

            return "Unknown move command.";
        }
        public string ResetBoard()
        {
            MoveSequence.Clear();
            CurrentCellIndex = -1;
            MoveCount = 0;
            ActiveCell = TheGrid[0, 0];
            PreviousCell = ActiveCell;
            RestoreCurrentPosition();
            return "Board reset.";
        }

        private void RestoreCurrentPosition()
        {
            ResetCellState();
            ActiveCell = CurrentCellIndex < 0
                ? TheGrid[0, 0]
                : MoveSequence[CurrentCellIndex];
            ActiveCell.CurrentlyOccupied = true;
            LegalMoves.Clear();
            UpdateLegalMoves(ActiveCell);
        }
    }
}
