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

        #region Helper Methods
        /// <summary>
        /// Determines if placing of the piece is a valid placement
        /// </summary>
        public bool IsPlacingAllowed(Piece piece)
        {
            return GetValidConnectingPieces(piece).Count() > 0;
        }

        /// <summary>
        /// Get the immediatly adjacent piece in specified direction
        /// </summary>
        //public Piece GetAdjacentPiece(Piece piece, Direction direction)
        //{
        //    switch (direction)
        //    {
        //        case Direction.North:
        //            if (piece.Y == 0)
        //                return Piece.Empty;
        //            else
        //                return GetPiece(piece.X, piece.Y - 1);
        //        case Direction.East:
        //            if (piece.X == Width - 1)
        //                return Piece.Empty;
        //            else
        //                return GetPiece(piece.X + 1, piece.Y);
        //        case Direction.South:
        //            if (piece.Y == Height - 1)
        //                return Piece.Empty;
        //            else
        //                return GetPiece(piece.X, piece.Y + 1);
        //        case Direction.West:
        //            if (piece.X == 0)
        //                return Piece.Empty;
        //            else
        //                return GetPiece(piece.X - 1, piece.Y);
        //        default:
        //            return Piece.Empty;
        //    }
        //}

        public IEnumerable<Piece> GetPiecesBetweenX(Piece p1, Piece p2)
        {
            // Make sure they share axis
            if (p1.Y != p2.Y)
                return Enumerable.Empty<Piece>();

            // Get first and last piece on line
            var first = p1.X < p2.X ? p1 : p2;
            var last = p1.X < p2.X ? p2 : p1;

            return Pieces.Where(p =>
                p.X > first.X &&
                p.X < last.X &&
                p.Y == p1.Y &&
                p.State != PieceState.Open);
        }

        public bool IsConnectedX(Piece p1, Piece p2)
        {
            return GetPiecesBetweenX(p1, p2).Count() == p1.X - p2.X;
        }

        public IEnumerable<Piece> GetPiecesBetweenY(Piece p1, Piece p2)
        {
            // Make sure they share axis
            if (p1.X != p2.X)
                return Enumerable.Empty<Piece>();

            // Get first and last piece on line
            var first = p1.Y < p2.Y ? p1 : p2;
            var last = p1.Y < p2.Y ? p2 : p1;

            return Pieces.Where(p =>
                p.Y > first.Y &&
                p.Y < last.Y &&
                p.X == p1.X &&
                p.State != PieceState.Open);
        }

        public bool IsConnectedY(Piece p1, Piece p2)
        {
            return GetPiecesBetweenY(p1, p2).Count() == p1.Y - p2.Y;
        }

        /// <summary>
        /// Returns all filled pieces in specified direction in relation to the speicified piece
        /// </summary>
        public IEnumerable<Piece> GetPiecesInDirection(Piece piece, Direction dir)
        {
            IEnumerable<Piece> pieces;
            switch (dir)
            {
                case Direction.North:
                    pieces = Pieces.Where(p =>
                        p.X == piece.X &&
                        p.Y > piece.Y
                    );
                    break;
                case Direction.East:
                    pieces = Pieces.Where(p =>
                        p.X > piece.X &&
                        p.Y == piece.Y
                    );
                    break;
                case Direction.South:
                    pieces = Pieces.Where(p =>
                        p.X == piece.X &&
                        p.Y < piece.Y
                    );
                    break;
                case Direction.West:
                    pieces = Pieces.Where(p =>
                        p.X < piece.X &&
                        p.Y == piece.Y
                    );
                    break;
                default:
                    return Enumerable.Empty<Piece>();
            }

            // Return only the fields which are not blank
            return pieces.Where(p => p.State != PieceState.Open);
        }

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
        #endregion
    }
}
