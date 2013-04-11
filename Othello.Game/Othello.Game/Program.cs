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

            var board = new Board(10, 10);
            var playerToMove = PieceState.White;

            while (true)
            {
                Console.WriteLine(board.ToString());

                Console.WriteLine();
                Console.WriteLine("{0}'s turn...", playerToMove);
                Console.WriteLine("Place a piece at:");
                Console.Write("X: ");
                var x = Console.ReadKey();
                Console.Write(" Y: ");
                var y = Console.ReadKey();

                try
                {
                    board.SetPiece(
                        (int)Char.GetNumericValue(x.KeyChar),
                        (int)Char.GetNumericValue(y.KeyChar),
                        playerToMove
                    );

                    // Switch player
                    playerToMove = playerToMove == PieceState.White ? PieceState.Black : PieceState.White;
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine();
                    Console.WriteLine("Press any key to try again");
                    Console.ReadKey(true);
                }
            }
        }
    }
#endif
}

