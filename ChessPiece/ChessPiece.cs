using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class ChessPiece
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

        public void SetLocation(int column, int row)
        {
            Location = new Point(column, row);
        }

        /// <summary>
        /// Tells if this piece is black
        /// </summary>
        /// <returns></returns>
        public bool IsBlack()
        {
            return Color == 'B';
        }

        /// <summary>
        /// Tells if this piece is white
        /// </summary>
        /// <returns></returns>
        public bool IsWhite()
        {
            return Color == 'W';
        }
    }
}
