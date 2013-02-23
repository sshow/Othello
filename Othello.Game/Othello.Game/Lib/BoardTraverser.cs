using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello.Game.Lib
{
    class BoardTraverser
    {
        private int[,] _arr;

        public BoardTraverser(int[,] arr)
        {
            _arr = arr;
        }

        public void FillPosition(int value, int x, int y)
        {
            _arr[x, y] = value;
        }

        public int GetValue(int x, int y)
        {
            return _arr[x, y];
        }

        public void IsLegalMove(int value, int x, int y)
        {
            throw new NotImplementedException();
        }

        public bool IsConnectedX(int x1, int x2, int y)
        {
            if (x1 < x2)
            {
                for (int x = x1; x < x2; x++)
                {
                    if (_arr[x, y] == 0) return false;
                }
                return true;
            }
            else
            {
                for (int x = x2; x > x1; x--)
                {
                    if (_arr[x, y] == 0) return false;
                }
                return true;
            }
        }
    }
}
