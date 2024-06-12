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
                    Console.Clear();
                    Screen.printScreen(chessMatch.board);

                    Console.WriteLine();
                    Console.WriteLine();

                    Console.Write("Origem: ");
                    Position initial = Screen.readChessPosition().toPosition();

                    bool[,] possiblePositions = chessMatch.board.piece(initial).possibleMoves();

                    Console.Clear();
                    Screen.printScreen(chessMatch.board, possiblePositions);

                    Console.WriteLine();
                    Console.WriteLine();

                    Console.Write("Destino: ");
                    Position final = Screen.readChessPosition().toPosition();

                    chessMatch.performChessMove(initial, final);
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