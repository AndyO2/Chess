using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class King : ChessPiece
    {
        public bool hasMoved;
        public King(char color, int row, int column) : base(color, row, column)
        {
            hasMoved = false;
        }
    }
}
