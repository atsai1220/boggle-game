using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boggle
{
    interface IBoggleState
    {

        void CreateUser(string nickname, string userToken);

        /// <summary>
        /// Returns the nickname for the given userToken.
        /// 
        /// If the userToken does not exist returns an empty string.
        /// </summary>
        /// <param name="userToken">The user token</param>
        /// <returns></returns>
        string GetNickname(string userToken);

        void AddGame(string gameId, string player1Token, int player1TimeLimit);

        void StartGame(string gameId, string player2Token, int player2TimeLimit, long startTime, string board);

        void CancelGame(string gameId);

        void AddWord(string gameId, string userToken, string word, int score);

        void GetTime(string gameId, out int timeLimit, out long startTime);

        List<string> GetWords(string gameId, string userToken);

        void SetScore(string gameId, string userToken, int score);

        int GetScore(string gameId, string userToken);

        /// <summary>
        /// Returns the board as a string, given the gameId
        /// </summary>
        /// <param name="gameId">The inquired game</param>
        /// <returns></returns>
        string GetBoard(string gameId);

        /// <summary>
        /// If game is pending player2Id is an empty string.
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="player1Id"></param>
        /// <param name="player2Id"></param>
        void GetPlayers(string gameId, out string player1Id, out string player2Id);

    }
}
