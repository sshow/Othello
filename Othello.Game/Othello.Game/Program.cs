using System;
using Othello.Game.GameElements;
using System.Linq;

namespace Othello.Game
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            //using (Game1 game = new Game1())
            //{
            //    game.Run();
            //}
            Console.SetWindowSize(100, 50);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            var board = new Board(10, 10);

            board.SetPiece(3, 0, PieceState.Black);
            board.SetPiece(4, 0, PieceState.Black);
            board.SetPiece(5, 0, PieceState.Black);

            //var between = board.Pieces.GetPiecesBetweenX(
            //    new Piece(2, 0),
            //    new Piece(9, 0)
            //);

            //bool isConnectedX = board.Pieces.IsConnectedX(
            //    new Piece(2, 0),
            //    new Piece(6, 0)
            //);

            board.GetPiecesInDirection(new Piece(6, 0), Direction.West);

            board.PrintBoardState();

            Console.ReadKey();
        }
    }
#endif
}

