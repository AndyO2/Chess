using Chess;
using System.Diagnostics;

namespace ChessGUI
{
    public partial class Chess : Form
    {
        /// <summary>
        /// The chess board
        /// </summary>
        private Square[,] chessBoard;

        // class member array of Panels to track chessboard tiles
        private Panel[,] chessBoardPanels;

        /// <summary>
        /// Indicates whose turn it is
        /// </summary>
        private bool whiteTurn;

        /// <summary>
        /// Keeps track of the current square the user has clicked and wants to move if possible
        /// </summary>
        private Square? currSquareClicked;

        /// <summary>
        /// The size of the tile
        /// </summary>
        private const int tileSize = 60;

        /// <summary>
        /// The number of tiles we want
        /// </summary>
        private const int gridSize = 8;

        /// <summary>
        /// Dark color on chess board
        /// </summary>
        private Color clr1 = Color.BurlyWood;

        /// <summary>
        /// Light color on board
        /// </summary>
        private Color clr2 = Color.White;

        public Chess()
        {
            //The chess board contains the status of the game
            chessBoard = new Square[8, 8];
            //The panels portrays the chessBoard onto the gui using panels
            chessBoardPanels = new Panel[8, 8];

            CreateBoard(8);

            whiteTurn = true;

            this.Paint += Draw_Board;
            DoubleBuffered = true;

            InitializeComponent();
        }

        /// <summary>
        /// Helper method that creates the chessBoard
        /// </summary>
        /// <param name="boardSize"></param>
        private void CreateBoard(int boardSize)
        {
            for (int column = 0; column < boardSize; column++)
            {
                for (int row = 0; row < boardSize; row++)
                {
                    // create new Panel control which will be one chess board tile
                    var newPanel = new Panel
                    {
                        Size = new Size(tileSize, tileSize),
                        //The +100 shifts the overall board down to (100,100) top left corner
                        Location = new Point(tileSize * column + 100, tileSize * row + 100),

                        BackgroundImageLayout = ImageLayout.Center,
                    };

                    newPanel.Click += Square_Click;

                    //Event handler when a panel is hovered over. Only do something if the square is occupied
                    newPanel.MouseHover += Square_Hover;

                    // add to our 2d array of panels for future use
                    chessBoardPanels[column, row] = newPanel;

                    // add to Form's Controls so that they show up
                    Controls.Add(newPanel);

                    // color the backgrounds
                    if (column % 2 == 0)
                    {
                        newPanel.BackColor = row % 2 != 0 ? clr1 : clr2;
                    }
                    else
                    {
                        newPanel.BackColor = row % 2 != 0 ? clr2 : clr1;
                    }

                    //White Pawn
                    if (row == 6)
                    {
                        chessBoard[column, row] = new Square(column, row, new Pawn('W', column, row));
                    }
                    //Black Pawn
                    else if (row == 1)
                    {
                        chessBoard[column, row] = new Square(column, row, new Pawn('B', column, row));
                    }
                    //White King
                    else if (row == 7 && column == 3)
                    {
                        chessBoard[column, row] = new Square(column, row, new King('W', column, row));
                    }
                    //Black King
                    else if (row == 0 && column == 3)
                    {
                        chessBoard[column, row] = new Square(column, row, new King('B', column, row));
                    }
                    //White Queen
                    else if (row == 7 && column == 4)
                    {
                        chessBoard[column, row] = new Square(column, row, new Queen('W', column, row));
                    }
                    //Black Queen
                    else if (row == 0 && column == 4)
                    {
                        chessBoard[column, row] = new Square(column, row, new Queen('B', column, row));
                    }
                    //White Rook
                    else if (row == 7 && column == 0 || row == 7 && column == 7)
                    {
                        chessBoard[column, row] = new Square(column, row, new Rook('W', column, row));
                    }
                    //Black Rook
                    else if (row == 0 && column == 0 || row == 0 && column == 7)
                    {
                        chessBoard[column, row] = new Square(column, row, new Rook('B', column, row));
                    }
                    //White Bishop
                    else if (row == 7 && column == 2 || row == 7 && column == 5)
                    {
                        chessBoard[column, row] = new Square(column, row, new Bishop('W', column, row));
                    }
                    //Black Bishop
                    else if (row == 0 && column == 2 || row == 0 && column == 5)
                    {
                        chessBoard[column, row] = new Square(column, row, new Bishop('B', column, row));
                    }
                    //White Knight
                    else if (row == 7 && column == 1 || row == 7 && column == 6)
                    {
                        chessBoard[column, row] = new Square(column, row, new Knight('W', column, row));
                    }
                    //Black Knight
                    else if (row == 0 && column == 1 || row == 0 && column == 6)
                    {
                        chessBoard[column, row] = new Square(column, row, new Knight('B', column, row));
                    }
                    //Empty Square
                    else
                    {
                        chessBoard[column, row] = new Square(column, row, null);
                    }
                }
            }
        }

