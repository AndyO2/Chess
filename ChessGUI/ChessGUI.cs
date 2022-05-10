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

        private const int tileSize = 60;
        private const int gridSize = 8;
        private Color clr1 = Color.RebeccaPurple;
        private Color clr2 = Color.White;

        public Chess()
        {
            //The chess board contains the status of the game
            chessBoard = new Square[8, 8];
            CreateBoard(8);

            //The panels portrays the chessBoard onto the gui using panels
            chessBoardPanels = new Panel[8, 8];

            whiteTurn = true;

            this.Paint += Draw_Board;
            DoubleBuffered = true;

            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000 / 30;  // 1000 milliseconds in a second divided by 30 frames per second
            timer.Tick += (a, b) => this.Invalidate();
            timer.Start();

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
                    //White Pawn
                    if(row == 6)
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

                    // Access the occupant of this square if there is one (can be null)
                    ChessPiece? occupant = chessBoard[column, row].getOccupant();

                    if (occupant is Pawn)
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
                    else if (occupant is King)
                    {
                        if (occupant.isWhite())
                        {
                            newPanel.BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\WhiteKing.png");
                        }
                        else
                        {
                            newPanel.BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\BlackKing.png");
                        }
                    }
                    else if (occupant is Queen)
                    {
                        if (occupant.isWhite())
                        {
                            newPanel.BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\WhiteQueen.png");
                        }
                        else
                        {
                            newPanel.BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\BlackQueen.png");
                        }
                    }
                    else if (occupant is Rook)
                    {
                        if (occupant.isWhite())
                        {
                            newPanel.BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\WhiteRook.png");
                        }
                        else
                        {
                            newPanel.BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\BlackRook.png");
                        }
                    }
                    else if (occupant is Bishop)
                    {
                        if (occupant.isWhite())
                        {
                            newPanel.BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\WhiteBishop.png");
                        }
                        else
                        {
                            newPanel.BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\BlackBishop.png");
                        }
                    }
                    else if (occupant is Knight)
                    {
                        if (occupant.isWhite())
                        {
                            newPanel.BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\WhiteKnight.png");
                        }
                        else
                        {
                            newPanel.BackgroundImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\BlackKnight.png");
                        }
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

        private void Square_Click(object? sender, EventArgs e)
        {
            chessBoard[2, 4].occupant = new King('W', 2, 4);

            Debug.WriteLine($"{chessBoard[2, 4].occupant}");
            Debug.WriteLine($"{chessBoard[3, 4].occupant}");



            Panel p = (Panel)sender;
            Debug.WriteLine($"{p.Location.X / 60 - 1} - {p.Location.Y / 60 - 1}");

            //if (currSquareClicked is null)
            //{
            //    //currSquareClicked = chessBoard[p.Location.X / 60 - 1, p.Location.Y / 60 - 1];
            //    currSquareClicked = chessBoard[p.Location.Y / 60 - 1, p.Location.X / 60 - 1];

            //    Debug.WriteLine($"{p.Location.X / 60 - 1} - {p.Location.Y / 60 - 1}");
            //}
            //else
            //{
            //    //chessBoard[p.Location.X / 60 - 1, p.Location.Y / 60 - 1].occupant = currSquareClicked.occupant;
            //    chessBoard[2, 4].occupant = new Pawn('W', 2 ,4);

            //    Debug.WriteLine($"{p.Location.X / 60 - 1} - {p.Location.Y / 60 - 1}");

            //    //currSquareClicked.occupant = null;
            //    currSquareClicked = null;

            //    //Debug.WriteLine($"{currSquareClicked.Row} - {currSquareClicked.Col}");
            //}
        }

        private void Square_Hover(object? sender, EventArgs e)
        {
            Panel p = (Panel)sender;
            Square squareClicked = chessBoard[p.Location.X / 60 - 1, p.Location.Y / 60 - 1];
            if (squareClicked.IsOccupiecd())
            {
                p.Cursor = Cursors.Hand;
            }
        }
    }
}