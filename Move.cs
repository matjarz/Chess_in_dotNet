using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szachy2
{
    class Move
    {
        Square startSquare;
        Square endSquare;

        Piece startSquarePiece;
        Piece endSquarePiece;


        public Square GetStartSquare()
        {
            return startSquare;
        }

        public Square GetEndSquare()
        {
            return endSquare;
        }

        public Move(Square startSquare, Square endSquare, Piece startSquarePiece, Piece endSquarePiece = null)
        {
            this.startSquare = startSquare;
            this.endSquare = endSquare;
            this.startSquarePiece = startSquarePiece;
            this.endSquarePiece = endSquarePiece;
        }

        public string GetMoveString()
        {
            string result = "";

            if(startSquarePiece.GetType() == typeof(King))
            {
                if(Math.Abs(startSquare.GetY() - endSquare.GetY()) > 1) //check if castle
                {
                    if (startSquare.GetY() > endSquare.GetY()) //castle left
                    {
                        result = "O-O-O";
                    }
                    else
                    {
                        result = "O-O";
                    }
                }
                else //not castling
                {
                    result = "K" + startSquare.GetSquareName();
                    if (endSquarePiece != null) //attack
                    {
                        result += "x";
                    }
                    result += endSquare.GetSquareName();
                }
            }
            else if(startSquarePiece.GetType() == typeof(Pawn))
            {
                result = startSquare.GetSquareName();
                if (startSquare.GetY() != endSquare.GetY()) //to catch enPassant
                    result += "x";
                result += endSquare.GetSquareName();
            }
            else if (startSquarePiece.GetType() == typeof(Rook))
            {
                result = "R" + startSquare.GetSquareName();
                if (endSquarePiece != null)
                    result += "x";
                result += endSquare.GetSquareName();
            }
            else if (startSquarePiece.GetType() == typeof(Bishop))
            {
                result = "B" + startSquare.GetSquareName();
                if (endSquarePiece != null)
                    result += "x";
                result += endSquare.GetSquareName();
            }
            else if (startSquarePiece.GetType() == typeof(Queen))
            {
                result = "Q" + startSquare.GetSquareName();
                if (endSquarePiece != null)
                    result += "x";
                result += endSquare.GetSquareName();
            }
            else if (startSquarePiece.GetType() == typeof(Knight))
            {
                result = "N" + startSquare.GetSquareName();
                if (endSquarePiece != null)
                    result += "x";
                result += endSquare.GetSquareName();
            }

            return result;
        }

        public bool GetTurn()
        {
            return startSquarePiece.GetColor();
        }

        public void PrintMove()
        {
            Console.WriteLine(GetMoveString());
        }

    }
}
