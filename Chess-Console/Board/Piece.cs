namespace board
{
    abstract class Piece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int qtMovements { get; protected set; }
        public Board board { get; protected set; }

        public Piece(Board board, Color color)
        {
            this.position = null;
            this.board = board;
            this.color = color;
            this.qtMovements = 0;
        }

        public void increaseQtMovements()
        {
            qtMovements++;
        }

        public bool isThereAnyPossibleMove()
        {
            bool[,] mat = possibleMoves();
            for (int i = 0; i < board.rows; i++)
            {
                for (int j = 0; j < board.columns; j++)
                {
                    if (mat[i,j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool canMoveTo(Position position)
        {
            return possibleMoves()[position.row, position.column];
        }

        public abstract bool[,] possibleMoves();

    }
}
