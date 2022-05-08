using System;
using System.Collections.Generic;
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
        /// The square that this piece is on
        /// </summary>
        public Square location;

        /// <summary>
        /// Each piece has its own definition of a legal move so those pieces must implement this
        /// </summary>
        /// <param name="requestedRow">The row we want to move to</param>
        /// <param name="requestedColumn">The column we want to move to</param>
        /// <returns></returns>
        public abstract bool MoveIsLegal(int requestedRow, int requestedColumn);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestedRow"></param>
        /// <param name="requestedColumn"></param>
        public void Move(int requestedRow, int requestedColumn)
        {
            if(MoveIsLegal(requestedRow, requestedColumn))
            {
                location.Row = requestedRow;
                location.Col = requestedColumn;
            }
        }

        /// <summary>
        /// ChessPiece constructor that gives a color, and location
        /// </summary>
        /// <param name="color"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public ChessPiece(char color, int row, int column)
        {
            Color = color;
            location = new Square(row, column);
        }
    }
}
