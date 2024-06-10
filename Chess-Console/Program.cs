using board;
using Chess_Console;

namespace chess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            Screen.printScreen(board);
        }
    }
}