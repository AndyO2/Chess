using Chess;

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

        public Chess()
        {
            //The chess board contains the status of the game
            chessBoard = new Square[8, 8];
            CreateBoard(8);

            //The panels portrays the chessBoard onto the gui using panels
            chessBoardPanels = new Panel[8, 8];

            whiteTurn = true;

            DoubleBuffered = true;

            this.Paint += Draw_Board;

            InitializeComponent();
        }

        /// <summary>
        /// Helper method that creates the chessBoard
        /// </summary>
        /// <param name="boardSize"></param>
        private void CreateBoard(int boardSize)
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    chessBoard[i, j] = new Square(i, j, null);
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
            const int tileSize = 60;
            const int gridSize = 8;
            var clr1 = Color.BurlyWood;
            var clr2 = Color.White;

            // initialize the "chess board"
            chessBoardPanels = new Panel[gridSize, gridSize];

            // double for loop to handle all rows and columns
            for (var column = 0; column < gridSize; column++)
            {
                for (var row = 0; row < gridSize; row++)
                {
                    // create new Panel control which will be one chess board tile
                    var newPanel = new Panel
                    {
                        Size = new Size(tileSize, tileSize),
                        //The +100 shifts the overall board down to (100,100) top left corner
                        Location = new Point(tileSize * column + 100, tileSize * row + 100),

                        BackgroundImageLayout = ImageLayout.Center
                    };

                    // Access the occupant of this square if there is one (can be null)
                    ChessPiece? occupant = chessBoard[column, row].getOccupant();

                    if(occupant is Pawn && occupant.isWhite())
                    {
                        if (occupant.isWhite())
                        {
                            newPanel.BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\WhitePawn.png");
                        }
                        else
                        {
                            newPanel.BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\BlackPawn.png");
                        }
                    }
                    //TODO: below just prints the basic starting position of the game. However, this needs to shift to print whatever occupies the square at the location on the Square 2D board

                    //Prints black pieces
                    if(row == 1)
                    {
                        newPanel.BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\BlackPawn.png");
                    }
                    else if (row == 0 && column == 0 || row == 0 && column == 7)
                    {
                        newPanel.BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\BlackRook.png");
                    }
                    else if (row == 0 && column == 1 || row == 0 && column == 6)
                    {
                        newPanel.BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\BlackKnight.png");
                    }
                    else if (row == 0 && column == 2 || row == 0 && column == 5)
                    {
                        newPanel.BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\BlackBishop.png");
                    }
                    else if (row == 0 && column == 4)
                    {
                        newPanel.BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\BlackQueen.png");
                    }
                    else if (row == 0 && column == 3)
                    {
                        newPanel.BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\BlackKing.png");
                    }
                    //Prints white pieces
                    else if (row == 6)
                    {
                        newPanel.BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\WhitePawn.png");
                    }
                    else if (row == 7 && column == 0 || row == 7 && column == 7)
                    {
                        newPanel.BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\WhiteRook.png");
                    }
                    else if (row == 7 && column == 1 || row == 7 && column == 6)
                    {
                        newPanel.BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\WhiteKnight.png");
                    }
                    else if (row == 7 && column == 2 || row == 7 && column == 5)
                    {
                        newPanel.BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\WhiteBishop.png");
                    }
                    else if (row == 7 && column == 3)
                    {
                        newPanel.BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\WhiteKing.png");
                    }
                    else if (row == 7 && column == 4)
                    {
                        newPanel.BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\WhiteQueen.png");
                    }

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
                }
            }
        }
    }
}