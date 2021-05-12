using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szachy2
{
    abstract class Piece
    {
        protected bool color;

        public Piece Copy()
        {
            return this.MemberwiseClone() as Piece; //The MemberwiseClone method creates a shallow copy by creating a new object
        }

        protected void Highlight(bool highligh, bool threat, Square s) //easier to look at
        {
            s.SetHighlight(highligh, threat);
        }

        public abstract void Move(Square startSquare, Square endSquare, ChessBoard chessBoard); //make move

        //highlights squares where piece can go (threat = false), or highlights square where piece threatenes opposite king (threat = true)
        public abstract void HighlightMovement(ChessBoard chessBoard, Square mySquare, bool threat = false);

        public Piece(bool color)
        {
            this.color = color;
        }

        public bool GetColor()
        {
            return color;
        }
    }
}
