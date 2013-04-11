using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello.Game.GameElements
{
    public class Piece
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public PieceState State { get; private set; }

        public Piece(int x, int y)
        {
            //if (x < 0 || y < 0)
            //{
            //    throw new ArgumentException("Coordinates cannot be negative values");
            //}

            X = x;
            Y = y;
        }

        public void SetState(PieceState state)
        {
            if (IsEmpty(this))
                throw new InvalidOperationException("Changing an empty objects state is not allowed");
            State = state;
        }

        /// <summary>
        /// Determine if two objects has the same coordinates
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Piece))
                return false;

            var piece = (Piece)obj;
            return this.X == piece.X && this.Y == piece.Y;
        }

        /// <summary>
        /// Determine if a piece is equal to Piece.Empty
        /// </summary>
        public static bool IsEmpty(Piece piece)
        {
            return piece.Equals(Empty);
        }
        public bool IsEmpty()
        {
            return IsEmpty(this);
        }

        /// <summary>
        /// An empty piece with X & Y set to -1
        /// </summary>
        public static readonly Piece Empty = new Piece(-1, -1);

        /// <summary>
        /// Determine if two pieces are of the opposite color (state). Throws an exception if one of the pieces are open.
        /// </summary>
        public static bool HasOppositeStates(Piece p1, Piece p2)
        {
            if (p1.State == PieceState.Open || p2.State == PieceState.Open)
                throw new ArgumentException("One of the states are open and therefore has no opposite state");
            return p1.State != p2.State;
        }
    }
}
