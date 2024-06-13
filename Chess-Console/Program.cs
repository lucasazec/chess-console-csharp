using board;
using chess;
using Chess_Console;
using System.Runtime.ConstrainedExecution;

namespace chess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch chessMatch = new ChessMatch();

                while (!chessMatch.finished)
                {
                    try
                    {
                        Console.Clear();
                        Screen.printMatch(chessMatch);

                        Console.WriteLine();

                        Console.Write("Source: ");
                        Position initial = Screen.readChessPosition().toPosition();
                        chessMatch.validateSourcePosition(initial);

                        bool[,] possiblePositions = chessMatch.board.piece(initial).possibleMoves();

                        Console.Clear();
                        Screen.printScreen(chessMatch.board, possiblePositions);

                        Console.WriteLine();
                        Console.WriteLine();

                        Console.Write("Target: ");
                        Position final = Screen.readChessPosition().toPosition();
                        chessMatch.validateTargetPosition(initial, final);

                        chessMatch.makeMove(initial, final);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }

                Screen.printScreen(chessMatch.board);

            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }


            Console.ReadLine();
        }
    }
}