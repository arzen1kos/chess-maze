using KnightsTaleMVC.Model;
using System;

namespace KnightsTaleMVC.View
{
    public class OutputConsoleView : IBoard, IPosition, IGame
    {
        public void Start()
        {
            if (!Console.IsOutputRedirected)
            {
                Console.Clear();
            }
        }

        public void PrintHorizontalBoarder(char design, int lengthFactor)
        {
            Console.WriteLine(new string(design, lengthFactor * 7));
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

        public void Print(string value)
        {
            Console.WriteLine(value);
        }

        public void GameComplete(int count)
        {
            Console.WriteLine($"\n-- Congratulations! Maze completed in {count} steps --");
        }

        public void Stop()
        {
            Console.WriteLine("Press any key to exit...");
            if (!Console.IsInputRedirected)
            {
                Console.ReadKey(true);
            }
        }

        public ConsoleKeyInfo GetOption(string prompt)
        {
            Console.WriteLine(prompt);
            if (Console.IsInputRedirected)
            {
                string command = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(command))
                {
                    return new ConsoleKeyInfo('\0', ConsoleKey.Escape, false, false, false);
                }

                if (string.Equals(command.Trim(), "Space", StringComparison.OrdinalIgnoreCase))
                {
                    return new ConsoleKeyInfo(' ', ConsoleKey.Spacebar, false, false, false);
                }

                if (Enum.TryParse(command.Trim(), true, out ConsoleKey key))
                {
                    return new ConsoleKeyInfo('\0', key, false, false, false);
                }

                return new ConsoleKeyInfo('\0', ConsoleKey.NoName, false, false, false);
            }

            return Console.ReadKey(true);
        }

        public string GetCoordinate(string axis, string direction, string tip)
        {
            Console.WriteLine($"Enter the current {axis} number ({direction})");
            Console.WriteLine($"Tip: You can find the number on the <<{tip.ToUpperInvariant()}>> side of the board.");

            if (Console.IsInputRedirected)
            {
                return Console.ReadLine()?.Trim() ?? string.Empty;
            }

            var input = string.Empty;
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    return input;
                }

                if (key.Key == ConsoleKey.Escape)
                {
                    return string.Empty;
                }

                if (key.Key == ConsoleKey.LeftArrow)
                {
                    return "Undo";
                }

                if (key.Key == ConsoleKey.RightArrow)
                {
                    return "Redo";
                }

                if (key.Key == ConsoleKey.Backspace)
                {
                    if (input.Length > 0)
                    {
                        input = input.Substring(0, input.Length - 1);
                        Console.Write("\b \b");
                    }
                    continue;
                }

                if (!char.IsControl(key.KeyChar))
                {
                    input += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
            }
        }

        public void DisplayCurrentMoveCount(int count)
        {
            Console.WriteLine($"Current move count: {count}");
        }

        public void DisplayWarning(string info)
        {
            Console.WriteLine($"\nWARNING: {info} Please try again.\n");
        }

        public void PrintHeader()
        {
            Console.WriteLine("KNIGHT'S TALE: S = Start; E = End");
            Console.WriteLine("Move the piece (Knight) from the starting point (S) to reach the end point (E).");
            Console.WriteLine("Left arrow: undo move; Right arrow: redo move; End: reset.");
        }
    }
}
