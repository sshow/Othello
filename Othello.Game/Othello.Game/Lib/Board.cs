using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello.Game.Lib
{
    class Board
    {
        #region Object Variables
        // Board settings
        private static readonly int WIDTH = 10;
        private static readonly int HEIGHT = 10;

        // Brick colors
        private static readonly int WHITE = 1;
        private static readonly int BLACK = 2;

        // Private variables
        private int[,] _board;
        #endregion

        public Board()
        {
            // Initialize board
            _board = new int[WIDTH, HEIGHT];
            
            PlaceInitialPieces();
        }

        #region Private Methods
        private void PlaceInitialPieces()
        {
            var bt = new BoardTraverser(_board);
            bt.FillPosition(WHITE, WIDTH / 2, HEIGHT / 2);
            bt.FillPosition(WHITE, (WIDTH / 2) - 1, (HEIGHT / 2) - 1);
            bt.FillPosition(BLACK, (WIDTH / 2) - 1, HEIGHT / 2);
            bt.FillPosition(BLACK, WIDTH / 2, (HEIGHT / 2) - 1);
        }
        #endregion

        #region Public Methods
        public void PlacePiece(int color, int x, int y)
        {
            var bt = new BoardTraverser(_board);
            bt.FillPosition(color, x, y);
        }
        #endregion

        #region Debugging

        public void PrintBoardState()
        {
            Console.Clear();

            for (int y = 0; y < HEIGHT; y++)
            {
                for (int x = 0; x < WIDTH; x++)
                {
                    Console.Write("{0}  ", _board[x, y]);
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        public void PrintBoardPositions()
        {
            Console.Clear();

            for (int x = 0; x < HEIGHT; x++)
            {
                for (int y = 0; y < WIDTH; y++)
                {
                    Console.Write("{0}x{1}   ", x, y);
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
            }
        }
        #endregion
    }
}
