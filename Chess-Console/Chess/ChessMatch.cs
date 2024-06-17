using board;

namespace chess
{
    internal class ChessMatch
    {
        public Board board { get; private set; }
        public int turn { get; private set; }
        public Color currentPlayer { get; private set; }
        public bool finished { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> captured;
        public bool check { get; private set; }
        public Piece vulnerableEnPassant { get; private set; }

        public ChessMatch()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            finished = false;
            check = false;
            vulnerableEnPassant = null;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            putPieces();
        }

        public Piece performChessMove(Position sourcePosition, Position targetPosition)
        {
            Piece p = board.removePiece(sourcePosition);
            p.increaseMoveCount();
            Piece capturedPiece = board.removePiece(targetPosition);
            board.placePiece(p, targetPosition);
            if (capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }

            // SPECIALMOVE KINGSIDE ROOK
            if (p is King && targetPosition.column == sourcePosition.column + 2)
            {
                Position sourceR = new Position(sourcePosition.row, sourcePosition.column + 3);
                Position targetR = new Position(sourcePosition.row, sourcePosition.column + 1);
                Piece R = board.removePiece(sourceR);
                R.increaseMoveCount();
                board.placePiece(R, targetR);
            }

            // SPECIALMOVE QUEENSIDE ROOK
            if (p is King && targetPosition.column == sourcePosition.column - 2)
            {
                Position sourceR = new Position(sourcePosition.row, sourcePosition.column - 4);
                Position targetR = new Position(sourcePosition.row, sourcePosition.column - 1);
                Piece R = board.removePiece(sourceR);
                R.increaseMoveCount();
                board.placePiece(R, targetR);
            }

            // SPECIALMOVE EN PASSANT
            if (p is Pawn)
            {
                if (sourcePosition.column != targetPosition.column && capturedPiece == null)
                {
                    Position posP;
                    if (p.color == Color.White)
                    {
                        posP = new Position(targetPosition.row + 1, targetPosition.column);
                    }
                    else
                    {
                        posP = new Position(targetPosition.row - 1, targetPosition.column);
                    }
                    capturedPiece = board.removePiece(posP);
                    captured.Add(capturedPiece);
                }
            }

            return capturedPiece;
        }

        public void undoMove(Position sourcePosition, Position targetPosition, Piece capturedPiece)
        {
            Piece p = board.removePiece(targetPosition);
            p.decreaseMoveCount();
            if (capturedPiece != null)
            {
                board.placePiece(capturedPiece, targetPosition);
                captured.Remove(capturedPiece);
            }
            board.placePiece(p, sourcePosition);

            // SPECIALMOVE KINGSIDE ROOK
            if (p is King && targetPosition.column == sourcePosition.column + 2)
            {
                Position sourceR = new Position(sourcePosition.row, sourcePosition.column + 3);
                Position targetR = new Position(sourcePosition.row, sourcePosition.column + 1);
                Piece R = board.removePiece(targetR);
                R.decreaseMoveCount();
                board.placePiece(R, sourceR);
            }

            // SPECIALMOVE QUEENSIDE ROOK
            if (p is King && targetPosition.column == sourcePosition.column - 2)
            {
                Position sourceR = new Position(sourcePosition.row, sourcePosition.column - 4);
                Position targetR = new Position(sourcePosition.row, sourcePosition.column - 1);
                Piece R = board.removePiece(targetR);
                R.decreaseMoveCount();
                board.placePiece(R, sourceR);
            }

            // SPECIALMOVE EN PASSANT
            if (p is Pawn)
            {
                if (sourcePosition.column != targetPosition.column && capturedPiece == vulnerableEnPassant)
                {
                    Piece pawn = board.removePiece(targetPosition);
                    Position posP;
                    if (p.color == Color.White)
                    {
                        posP = new Position(3, targetPosition.column);
                    }
                    else{
                        posP = new Position(4, targetPosition.column);
                    }
                    board.placePiece(pawn, posP);
                }
            }
        }

        public void makeMove(Position sourcePosition, Position targetPosition)
        {
            Piece capturedPiece = performChessMove(sourcePosition, targetPosition);

            if (testCheck(currentPlayer))
            {
                undoMove(sourcePosition, targetPosition, capturedPiece);
                throw new BoardException("You can't put yourself in check!");
            }

            if (testCheck(opponent(currentPlayer)))
            {
                check = true;
            }
            else
            {
                check = false;
            }

            if (testCheckMate(opponent(currentPlayer)))
            {
                finished = true;
            }
            else
            {
                turn++;
                changePlayer();
            }

            Piece p = board.piece(targetPosition);

            // SPECIALMOVE EN PASSANT
            if (p is Pawn && (targetPosition.row == sourcePosition.row - 2 || targetPosition.row == sourcePosition.row + 2))
            {
                vulnerableEnPassant = p;
            }
            else
            {
                vulnerableEnPassant = null;
            }

        }