        /// <summary>
        /// Draws the status of the board
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Draw_Board(object? sender, PaintEventArgs e)
        {
            // double for loop to handle all rows and columns
            for (var column = 0; column < gridSize; column++)
            {
                for (var row = 0; row < gridSize; row++)
                {
                    // Access the occupant of this square if there is one (can be null)
                    ChessPiece? occupant = chessBoard[column, row].GetOccupant();

                    if (occupant is Pawn)
                    {
                        if (occupant.isWhite())
                        {
                            chessBoardPanels[column,row].BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\WhitePawn.png");
                        }
                        else
                        {
                            chessBoardPanels[column, row].BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\BlackPawn.png");
                        }
                    }
                    else if (occupant is King)
                    {
                        if (occupant.isWhite())
                        {
                            chessBoardPanels[column, row].BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\WhiteKing.png");
                        }
                        else
                        {
                            chessBoardPanels[column, row].BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\BlackKing.png");
                        }
                    }
                    else if (occupant is Queen)
                    {
                        if (occupant.isWhite())
                        {
                            chessBoardPanels[column, row].BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\WhiteQueen.png");
                        }
                        else
                        {
                            chessBoardPanels[column, row].BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\BlackQueen.png");
                        }
                    }
                    else if (occupant is Rook)
                    {
                        if (occupant.isWhite())
                        {
                            chessBoardPanels[column, row].BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\WhiteRook.png");
                        }
                        else
                        {
                            chessBoardPanels[column, row].BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\BlackRook.png");
                        }
                    }
                    else if (occupant is Bishop)
                    {
                        if (occupant.isWhite())
                        {
                            chessBoardPanels[column, row].BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\WhiteBishop.png");
                        }
                        else
                        {
                            chessBoardPanels[column, row].BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\BlackBishop.png");
                        }
                    }
                    else if (occupant is Knight)
                    {
                        if (occupant.isWhite())
                        {
                            chessBoardPanels[column, row].BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\WhiteKnight.png");
                        }
                        else
                        {
                            chessBoardPanels[column, row].BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\BlackKnight.png");
                        }
                    }
                    else
                    {
                        chessBoardPanels[column, row].BackgroundImage = null;
                    }
                }
            }
        }

        /// <summary>
        /// Helper method that handles piece moving as well as valid/invalid moves
        /// </summary>
        private void MovePiece(int requestedColumn, int requestedRow)
        {
            ChessPiece pieceToMove = currSquareClicked.GetOccupant();

            if(MoveIsLegal(requestedColumn, requestedRow))
            {
                //Update the location of the piece on the chess board
                chessBoard[requestedColumn, requestedRow].occupant = currSquareClicked.occupant;

                //Update the piece's location
                chessBoard[requestedColumn, requestedRow].occupant.Location = new Point(requestedColumn,requestedRow);

                chessBoard[currSquareClicked.Col, currSquareClicked.Row].occupant = null;

                currSquareClicked = null;

                if (whiteTurn)
                {
                    whiteTurn = false;
                }
                else
                {
                    whiteTurn = true;
                }
            }
            else
            {
                Debug.WriteLine("Invalid Move");
            }
        }

