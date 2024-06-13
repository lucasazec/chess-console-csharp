using board;
using chess;
using System.Collections.Generic;

namespace Chess_Console
{
    internal class Screen
    {

        public static void printMatch(ChessMatch match)
        {
            printScreen(match.board);
            Console.WriteLine();
            Console.WriteLine();
            printCapturedPieces(match);
            Console.WriteLine();
            Console.WriteLine("Turn: " + match.turn);
            Console.WriteLine("Waiting player: " + match.currentPlayer);
            if (match.check)
            {
                Console.WriteLine("CHECK!");
            }
        }

        public static void printCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Captured pieces: ");
            Console.Write("White: ");
            printGroup(match.capturedPieces(Color.White));
            Console.WriteLine();
            Console.Write("Black: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            printGroup(match.capturedPieces(Color.Black));
            Console.ForegroundColor= aux;
            Console.WriteLine();
        }

        public static void printGroup(HashSet<Piece> group)
        {
            Console.Write("[");
            foreach(Piece x in group)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }
        public static void printScreen(Board board)
        {
            for (int i = 0; i < board.rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.columns; j++)
                {
                    Screen.printPiece(board.piece(i, j));
                }

                Console.WriteLine();
            }
            Console.Write("  a b c d e f g h");
        }

        public static void printScreen(Board board, bool[,] possiblePositions)
        {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor changedBackground = ConsoleColor.DarkGray;

            for (int i = 0; i < board.rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.columns; j++)
                {
                    if (possiblePositions[i, j])
                    {
                        Console.BackgroundColor = changedBackground;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBackground;
                    }
                    Screen.printPiece(board.piece(i, j));
                    Console.BackgroundColor = originalBackground;
                }

                Console.WriteLine();
            }
            Console.Write("  a b c d e f g h");
            Console.BackgroundColor = originalBackground;
        }

        public static ChessPosition readChessPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int row = int.Parse(s[1] + "");
            return new ChessPosition(column, row);

        }

        public static void printPiece(Piece piece)
        {

            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.color == Color.White)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }

        }
    }
}
