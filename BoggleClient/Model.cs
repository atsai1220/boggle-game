using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace BoggleClient
{
    class Model
    {
        
        public int wordsPlayed { get; set; }
        public string domain { get; set; }
        public string gameToken { get; set; }

        private Player you;


        public Model()
        {
            you = new Player();
            wordsPlayed = 0;
        }

        public string GetName()
        {
            return you.nickname;
        }

        public void setPlayerID(string _userToken)
        {
            you.userToken = _userToken;
        }
    }


    /// <summary>
    /// Player class.
    /// This will hold player information
    /// </summary>
    class Player
    {
        public string userToken { get; set; }
        public string nickname { get; set; }
        public int score { get; set; }

        public Player()
        {
            userToken = null;
            nickname = null;
            score = 0;
        }

        public Player(string _userToken, string _nickname)
        {
            if (_userToken != null && _nickname != null)
            {
                userToken = _userToken;
                nickname = _nickname;
            }
            // TODO handle exception
        }
    }

    /// <summary>
    /// Ordered collection of recorded words with their respective points
    /// </summary>
    class WordsCollection : KeyedCollection<string, int>
    {
        protected override string GetKeyForItem(int item)
        {
            throw new NotImplementedException();
        }
    }





}
