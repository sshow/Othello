using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello.Game.GameElements
{
    class Piece
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public PieceState State { get; private set; }

        public Piece(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void SetState(PieceState state)
        {
            State = state;
        }
    }
}
