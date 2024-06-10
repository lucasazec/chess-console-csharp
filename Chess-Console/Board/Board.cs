namespace board
{
    internal class Board
    {
        public int rows { get; set; }
        public int columns { get; set; }
        private Piece[,] _pieces;

        public Board(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            _pieces = new Piece[rows, columns];
        }
    }
}
