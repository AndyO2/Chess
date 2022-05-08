using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoard
{
    internal class ChessBoard
    {
        /// <summary>
        /// The size of the square board (Size squared is the total number of Squares)
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// The chess board
        /// </summary>
        public Square[,] Board { get; set; }

        public ChessPiece occupant;

        /// <summary>
        /// Chess board constructor
        /// </summary>
        /// <param name="size">The size we want the chess board</param>
        public ChessBoard(int size)
        {
            //Create a new board
            Board = new Square[size,size];

            //Update the size of the board
            Size = size;

            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    Board[i, j] = new Square(i, j);
                }
            }
        }
    }
}
