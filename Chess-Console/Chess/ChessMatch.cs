using board;

namespace chess
{
    internal class ChessMatch
    {
        public Board board { get; private set; }
        public int turn { get; private set; }
        public Color currentPlayer { get; private set; }
        public bool finished { get; private set; }

        public ChessMatch()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            finished = false;
            putPieces();
        }

        public void performChessMove(Position sourcePosition, Position targetPosition)
        {
            Piece p = board.removePiece(sourcePosition);
            p.increaseQtMovements();
            Piece capturedPiece = board.removePiece(targetPosition);
            board.placePiece(p, targetPosition);
        }

        public void makeMove(Position sourcePosition, Position targetPosition)
        {
            performChessMove(sourcePosition, targetPosition);
            turn++;
            changePlayer();
        }

        public void validateSourcePosition(Position position)
        {
            if (board.piece(position) == null)
            {
                throw new BoardException("Não existe peça na posição de origem escolhida!");
            }
            if (currentPlayer != board.piece(position).color)
            {
                throw new BoardException("A peça de origem escolhida não é sua!");
            }
            if (!board.piece(position).isThereAnyPossibleMove())
            {
                throw new BoardException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void validateTargetPosition(Position source, Position target)
        {
            if (!board.piece(source).canMoveTo(target))
            {
                throw new BoardException("Posição de destino inválida!");
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

        private void putPieces()
        {
            board.placePiece(new Tower(board, Color.White), new ChessPosition('c', 1).toPosition());
            board.placePiece(new Tower(board, Color.White), new ChessPosition('c', 2).toPosition());
            board.placePiece(new Tower(board, Color.White), new ChessPosition('d', 2).toPosition());
            board.placePiece(new Tower(board, Color.White), new ChessPosition('e', 2).toPosition());
            board.placePiece(new Tower(board, Color.White), new ChessPosition('e', 1).toPosition());
            board.placePiece(new King(board, Color.White), new ChessPosition('d', 1).toPosition());

            board.placePiece(new Tower(board, Color.Black), new ChessPosition('c', 7).toPosition());
            board.placePiece(new Tower(board, Color.Black), new ChessPosition('c', 8).toPosition());
            board.placePiece(new Tower(board, Color.Black), new ChessPosition('d', 7).toPosition());
            board.placePiece(new Tower(board, Color.Black), new ChessPosition('e', 7).toPosition());
            board.placePiece(new Tower(board, Color.Black), new ChessPosition('e', 8).toPosition());
            board.placePiece(new King(board, Color.Black), new ChessPosition('d', 8).toPosition());
        }
    }
}