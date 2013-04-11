using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello.Game.GameElements
{
    public class Board
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
            _pieces.Single(p => p.X == Width / 2 && p.Y == Height / 2).SetState(PieceState.White);
            _pieces.Single(p => p.X == Width / 2 - 1 && p.Y == Height / 2 - 1).SetState(PieceState.White);
            _pieces.Single(p => p.X == Width / 2 - 1 && p.Y == Height / 2).SetState(PieceState.Black);
            _pieces.Single(p => p.X == Width / 2 && p.Y == Height / 2 - 1).SetState(PieceState.Black);
        }

        public Piece GetPiece(int x, int y)
        {
            return _pieces.Single(p => p.X == x && p.Y == y);
        }

        public void SetPiece(int x, int y, PieceState state)
        {
            var piece = GetPiece(x, y);
            var dummy = new Piece(piece.X, piece.Y);
            dummy.SetState(state);

            // Throw an exception if the location has already been set
            if (piece.State != PieceState.Open)
                throw new ArgumentException("Location already set");

            // Throw an exception if placement is invalid
            if (!IsPlacingAllowed(dummy))
                throw new ArgumentException("The piece does not have any valid adjacent pieces");

            // Place the piece
            piece.SetState(state);

            // Turn connecting pieces
            var connectingPieces = GetValidConnectingPieces(piece);
            foreach (var p in connectingPieces)
            {
                p.SetState(state);
            }
        }

        /// <summary>
        /// Returns the current state of the board
        /// </summary>
        public override string ToString()
        {
            string board = string.Empty;

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    board += string.Format("{0}  ", (int)GetPiece(x, y).State);
                }

                board += "\r\n\r\n";
            }

            return board;
        }

        /// <summary>
        /// Determines if placing of the piece is a valid placement
        /// </summary>
        public bool IsPlacingAllowed(Piece piece)
        {
            return GetValidConnectingPieces(piece).Count() > 0;
        }

        /// <summary>
        /// Returns a collection of the pieces that are to be turned if the supplied piece is placed
        /// </summary>
        public IEnumerable<Piece> GetValidConnectingPieces(Piece piece)
        {
            var connectingPieces = new List<Piece>();
            // Create a new traverser and move it one step in current direction
            var traverser = new BoardTraverser(this);

            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                // List to store connecting pieces in current direction
                var connectingPiecesInDirection = new List<Piece>();

                // Reset traverser
                traverser.SetCoords(piece);
                traverser.MovePointer(direction);

                // Loop while traverser is within bounds of the board
                while (!traverser.OutOfBounds)
                {
                    var current = traverser.GetCurrentPiece();

                    if (current.State == PieceState.Open)
                    {
                        break;
                    }
                    else if (Piece.HasOppositeStates(current, piece))
                    {
                        connectingPiecesInDirection.Add(current);
                    }
                    else if (connectingPiecesInDirection.Count > 0 && current.State != PieceState.Open)
                    {
                        // If the last connecting piece is of the same color
                        // and there are opposite colors in between, add to the list
                        connectingPieces.AddRange(connectingPiecesInDirection);
                        break;
                    }

                    // Move pointer to the next place
                    traverser.MovePointer(direction);
                }
            }

            return connectingPieces.AsEnumerable();
        }
    }
}
