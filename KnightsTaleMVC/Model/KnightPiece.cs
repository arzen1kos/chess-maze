using KnightsTaleMVC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsTaleMVC
{
    class Piece
    {
        public (int Row, int Column)[] movement { get; set; }
        public Piece(string name)
        {
            switch (name)
            {
                case "Knight":
                    movement = KnigtMoves;
                    break;
            }
        }
        public static (int Row, int Column)[] KnigtMoves =
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
    }
}
