using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boggle
{
    class BoggleState : IBoggleState
    {
        // userToken -> nickName
        Dictionary<string, string> players;
        Dictionary<string, BoggleGame> gameId;

        struct BoggleGame
        {
            string gameId;
            string board;
            int timeLimit;
            long startTime;

            string player1UserToken;
            int player1Score;
            List<string> player1Words;

            string player2UserToken;
            int player2Score;
            List<string> player2Words;
        }

        //s
        public void AddGame(string gameId, string player1Token, int player1TimeLimit)
        {
            throw new NotImplementedException();
        }

        //a
        public void AddWord(string gameId, string userToken, string word, int score)
        {
            throw new NotImplementedException();
        }

        //s
        public void CancelGame(string gameId)
        {
            throw new NotImplementedException();
        }

        //a
        public void CreateUser(string nickname, string userToken)
        {
            throw new NotImplementedException();
        }

        //s
        public string GetBoard(string gameId)
        {
            throw new NotImplementedException();
        }

        //a
        public string GetNickname(string userToken)
        {
            throw new NotImplementedException();
        }

        //s
        public void GetPlayers(string gameId, out string player1Id, out string player2Id)
        {
            throw new NotImplementedException();
        }

        //a
        public int GetScore(string gameId, string userToken)
        {
            throw new NotImplementedException();
        }

        //s
        public void GetTime(string gameId, out int timeLimit, out long startTime)
        {
            throw new NotImplementedException();
        }

        //a
        public List<string> GetWords(string gameId, string userToken)
        {
            throw new NotImplementedException();
        }

        //s
        public void SetScore(string gameId, string userToken, int score)
        {
            throw new NotImplementedException();
        }

        //a
        public void StartGame(string gameId, string player2Token, int player2TimeLimit, long startTime, string board)
        {
            throw new NotImplementedException();
        }
    }
}
