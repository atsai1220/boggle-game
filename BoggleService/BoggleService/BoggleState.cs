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
        Dictionary<string, BoggleGame> games;

        struct BoggleGame
        {
            public string gameId;
            public string board;
            public int timeLimit;
            public long startTime;

            public string player1UserToken;
            public int player1Score;
            public List<string> player1Words;

            public string player2UserToken;
            public int player2Score;
            public List<string> player2Words;
        }

        /// <summary>
        /// Creates a new game
        /// </summary>
        /// <param name="gameId">Id of game</param>
        /// <param name="player1Token">User token of player 1</param>
        /// <param name="player1TimeLimit">Time limit requested by player 1</param>
        public void AddGame(string gameId, string player1Token, int player1TimeLimit)
        {
            BoggleGame game = new BoggleGame();

            game.gameId = gameId;
            game.player1UserToken = player1Token;
            game.timeLimit = player1TimeLimit;
        }

        //a
        public void AddWord(string gameId, string userToken, string word, int score)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes a game from the list.
        /// </summary>
        /// <param name="gameId">The game to remove</param>
        public void CancelGame(string gameId)
        {
            games.Remove(gameId);
        }

        //a
        public void CreateUser(string nickname, string userToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the board for the game.
        /// </summary>
        /// <param name="gameId">The gameId</param>
        /// <returns></returns>
        public string GetBoard(string gameId)
        {
            return games[gameId].board;
        }

        //a
        public string GetNickname(string userToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the user tokens of both players
        /// </summary>
        /// <param name="gameId">The game id</param>
        /// <param name="player1Id">The user token of player 1</param>
        /// <param name="player2Id">The user token of player 2</param>
        public void GetPlayers(string gameId, out string player1Id, out string player2Id)
        {
            player1Id = games[gameId].player1UserToken;
            player2Id = games[gameId].player2UserToken;
        }

        //a
        public int GetScore(string gameId, string userToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns time information from the game.
        /// </summary>
        /// <param name="gameId">The id of the game queried</param>
        /// <param name="timeLimit">The time limit of the game</param>
        /// <param name="startTime">The time that the game started</param>
        public void GetTime(string gameId, out int timeLimit, out long startTime)
        {
            timeLimit = games[gameId].timeLimit;
            startTime = games[gameId].startTime;
        }

        //a
        public List<string> GetWords(string gameId, string userToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Set the score of the player refered to by usertoken.
        /// 
        /// User token should refer to a user in the game.
        /// </summary>
        /// <param name="gameId">The id of the game queried</param>
        /// <param name="userToken">The user token of the user getting their score changed</param>
        /// <param name="score">The score</param>
        public void SetScore(string gameId, string userToken, int score)
        {
            var game = games[gameId];

            if(userToken == game.player1UserToken)
            {
                game.player1Score = score;
            }
            else if(userToken == game.player2UserToken)
            {
                game.player2Score = score;
            }
            else
            {
                //This shouldn't happen.
            }
        }

        //a
        public void StartGame(string gameId, string player2Token, int player2TimeLimit, long startTime, string board)
        {
            throw new NotImplementedException();
        }
    }
}
