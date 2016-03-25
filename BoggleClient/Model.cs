using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace BoggleClient
{
    /// <summary>
    /// Class for Model for the Boggle game
    /// </summary>
    class Model
    {
        /// <summary>
        /// The number of words you played
        /// </summary>
        public int wordsPlayed { get; set; }

        /// <summary>
        /// The domain the game is connected to
        /// </summary>
        public string domain { get; set; }

        /// <summary>
        /// List to hold the words and score
        /// </summary>
        public List<string> wordRecord;

        /// <summary>
        /// Player class
        /// </summary>
        private Player you;

        /// <summary>
        /// Constructor to initialize Model
        /// Sets default domain to http://bogglecs3500s16.azurewebsites.net
        /// </summary>
        public Model()
        {
            you = new Player();
            domain = "http://bogglecs3500s16.azurewebsites.net";
            wordRecord = new List<string>();
            wordsPlayed = 0;
        }

        /// <summary>
        /// Client's UserToken
        /// </summary>
        public string UserToken { get; set; }

        /// <summary>
        /// Client's game ID
        /// </summary>
        public int GameId { get; set; }
    }


    /// <summary>
    /// Player class.
    /// This will hold player information
    /// </summary>
    class Player
    {
        /// <summary>
        /// Holds a player's user token
        /// </summary>
        public string userToken { get; set; }

        /// <summary>
        /// Player's nickname 
        /// </summary>
        public string nickname { get; set; }

        /// <summary>
        /// Player's score
        /// </summary>
        public int score { get; set; }

        /// <summary>
        /// Player class constructor
        /// </summary>
        public Player()
        {
            userToken = null;
            nickname = null;
            score = 0;
        }
    }







}
