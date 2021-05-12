using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szachy2
{
    class ChessGameMoves
    { 
        private List<Move> moves = new List<Move>();

        public void AddMove(Move move)
        {
            moves.Add(move);
        }

        public string GeneratePGNString(Game game)
        {
            string result = "[Date \"" + DateTime.Now.ToString("yyyy.MM.dd") + "\"]";
            result += "\n[White \"" + game.GetNazwaBialy() + "\"]";
            result += "\n[Black \"" + game.GetNazwaCzarny() + "\"]";
            int round = 1;
            foreach (Move m in moves)
            {
                if (m.GetTurn() == Constants.White)
                    result += round.ToString() + ". " + m.GetMoveString();
                else
                {
                    result += " " + m.GetMoveString() + " ";
                    round++;
                }
            }

            return result;
        }

        public void saveAsPGN(Game game)
        {
            string path = Directory.GetCurrentDirectory() + "/Game" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss") + ".pgn";
            Console.WriteLine(path);
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(GeneratePGNString(game));
            }
        }

        public void saveAsPGN(String str)
        {
            string path = Directory.GetCurrentDirectory() + "/Game" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss") + ".pgn";
            Console.WriteLine(path);
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(str);
            }
        }
    }
}
