using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Szachy2
{
	// Main window class
	public partial class MainWindow : Window
	{
		// Constants used to color the events on the chessboard
		SolidColorBrush light = new SolidColorBrush(Colors.Moccasin);
		SolidColorBrush dark = new SolidColorBrush(Colors.Brown);
		SolidColorBrush highlighted = new SolidColorBrush(Colors.Green);
		SolidColorBrush threat = new SolidColorBrush(Colors.Yellow);
		SolidColorBrush lastMove = new SolidColorBrush(Colors.Black);
		SolidColorBrush white = new SolidColorBrush(Colors.White);

		// Reference to the game class
		Game game = new Game();

		// Images representing the figures
		ImageBrush bPionJ = new ImageBrush(new BitmapImage(new Uri("../../ikony/bPionekJ.png", UriKind.Relative)));
		ImageBrush bWiezaJ = new ImageBrush(new BitmapImage(new Uri("../../ikony/bWiezaJ.png", UriKind.Relative)));
		ImageBrush bKonJ = new ImageBrush(new BitmapImage(new Uri("../../ikony/bKonJ.png", UriKind.Relative)));
		ImageBrush bLauferJ = new ImageBrush(new BitmapImage(new Uri("../../ikony/bLauferJ.png", UriKind.Relative)));
		ImageBrush bKrolowaJ = new ImageBrush(new BitmapImage(new Uri("../../ikony/bKrolowaJ.png", UriKind.Relative)));
		ImageBrush bKrolJ = new ImageBrush(new BitmapImage(new Uri("../../ikony/bKrolJ.png", UriKind.Relative)));
		ImageBrush cPionJ = new ImageBrush(new BitmapImage(new Uri("../../ikony/cPionekJ.png", UriKind.Relative)));
		ImageBrush cWiezaJ = new ImageBrush(new BitmapImage(new Uri("../../ikony/cWiezaJ.png", UriKind.Relative)));
		ImageBrush cKonJ = new ImageBrush(new BitmapImage(new Uri("../../ikony/cKonJ.png", UriKind.Relative)));
		ImageBrush cLauferJ = new ImageBrush(new BitmapImage(new Uri("../../ikony/cLauferJ.png", UriKind.Relative)));
		ImageBrush cKrolowaJ = new ImageBrush(new BitmapImage(new Uri("../../ikony/cKrolowaJ.png", UriKind.Relative)));
		ImageBrush cKrolJ = new ImageBrush(new BitmapImage(new Uri("../../ikony/cKrolJ.png", UriKind.Relative)));

		ImageBrush bPionC = new ImageBrush(new BitmapImage(new Uri("../../ikony/bPionekC.png", UriKind.Relative)));
		ImageBrush bWiezaC = new ImageBrush(new BitmapImage(new Uri("../../ikony/bWiezaC.png", UriKind.Relative)));
		ImageBrush bKonC = new ImageBrush(new BitmapImage(new Uri("../../ikony/bKonC.png", UriKind.Relative)));
		ImageBrush bLauferC = new ImageBrush(new BitmapImage(new Uri("../../ikony/bLauferC.png", UriKind.Relative)));
		ImageBrush bKrolowaC = new ImageBrush(new BitmapImage(new Uri("../../ikony/bKrolowaC.png", UriKind.Relative)));
		ImageBrush bKrolC = new ImageBrush(new BitmapImage(new Uri("../../ikony/bKrolC.png", UriKind.Relative)));
		ImageBrush cPionC = new ImageBrush(new BitmapImage(new Uri("../../ikony/cPionekC.png", UriKind.Relative)));
		ImageBrush cWiezaC = new ImageBrush(new BitmapImage(new Uri("../../ikony/cWiezaC.png", UriKind.Relative)));
		ImageBrush cKonC = new ImageBrush(new BitmapImage(new Uri("../../ikony/cKonC.png", UriKind.Relative)));
		ImageBrush cLauferC = new ImageBrush(new BitmapImage(new Uri("../../ikony/cLauferC.png", UriKind.Relative)));
		ImageBrush cKrolowaC = new ImageBrush(new BitmapImage(new Uri("../../ikony/cKrolowaC.png", UriKind.Relative)));
		ImageBrush cKrolC = new ImageBrush(new BitmapImage(new Uri("../../ikony/cKrolC.png", UriKind.Relative)));

		// Arrays holding the front and background fields
		public Button[,] chessboard = new Button[8, 8];
		public Button[,] fchessboard = new Button[8, 8];

		SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\BazaDanych.mdf;Integrated Security=True");
		int rowindex = 0;

		// Assigning the fields to the arrays
		private void assignFields()
		{
			// Front array
			chessboard[7, 0] = a1;
			chessboard[7, 1] = b1;
			chessboard[7, 2] = c1;
			chessboard[7, 3] = d1;
			chessboard[7, 4] = e1;
			chessboard[7, 5] = f1;
			chessboard[7, 6] = g1;
			chessboard[7, 7] = h1;

			chessboard[6, 0] = a2;
			chessboard[6, 1] = b2;
			chessboard[6, 2] = c2;
			chessboard[6, 3] = d2;
			chessboard[6, 4] = e2;
			chessboard[6, 5] = f2;
			chessboard[6, 6] = g2;
			chessboard[6, 7] = h2;

			chessboard[5, 0] = a3;
			chessboard[5, 1] = b3;
			chessboard[5, 2] = c3;
			chessboard[5, 3] = d3;
			chessboard[5, 4] = e3;
			chessboard[5, 5] = f3;
			chessboard[5, 6] = g3;
			chessboard[5, 7] = h3;

			chessboard[4, 0] = a4;
			chessboard[4, 1] = b4;
			chessboard[4, 2] = c4;
			chessboard[4, 3] = d4;
			chessboard[4, 4] = e4;
			chessboard[4, 5] = f4;
			chessboard[4, 6] = g4;
			chessboard[4, 7] = h4;

			chessboard[3, 0] = a5;
			chessboard[3, 1] = b5;
			chessboard[3, 2] = c5;
			chessboard[3, 3] = d5;
			chessboard[3, 4] = e5;
			chessboard[3, 5] = f5;
			chessboard[3, 6] = g5;
			chessboard[3, 7] = h5;

			chessboard[2, 0] = a6;
			chessboard[2, 1] = b6;
			chessboard[2, 2] = c6;
			chessboard[2, 3] = d6;
			chessboard[2, 4] = e6;
			chessboard[2, 5] = f6;
			chessboard[2, 6] = g6;
			chessboard[2, 7] = h6;

			chessboard[1, 0] = a7;
			chessboard[1, 1] = b7;
			chessboard[1, 2] = c7;
			chessboard[1, 3] = d7;
			chessboard[1, 4] = e7;
			chessboard[1, 5] = f7;
			chessboard[1, 6] = g7;
			chessboard[1, 7] = h7;

			chessboard[0, 0] = a8;
			chessboard[0, 1] = b8;
			chessboard[0, 2] = c8;
			chessboard[0, 3] = d8;
			chessboard[0, 4] = e8;
			chessboard[0, 5] = f8;
			chessboard[0, 6] = g8;
			chessboard[0, 7] = h8;

			// Back array
			fchessboard[7, 0] = fa1;
			fchessboard[7, 1] = fb1;
			fchessboard[7, 2] = fc1;
			fchessboard[7, 3] = fd1;
			fchessboard[7, 4] = fe1;
			fchessboard[7, 5] = ff1;
			fchessboard[7, 6] = fg1;
			fchessboard[7, 7] = fh1;

			fchessboard[6, 0] = fa2;
			fchessboard[6, 1] = fb2;
			fchessboard[6, 2] = fc2;
			fchessboard[6, 3] = fd2;
			fchessboard[6, 4] = fe2;
			fchessboard[6, 5] = ff2;
			fchessboard[6, 6] = fg2;
			fchessboard[6, 7] = fh2;

			fchessboard[5, 0] = fa3;
			fchessboard[5, 1] = fb3;
			fchessboard[5, 2] = fc3;
			fchessboard[5, 3] = fd3;
			fchessboard[5, 4] = fe3;
			fchessboard[5, 5] = ff3;
			fchessboard[5, 6] = fg3;
			fchessboard[5, 7] = fh3;

			fchessboard[4, 0] = fa4;
			fchessboard[4, 1] = fb4;
			fchessboard[4, 2] = fc4;
			fchessboard[4, 3] = fd4;
			fchessboard[4, 4] = fe4;
			fchessboard[4, 5] = ff4;
			fchessboard[4, 6] = fg4;
			fchessboard[4, 7] = fh4;

			fchessboard[3, 0] = fa5;
			fchessboard[3, 1] = fb5;
			fchessboard[3, 2] = fc5;
			fchessboard[3, 3] = fd5;
			fchessboard[3, 4] = fe5;
			fchessboard[3, 5] = ff5;
			fchessboard[3, 6] = fg5;
			fchessboard[3, 7] = fh5;

			fchessboard[2, 0] = fa6;
			fchessboard[2, 1] = fb6;
			fchessboard[2, 2] = fc6;
			fchessboard[2, 3] = fd6;
			fchessboard[2, 4] = fe6;
			fchessboard[2, 5] = ff6;
			fchessboard[2, 6] = fg6;
			fchessboard[2, 7] = fh6;

			fchessboard[1, 0] = fa7;
			fchessboard[1, 1] = fb7;
			fchessboard[1, 2] = fc7;
			fchessboard[1, 3] = fd7;
			fchessboard[1, 4] = fe7;
			fchessboard[1, 5] = ff7;
			fchessboard[1, 6] = fg7;
			fchessboard[1, 7] = fh7;

			fchessboard[0, 0] = fa8;
			fchessboard[0, 1] = fb8;
			fchessboard[0, 2] = fc8;
			fchessboard[0, 3] = fd8;
			fchessboard[0, 4] = fe8;
			fchessboard[0, 5] = ff8;
			fchessboard[0, 6] = fg8;
			fchessboard[0, 7] = fh8;
		}

		// Window constructor assigning fields and calling the clear and draw function
		public MainWindow()
		{
			InitializeComponent();
			assignFields();
			clearSelection();
		}

		// Clear and draw function
		public void clearSelection()
		{
			// Clear the background fields
			for (int i = 0; i < 8; i++)
				for (int j = 0; j < 8; j++) {
					fchessboard[i, j].BorderBrush = white;
				}

			// Highlight the last move fields
			lastMoveHighlight();

			// Clear and draw the pieces on the chessboard
			for (int i = 0; i < 8; i += 2)
				for (int j = 0; j < 8; j += 2)
				{
					chessboard[i, j].Background = light;
					
					// If the field is not empty
					if (game.GetChessBoard().GetSquare(i, j).GetPiece() != null)
					{
						// Draw all the white pieces
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.White && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Pawn))
						{
							chessboard[i, j].Background = bPionJ;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.White && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Rook))
						{
							chessboard[i, j].Background = bWiezaJ;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.White && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Knight))
						{
							chessboard[i, j].Background = bKonJ;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.White && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Bishop))
						{
							chessboard[i, j].Background = bLauferJ;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.White && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Queen))
						{
							chessboard[i, j].Background = bKrolowaJ;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.White && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(King))
						{
							chessboard[i, j].Background = bKrolJ;
						}

						// Draw all the black pieces
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.Black && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Pawn))
						{
							chessboard[i, j].Background = cPionJ;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.Black && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Rook))
						{
							chessboard[i, j].Background = cWiezaJ;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.Black && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Knight))
						{
							chessboard[i, j].Background = cKonJ;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.Black && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Bishop))
						{
							chessboard[i, j].Background = cLauferJ;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.Black && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Queen))
						{
							chessboard[i, j].Background = cKrolowaJ;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.Black && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(King))
						{
							chessboard[i, j].Background = cKrolJ;
						}
					}
				}

			// Same but for other fields
			for (int i = 1; i < 8; i += 2)
				for (int j = 1; j < 8; j += 2)
				{
					chessboard[i, j].Background = light;

					// If the field is not empty
					if (game.GetChessBoard().GetSquare(i, j).GetPiece() != null)
					{
						// Draw all the white pieces
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.White && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Pawn))
						{
							chessboard[i, j].Background = bPionJ;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.White && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Rook))
						{
							chessboard[i, j].Background = bWiezaJ;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.White && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Knight))
						{
							chessboard[i, j].Background = bKonJ;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.White && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Bishop))
						{
							chessboard[i, j].Background = bLauferJ;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.White && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Queen))
						{
							chessboard[i, j].Background = bKrolowaJ;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.White && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(King))
						{
							chessboard[i, j].Background = bKrolJ;
						}

						// Draw all the black pieces
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.Black && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Pawn))
						{
							chessboard[i, j].Background = cPionJ;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.Black && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Rook))
						{
							chessboard[i, j].Background = cWiezaJ;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.Black && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Knight))
						{
							chessboard[i, j].Background = cKonJ;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.Black && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Bishop))
						{
							chessboard[i, j].Background = cLauferJ;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.Black && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Queen))
						{
							chessboard[i, j].Background = cKrolowaJ;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.Black && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(King))
						{
							chessboard[i, j].Background = cKrolJ;
						}
					}
				}

			// Same but for dark fields
			for (int i = 1; i < 8; i += 2)
				for (int j = 0; j < 8; j += 2)
				{
					chessboard[i, j].Background = dark;
					
					if (game.GetChessBoard().GetSquare(i, j).GetPiece() != null)
					{
						// White
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.White && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Pawn))
						{
							chessboard[i, j].Background = bPionC;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.White && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Rook))
						{
							chessboard[i, j].Background = bWiezaC;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.White && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Knight))
						{
							chessboard[i, j].Background = bKonC;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.White && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Bishop))
						{
							chessboard[i, j].Background = bLauferC;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.White && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Queen))
						{
							chessboard[i, j].Background = bKrolowaC;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.White && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(King))
						{
							chessboard[i, j].Background = bKrolC;
						}

						// Black
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.Black && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Pawn))
						{
							chessboard[i, j].Background = cPionC;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.Black && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Rook))
						{
							chessboard[i, j].Background = cWiezaC;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.Black && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Knight))
						{
							chessboard[i, j].Background = cKonC;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.Black && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Bishop))
						{
							chessboard[i, j].Background = cLauferC;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.Black && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Queen))
						{
							chessboard[i, j].Background = cKrolowaC;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.Black && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(King))
						{
							chessboard[i, j].Background = cKrolC;
						}
					}
				}

			for (int i = 0; i < 8; i += 2)
				for (int j = 1; j < 8; j += 2)
				{
					chessboard[i, j].Background = dark;

					if (game.GetChessBoard().GetSquare(i, j).GetPiece() != null)
					{
						// White
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.White && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Pawn))
						{
							chessboard[i, j].Background = bPionC;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.White && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Rook))
						{
							chessboard[i, j].Background = bWiezaC;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.White && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Knight))
						{
							chessboard[i, j].Background = bKonC;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.White && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Bishop))
						{
							chessboard[i, j].Background = bLauferC;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.White && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Queen))
						{
							chessboard[i, j].Background = bKrolowaC;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.White && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(King))
						{
							chessboard[i, j].Background = bKrolC;
						}

						// Black
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.Black && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Pawn))
						{
							chessboard[i, j].Background = cPionC;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.Black && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Rook))
						{
							chessboard[i, j].Background = cWiezaC;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.Black && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Knight))
						{
							chessboard[i, j].Background = cKonC;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.Black && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Bishop))
						{
							chessboard[i, j].Background = cLauferC;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.Black && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(Queen))
						{
							chessboard[i, j].Background = cKrolowaC;
						}
						if (game.GetChessBoard().GetSquare(i, j).GetPiece().GetColor() == Constants.Black && game.GetChessBoard().GetSquare(i, j).GetPiece().GetType() == typeof(King))
						{
							chessboard[i, j].Background = cKrolC;
						}
					}
				}
		}

		// Called upon clicking a field
		private void fieldClick(object sender, RoutedEventArgs e)
		{
			// Find the button that was clicked...
			for (int i = 0; i < 8; i++)
				for (int j = 0; j < 8; j++)
					if (chessboard[i, j].Name == (sender as Button).Name)
						// ...and call the click method on it.
						game.Click(i, j);

			// Clear and redraw the chessboard
			clearSelection();

			// highlight the allowed fields
			highlight(sender);

			// highlight the threat fields
			threatHighlight();
		}

		// Highlight the threat fields
		private void threatHighlight()
		{
			// For every square with a threat...
			for (int i = 0; i < 8; i++)
				for (int j = 0; j < 8; j++)
					//if (game.GetChessBoard().GetSquare(i, j).GetEnPassant() != null) debug 
					if (game.GetChessBoard().GetSquare(i, j).GetThreat())
					{
						// ...set it's background grid border to threat color
						fchessboard[i, j].BorderThickness = new Thickness(2);
						fchessboard[i, j].BorderBrush = threat;
					}
		}

		// Highlight the last move fields
		public void lastMoveHighlight()
		{
			// For every square with the last move flag...
			for (int i = 0; i < 8; i++)
				for (int j = 0; j < 8; j++)
					//if (game.GetChessBoard().GetSquare(i, j).GetEnPassant() != null) debug 
					if (game.GetChessBoard().GetSquare(i, j).GetLastMove())
					{
						// ...set it's background grid border to last move color
						fchessboard[i, j].BorderThickness = new Thickness(2);
						fchessboard[i, j].BorderBrush = lastMove;
					}
		}

		// Highlight the allowed move fields
		private void highlight(object sender)
		{
			// For every square with the allowed move flag...
			for (int i = 0; i < 8; i++)
				for (int j = 0; j < 8; j++)
					if (game.GetChessBoard().GetSquare(i, j).GetHighlight())
					{
						// ...set it's background grid border to allowed move color
						fchessboard[i, j].BorderThickness = new Thickness(2);
						fchessboard[i, j].BorderBrush = highlighted;
					}
		}

		// Invoked when the start button is pressed
		private void startButton_Click(object sender, RoutedEventArgs e)
		{
			// Hide the start menu UI elements
			startButton.Visibility = Visibility.Hidden;
			startBackground.Visibility = Visibility.Hidden;
			nazwaBialy.Visibility = Visibility.Hidden;
			nazwaCzarny.Visibility = Visibility.Hidden;
			nazwaLabel.Visibility = Visibility.Hidden;
			// Set the player names
			game.SetNazwaBialy(nazwaBialy.Text);
			game.SetNazwaCzarny(nazwaCzarny.Text);
		}
		
		// Invoked when the draw button is pressed
		private void endButtton_Click(object sender, RoutedEventArgs e)
		{
			// Show the draw menu elements
			remisBackground.Visibility = Visibility.Visible;
			remisLabel.Visibility = Visibility.Visible;
			zapisCheck.Visibility = Visibility.Visible;
			zamknijButton.Visibility = Visibility.Visible;
			podglad.Visibility = Visibility.Visible;
		}

		// Invoked when the close app button is pressed
		private void zamknijButton_Click(object sender, RoutedEventArgs e)
		{
			// Save if the checkbox was checked
			if ((bool)zapisCheck.IsChecked)
			{
				con.Open();
				String zwyciezca = "";
				if (game.GetTurn())
					zwyciezca = "Czarny";
				else
					zwyciezca = "Biały";
				if (remisBackground.IsVisible)
					zwyciezca = "Remis";
				//SqlCommand cmd2 = new SqlCommand("DBCC CHECKIDENT ('Tabela_HistoriaGier', RESEED, 0)", con);
				//cmd2.ExecuteNonQuery();

				SqlCommand cmd = new SqlCommand("insert into Tabela_HistoriaGier values('"+zwyciezca+"','" + game.GetNazwaBialy() + "', '" + game.GetNazwaCzarny()+"', GETDATE(), '"+game.GetGameMoves().GeneratePGNString(game)+"')", con);
				int i = cmd.ExecuteNonQuery();
				if (i > 0)
				{
					Console.WriteLine("Dodano");
				}
				else
				{
					Console.WriteLine("Blad");
				}
				//Console.WriteLine("Wchodze");
				//Szachy2.BazaDanychDataSet bazaDanychDataSet = ((Szachy2.BazaDanychDataSet)(this.FindResource("bazaDanychDataSet")));

				//BazaDanychDataSetTableAdapters.Tabela_HistoriaGierTableAdapter TableAdapter = new BazaDanychDataSetTableAdapters.Tabela_HistoriaGierTableAdapter();

				//TableAdapter.Insert(null, game.GetNazwaBialy(), game.GetNazwaCzarny(), DateTime.Now);

				//// Create a new row.
				//BazaDanychDataSet.Tabela_HistoriaGierRow newhgRow;
				//newhgRow = bazaDanychDataSet.Tabela_HistoriaGier.NewTabela_HistoriaGierRow();
				//newhgRow.BiałyGracz = game.GetNazwaBialy();
				//newhgRow.CarnyGracz = game.GetNazwaCzarny();
				//newhgRow.Data = DateTime.Now;
				//newhgRow.Zwyciezca = null;

				//// Add the row to the table
				//bazaDanychDataSet.Tabela_HistoriaGier.Rows.Add(newhgRow);

				//// Save the new row to the database
				//TableAdapter.Update(bazaDanychDataSet.Tabela_HistoriaGier);

				//game.SaveGame();
			}
				

			// Close the app
			Close();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{

			Szachy2.BazaDanychDataSet bazaDanychDataSet = ((Szachy2.BazaDanychDataSet)(this.FindResource("bazaDanychDataSet")));
			// Load data into the table Tabela_HistoriaGier. You can modify this code as needed.
			Szachy2.BazaDanychDataSetTableAdapters.Tabela_HistoriaGierTableAdapter bazaDanychDataSetTabela_HistoriaGierTableAdapter = new Szachy2.BazaDanychDataSetTableAdapters.Tabela_HistoriaGierTableAdapter();
			bazaDanychDataSetTabela_HistoriaGierTableAdapter.Fill(bazaDanychDataSet.Tabela_HistoriaGier);
			System.Windows.Data.CollectionViewSource tabela_HistoriaGierViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("tabela_HistoriaGierViewSource")));
			tabela_HistoriaGierViewSource.View.MoveCurrentToFirst();
		}

		private void dbButton_Click(object sender, RoutedEventArgs e)
		{
			if (bazaGrid.Visibility == Visibility.Visible)
			{
				bazaGrid.Visibility = Visibility.Hidden;
				pgnButton.Visibility = Visibility.Hidden;
			}
			else
			{
				bazaGrid.Visibility = Visibility.Visible;
				pgnButton.Visibility = Visibility.Visible;
			}
				
		}

		private void bazaGrid_Loaded(object sender, RoutedEventArgs e)
		{
			DataGrid dg = sender as DataGrid;
			foreach (var column in dg.Columns)
			{
				column.Width = new DataGridLength(dg.ActualWidth / dg.Columns.Count - 2, DataGridLengthUnitType.Pixel);
			}
		}

		private void pgnButton_Click(object sender, RoutedEventArgs e)
		{

			//bazaGrid.Items.IndexOf(bazaGrid.CurrentItem);
			TextBlock x = null;
			if (rowindex != -1)
				x = bazaGrid.Columns[5].GetCellContent(bazaGrid.Items[rowindex]) as TextBlock;
			if (x != null)
				game.GetGameMoves().saveAsPGN(x.Text);
		}

		private void bazaGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			rowindex = bazaGrid.Items.IndexOf(bazaGrid.CurrentItem);
		}

		private void podglad_Click(object sender, RoutedEventArgs e)
		{
			if (game.GetWin() == true)
			{
				if (endBackground.Visibility == Visibility.Hidden)
				{
					endBackground.Visibility = Visibility.Visible;
					endLabel.Visibility = Visibility.Visible;
					zapisCheck.Visibility = Visibility.Visible;
					zamknijButton.Visibility = Visibility.Visible;

				}
				else
				{
					endBackground.Visibility = Visibility.Hidden;
					endLabel.Visibility = Visibility.Hidden;
					zapisCheck.Visibility = Visibility.Hidden;
					zamknijButton.Visibility = Visibility.Hidden;
				}
			}
			else
			{
				if (remisBackground.Visibility == Visibility.Hidden)
				{
					remisBackground.Visibility = Visibility.Visible;
					remisLabel.Visibility = Visibility.Visible;
					zapisCheck.Visibility = Visibility.Visible;
					zamknijButton.Visibility = Visibility.Visible;

				}
				else
				{
					remisBackground.Visibility = Visibility.Hidden;
					remisLabel.Visibility = Visibility.Hidden;
					zapisCheck.Visibility = Visibility.Hidden;
					zamknijButton.Visibility = Visibility.Hidden;
				}
			}
				
		}
	}
}
