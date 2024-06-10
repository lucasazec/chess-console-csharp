using board;
using chess;
using Chess_Console;

namespace chess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Board board = new Board(8, 8);

                board.placePiece(new Tower(board, Color.Black), new Position(0, 0));
                board.placePiece(new Tower(board, Color.Black), new Position(1, 3));
                board.placePiece(new King(board, Color.Black), new Position(2, 4));


                Screen.printScreen(board);
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }


            Console.ReadLine();
        }
    }
}