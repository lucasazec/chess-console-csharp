using board;
using System.Runtime.ConstrainedExecution;

namespace chess
{
    internal class Queen : Piece
    {
        public Queen(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "Q";
        }

        private bool canMove(Position position)
        {
            Piece piece = board.piece(position);
            return piece == null || piece.color != color;
        }
        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[board.rows, board.columns];

            Position pos = new Position(0, 0);

            // LEFT
            pos.defineValues(position.row, position.column - 1);
            while (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.defineValues(pos.row, pos.column - 1);
            }

            // RIGHT
            pos.defineValues(position.row, position.column + 1);
            while (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.defineValues(pos.row, pos.column + 1);
            }

            // ABOVE
            pos.defineValues(position.row - 1, position.column);
            while (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.defineValues(pos.row - 1, pos.column);
            }

            // BELOW
            pos.defineValues(position.row + 1, position.column);
            while (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.defineValues(pos.row + 1, pos.column);
            }

            // NORTH WEST
            pos.defineValues(position.row - 1, position.column - 1);
            while (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.defineValues(pos.row - 1, pos.column - 1);
            }

            // NORTH EAST
            pos.defineValues(position.row - 1, position.column + 1);
            while (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.defineValues(pos.row - 1, pos.column + 1);
            }

            // SOUTH EAST
            pos.defineValues(position.row + 1, position.column + 1);
            while (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.defineValues(pos.row + 1, pos.column + 1);
            }

            // SOUTH WEST
            pos.defineValues(position.row + 1, position.column - 1);
            while (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.defineValues(pos.row + 1, pos.column - 1);
            }

            return mat;
        }
    }
}

