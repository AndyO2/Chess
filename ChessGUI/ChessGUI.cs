using Chess;

namespace ChessGUI
{
    public partial class ChessGUI : Form
    {
        /// <summary>
        /// Store the chess board
        /// </summary>
        private ChessBoard board;

        // class member array of Panels to track chessboard tiles
        private Panel[,] chessBoardPanels;

        public ChessGUI()
        {
            board = new ChessBoard(8);

            DoubleBuffered = true;

            this.Paint += Draw_Board;

            InitializeComponent();
        }

        private void Draw_Board(object? sender, PaintEventArgs e)
        {
            const int tileSize = 60;
            const int gridSize = 8;
            var clr1 = Color.DarkGray;
            var clr2 = Color.White;

            // initialize the "chess board"
            chessBoardPanels = new Panel[gridSize, gridSize];

            // double for loop to handle all rows and columns
            for (var row = 0; row < gridSize; row++)
            {
                for (var column = 0; column < gridSize; column++)
                {
                    // create new Panel control which will be one chess board tile
                    var newPanel = new Panel
                    {
                        Size = new Size(tileSize, tileSize),
                        //The +100 shifts the overall board down to (100,100) top left corner
                        Location = new Point(tileSize * row + 100, tileSize * column + 100),
                    };

                    Image newImage = Image.FromFile("..\\..\\..\\..\\ChessPieceImages\\WhiteKing.png");
                    
                    //newPanel.BackgroundImage = newImage;
                    //newImage.Dispose();

                    // add to our 2d array of panels for future use
                    chessBoardPanels[row, column] = newPanel;

                    // add to Form's Controls so that they show up
                    Controls.Add(newPanel);

                    // color the backgrounds
                    if (row % 2 == 0)
                    {
                        newPanel.BackColor = column % 2 != 0 ? clr1 : clr2;
                    }
                    else
                    {
                        newPanel.BackColor = column % 2 != 0 ? clr2 : clr1;
                    }
                }
            }
        }
    }
}