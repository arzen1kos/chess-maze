using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsTaleMVC.Model
{
    public class Cell
    {
        public bool CurrentlyOccupied { get; set; }
        public bool LegalNextMove { get; set; }

        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
        public Cell(int x, int y)
        {
            RowNumber = x;
            ColumnNumber = y;
        }
    }
}
