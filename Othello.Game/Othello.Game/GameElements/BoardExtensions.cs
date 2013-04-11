using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello.Game.GameElements
{
    static class BoardExtensions
    {
        public static IEnumerable<Piece> GetPiecesBetweenX(this Board board, Piece p1, Piece p2)
        {
            // Make sure they share axis
            if (p1.Y != p2.Y)
                return Enumerable.Empty<Piece>();

            // Get first and last piece on line
            var first = p1.X < p2.X ? p1 : p2;
            var last = p1.X < p2.X ? p2 : p1;

            return board.Pieces.Where(p =>
                p.X > first.X &&
                p.X < last.X &&
                p.Y == p1.Y &&
                p.State != PieceState.Open);
        }

        public static bool IsConnectedX(this Board board, Piece p1, Piece p2)
        {
            return GetPiecesBetweenX(board, p1, p2).Count() == p1.X - p2.X;
        }

        public static IEnumerable<Piece> GetPiecesBetweenY(this Board board, Piece p1, Piece p2)
        {
            // Make sure they share axis
            if (p1.X != p2.X)
                return Enumerable.Empty<Piece>();

            // Get first and last piece on line
            var first = p1.Y < p2.Y ? p1 : p2;
            var last = p1.Y < p2.Y ? p2 : p1;

            return board.Pieces.Where(p =>
                p.Y > first.Y &&
                p.Y < last.Y &&
                p.X == p1.X &&
                p.State != PieceState.Open);
        }

        public static bool IsConnectedY(this Board board, Piece p1, Piece p2)
        {
            return GetPiecesBetweenY(board, p1, p2).Count() == p1.Y - p2.Y;
        }

        /// <summary>
        /// Returns all filled pieces in specified direction in relation to the speicified piece
        /// </summary>
        public static IEnumerable<Piece> GetPiecesInDirection(this Board board, Piece piece, Direction dir)
        {
            IEnumerable<Piece> pieces;
            switch (dir)
            {
                case Direction.North:
                    pieces = board.Pieces.Where(p =>
                        p.X == piece.X &&
                        p.Y > piece.Y
                    );
                    break;
                case Direction.East:
                    pieces = board.Pieces.Where(p =>
                        p.X > piece.X &&
                        p.Y == piece.Y
                    );
                    break;
                case Direction.South:
                    pieces = board.Pieces.Where(p =>
                        p.X == piece.X &&
                        p.Y < piece.Y
                    );
                    break;
                case Direction.West:
                    pieces = board.Pieces.Where(p =>
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
    }
}
