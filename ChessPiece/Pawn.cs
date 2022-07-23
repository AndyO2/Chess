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
    }
}
