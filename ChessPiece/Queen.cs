﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    internal class Queen : ChessPiece
    {
        public Queen(char color, int row, int column) : base(color, row, column)
        {
        }

        public override bool MoveIsLegal(int requestedRow, int requestedColumn)
        {
            throw new NotImplementedException();
        }
    }
}
