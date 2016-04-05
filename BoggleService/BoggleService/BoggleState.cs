using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boggle
{
    class BoggleState
    {
        public static string boggleDB
        {
            get
            {
                if(boggleDB == null)
                {
                    boggleDB = ConfigurationManager.ConnectionStrings["BoggleDB"].ConnectionString;
                }

                return boggleDB;
            }

            private set { }
        }

        private string empty;
        /// <summary>
        /// Instance of bogglestate
        /// </summary>
        private static BoggleState boggleState;

        /// <summary>
        /// Singleton pattern for bogglestate
        /// </summary>
        /// <returns></returns>
        public static BoggleState getBoggleState()
        {
            if (boggleState == null)
            {
                boggleState = new BoggleState();
            }
            return boggleState;
        }

        // userToken -> nickName
        private Dictionary<string, string> players;
        // gameId -> BoggleGame
        private Dictionary<string, BoggleGame> games;

        private int lastGameId;

        private class BoggleGame
        {
            public string gameId;
            public string board;
            public int timeLimit;
            public long startTime;

            public string player1UserToken = "";
            public int player1Score = 0;
            public List<WordPair> player1Words = new List<WordPair>();

            public string player2UserToken = "";
            public int player2Score = 0;
            public List<WordPair> player2Words = new List<WordPair>();
        }

        /// <summary>
        /// Private constructor for boggleState.
        /// </summary>
        private BoggleState()
        {
            players = new Dictionary<string, string>();
            games = new Dictionary<string, BoggleGame>();

            lastGameId = 0;
        }

        /// <summary>
        /// Creates a new game
        /// </summary>
        /// <param name="gameId">Id of game</param>
        /// <param name="player1Token">User token of player 1</param>
        /// <param name="player1TimeLimit">Time limit requested by player 1</param>
        public void AddGame(string gameId, string player1Token, int player1TimeLimitm)
        {
            BoggleGame game = games[gameId];

            game.gameId = gameId;
            game.player1UserToken = player1Token;
            game.timeLimit = player1TimeLimit;

        }

        /// <summary>
        /// Adds a word to a specific game for a specific player
        /// then sets the score.
        /// </summary>
        /// <param name="gameId">Id of game</param>
        /// <param name="userToken">User token of player in action</param>
        /// <param name="word">Word being played</param>
        /// <param name="score">Score acquired</param>
        public void AddWord(string gameId, string userToken, string word, int score)
        {
            BoggleGame game;
            if (games.TryGetValue(gameId, out game))
            {
                WordPair pair = new WordPair();
                pair.Word = word;
                pair.Score = score;
                // If Player 1 token
                if (userToken.Equals(game.player1UserToken))
                {
                    game.player1Words.Add(pair);
                }
                // If Player 2 token
                else if (userToken.Equals(game.player2UserToken))
                {
                    game.player2Words.Add(pair);
                }
                // This will only happen if there is a bug in BoggleService.
                else
                {
                    throw new ArgumentException();
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Removes a game from the list.
        /// </summary>
        /// <param name="gameId">The game to remove</param>
        public void CancelGame(string gameId)
        {
            games.Remove(gameId);
        }

        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="nickname">Nickname of player</param>
        /// <param name="userToken">User token of player</param>
        public void CreateUser(string nickname, string userToken)
        {
            using (SqlCommand command =
                    new SqlCommand("insert into Users (UserID, Name, Email) values(@UserID, @Nickname, @Email)",
                                    conn,
                                    trans))
            {

            }
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

        /// <summary>
        /// Returns the nickname given the userToken
        /// </summary>
        /// <param name="userToken">User token of player</param>
        /// <returns></returns>
        public string GetNickname(string userToken)
        {
            string nickname;
            if (players.TryGetValue(userToken, out nickname))
            {
                return nickname;
            }
            // This will only happen if there is a bug in BoggleService.
            else
            {
                throw new ArgumentException();
            }
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

        /// <summary>
        /// Returns the score of a player
        /// </summary>
        /// <param name="gameId">Id of game</param>
        /// <param name="userToken">User token of player</param>
        /// <returns></returns>
        public int GetScore(string gameId, string userToken)
        {
            BoggleGame game;
            if (games.TryGetValue(gameId, out game))
            {
                // Player 1
                if (game.player1UserToken == userToken)
                {
                    return game.player1Score;
                }
                // Player 2
                else if (game.player2UserToken == userToken)
                {
                    return game.player2Score;
                }
                // This will only happen if there is a bug in BoggleService.
                else
                {
                    throw new ArgumentException();
                }
            }
            // This will only happen if there is a bug in BoggleService.
            else
            {
                throw new ArgumentException();
            }
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

        /// <summary>
        /// Returns a List of strings that contains the played words of a player
        /// </summary>
        /// <param name="gameId">Id of game</param>
        /// <param name="userToken">Token of player</param>
        /// <returns></returns>
        public List<WordPair> GetWords(string gameId, string userToken)
        {
            var game = games[gameId];
            // Player 1
            if (game.player1UserToken.Equals(userToken))
            {
                return game.player1Words;
            }
            // Player 2
            else if (game.player2UserToken.Equals(userToken))
            {
                return game.player2Words;
            }
            // This will only happen if there is a bug in BoggleService.
            else
            {
                throw new ArgumentException();
            }
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

            if (userToken == game.player1UserToken)
            {
                game.player1Score = score;
            }
            else if (userToken == game.player2UserToken)
            {
                game.player2Score = score;
            }
        }

        /// <summary>
        /// Starts a specific game
        /// </summary>
        /// <param name="gameId">Id of game</param>
        /// <param name="player2Token">Player 2 user token</param>
        /// <param name="player2TimeLimit">Player 2's specified time limit</param>
        /// <param name="startTime">System start time of game</param>
        /// <param name="board">String that rep the board</param>
        public void StartGame(string gameId, string player2Token, int player2TimeLimit, long startTime, string board)
        {
            BoggleGame game = games[gameId];
            game.player2UserToken = player2Token;
            game.timeLimit = (game.timeLimit + player2TimeLimit) / 2;
            game.startTime = startTime;
            game.board = board;
        }

        public bool GameExists(string gameId)
        {
            return games.ContainsKey(gameId);
        }

        public string CreateGame()
        {
            lastGameId++;
            string gameId = lastGameId.ToString();

            BoggleGame boggleGame = new BoggleGame();
            boggleGame.gameId = gameId;

            games[gameId] = boggleGame;

            return gameId;
        }

        public string GetLastGameId()
        {
            return lastGameId.ToString();
        }
    }
}
