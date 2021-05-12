using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szachy2
{
    class Bishop : Piece
    {
        public Bishop(bool color) : base(color)
        {
        }


        public override void HighlightMovement(ChessBoard chessBoard, Square mySquare, bool threat = false)
        {
            int x = mySquare.GetX();
            int y = mySquare.GetY();
            Square[,] squares = chessBoard.GetSquares();

            //left-up
            int i = x;
            int j = y;
            if(i > 0 && j < 7)
            {
                i--;
                j++;
                do
                {
                    if (squares[i, j].GetPiece() == null) //no piece
                    {   
                        Highlight(true, threat, squares[i, j]);
                        i--;
                        j++;
                    }
                    else if (squares[i, j].GetPiece().GetColor() == color) //same color piece
                    {
                        break;
                    }
                    else //another color piece
                    {
                        Highlight(true, threat, squares[i, j]);
                        break;
                    }
                } while (i >= 0 && j <= 7);
            }

            //left-down
            i = x;
            j = y;
            if (i > 0 && j > 0)
            {
                i--;
                j--;
                do
                {
                    if (squares[i, j].GetPiece() == null) //no piece
                    {
                        Highlight(true, threat, squares[i, j]);
                        i--;
                        j--;
                    }
                    else if (squares[i, j].GetPiece().GetColor() == color) //same color piece
                    {
                        break;
                    }
                    else //another color piece
                    {
                        Highlight(true, threat, squares[i, j]);
                        break;
                    }
                } while (i >= 0 && j >= 0);
            }

            //right-down
            i = x;
            j = y;
            if (i < 7 && j > 0)
            {
                i++;
                j--;
                do
                {
                    if (squares[i, j].GetPiece() == null) //no piece
                    {
                        Highlight(true, threat, squares[i, j]);
                        i++;
                        j--;
                    }
                    else if (squares[i, j].GetPiece().GetColor() == color) //same color piece
                    {
                        break;
                    }
                    else //another color piece
                    {
                        Highlight(true, threat, squares[i, j]);
                        break;
                    }
                } while (i <= 7 && j >= 0);
            }

            //right-up
            i = x;
            j = y;
            if (i < 7 && j < 7)
            {
                i++;
                j++;
                do
                {
                    if (squares[i, j].GetPiece() == null) //no piece
                    {
                        Highlight(true, threat, squares[i, j]);
                        i++;
                        j++;
                    }
                    else if (squares[i, j].GetPiece().GetColor() == color) //same color piece
                    {
                        break;
                    }
                    else //another color piece
                    {
                        Highlight(true, threat, squares[i, j]);
                        break;
                    }
                } while (i <= 7 && j <= 7);
            }

        }

        public override void Move(Square startSquare, Square endSquare, ChessBoard chessBoard)
        {
            startSquare.SetPiece(null);
            endSquare.SetPiece(this);
        }
    }
}
