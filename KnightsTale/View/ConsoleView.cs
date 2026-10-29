//using KnightsTaleMVC;
using KnightsTaleMVC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KnightsTaleMVC.View
{
    public class ConsoleView : IBoard, IPosition, IGame
    {
        public void Start()
        {
            Console.Clear();
        }

        public void PrintHorizontalBoarder(char design, int lengthFactor)
        {
            int repeater = lengthFactor * 7;
            string border = new string(design, repeater);
            Console.WriteLine(border);
        }
        public void PrintInnerGridBorder()
        {
            Console.Write(" + ---");
        }
        public void PrintCell(char piece)
        {
            Console.Write($" |  {piece} ");
        }
        public void PrintGridNumber(int count, string lead)
        {
            Console.Write($"{lead} {count} ");
        }
        public void GameComplete()
        {
            //Console.Clear();
            string finish = "\n-- WELL DONE! GAME COMPLETE --";
            Console.WriteLine(finish);
        }
        public void Stop()
        {
            // End game
            string exit = "Press any key to exit...";
            Console.WriteLine(exit);
            //var input = Console.ReadKey(true);
            Console.ReadKey(true);
            Environment.Exit(3);
        }
        public ConsoleKeyInfo GetOption(string prompt)
        {
            Console.WriteLine(prompt);
            var input = Console.ReadKey(true);
            return input;
        }
        public string GetCoordinate(string prompt, string tip)
        {
            Console.WriteLine(prompt);
            Console.WriteLine($"Tip: You can find the number on the <<{tip.ToUpper()}>> side of the board.");

            string inputString = String.Empty;
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    // Are there any characters to erase?
                    if (inputString.Length >= 1)
                    {
                        // Determine where we are in the console buffer.
                        int cursorCol = Console.CursorLeft - 1;
                        int oldLength = inputString.Length;
                        int extraRows = oldLength / 80;

                        inputString = inputString.Substring(0, oldLength - 1);
                        Console.CursorLeft = 0;
                        Console.CursorTop -= extraRows;
                        Console.Write(inputString + new String(' ', oldLength - inputString.Length));
                        Console.CursorLeft = cursorCol;
                    }
                    continue;
                }
                // Handle Escape key.
                if (keyInfo.Key == ConsoleKey.Escape) break;

                // Handle key by adding it to input string.
                Console.Write(keyInfo.KeyChar);
                inputString += keyInfo.KeyChar;

                if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    inputString = "Undo";
                    break;
                }
                if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    inputString = "Redo";
                    break;
                }
            }
            while (keyInfo.Key != ConsoleKey.Enter);

            var output = inputString;
            if (String.IsNullOrEmpty(inputString))
            {
                output = "<nothing>";
            }
            return output;
        }
        public void DisplayCurrentMoveCount(int count)
        {
            Console.WriteLine($"Current move count: {count}");
        }
        public void DisplayWarning(string info)
        {
            Console.WriteLine($"\nWARNING: <<{info} Please Try again. >>>\n");
        }
        public void PrintHeader()
        {
            Console.WriteLine("KNIGHT'S TALE: S = Start; E = End");
            Console.WriteLine("Move the piece (Knight) from the starting point (S) to reach the end point (E).");
            Console.WriteLine(
                "\nControls:\n" +
                "TAB / RIGHT ARROW -> undo move\n" +
                "BACKSPACE / LEFT ARROW -> redo move\n"
            );
        }
    }
}
