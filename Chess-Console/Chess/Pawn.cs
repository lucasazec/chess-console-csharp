﻿using board;

namespace chess
{
    internal class Pawn : Piece
    {

        private ChessMatch match;
        public Pawn(Board board, Color color, ChessMatch match) : base(board, color)
        {
            this.match = match;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool thereIsEnemy(Position position)
        {
            Piece piece = board.piece(position);
            return piece != null && piece.color != color;
        }

        private bool free(Position position)
        {
            return board.piece(position) == null;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[board.rows, board.columns];
            Position pos = new Position(0, 0);

            if (color == Color.White)
            {
                pos.defineValues(position.row - 1, position.column);
                if (board.validPosition(pos) && free(pos))
                {
                    mat[pos.row, pos.column] = true;
                }
                pos.defineValues(position.row - 2, position.column);
                Position p2 = new Position(position.row - 1, position.column);
                if (board.validPosition(p2) && free(p2) && board.validPosition(pos) && free(pos) && moveCount == 0)
                {
                    mat[pos.row, pos.column] = true;
                }
                pos.defineValues(position.row - 1, position.column - 1);
                if (board.validPosition(pos) && thereIsEnemy(pos))
                {
                    mat[pos.row, pos.column] = true;
                }
                pos.defineValues(position.row - 1, position.column + 1);
                if (board.validPosition(pos) && thereIsEnemy(pos))
                {
                    mat[pos.row, pos.column] = true;
                }

                // SPECIALMOVE EN PASSANT
                if (position.row == 3)
                {
                    Position left = new Position(position.row, position.column - 1);
                    if (board.validPosition(left) && thereIsEnemy(left) && board.piece(left) == match.vulnerableEnPassant)
                    {
                        mat[left.row - 1, left.column] = true;
                    }
                    Position right = new Position(position.row, position.column + 1);
                    if (board.validPosition(right) && thereIsEnemy(right) && board.piece(right) == match.vulnerableEnPassant)
                    {
                        mat[right.row - 1, right.column] = true;
                    }
                }
            }
            else
            {
                pos.defineValues(position.row + 1, position.column);
                if (board.validPosition(pos) && free(pos))
                {
                    mat[pos.row, pos.column] = true;
                }
                pos.defineValues(position.row + 2, position.column);
                Position p2 = new Position(position.row + 1, position.column);
                if (board.validPosition(p2) && free(p2) && board.validPosition(pos) && free(pos) && moveCount == 0)
                {
                    mat[pos.row, pos.column] = true;
                }
                pos.defineValues(position.row + 1, position.column - 1);
                if (board.validPosition(pos) && thereIsEnemy(pos))
                {
                    mat[pos.row, pos.column] = true;
                }
                pos.defineValues(position.row + 1, position.column + 1);
                if (board.validPosition(pos) && thereIsEnemy(pos))
                {
                    mat[pos.row, pos.column] = true;
                }

                // SPECIALMOVE EN PASSANT
                if (position.row == 4)
                {
                    Position left = new Position(position.row, position.column - 1);
                    if (board.validPosition(left) && thereIsEnemy(left) && board.piece(left) == match.vulnerableEnPassant)
                    {
                        mat[left.row + 1, left.column] = true;
                    }
                    Position right = new Position(position.row, position.column + 1);
                    if (board.validPosition(right) && thereIsEnemy(right) && board.piece(right) == match.vulnerableEnPassant)
                    {
                        mat[right.row + 1, right.column] = true;
                    }
                }
            }

            return mat;
        }
    }
}
