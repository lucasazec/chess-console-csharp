using board;

namespace chess
{
    internal class King : Piece
    {

        private ChessMatch match;
        public King(Board board, Color color, ChessMatch match) : base(board, color)
        {
            this.match = match;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool canMove(Position position)
        {
            Piece piece = board.piece(position);
            return piece == null || piece.color != color;
        }

        private bool testRook(Position position)
        {
            Piece piece = board.piece(position);
            return piece != null && piece is Rook && piece.color == color && moveCount == 0;
        }
        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[board.rows, board.columns];

            Position pos = new Position(0, 0);

            // ABOVE
            pos.defineValues(position.row - 1, position.column);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }

            // NORTH EAST
            pos.defineValues(position.row - 1, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }

            // RIGHT
            pos.defineValues(position.row, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }

            // SOUTH EAST
            pos.defineValues(position.row + 1, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }

            // BELOW
            pos.defineValues(position.row + 1, position.column);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }

            // SOUTH WEST
            pos.defineValues(position.row + 1, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }

            // LEFT
            pos.defineValues(position.row, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }

            // NORTH WEST
            pos.defineValues(position.row - 1, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }

            // SPECIALMOVE ROOK
            if (moveCount == 0 && !match.check)
            {
                // SPECIALMOVE KINGSIDE ROOK
                Position posR1 = new Position(position.row, position.column + 3);
                if (testRook(posR1))
                {
                    Position r1 = new Position(position.row, position.column + 1);
                    Position r2 = new Position(position.row, position.column + 2);
                    if (board.piece(r1) == null && board.piece(r2) == null)
                    {
                        mat[position.row, position.column + 2] = true;
                    }
                }

                // SPECIALMOVE QUEENSIDE ROOK
                Position posR2 = new Position(position.row, position.column - 4);
                if (testRook(posR2))
                {
                    Position r1 = new Position(position.row, position.column - 1);
                    Position r2 = new Position(position.row, position.column - 2);
                    Position r3 = new Position(position.row, position.column - 3);
                    if (board.piece(r1) == null && board.piece(r2) == null && board.piece(r3) == null)
                    {
                        mat[position.row, position.column - 2] = true;
                    }
                }
            }

            return mat;
        }
    }
}
