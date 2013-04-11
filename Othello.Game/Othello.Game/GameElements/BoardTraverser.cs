using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello.Game.GameElements
{
    public class BoardTraverser
    {
        public int CurrentX { get; private set; }
        public int CurrentY { get; private set; }
        public bool OutOfBounds
        {
            get
            {
                return (CurrentX < 0 || CurrentX >= _board.Width || CurrentY < 0 || CurrentY >= _board.Height);
            }
        }

        private Board _board;

        public BoardTraverser(Board board)
        {
            _board = board;
        }

        public void SetCoords(int x, int y)
        {
            //if (x < 0 || x >= _board.Width || y < 0 || y >= _board.Height)
            //    throw new ArgumentOutOfRangeException("Coordinates are out of the boards bounds");

            CurrentX = x;
            CurrentY = y;
        }

        public void SetCoords(Piece piece)
        {
            SetCoords(piece.X, piece.Y);
        }

        public void ResetPointer()
        {
            CurrentX = 0;
            CurrentY = 0;
        }

        public void MovePointer(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    SetCoords(CurrentX, CurrentY - 1);
                    break;
                case Direction.East:
                    SetCoords(CurrentX + 1, CurrentY);
                    break;
                case Direction.South:
                    SetCoords(CurrentX, CurrentY + 1);
                    break;
                case Direction.West:
                    SetCoords(CurrentX - 1, CurrentY);
                    break;
            }
        }

        public Piece GetCurrentPiece()
        {
            if (OutOfBounds)
                throw new IndexOutOfRangeException("Current coordinates are out of bounds.");
            return _board.GetPiece(CurrentX, CurrentY);
        }
    }
}
