namespace board
{
    internal class Piece
    {
        public Position position { get; set; }
        public Color color { get; set; }
        public int qtMovements { get; set; }
        public Board board { get; set; }

        public Piece(Position position, Color color, Board board)
        {
            this.position = position;
            this.color = color;
            this.board = board;
        }
    }
}
