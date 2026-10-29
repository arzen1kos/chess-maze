using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsTale
{
    class Program
    {
        static Board myBoard = new Board(8);

        static void Main(string[] args)
        {
            // show empty chess board
            printBoard(myBoard);

            // ask user for x and y coordinates to place the knight
            Cell currentCell = setCurrentCell();
            currentCell.CurrentlyOccupied = true;

            //calculate all legal moves for the piece
            myBoard.MarkNextLegalMove(currentCell); //Knight

            // print the board
            printBoard(myBoard);

            // wait for another enter key before ending the program
            Console.ReadLine();

        }

        private static Cell setCurrentCell()
        {
            while (true)
            {
                Console.WriteLine($"Enter the current row number (0-{myBoard.Size - 1})");
                if (!int.TryParse(Console.ReadLine(), out int currentRow)
                    || currentRow < 0
                    || currentRow >= myBoard.Size)
                {
                    Console.WriteLine("Enter a row within the board.");
                    continue;
                }

                Console.WriteLine($"Enter the current column number (0-{myBoard.Size - 1})");
                if (!int.TryParse(Console.ReadLine(), out int currentColumn)
                    || currentColumn < 0
                    || currentColumn >= myBoard.Size)
                {
                    Console.WriteLine("Enter a column within the board.");
                    continue;
                }

                return myBoard.TheGrid[currentRow, currentColumn];
            }
        }

        private static void printBoard(Board myBoard)
        {
            for (int a = 0; a < myBoard.Size; a++)
            {
                Console.Write(" | ");
                Console.Write($" {a} ");
            }
            Console.WriteLine();
            Console.WriteLine("======================================================");

            for (int i = 0; i < myBoard.Size; i++)
            {
                for (int h = 0; h < myBoard.Size; h++)
                {
                    Console.Write(" + ---");
                }
                Console.WriteLine();
                
                for (int j = 0; j < myBoard.Size; j++)
                {
                    // print the chess board to console. Use X for the piece location and  + for legal moves. Use dot (.) for empty space
                    Cell c = myBoard.TheGrid[i, j];
                    if (c.CurrentlyOccupied == true)
                    {
                        Console.Write(" |  X ");
                    }
                    else if (c.LegalNextMove == true)
                    {
                        //Console.Write(" | "); 
                        Console.Write(" |  # ");
                    }
                    else
                    {
                        if (i == 0 && j == 0)
                        {
                            Console.Write(" |  H ");
                        }
                        else if (i == myBoard.Size - 1 && j == myBoard.Size - 1)
                        {
                            Console.Write(" |  O ");
                        }
                        else
                        {
                            Console.Write(" |    ");
                        }
                    }
                }
                Console.Write(" | ");
                Console.WriteLine(i);
            }
            Console.WriteLine("======================================================");
        }
    }
}
