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

        public Piece piece(int row, int column)
        {
            return _pieces[row, column];
        }

        public Piece piece(Position position)
        {
            return _pieces[position.row, position.column];
        }

        public bool thereIsAPiece(Position position)
        {
            positionValidation(position);
            return piece(position) != null;
        }

        public void placePiece(Piece piece, Position position)
        {
            if (thereIsAPiece(position))
            {
                throw new BoardException("There is already a piece in that position!");
            }

            _pieces[position.row, position.column] = piece;
            piece.position = position;
        }


        public bool validPosition(Position position)
        {
            if (position.row < 0 || position.row >= rows || position.column < 0 || position.column >= columns)
            {
                return false;
            }
            return true;
        }

        public void positionValidation(Position position)
        {
            if (!validPosition(position))
            {
                throw new BoardException("Invalid Position!");
            }
        }
    }
}
