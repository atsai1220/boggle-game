using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoggleClient
{
    class Model
    {
        private string[,] board { get; set; }
        private string[,] cubes { get; set; }


        private int wordsPlayed { get; set; }
        private string domain { get; set; }
        private string gameToken { get; set; }

        private Player you;


        public Model()
        {
            string[,] board = new string[4, 4];
            string[,] cubes = new string[16, 6];
            you = new Player();
            wordsPlayed = 0;
        }

        public string GetName()
        {
            return you.nickname;
        }

        public void setPlayerID(string _id)
        {
            you.id = _id;
        }
    }


    /// <summary>
    /// Player class.
    /// This will hold player information
    /// </summary>
    class Player
    {
        public string id { get; set; }
        public string nickname { get; set; }
        public int score { get; set; }

        public Player()
        {
            id = null;
            nickname = null;
            score = 0;
        }

        public Player(string _id, string _nickname)
        {
            if (_id != null && _nickname != null)
            {
                id = _id;
                nickname = _nickname;
            }
        }
    }


}
