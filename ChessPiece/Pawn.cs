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
        /// Keeps track if this is the first move this pawn makes (double square forward)
        /// </summary>
        public bool firstMove;

        /// <summary>
        /// Pawn constructor that simply calls on ChessPiece base class constructor
        /// </summary>
        /// <param name="color"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public Pawn(char color, int row, int column) : base(color, row, column)
        {
            firstMove = true;
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
            if(requestedRow < 0 || requestedRow > 7 || requestedColumn < 0 || requestedColumn > 7)
            {
                return false;
            }

            if (firstMove)
            {

            }

            return true;
        }
    }
}
