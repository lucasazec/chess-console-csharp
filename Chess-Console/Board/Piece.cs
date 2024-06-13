namespace board
{
    abstract class Piece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int moveCount { get; protected set; }
        public Board board { get; protected set; }

        public Piece(Board board, Color color)
        {
            this.position = null;
            this.board = board;
            this.color = color;
            this.moveCount = 0;
        }

        public void increaseMoveCount()
        {
            moveCount++;
        }

        public void decreaseMoveCount()
        {
            moveCount--;
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
