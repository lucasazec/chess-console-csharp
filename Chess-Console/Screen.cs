using board;

namespace Chess_Console
{
    internal class Screen
    {
        public static void printScreen( Board board)
        {
            for (int i = 0; i<board.rows; i++)
            {
                for(int j = 0;j<board.columns; j++)
                {
                    if (board.piece(j,i) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(board.piece(i, j) + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
