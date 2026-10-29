using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMazeConsoleApp
{
    class Board
    {
        private int board_dimension = 5;
        /*
        internal string GenerateCell()
        {
            string cell = "";
        }
        */

        public string GenerateBoard() //int size
        {
            //board_dimension = size;
            //return board_dimension;
            string result = "";

            // Print the board
            int rows = board_dimension;
            int columns = board_dimension;

            string border = "+---";
            string sides = "|   ";

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    result += border;
                }

                result += "+\n";

                for (int j = 0; j < columns; j++)
                {
                    result += sides;
                }
                result += "|\n";
            }
            // last row
            for (int j = 0; j < columns; j++)
            {
                result += border;
            }
            result += "+\n";

            return result;
        }
        
    }
}
