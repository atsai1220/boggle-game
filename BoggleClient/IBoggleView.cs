using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoggleClient
{
    /// <summary>
    /// Interface for better seperation of code.
    /// </summary>
    interface IBoggleView
    {
        // menu events
        /// <summary>
        /// Event called when the help is requested.
        /// </summary>
        event Action helpEvent;

        /// <summary>
        /// Event called when the game is closed.
        /// </summary>
        event Action closeEvent;

        // program events

        /// <summary>
        /// Event triggered when game is cancelled
        /// </summary>
        event Action joinCanceledEvent;
        

        /// <summary>
        /// Player entered a word.
        /// String parameter is the word entered.
        /// </summary>
        event Action<string> wordEnteredEvent;

      
        




        /// <summary>
        /// Called when game is closed.
        /// </summary>
        void DoClose();


        /// <summary>
        /// Helper method to make a popup message appear.
        /// </summary>
        /// <param name="_message">The message to show.</param>
        void MessagePopUp(string _message);

        /// <summary>
        /// Adds a word to the list of words entered.
        /// </summary>
        /// <param name="word">The word entered</param>
        /// <param name="score">The score</param>
        void AddWord(string word, int score);

        /// <summary>
        /// The nickname of the player.
        /// </summary>
        string Nickname
        {
            set;
        }

        /// <summary>
        /// The nickname of player 2.
        /// </summary>
        string Player2Nickname
        {
            set;
        }

        /// <summary>
        /// The score of player 1.
        /// </summary>
        int Player1Score
        {
            set;
            
        }

        /// <summary>
        /// The score of player 2.
        /// </summary>
        int Player2Score
        {
            set;
        }

        /// <summary>
        /// The time remaining.
        /// </summary>
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
