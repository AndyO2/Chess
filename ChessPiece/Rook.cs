﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Rook : ChessPiece
    {
        public bool hasMoved;

        public Rook(char color, int row, int column) : base(color, row, column)
        {
            hasMoved = false;
        }

        public override bool MoveIsLegal(int requestedRow, int requestedColumn)
        {
            throw new NotImplementedException();
        }
    }
}
