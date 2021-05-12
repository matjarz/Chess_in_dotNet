using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szachy2
{
    class Rook : Piece
    {
        public Rook(bool color) : base(color)
        {
        }

        private bool firstMove = true;

        public override void HighlightMovement(ChessBoard chessBoard, Square mySquare, bool threat = false)
        {
            int x = mySquare.GetX();
            int y = mySquare.GetY();
            Square[,] squares = chessBoard.GetSquares();

            int i = x;
            if(x > 0) //go left
            {
                i--;
                do
                {
                    if(squares[i, y].GetPiece() == null) //no piece
                    {
                        Highlight(true, threat, squares[i, y]);
                        i--;
                    }
                    else if(squares[i, y].GetPiece().GetColor() == color) //same color piece
                    {
                        break;
                    }
                    else //another color piece
                    {
                        Highlight(true, threat, squares[i, y]);
                        break;
                    }
                } while (i >= 0);
            }

            i = x;
            if (x < 7) //go right
            {
                i++;
                do
                {
                    if (squares[i, y].GetPiece() == null) //no piece
                    {
                        Highlight(true, threat, squares[i, y]);
                        i++;
                    }
                    else if (squares[i, y].GetPiece().GetColor() == color) //same color piece
                    {
                        break;
                    }
                    else //another color piece
                    {
                        Highlight(true, threat, squares[i, y]);
                        break;
                    }
                } while (i <= 7);
            }

            i = y;
            if (y < 7) //go up
            {
                i++;
                do
                {
                    if (squares[x, i].GetPiece() == null) //no piece
                    {
                        Highlight(true, threat, squares[x, i]);
                        i++;
                    }
                    else if (squares[x, i].GetPiece().GetColor() == color) //same color piece
                    {
                        break;
                    }
                    else //another color piece
                    {
                        Highlight(true, threat, squares[x, i]);
                        break;
                    }
                } while (i <= 7);
            }

            i = y;
            if (y > 0) //go down
            {
                i--;
                do
                {
                    if (squares[x, i].GetPiece() == null) //no piece
                    {
                        Highlight(true, threat, squares[x, i]);
                        i--;
                    }
                    else if (squares[x, i].GetPiece().GetColor() == color) //same color piece
                    {
                        break;
                    }
                    else //another color piece
                    {
                        Highlight(true, threat, squares[x, i]);
                        break;
                    }
                } while (i >= 0);
            }
        }

        public override void Move(Square startSquare, Square endSquare, ChessBoard chessBoard) //rook cannot "really" castle (we want to be albe to move rook without touching the king)
        {
            startSquare.SetPiece(null);
            endSquare.SetPiece(this);
            firstMove = false;
        }

        public bool GetFirstMove()
        {
            return firstMove;
        }

        public void setFirstMove(bool b)
        {
            firstMove = b;
        }
    }
}
