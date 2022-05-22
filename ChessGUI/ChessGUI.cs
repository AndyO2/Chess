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

        private King whiteKing;

        private King blackKing;

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
                        whiteKing = new King('W', column, row);

                        chessBoard[column, row] = new Square(column, row, whiteKing);
                    }
                    //Black King
                    else if (row == 0 && column == 3)
                    {
                        blackKing = new King('B', column, row);

                        chessBoard[column, row] = new Square(column, row, blackKing);
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
            if (checkWhiteIsInCheck())
            {
                GameMessagesTextBox.Text = "White Is In Check!";
                whiteTurn = true;
            }
            else if (checkBlackIsInCheck())
            {
                GameMessagesTextBox.Text = "Black Is In Check!";
                whiteTurn = false;
            }
            else
            {
                if (whiteTurn)
                {
                    whiteTurn = false;
                    GameMessagesTextBox.Text = "Black To Move";
                }
                else
                {
                    whiteTurn = true;
                    GameMessagesTextBox.Text = "White To Move";
                }
            }

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
                            chessBoardPanels[column, row].BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\WhitePawn.png");
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

            if (MoveIsLegal(requestedColumn, requestedRow))
            {
                //Special Move: Castling
                if(pieceToMove is King)
                {
                    //castle right
                    if(requestedColumn == 5)
                    {
                        Castle(true);
                    }
                    //castle left
                    else if( requestedColumn == 1)
                    {
                        Castle(false);
                    }
                }
                else
                {
                    //Update the location of the piece on the chess board
                    chessBoard[requestedColumn, requestedRow].occupant = currSquareClicked.occupant;

                    //Update the piece's location
                    chessBoard[requestedColumn, requestedRow].occupant.Location = new Point(requestedColumn, requestedRow);

                    chessBoard[currSquareClicked.Col, currSquareClicked.Row].occupant = null;
                }

                currSquareClicked = null;

                this.Invalidate();
            }
            else
            {
                GameMessagesTextBox.Text = "Invalid Move";
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

            if (piece is Pawn)
            {
                if (piece.isWhite())
                {
                    //CAN NEVER OCCUPY A SQUARE THAT IS ALREADY OCCUPIED BY SAME COLOR
                    if (chessBoard[requestedColumn, requestedRow].IsOccupiedByWhite())
                    {
                        return false;
                    }

                    //CANNOT MOVE BACKWARDS
                    if (requestedRow > piece.Location.Y)
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
                    if (Math.Abs(requestedRow - piece.Location.Y) == 1 && requestedColumn - piece.Location.X == 0 && !chessBoard[requestedColumn, requestedRow].IsOccupied())
                    {
                        return true;
                    }

                    //TAKE PIECE DIAGONAL
                    if (chessBoard[requestedColumn, requestedRow].IsOccupiedByBlack())
                    {
                        if (Math.Abs(requestedColumn - piece.Location.X) == 1 && Math.Abs(requestedRow - piece.Location.Y) == 1)
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
                    if (Math.Abs(requestedRow - piece.Location.Y) == 1 && requestedColumn - piece.Location.X == 0 && !chessBoard[requestedColumn, requestedRow].IsOccupied())
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
            else if (piece is Knight)
            {
                if (piece.isWhite())
                {
                    //CAN NEVER OCCUPY A SQUARE THAT IS ALREADY OCCUPIED BY SAME COLOR
                    if (chessBoard[requestedColumn, requestedRow].IsOccupiedByWhite())
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
                //CAN NEVER OCCUPY A SQUARE THAT IS ALREADY OCCUPIED BY SAME COLOR
                if (chessBoard[requestedColumn, requestedRow].IsOccupiedByWhite() && whiteTurn || chessBoard[requestedColumn, requestedRow].IsOccupiedByBlack() && !whiteTurn)
                {
                    return false;
                }

                // directions possible by the bishop
                const int NORTH_WEST = 0;
                const int NORTH_EAST = 1;
                const int SOUTH_EAST = 2;
                const int SOUTH_WEST = 3;

                int direction = 0;

                int[,,] legalOffsets = new int[4,7,2]
                {
                        { { -1, 1}, {-2, 2}, {-3,  3}, {-4,  4}, {-5,  5}, {-6,  6}, {-7, 7} },// top-left     branch
                        { { 1,  1}, { 2, 2}, { 3,  3}, { 4,  4}, { 5,  5}, { 6,  6}, {7,  7} },// top-right    branch
                        { { -1,-1}, {-2,-2}, {-3, -3}, {-4, -4}, {-5, -5}, {-6, -6}, {-7,-7} },// bottom-left  branch
                        { { 1, -1}, { 2,-2}, { 3, -3}, { 4, -4}, { 5, -5}, {6,  -6}, {7, -7} } // bottom-right branch
                };

                int[] requestedOffset = new int[2]; // offset of the requested square relative to the current square
                int[] requestedSquare = new int[2]; // current square being checked for obstacles

                // get offset of requested square relative to current square
                requestedOffset[0] = requestedColumn - piece.Location.X;
                requestedOffset[1] = requestedRow - piece.Location.Y;

                // check if offset is diagonal
                if (Math.Abs(requestedOffset[0]) != Math.Abs(requestedOffset[1]))
                {
                    return false;
                }

                // get the direction of the requested square relative to current location
                if (requestedOffset[0] < 0 && requestedOffset[1] > 0) { direction = NORTH_WEST; }
                if (requestedOffset[0] > 0 && requestedOffset[1] > 0) { direction = NORTH_EAST; }
                if (requestedOffset[0] < 0 && requestedOffset[1] < 0) { direction = SOUTH_EAST; }
                if (requestedOffset[0] > 0 && requestedOffset[1] < 0) { direction = SOUTH_WEST; }

                // loop through all square between the current square and requested square
                for (int i = 0; i < 8; i++)
                {

                    // get location of a square in the path to the requested square
                    requestedSquare[0] = piece.Location.X + legalOffsets[direction, i, 0];
                    requestedSquare[1] = piece.Location.Y + legalOffsets[direction, i, 1];

                    // check if current square being checked is the requested square; if so stop
                    if (requestedSquare[0] == requestedColumn && requestedSquare[1] == requestedRow)
                    {
                        return true;
                    }

                    // check if current square being checked is occupied; if so declare move illegal
                    if (chessBoard[requestedSquare[0], requestedSquare[1]].IsOccupied())
                    {
                        return false;
                    }
                }
            }
            else if (piece is Rook)
            {
                //CAN NEVER OCCUPY A SQUARE THAT IS ALREADY OCCUPIED BY SAME COLOR
                if (chessBoard[requestedColumn, requestedRow].IsOccupiedByWhite() && whiteTurn || chessBoard[requestedColumn, requestedRow].IsOccupiedByBlack() && !whiteTurn)
                {
                    return false;
                }

                const int NORTH = 0;
                const int SOUTH = 1;
                const int EAST = 2;
                const int WEST = 3;

                int[,,] legal_offsets = new int[4,7,2]
                {
                  
                    { { 0,  1}, { 0,  2}, { 0,  3}, { 0,  4}, { 0,  5}, { 0,  6}, { 0,  7} }, // top    branch
                    { { 0, -1}, { 0, -2}, { 0, -3}, { 0, -4}, { 0, -5}, { 0, -6}, { 0, -7} }, // bottom branch
                    { { 1,  0}, { 2,  0}, { 3,  0}, { 4,  0}, { 5,  0}, { 6,  0}, { 7,  0} }, // right  branch
                    { { -1, 0}, { -2, 0}, { -3, 0}, { -4, 0}, { -5, 0}, { -6, 0}, { -7, 0} }  // left   branch
                };

                int direction = 0;

                int[] requested_offset = new int[2];
                int[] requested_square = new int[2];

                requested_offset[0] = requestedColumn  - piece.Location.X;
                requested_offset[1] = requestedRow     - piece.Location.Y;

                if (requested_offset[0] != 0 && requested_offset[1] != 0)
                {
                    return false;
                }

                if (requested_offset[0] == 0 && requested_offset[1] > 0) { direction = NORTH; }
                if (requested_offset[0] == 0 && requested_offset[1] < 0) { direction = SOUTH; }
                if (requested_offset[0] > 0 && requested_offset[1] == 0) { direction = EAST; }
                if (requested_offset[0] < 0 && requested_offset[1] == 0) { direction = WEST; }

                for (int i = 0; i < 7; i++)
                {
                    requested_square[0] = piece.Location.X + legal_offsets[direction,i,0];
                    requested_square[1] = piece.Location.Y + legal_offsets[direction,i,1];

                    if (requested_square[0] == requestedColumn && requested_square[1] == requestedRow)
                    {
                        return true;
                    }

                    if (chessBoard[requested_square[0], requested_square[1]].IsOccupied())
                    {
                        return false;
                    }
                }
            }
            else if (piece is King)
            {
                int[,] legal_offsets = new int[10, 2]
                {
                    { -1,  1}, { 0,  1}, { 1, 1},
          {-2 , 0}, { -1,  0},           { 1, 0 }, { 2, 0 },
                    { -1, -1}, { 0, -1}, { 1, -1}
                };

                int[] requested_offset = new int [2];

                requested_offset[0] = requestedColumn - piece.Location.X;
                requested_offset[1] = requestedRow    - piece.Location.Y;

                //If the requested offset is the castling move, then check if the required conditions are met to castle
                if(requested_offset[0] == 2 && requested_offset[1] == 0 || requested_offset[0] == -2 && requested_offset[1] == 0)
                {
                    return CheckCastlingAllowed(requestedColumn, requestedRow);
                }

                //regular moves
                for (int i = 0; i < 10; i++)
                {
                    if (requested_offset[0] == legal_offsets[i, 0] && requested_offset[1] == legal_offsets[i, 1])
                    {
                        return true;
                    }
                }

                return false;
            }
            else if (piece is Queen)
            {
                //CAN NEVER OCCUPY A SQUARE THAT IS ALREADY OCCUPIED BY SAME COLOR
                if (chessBoard[requestedColumn, requestedRow].IsOccupiedByWhite() && whiteTurn || chessBoard[requestedColumn, requestedRow].IsOccupiedByBlack() && !whiteTurn)
                {
                    return false;
                }

                // directions possible by the Queen
                const int NORTH_WEST = 0;
                const int NORTH_EAST = 1;
                const int SOUTH_WEST = 2;
                const int SOUTH_EAST = 3;
                const int NORTH = 4;
                const int SOUTH = 5;
                const int EAST = 6;
                const int WEST = 7;

                int direction = 0;

                // all legal square offsets relative to the current square
                int[,,] legal_offsets = new int[8,7,2]
                {
                    // Bishop offsets
                    { { -1,  1}, { -2,  2}, { -3,  3}, { -4,  4}, { -5,  5}, { -6,  6}, { -7,  7} }, // top-left     branch
                    { { 1,  1}, { 2,  2}, { 3,  3}, { 4,  4}, { 5,  5}, { 6,  6}, { 7,  7} }, // top-right    branch
                    { { -1, -1}, { -2, -2}, { -3, -3}, { -4, -4}, { -5, -5}, { -6, -6}, { -7, -7} }, // bottom-left  branch
                    { { 1, -1}, { 2, -2}, { 3, -3}, { 4, -4}, { 5, -5}, { 6, -6}, { 7, -7} }, // bottom-right branch
                    // Rook offsets
                    { { 0,  1}, { 0,  2}, { 0,  3}, { 0,  4}, { 0,  5}, { 0,  6}, { 0,  7} },  // top    branch
                    { { 0, -1}, { 0, -2}, { 0, -3}, { 0, -4}, { 0, -5}, { 0, -6}, { 0, -7} },  // bottom branch
                    { { 1,  0}, { 2,  0}, { 3,  0}, { 4,  0}, { 5,  0}, { 6,  0}, { 7,  0} },  // right  branch
                    { { -1,  0}, { -2,  0}, { -3,  0}, { -4,  0}, { -5, 0}, { -6,  0}, { -7,  0} }   // left   branch
                };

                int[] requested_offset = new int[2]; // offset of the requested square relative to the current square
                int[] requested_square = new int[2]; // current square being checked for obstacles

                // get offset of requested square relative to current square
                requested_offset[0] = requestedColumn - piece.Location.X;
                requested_offset[1] = requestedRow    - piece.Location.Y;

                // check if offset is diagonal
                if ((Math.Abs(requested_offset[0]) != Math.Abs(requested_offset[1])) && (requested_offset[0] != 0 && requested_offset[1] != 0))
                {
                    return false;
                }

                // get the direction of the requested square relative to current location
                if (requested_offset[0] < 0 && requested_offset[1] > 0) { direction = NORTH_WEST; }
                if (requested_offset[0] > 0 && requested_offset[1] > 0) { direction = NORTH_EAST; }
                if (requested_offset[0] < 0 && requested_offset[1] < 0) { direction = SOUTH_WEST; }
                if (requested_offset[0] > 0 && requested_offset[1] < 0) { direction = SOUTH_EAST; }

                if (requested_offset[0] == 0 && requested_offset[1] > 0) { direction = NORTH; }
                if (requested_offset[0] == 0 && requested_offset[1] < 0) { direction = SOUTH; }
                if (requested_offset[0] > 0 && requested_offset[1] == 0) { direction = EAST; }
                if (requested_offset[0] < 0 && requested_offset[1] == 0) { direction = WEST; }

                // loop through all square between the current square and requested square
                for (int i = 0; i < 8; i++)
                {
                    // get location of a square in the path to the requested square
                    requested_square[0] = piece.Location.X + legal_offsets[direction,i,0];
                    requested_square[1] = piece.Location.Y + legal_offsets[direction,i,1];

                    // check if current square being checked is the requested square; if so stop
                    if (requested_square[0] == requestedColumn && requested_square[1] == requestedRow)
                    {
                        return true;
                    }

                    // check if current square being checked is occupied; if so declare move illegal
                    if (chessBoard[requested_square[0], requested_square[1]].IsOccupied())
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Helper method that castles after checking that castling is allowed
        /// </summary>
        /// <param name="requestedCol"></param>
        /// <param name="requestedRow"></param>
        private void Castle(bool castleRight)
        {
            if (whiteTurn)
            {
                //castle right
                if(castleRight)
                {
                    //Move the white king to the right two squares
                    chessBoard[5, 7].occupant = currSquareClicked.occupant;
                    chessBoard[5, 7].occupant.Location = new Point(5, 7);
                    chessBoard[3, 7].occupant = null;

                    //Move the white rook to the left
                    chessBoard[4, 7].occupant = chessBoard[7, 7].occupant;
                    chessBoard[4, 7].occupant.Location = new Point(4, 7);
                    chessBoard[7, 7].occupant = null;
                }
                //castle left
                else
                {
                    //Move white king to the left two squares
                    chessBoard[1,7].occupant = currSquareClicked.occupant;
                    chessBoard[1,7].occupant.Location = new Point(1, 7);
                    chessBoard[3, 7].occupant = null;

                    //Move the rook to the right
                    chessBoard[2,7].occupant = chessBoard[0,7].occupant;
                    chessBoard[2,7].occupant.Location = new Point(2, 7);
                    chessBoard[0,7].occupant = null;
                }
            }
            else
            {
                //castle right
                if (castleRight)
                {
                    //Move the white king to the right two squares
                    chessBoard[5, 0].occupant = currSquareClicked.occupant;
                    chessBoard[5, 0].occupant.Location = new Point(5, 0);
                    chessBoard[3, 0].occupant = null;

                    //Move the black rook to the left
                    chessBoard[4, 0].occupant = chessBoard[7, 0].occupant;
                    chessBoard[4, 0].occupant.Location = new Point(4, 0);
                    chessBoard[7, 0].occupant = null;
                }
                //castle left
                else
                {
                    //Move black king to the left two squares
                    chessBoard[1, 0].occupant = currSquareClicked.occupant;
                    chessBoard[1, 0].occupant.Location = new Point(1, 0);
                    chessBoard[3, 0].occupant = null;

                    //Move the rook to the right
                    chessBoard[2, 0].occupant = chessBoard[0, 0].occupant;
                    chessBoard[2, 0].occupant.Location = new Point(2, 0);
                    chessBoard[0, 0].occupant = null;
                }
            }
        }

        /// <summary>
        /// Helper method that determines if a castle move is allowed
        /// </summary>
        /// <param name="requestedCol"></param>
        /// <param name="requestedRow"></param>
        /// <returns></returns>
        private bool CheckCastlingAllowed(int requestedCol, int requestedRow)
        {
            //White King requested location is (1,7) going to the left and (5,7) going to the right
            //Black King requested location is (1,0) going to the left and (5,0) going to the right

            //TODO: Doesn't detect if the king has already moved
            if (currSquareClicked.occupant is King k)
            {
                //If the king has moved, castling is never allowed
                if (k.hasMoved)
                {
                    return false;
                }

                //White King castle
                if (whiteTurn)
                {
                    //CASTLING QUEEN SIDE (to the right)
                    if(requestedCol == 5 && requestedRow == 7)
                    {
                        //1) Check if there is a rook at (7,7), it is the same color and it has not moved
                        if(chessBoard[7,7].GetOccupant() is Rook r && r.isWhite() && !r.hasMoved)
                        {
                            //2) Check that there is not a piece at (4,7) (5,7) and (6,7)
                            if(!chessBoard[4,7].IsOccupied() && !chessBoard[5,7].IsOccupied() && !chessBoard[6, 7].IsOccupied())
                            {
                                return true;
                            }
                        }
                    }
                    //CASTLING KING SIDE (to the left)
                    else if(requestedCol == 1 && requestedRow == 7)
                    {
                        //1) Check if there is a rook at (0,7), it is the same color and it has not moved
                        if (chessBoard[0, 7].GetOccupant() is Rook r && r.isWhite() && !r.hasMoved)
                        {
                            //2) Check that there is not a piece at (4,7) (5,7) and (6,7)
                            if (!chessBoard[1, 7].IsOccupied() && !chessBoard[2, 7].IsOccupied())
                            {
                                return true;
                            }
                        }
                    }
                }
                //Black king castle
                else
                {
                    //CASTLING QUEEN SIDE (to the right)
                    if (requestedCol == 5 && requestedRow == 0)
                    {
                        //1) Check if there is a rook at (7,0), it is the same color and it has not moved
                        if (chessBoard[7, 0].GetOccupant() is Rook r && r.isBlack() && !r.hasMoved)
                        {
                            //2) Check that there is not a piece at (4,0) (5,0) and (6,0)
                            if (!chessBoard[4, 0].IsOccupied() && !chessBoard[5, 0].IsOccupied() && !chessBoard[6, 0].IsOccupied())
                            {
                                return true;
                            }
                        }
                    }
                    //CASTLING KING SIDE (to the left)
                    else if (requestedCol == 1 && requestedRow == 0)
                    {
                        //1) Check if there is a rook at (0,0), it is the same color and it has not moved
                        if (chessBoard[0, 0].GetOccupant() is Rook r && r.isBlack() && !r.hasMoved)
                        {
                            //2) Check that there is not a piece at (1,0) (2,0)
                            if (!chessBoard[1, 0].IsOccupied() && !chessBoard[2, 0].IsOccupied())
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Helper method that determines if white is in check
        /// </summary>
        /// <returns>Whether white is in check</returns>
        private bool checkWhiteIsInCheck()
        {
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if (chessBoard[i, j].IsOccupiedByBlack())
                    {
                        currSquareClicked = chessBoard[i, j];
                        //TODO: Need to know where the king is at all times
                        if(MoveIsLegal(whiteKing.Location.X, whiteKing.Location.Y))
                        {
                            //reset curr square clicked. We need to set it because move is legal checks if the current square clicked's move is legal
                            currSquareClicked = null;
                            return true;
                        }
                    }
                }
            }
            currSquareClicked = null;
            return false;
        }

        /// <summary>
        /// Helper method that determines if black is in check
        /// </summary>
        /// <returns>Whether black is in check</returns>
        private bool checkBlackIsInCheck()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (chessBoard[i, j].IsOccupiedByWhite())
                    {
                        currSquareClicked = chessBoard[i, j];
                        //TODO: Need to know where the king is at all times
                        if (MoveIsLegal(blackKing.Location.X, blackKing.Location.Y))
                        {
                            //reset curr square clicked. We need to set it because move is legal checks if the current square clicked's move is legal
                            currSquareClicked = null;
                            return true;
                        }
                    }
                }
            }
            currSquareClicked = null;
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
                    GameMessagesTextBox.Text = "Select a piece";
                }
                else
                {
                    GameMessagesTextBox.Text = "It is not your turn";
                }
            }
            //MOVING PHASE: where a current piece is selected. The only possible moves is if now the user clicks on an empty square or on a square containing opposite color piece
            else
            {
                //RESELECTING PIECE: if the user already clicked its own colored piece and clicks on another square with its own colored piece, change the currently selected piece to this new one
                if (currSquareClicked.IsOccupiedByWhite() && squareClicked.IsOccupiedByWhite())
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
        }

        /// <summary>
        /// Event handler when the user hovers over a square
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Square_Hover(object? sender, EventArgs e)
        {
            Panel p = (Panel)sender;

            int requestedCol = p.Location.X / 60 - 1;
            int requestedRow = p.Location.Y / 60 - 1;

            Square squareHovering = chessBoard[requestedCol, requestedRow];

            if (squareHovering.IsOccupied() && currSquareClicked is null)
            {
                p.Cursor = Cursors.Hand;
            }
            else if (currSquareClicked is not null && MoveIsLegal(requestedCol,requestedRow))
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