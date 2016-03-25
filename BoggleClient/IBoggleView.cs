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

        /// <summary>
        /// Event triggered when game is cancelled
        /// </summary>
        event Action joinCanceledEvent;
        

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

        string Player2Nickname
        {
            set;
        }

        int Player1Score
        {
            set;
            
        }

        int Player2Score
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
