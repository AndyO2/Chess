using Chess;

namespace ChessGUI
{
    public partial class ChessGUI : Form
    {
        /// <summary>
        /// Store the chess board
        /// </summary>
        private ChessBoard board;

        public ChessGUI()
        {
            board = new ChessBoard(8);

            DoubleBuffered = true;

            this.Paint += Draw_Board;

            InitializeComponent();
        }

        // class member array of Panels to track chessboard tiles
        private Panel[,] _chessBoardPanels;

        private void Draw_Board(object? sender, PaintEventArgs e)
        {
            const int tileSize = 60;
            const int gridSize = 8;
            var clr1 = Color.DarkGray;
            var clr2 = Color.White;

            // initialize the "chess board"
            _chessBoardPanels = new Panel[gridSize, gridSize];

            // double for loop to handle all rows and columns
            for (var n = 0; n < gridSize; n++)
            {
                for (var m = 0; m < gridSize; m++)
                {
                    // create new Panel control which will be one chess board tile
                    var newPanel = new Panel
                    {
                        Size = new Size(tileSize, tileSize),
                        //The +100 shifts the overall board down to (100,100) top left corner
                        Location = new Point(tileSize * n + 100, tileSize * m + 100)
                    };

                    // add to our 2d array of panels for future use
                    _chessBoardPanels[n, m] = newPanel;

                    // color the backgrounds
                    if (n % 2 == 0)
                        newPanel.BackColor = m % 2 != 0 ? clr1 : clr2;
                    else
                        newPanel.BackColor = m % 2 != 0 ? clr2 : clr1;

                    // add to Form's Controls so that they show up
                    Controls.Add(newPanel);
                }
            }
        }
    }
}