using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szachy2
{
    class Pawn : Piece
    {
        bool firstMove = true;
        public Pawn(bool color) : base(color)
        {
        }

        public void HighlightAttackSquares(int gotoSquareX, int gotoSquareY, Square[,] squares, bool threat)
        {
            if (!threat)
            {
                Piece possiblePiece = squares[gotoSquareX, gotoSquareY].GetPiece();
                if (possiblePiece != null)
                {
                    if (possiblePiece.GetColor() == !color)
                    {
                        squares[gotoSquareX, gotoSquareY].SetHighlight(true);
                    }
                }
                else if (squares[gotoSquareX, gotoSquareY].GetEnPassant() != null)
                {
                    squares[gotoSquareX, gotoSquareY].SetHighlight(true);
                }
            }
            else //king never can go to the diagonal of the pawn
            {
                squares[gotoSquareX, gotoSquareY].SetHighlight(true, true);
            }
        }

        private void HighlightAttack(Square[,] squares, Square mySquare, bool threat)
        {
            if(color == Constants.White)
            {
                if(mySquare.GetY() > 0) //can attack left
                {
                    HighlightAttackSquares(mySquare.GetX() + 1, mySquare.GetY() - 1, squares, threat);
                }

                if(mySquare.GetY() < 7) //can attack right
                {
                    HighlightAttackSquares(mySquare.GetX() + 1, mySquare.GetY() + 1, squares, threat);
                }
            } else //black
            {
                if (mySquare.GetY() > 0) //can attack left
                {
                    HighlightAttackSquares(mySquare.GetX() - 1, mySquare.GetY() - 1, squares, threat);
                }

                if (mySquare.GetY() < 7) //can attack right
                {
                    HighlightAttackSquares(mySquare.GetX() - 1, mySquare.GetY() + 1, squares, threat);
                }
            }
        }

        public override void HighlightMovement(ChessBoard chessBoard, Square mySquare, bool threat = false)
        {
            Square[,] squares = chessBoard.GetSquares();

            if (color == Constants.White)
            {
                if (!threat)
                {
                    if (squares[mySquare.GetX() + 1, mySquare.GetY()].GetPiece() == null)
                    {
                        squares[mySquare.GetX() + 1, mySquare.GetY()].SetHighlight(true);
                        if (firstMove)
                        {
                            if (squares[mySquare.GetX() + 2, mySquare.GetY()].GetPiece() == null)
                            {
                                squares[mySquare.GetX() + 2, mySquare.GetY()].SetHighlight(true);
                            }
                        }
                    }
                }
            }
            else //black
            {
                if (!threat)
                {
                    if (squares[mySquare.GetX() - 1, mySquare.GetY()].GetPiece() == null)
                    {
                        squares[mySquare.GetX() - 1, mySquare.GetY()].SetHighlight(true);
                        if (firstMove)
                        {
                            if (squares[mySquare.GetX() - 2, mySquare.GetY()].GetPiece() == null)
                            {
                                squares[mySquare.GetX() - 2, mySquare.GetY()].SetHighlight(true);
                            }
                        }
                    }
                }
            }
            HighlightAttack(squares, mySquare, threat);
        }

        public override void Move(Square startSquare, Square endSquare, ChessBoard chessBoard)
        {
            startSquare.SetPiece(null);
            if(endSquare.GetEnPassant() != null)
            {
                endSquare.GetEnPassant().SetPiece(null);
                endSquare.SetEnPassant(null);
            } 
            endSquare.SetPiece(this);
            firstMove = false;
        }
    }
}
