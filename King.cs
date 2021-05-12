using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szachy2
{
    class King : Piece
    {
        public King(bool color) : base(color)
        {
        }

        private bool firstMove = true;
        private bool inDanger = false;

        private void HighlightKing(int i, int j, Square[,] squares, bool threat)
        {
            if (squares[i, j].GetPiece() == null) //no piece
            {
                Highlight(true, threat, squares[i, j]);
            }
            else if (squares[i, j].GetPiece().GetColor() == color) //same color piece
            {
                //no highlight - cannot go
            }
            else //another color piece
            {
                Highlight(true, threat, squares[i, j]);
            }
        }

        public override void HighlightMovement(ChessBoard chessBoard, Square mySquare, bool threat = false) 
        {
            int x = mySquare.GetX();
            int y = mySquare.GetY();
            Square[,] squares = chessBoard.GetSquares();

            //left is ok
            if(x > 0)
            {
                HighlightKing(x - 1, y, squares, threat);
                if(y > 0)
                    HighlightKing(x - 1, y - 1, squares, threat);
                if(y < 7)
                    HighlightKing(x - 1, y + 1, squares, threat);
            }
            
            if (x < 7) //right is ok
            {
                HighlightKing(x + 1, y, squares, threat);
                if (y > 0)
                    HighlightKing(x + 1, y - 1, squares, threat);
                if (y < 7)
                    HighlightKing(x + 1, y + 1, squares, threat);
            }
            
            if (y > 0) //down is ok
            {
                HighlightKing(x, y - 1, squares, threat);
                if (x > 0)
                    HighlightKing(x - 1, y - 1, squares, threat);
                if (x < 7)
                    HighlightKing(x + 1, y - 1, squares, threat);
            }

            if(y < 7)  //up is ok
            {
                HighlightKing(x, y + 1, squares, threat);
                if (x > 0)
                    HighlightKing(x - 1, y + 1, squares, threat);
                if (x < 7)
                    HighlightKing(x + 1, y + 1, squares, threat);
            }

            if (firstMove && !inDanger) //there's option for castling
            {
                int i;
                if(color == Constants.White)
                {
                    i = 0;
                }
                else //black
                {
                    i = 7;
                }
                //left side
                if (squares[i, 1].GetPiece() == null && squares[i, 2].GetPiece() == null && squares[i, 3].GetPiece() == null && !squares[i, 2].GetThreat() && !squares[i, 3].GetThreat()) //no pieces in between and king doesnt go through threats
                {
                    if (squares[i, 0].GetPiece() != null)
                    {
                        if (squares[i, 0].GetPiece().GetType() == typeof(Rook))
                        {
                            Rook r = squares[i, 0].GetPiece() as Rook;
                            if (r.GetFirstMove())
                            {
                                if(!threat)
                                    squares[i, 2].SetHighlight(true); //castling is not really a threat
                            }
                        }
                    }
                }
                //right side
                if (squares[i, 5].GetPiece() == null && squares[i, 6].GetPiece() == null && !squares[i, 5].GetThreat() && !squares[i, 6].GetThreat()) //no pieces in between
                {
                    if (squares[i, 7].GetPiece() != null)
                    {
                        if (squares[i, 7].GetPiece().GetType() == typeof(Rook))
                        {
                            Rook r = squares[i, 7].GetPiece() as Rook;
                            if (r.GetFirstMove())
                            {
                                if(!threat)
                                    squares[i, 6].SetHighlight(true);
                            }
                        }
                    }
                }
            }
        }

        public override void Move(Square startSquare, Square endSquare, ChessBoard chessBoard)
        {
            Square[,] squares = chessBoard.GetSquares();
            if(Math.Abs(startSquare.GetY() - endSquare.GetY()) > 1) //castling
            {
                startSquare.SetPiece(null);
                endSquare.SetPiece(this);
                if(startSquare.GetY() > endSquare.GetY()) //castle left
                {
                    Rook rook = squares[startSquare.GetX(), 0].GetPiece() as Rook;
                    rook.setFirstMove(false);
                    squares[startSquare.GetX(), 0].SetPiece(null);
                    squares[startSquare.GetX(), 3].SetPiece(rook);
                }
                else //castle right
                {
                    Rook rook = squares[startSquare.GetX(), 7].GetPiece() as Rook;
                    rook.setFirstMove(false);
                    squares[startSquare.GetX(), 7].SetPiece(null);
                    squares[startSquare.GetX(), 5].SetPiece(rook);
                }
            }
            else
            {
                startSquare.SetPiece(null);
                endSquare.SetPiece(this);
            }
            firstMove = false;
        }

        public void SetInDanger(bool inDanger)
        {
            this.inDanger = inDanger;
        }
    }
}