        public void validateSourcePosition(Position position)
        {
            if (board.piece(position) == null)
            {
                throw new BoardException("There is no piece in the chosen origin position!");
            }
            if (currentPlayer != board.piece(position).color)
            {
                throw new BoardException("The original piece chosen is not yours!");
            }
            if (!board.piece(position).isThereAnyPossibleMove())
            {
                throw new BoardException("There are no possible moves for the chosen source piece!");
            }
        }

        public void validateTargetPosition(Position source, Position target)
        {
            if (!board.piece(source).possibleMove(target))
            {
                throw new BoardException("Invalid target position!");
            }
        }

        private void changePlayer()
        {
            if (currentPlayer == Color.White)
            {
                currentPlayer = Color.Black;
            }
            else
            {
                currentPlayer = Color.White;
            }
        }

        public HashSet<Piece> capturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in captured)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> piecesInPlay(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in pieces)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(capturedPieces(color));
            return aux;
        }

        private Color opponent(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece king(Color color)
        {
            foreach (Piece x in piecesInPlay(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }

        public bool testCheck(Color color)
        {
            Piece K = king(color);
            if (K == null)
            {
                throw new BoardException("There is no king of the color" + color + "on the board");
            }

            foreach (Piece x in piecesInPlay(opponent(color)))
            {
                bool[,] mat = x.possibleMoves();
                if (mat[K.position.row, K.position.column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool testCheckMate(Color color)
        {
            if (!testCheck(color))
            {
                return false;
            }
            foreach (Piece x in piecesInPlay(color))
            {
                bool[,] mat = x.possibleMoves();
                for (int i = 0; i < board.rows; i++)
                {
                    for (int j = 0; j < board.columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position source = x.position;
                            Position target = new Position(i, j);
                            Piece capturedPiece = performChessMove(source, target);
                            bool test = testCheck(color);
                            undoMove(source, target, capturedPiece);
                            if (!test)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void putNewPiece(char column, int row, Piece piece)
        {
            board.placePiece(piece, new ChessPosition(column, row).toPosition());
            pieces.Add(piece);
        }

        private void putPieces()
        {
            putNewPiece('a', 1, new Rook(board, Color.White));
            putNewPiece('b', 1, new Knight(board, Color.White));
            putNewPiece('c', 1, new Bishop(board, Color.White));
            putNewPiece('d', 1, new Queen(board, Color.White));
            putNewPiece('e', 1, new King(board, Color.White, this));
            putNewPiece('f', 1, new Bishop(board, Color.White));
            putNewPiece('g', 1, new Knight(board, Color.White));
            putNewPiece('h', 1, new Rook(board, Color.White));
            putNewPiece('a', 2, new Pawn(board, Color.White, this));
            putNewPiece('b', 2, new Pawn(board, Color.White, this));
            putNewPiece('c', 2, new Pawn(board, Color.White, this));
            putNewPiece('d', 2, new Pawn(board, Color.White, this));
            putNewPiece('e', 2, new Pawn(board, Color.White, this));
            putNewPiece('f', 2, new Pawn(board, Color.White, this));
            putNewPiece('g', 2, new Pawn(board, Color.White, this));
            putNewPiece('h', 2, new Pawn(board, Color.White, this));

            putNewPiece('a', 8, new Rook(board, Color.Black));
            putNewPiece('b', 8, new Knight(board, Color.Black));
            putNewPiece('c', 8, new Bishop(board, Color.Black));
            putNewPiece('d', 8, new Queen(board, Color.Black));
            putNewPiece('e', 8, new King(board, Color.Black, this));
            putNewPiece('f', 8, new Bishop(board, Color.Black));
            putNewPiece('g', 8, new Knight(board, Color.Black));
            putNewPiece('h', 8, new Rook(board, Color.Black));
            putNewPiece('a', 7, new Pawn(board, Color.Black, this));
            putNewPiece('b', 7, new Pawn(board, Color.Black, this));
            putNewPiece('c', 7, new Pawn(board, Color.Black, this));
            putNewPiece('d', 7, new Pawn(board, Color.Black, this));
            putNewPiece('e', 7, new Pawn(board, Color.Black, this));
            putNewPiece('f', 7, new Pawn(board, Color.Black, this));
            putNewPiece('g', 7, new Pawn(board, Color.Black, this));
            putNewPiece('h', 7, new Pawn(board, Color.Black, this));
        }
    }
}