        /// <summary>
        /// Helper method that determines if a move is legal
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="requestedColumn"></param>
        /// <param name="requestedRow"></param>
        /// <returns></returns>
        private bool MoveIsLegal(int requestedColumn, int requestedRow)
        {
            ChessPiece piece = currSquareClicked.GetOccupant();

            if(piece is Pawn)
            {
                if (piece.isWhite())
                {
                    //CAN NEVER OCCUPY A SQUARE THAT IS ALREADY OCCUPIED BY SAME COLOR
                    if (chessBoard[requestedColumn, requestedRow].IsOccupiedByWhite())
                    {
                        return false;
                    }

                    //CANNOT MOVE BACKWARDS
                    if(requestedRow > piece.Location.Y)
                    {
                        return false;
                    }

                    //FIRST MOVE
                    if (piece.Location.Y == 6)
                    {
                        if (Math.Abs(requestedRow - piece.Location.Y) <= 2 && requestedColumn - piece.Location.X == 0)
                        {
                            return true;
                        } 
                    }

                    //REGULAR MOVE FORWARD
                    if(Math.Abs(requestedRow - piece.Location.Y) == 1 && requestedColumn - piece.Location.X == 0)
                    {
                        return true;
                    }

                    //TAKE PIECE DIAGONAL
                    if(chessBoard[requestedColumn, requestedRow].IsOccupiedByBlack())
                    {
                        if(Math.Abs(requestedColumn - piece.Location.X) == 1 && Math.Abs(requestedRow - piece.Location.Y) == 1)
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    //CAN NEVER OCCUPY A SQUARE THAT IS ALREADY OCCUPIED BY SAME COLOR
                    if (chessBoard[requestedColumn, requestedRow].IsOccupiedByBlack())
                    {
                        return false;
                    }

                    //CANNOT MOVE BACKWARDS
                    if (requestedRow < piece.Location.Y)
                    {
                        return false;
                    }

                    //FIRST MOVE DOUBLE SPACE
                    if (piece.Location.Y == 1)
                    {
                        if (Math.Abs(requestedRow - piece.Location.Y) <= 2 && requestedColumn - piece.Location.X == 0)
                        {
                            return true;
                        }
                    }

                    //REGULAR MOVE FORWARD
                    if (Math.Abs(requestedRow - piece.Location.Y) == 1 && requestedColumn - piece.Location.X == 0)
                    {
                        return true;
                    }

                    //TAKE PIECE DIAGONAL
                    if (chessBoard[requestedColumn, requestedRow].IsOccupiedByWhite())
                    {
                        if (Math.Abs(requestedColumn - piece.Location.X) == 1 && Math.Abs(requestedRow - piece.Location.Y) == 1)
                        {
                            return true;
                        }
                    }
                }
            }
            else if(piece is Knight)
            {
                if (piece.isWhite())
                {
                    //CAN NEVER OCCUPY A SQUARE THAT IS ALREADY OCCUPIED BY SAME COLOR
                    if (chessBoard[requestedColumn, requestedRow].IsOccupiedByWhite())
                    {
                        return false;
                    }

                    //REGULAR L-Shaped Move
                    if(Math.Abs(requestedColumn - piece.Location.X) == 2 && Math.Abs(requestedRow - piece.Location.Y) == 1)
                    {
                        return true;
                    }
                    else if (Math.Abs(requestedColumn - piece.Location.X) == 1 && Math.Abs(requestedRow - piece.Location.Y) == 2)
                    {
                        return true;
                    }
                }
                else
                {
                    //CAN NEVER OCCUPY A SQUARE THAT IS ALREADY OCCUPIED BY SAME COLOR
                    if (chessBoard[requestedColumn, requestedRow].IsOccupiedByBlack())
                    {
                        return false;
                    }

                    //REGULAR L-Shaped Move
                    if (Math.Abs(requestedColumn - piece.Location.X) == 2 && Math.Abs(requestedRow - piece.Location.Y) == 1)
                    {
                        return true;
                    }
                    else if (Math.Abs(requestedColumn - piece.Location.X) == 1 && Math.Abs(requestedRow - piece.Location.Y) == 2)
                    {
                        return true;
                    }
                }
            }
            else if (piece is Bishop)
            {
                if (piece.isWhite())
                {

                }
                else
                {

                }
            }
            else if (piece is Rook)
            {
                if (piece.isWhite())
                {

                }
                else
                {

                }
            }
            else if (piece is King)
            {
                if (piece.isWhite())
                {

                }
                else
                {

                }
            }
            else if (piece is Queen)
            {
                if (piece.isWhite())
                {

                }
                else
                {

                }
            }

            return false;
        }

        /// <summary>
        /// Event handler when a square is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Square_Click(object? sender, EventArgs e)
        {
            Panel p = (Panel)sender;

            int squareColumn = p.Location.X / 60 - 1;
            int squareRow = p.Location.Y / 60 - 1;

            Square squareClicked = chessBoard[squareColumn, squareRow];
            
            //SELECTING PHASE: this is where a piece to move is not selected yet
            if (currSquareClicked is null)
            {
                //Only set the current square clicked if it is a valid piece that can be moved (depending on whose turn)
                if (squareClicked.IsOccupiedByWhite() && whiteTurn)
                {
                    currSquareClicked = squareClicked;
                    currSquareClicked.Col = p.Location.X / 60 - 1;
                    currSquareClicked.Row = p.Location.Y / 60 - 1;
                }
                //Only set the current square clicked if it is a valid piece that can be moved (depending on whose turn)
                else if (squareClicked.IsOccupiedByBlack() && !whiteTurn)
                {
                    currSquareClicked = squareClicked;
                    currSquareClicked.Col = p.Location.X / 60 - 1;
                    currSquareClicked.Row = p.Location.Y / 60 - 1;
                }
                //If a square that is not occupied is selected, tell user to select an occupied square
                else if (!squareClicked.IsOccupied())
                {
                    Debug.WriteLine("Select a piece");
                }
                else
                {
                    Debug.WriteLine("It is not your turn");
                }
            }
            //MOVING PHASE: where a current piece is selected. The only possible moves is if now the user clicks on an empty square or on a square containing opposite color piece
            else
            {
                //RESELECTING PIECE: if the user already clicked its own colored piece and clicks on another square with its own colored piece, change the currently selected piece to this new one
                if(currSquareClicked.IsOccupiedByWhite() && squareClicked.IsOccupiedByWhite())
                {
                    currSquareClicked = squareClicked;
                    currSquareClicked.Col = p.Location.X / 60 - 1;
                    currSquareClicked.Row = p.Location.Y / 60 - 1;
                }
                else if (currSquareClicked.IsOccupiedByBlack() && squareClicked.IsOccupiedByBlack())
                {
                    currSquareClicked = squareClicked;
                    currSquareClicked.Col = p.Location.X / 60 - 1;
                    currSquareClicked.Row = p.Location.Y / 60 - 1;
                }
                //MOVE PHASE:
                else
                {
                    MovePiece(squareColumn, squareRow);
                }
            }

            this.Invalidate();
        }

        /// <summary>
        /// Event handler when the user hovers over a square
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Square_Hover(object? sender, EventArgs e)
        {
            Panel p = (Panel)sender;
            Square squareClicked = chessBoard[p.Location.X / 60 - 1, p.Location.Y / 60 - 1];
            if (squareClicked.IsOccupied())
            {
                p.Cursor = Cursors.Hand;
            }
            else
            {
                p.Cursor = Cursors.No;
            }
        }
    }
}