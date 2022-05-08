namespace Chess
{
    public class Square
    {
        /// <summary>
        /// Indicates the row number on the chess board (0-7)
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Indicates the column on the chess board (0-7)
        /// </summary>
        public int Col { get; set; }

        public ChessPiece occupant;

        /// <summary>
        /// Square constructor
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public Square(int row, int col)
        {
            Row = row;
            Col = col;
        }

        /// <summary>
        /// Determines if this square is currently occupied
        /// </summary>
        /// <returns></returns>
        public bool isOccupiecd()
        {
            return true;
        }
    }
}