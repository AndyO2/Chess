using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public abstract class ChessPiece
    {
        /// <summary>
        /// The color of the piece indicated by 'w' for white or 'b' for black
        /// </summary>
        public char Color { get; set; }

        /// <summary>
        /// Store the location of this piece (row, col)
        /// </summary>
        public Point Location { get; set; }

        /// <summary>
        /// Each piece has its own definition of a legal move so those pieces must implement this
        /// </summary>
        /// <param name="requestedRow">The row we want to move to</param>
        /// <param name="requestedColumn">The column we want to move to</param>
        /// <returns></returns>
        public abstract bool MoveIsLegal(int requestedColumn, int requestedRow);

        /// <summary>
        /// Tells if this piece is black
        /// </summary>
        /// <returns></returns>
        public bool isBlack()
        {
            return Color == 'B';
        }

        /// <summary>
        /// Tells if this piece is white
        /// </summary>
        /// <returns></returns>
        public bool isWhite()
        {
            return Color == 'W';
        }

        /// <summary>
        /// ChessPiece constructor that gives a color, and location
        /// </summary>
        /// <param name="color"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public ChessPiece(char color, int column, int row)
        {
            Color = color;
            Location = new Point(column, row);
        }
    }
}
