namespace Chess
{
    public class ChessBoard
    {
        /// <summary>
        /// The chessboard that contains squares
        /// </summary>
        public Square[,] chessBoard;

        /// <summary>
        /// Size of the square board (row or column length)
        /// </summary>
        public int Size { get; set; }

        public ChessBoard(int boardSize)
        {
            Size = boardSize;
            CreateBoard(boardSize);
        }

        public void CreateBoard(int boardSize)
        {
            //Create a new chessboard indicated by the board size
            chessBoard = new Square[boardSize,boardSize];

            for(int i = 0; i < boardSize; i++)
            {
                for(int j = 0; j < boardSize; j++)
                {
                    chessBoard[i, j] = new Square(i, j, null);
                }
            }
        }
    }
}