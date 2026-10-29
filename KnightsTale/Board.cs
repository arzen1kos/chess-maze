using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsTale
{
    public class Board
    {
        public int Size { get; set; }
        public Cell [,] TheGrid { get; set; }

        public Board(int s)
        {
            // initialise the size of the chess board
            Size = s;

            // create a new 2D array of type cell
            TheGrid = new Cell[Size, Size];

            // fill 2D array with new cells
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    TheGrid[i, j] = new Cell(i, j);
                }
            }
        }
        internal bool IsSafe(int x, int y)
        {
           bool IsInRange = x >= 0 && x < Size && y >= 0 && y < Size;
            //bool IsValidType = (x is int && y is int);
            //return (IsInRange);
            return IsInRange;
        }

        
        private static readonly (int RowOffset, int ColumnOffset)[] KnightMoves =
        {
            (1,2),
            (1,-2),
            (-1,2),
            (-1,-2),
            (2,1),
            (2,-1),
            (-2,1),
            (-2,-1)
        };

        private void UpdateLegalMoves(Cell currentCell)
        {
            foreach (var move in KnightMoves)
            {
                int newRow = currentCell.RowNumber + move.RowOffset;
                int newColumn = currentCell.ColumnNumber+ move.ColumnOffset;

                if (IsSafe(newRow, newColumn))
                {
                    TheGrid[newRow, newColumn].LegalNextMove = true;
                }
            }
        }
        
        public void MarkNextLegalMove(Cell currentCell) //string chessPiece
        {
            // clear all previous legal moves
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    TheGrid[i, j].LegalNextMove = false;
                    TheGrid[i, j].CurrentlyOccupied = false;
                }
            }

            // find all legal moves and mark as legal
            UpdateLegalMoves(currentCell);

            TheGrid[currentCell.RowNumber, currentCell.ColumnNumber].CurrentlyOccupied = true;
        }
    }
}
