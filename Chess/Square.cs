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

        /// <summary>
        /// The piece currently at this square
        /// </summary>
        public ChessPiece occupant;

        /// <summary>
        /// Square constructor
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public Square(int row, int col, ChessPiece? occupant)
        {
            Row = row;
            Col = col;
            this.occupant = occupant;
        }

        /// <summary>
        /// This is called when a new piece occupies this square
        /// </summary>
        /// <param name="newOccupant">the new chess piece that occupies this square</param>
        public void SetOccupant(ChessPiece newOccupant)
        {
            occupant = newOccupant;
        }

        /// <summary>
        /// Determines if this square is currently occupied
        /// </summary>
        /// <returns></returns>
        public bool IsOccupiecd()
        {
            return true;
        }
    }
}