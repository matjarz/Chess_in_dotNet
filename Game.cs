using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Szachy2
{
    class Game
    {
        private ChessGameMoves gameMoves = new ChessGameMoves();
        private ChessBoard chessBoard = new ChessBoard();
        private Square selectedSquare = null;
        private bool turn = Constants.White;
        private String nazwaBialy = "Biały";
        private String nazwaCzarny = "Czarny";
        private static bool win = false;

        public static void EndGame(ChessBoard chessboard)
        {
            chessboard.HighlightThreats(true, false);
            chessboard.HighlightThreats(false, false);
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    if (win)
                    {
                        (window as MainWindow).endBackground.Visibility = Visibility.Visible;
                        (window as MainWindow).endLabel.Visibility = Visibility.Visible;
                        (window as MainWindow).zapisCheck.Visibility = Visibility.Visible;
                        (window as MainWindow).zamknijButton.Visibility = Visibility.Visible;
                        (window as MainWindow).podglad.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        (window as MainWindow).remisBackground.Visibility = Visibility.Visible;
                        (window as MainWindow).remisLabel.Visibility = Visibility.Visible;
                        (window as MainWindow).zapisCheck.Visibility = Visibility.Visible;
                        (window as MainWindow).zamknijButton.Visibility = Visibility.Visible;
                        (window as MainWindow).podglad.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        public void Click(int x, int y)
        {
            Square s = chessBoard.GetSquare(x, y);
            if(s.GetHighlight() == true) //jeśli hilighted to mozna tam isc
            {
                MakeMove(s);
            } 
            else
            {
                SelectStartSquare(s); //wybierz inne pole
            }
        }

        public void SelectStartSquare(Square square)
        {
            if (selectedSquare == square)
                return; //wybrane to samo pole

            if (chessBoard.PromotionTime())
            {
                if(square.GetPiece() != null) //chosen piece
                {
                    chessBoard.Promote(square.GetPiece());
                    turn = !turn;
                    CheckForWin();
                }
                return;
            }

            chessBoard.ClearHighlights();
            selectedSquare = null;
            foreach (Move m in chessBoard.GetMoves())
            {
                if (m.GetStartSquare() == square)    //one of possible moves!
                {
                    selectedSquare = square;
                    m.GetEndSquare().SetHighlight(true); //highlight one of legal moves
                }
            }
        }

        public Game()
        {
            chessBoard.GenerateLegalMoves(turn);
        }

        public void SaveGame()
        {
            gameMoves.saveAsPGN(this);
        }

        private void CheckForWin()
        {
            //checkm8 - king in check and no legal moves
            //stalem8 - king not in check but no legal moves
            chessBoard.GenerateLegalMoves(turn);
            if (chessBoard.GetMoves().Count() == 0)
            {
                //chessBoard.HighlightThreats(!turn, false);
                //chessBoard.ClearHighlights();
                //foreach (Window window in Application.Current.Windows)
                //{
                //    if (window.GetType() == typeof(MainWindow))
                //    {

                //        (window as MainWindow).clearSelection();
                            
                //    }
                //}
                if (chessBoard.KingInDanger(turn))
                {
                    win = true;
                    EndGame(chessBoard);
                }
                else
                {
                    win = false;
                    EndGame(chessBoard);
                }
            }
        }

        public bool GetWin()
		{
            return win;
		}

        public void MakeMove(Square endSquare)
        {
            //Console.WriteLine("Making Move to: " + endSquare.GetSquareName());
            Move m = new Move(selectedSquare, endSquare, selectedSquare.GetPiece(), endSquare.GetPiece());
            gameMoves.AddMove(m);
            chessBoard.MakeMove(selectedSquare, endSquare);
            chessBoard.HighlightLastMove(selectedSquare, endSquare);
            selectedSquare = null;

            if (chessBoard.PromotionTime())
            {
                chessBoard.ClearHighlights();
                chessBoard.SetupPromotion();
            }
            else
            {
                turn = !turn;
                CheckForWin();
            }
        }

        public ChessBoard GetChessBoard()
		{
            return chessBoard;
		}

        public ChessGameMoves GetGameMoves()
        {
            return gameMoves;
        }

        public bool GetTurn()
        {
            return turn;
        }

        public void SetNazwaBialy(String nazwa)
		{
            this.nazwaBialy = nazwa;
		}

        public void SetNazwaCzarny(String nazwa)
        {
            this.nazwaCzarny = nazwa;
        }

        public String GetNazwaBialy()
        {
            return this.nazwaBialy;
        }

        public String GetNazwaCzarny()
        {
            return this.nazwaCzarny;
        }

    }
}
