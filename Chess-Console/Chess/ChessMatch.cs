using board;
using System.Collections.Generic;

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


        public ChessMatch()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            finished = false;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            putPieces();
        }

        public void performChessMove(Position sourcePosition, Position targetPosition)
        {
            Piece p = board.removePiece(sourcePosition);
            p.increaseQtMovements();
            Piece capturedPiece = board.removePiece(targetPosition);
            board.placePiece(p, targetPosition);
            if(capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }
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

        public HashSet<Piece> capturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach(Piece x in captured)
            {
                if(x.color == color)
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

        public void putNewPiece(char column, int row, Piece piece)
        {
            board.placePiece(piece, new ChessPosition(column, row).toPosition());
            pieces.Add(piece);
        }

        private void putPieces()
        {
            putNewPiece('c', 1, new Tower(board, Color.White));
            putNewPiece('c', 2, new Tower(board, Color.White));
            putNewPiece('d', 2, new Tower(board, Color.White));
            putNewPiece('e', 2, new Tower(board, Color.White));
            putNewPiece('e', 1, new Tower(board, Color.White));
            putNewPiece('d', 1, new King(board, Color.White));

            putNewPiece('c', 7, new Tower(board, Color.Black));
            putNewPiece('c', 8, new Tower(board, Color.Black));
            putNewPiece('d', 7, new Tower(board, Color.Black));
            putNewPiece('e', 7, new Tower(board, Color.Black));
            putNewPiece('e', 8, new Tower(board, Color.Black));
            putNewPiece('d', 8, new King(board, Color.Black));


        }
    }
}