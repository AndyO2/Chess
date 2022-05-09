using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Pawn : ChessPiece
    {
        /// <summary>
        /// Pawn constructor that simply calls on ChessPiece base class constructor
        /// </summary>
        /// <param name="color"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public Pawn(char color, int row, int column) : base(color, row, column)
        {
        }
         
        /// <summary>
        /// Method that determines if a move is legal for this piece
        /// </summary>
        /// <param name="requestedRow"></param>
        /// <param name="requestedColumn"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override bool MoveIsLegal(int requestedRow, int requestedColumn)
        {
            throw new NotImplementedException();
        }
    }
}
