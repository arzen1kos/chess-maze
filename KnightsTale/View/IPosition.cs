using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsTaleMVC.View
{
    public interface IPosition
    {
        /// <summary>
        /// Gets the row number of the position.
        /// </summary>
        //int Row { get; }

        /// <summary>
        /// Gets the column number of the position.
        /// </summary>
        //int Column { get; }
        string GetCoordinate(string prompt, string tip);// Ask for coordinate (x/y)
        //ConsoleKeyInfo GetCoordinate(string prompt, string tip);// Ask for coordinate (x/y)

    }
}
