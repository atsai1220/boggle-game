using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoggleClient
{
    interface IBoggleView
    {
        // menu events
        event Action aboutEvent;
        event Action helpEvent;
        event Action closeEvent;

        // program events
        /// <summary>
        /// Event triggered when domain name is entered
        /// 
        /// String parameter is the domain name.
        /// </summary>
        event Action<string> domainNameEntered;

        /// <summary>
        /// Event triggered when player registers.
        /// 
        /// String is the desired nickname.
        /// </summary>
        event Action<string> registerPlayerEvent;

        /// <summary>
        /// Event triggered when game is joined.
        /// 
        /// Int is the desired time limit.
        /// </summary>
        event Action<int> joinGameEvent;

        /// <summary>
        /// Event triggered when game is cancelled
        /// </summary>
        event Action joinCanceledEvent;

        /// <summary>
        /// Event triggered when game starts.
        /// </summary>
        event Action gameStartEvent;

        /// <summary>
        /// Event triggered when game ends.
        /// </summary>
        event Action gameEndEvent;
        

        /// <summary>
        /// Player entered a word.
        /// String parameter is the word entered.
        /// </summary>
        event Action<string> wordEnteredEvent;



        void DoClose();

        void MessagePopUp(string _message);

        /// <summary>
        /// Adds a word to the list of words entered.
        /// </summary>
        /// <param name="word">The word entered</param>
        /// <param name="score">The score</param>
        void AddWord(string word, int score);

        string Nickname
        {
            set;
        }

        int TimeRemaining
        {
            set;
        }

        /// <summary>
        /// String representing the boggle board.
        /// </summary>
        string BoardString
        {
            set;
        }

    }
}
