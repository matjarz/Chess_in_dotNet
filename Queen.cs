using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szachy2
{
    class Queen : Piece
    {
        private Bishop bishop;
        private Rook rook;
        public Queen(bool color) : base(color)
        {
            bishop = new Bishop(color);
            rook = new Rook(color);
        }

        public override void HighlightMovement(ChessBoard chessBoard, Square mySquare, bool threat = false)
        {
            bishop.HighlightMovement(chessBoard, mySquare, threat);
            rook.HighlightMovement(chessBoard, mySquare, threat);
        }

        public override void Move(Square startSquare, Square endSquare, ChessBoard chessBoard)
        {
            startSquare.SetPiece(null);
            endSquare.SetPiece(this);
        }
    }
}
