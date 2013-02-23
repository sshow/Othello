using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello.Game.GameElements
{
    class Board
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        private List<Piece> _pieces;
        public List<Piece> Pieces
        {
            get { return _pieces; }
        }

        public Board(int width, int height)
        {
            Width = width;
            Height = height;

            // Initialize board
            _pieces = new List<Piece>(Width * Height);
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    _pieces.Add(new Piece(x, y));
                }
            }

            // Set initial pieces
            //_pieces.Single(p => p.X == Width / 2 && p.Y == Height / 2).SetState(PieceState.White);
            //_pieces.Single(p => p.X == Width / 2 - 1 && p.Y == Height / 2 - 1).SetState(PieceState.White);
            //_pieces.Single(p => p.X == Width / 2 - 1 && p.Y == Height / 2).SetState(PieceState.Black);
            //_pieces.Single(p => p.X == Width / 2 && p.Y == Height / 2 - 1).SetState(PieceState.Black);
        }

        public Piece GetPiece(int x, int y)
        {
            return _pieces.Single(p => p.X == x && p.Y == y);
        }

        public void SetPiece(int x, int y, PieceState state)
        {
            var piece = GetPiece(x, y);

            // Throw an exception if the location has already been set
            if (piece.State != PieceState.Open)
                throw new ArgumentException("Location already set");

            piece.SetState(state);
        }

        public void PrintBoardState()
        {
            Console.Clear();

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Console.Write("{0}  ", (int)GetPiece(x, y).State);
                }

                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
