using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
                if (boggleDB == null)
                {
                    boggleDB = ConfigurationManager.ConnectionStrings["BoggleDB"].ConnectionString;
                }

                return boggleDB;
            }

            private set { }
        }

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

        /// <summary>
        /// Creates a new game
        /// </summary>
        /// <param name="gameId">Id of game</param>
        /// <param name="player1Token">User token of player 1</param>
        /// <param name="player1TimeLimit">Time limit requested by player 1</param>
        public void AddGame(string gameId, string player1Token, int player1TimeLimit, SqlConnection conn, SqlTransaction trans)
        {
            using (SqlCommand command = new SqlCommand("INSERT INTO Games(Player1, Player2, Board, TimeLimit, StartTime) VALUES(@UserToken1, @UserToken2, @Board, @TimeLimit, @StartTime)",
                    conn,
                    trans))
            {
                command.Parameters.AddWithValue("@UserToken1", player1Token);
                command.Parameters.AddWithValue("@UserToken2", "");
                command.Parameters.AddWithValue("@Board", "");
                command.Parameters.AddWithValue("@TimeLimit", player1TimeLimit);
                command.Parameters.AddWithValue("@StartTime", "");

                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Adds a word to a specific game for a specific player
        /// and sets score of player in table
        /// </summary>
        /// <param name="gameId">Id of game</param>
        /// <param name="userToken">User token of player in action</param>
        /// <param name="word">Word being played</param>
        /// <param name="score">Score acquired</param>
        public void AddWord(string gameId, string userToken, string word, int score, SqlConnection conn, SqlTransaction trans)
        {
            string script = "INSERT INTO Words(Word, GameID, Player, Score) VALUES (@Word, @GameID, @Player, @Score)";

            using (SqlCommand command = new SqlCommand(script, conn, trans))
            {
                command.Parameters.AddWithValue("@Word", word);
                command.Parameters.AddWithValue("@GameID", gameId);
                command.Parameters.AddWithValue("@Player", userToken);
                command.Parameters.AddWithValue("@Score", score);
                if (command.ExecuteNonQuery() != 1)
                {
                    throw new ArgumentException();
                }
            }
        }

        /// <summary>
        /// Removes a game from the list.
        /// </summary>
        /// <param name="gameId">The game to remove</param>
        public void CancelGame(string gameId, SqlConnection conn, SqlTransaction trans)
        {
            using (SqlCommand command = new SqlCommand("DELETE FROM Games WHERE GameID = @GameID",
                conn,
                trans))
            {
                command.Parameters.AddWithValue("@GameID", gameId);

                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="nickname">Nickname of player</param>
        /// <param name="userToken">User token of player</param>
        public void CreateUser(string nickname, string userToken, SqlConnection conn, SqlTransaction trans)
        {
            using (SqlCommand command = new SqlCommand("insert into Users(UserID, Nickname) values(@UserID, @Nickname)",
                    conn,
                    trans))
            {
                command.Parameters.AddWithValue("@UserID", userToken);
                command.Parameters.AddWithValue("@Nickname", nickname.Trim());

                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Returns the board for the game.
        /// </summary>
        /// <param name="gameId">The gameId</param>
        /// <returns></returns>
        public string GetBoard(string gameId, SqlConnection conn, SqlTransaction trans)
        {
            string script = "SELECT Board FROM Games WHERE GameId = @GameId";

            using (SqlCommand command = new SqlCommand(script, conn, trans))
            {
                command.Parameters.AddWithValue("@GameId", gameId);
                SqlDataReader reader = command.ExecuteReader();
                return reader.GetString(0);
            }
        }

        /// <summary>
        /// Returns the nickname given the userToken
        /// </summary>
        /// <param name="userToken">User token of player</param>
        /// <returns></returns>
        public string GetNickname(string userToken, SqlConnection conn, SqlTransaction trans)
        {
            string script = "SELECT Nickname FROM Users WHERE UserId = @UserId";
            using (SqlCommand command = new SqlCommand(script, conn, trans))
            {
                command.Parameters.AddWithValue("@UserId", userToken);
                SqlDataReader reader = command.ExecuteReader();
                return reader.GetString(0);
            }
        }

        /// <summary>
        /// Get the user tokens of both players
        /// </summary>
        /// <param name="gameId">The game id</param>
        /// <param name="player1Id">The user token of player 1</param>
        /// <param name="player2Id">The user token of player 2</param>
        public void GetPlayers(string gameId, out string player1Id, out string player2Id, SqlConnection conn, SqlTransaction trans)
        {
            string script = "SELECT Player1, Player2 FROM Games WHERE GameId = @GameId";
            // Column 0 is player 1
            using (SqlCommand command = new SqlCommand(script, conn, trans))
            {
                command.Parameters.AddWithValue("@GameId", gameId);
                SqlDataReader reader = command.ExecuteReader();
                player1Id =  reader.GetString(0);
                player2Id = reader.GetString(1);
            }
        }

        /// <summary>
        /// Returns the score of a player
        /// </summary>
        /// <param name="gameId">Id of game</param>
        /// <param name="userToken">User token of player</param>
        /// <returns></returns>
        public int GetScore(string gameId, string userToken, SqlConnection conn, SqlTransaction trans)
        {
            string script = "SELECT SUM(Score) FROM Words WHERE GameID = @GameID AND Player = @UserId";
            using (SqlCommand command = new SqlCommand(script, conn, trans))
            {
                command.Parameters.AddWithValue("@GameID", gameId);
                command.Parameters.AddWithValue("@UserId", userToken);
                SqlDataReader reader = command.ExecuteReader();
                return reader.GetInt32(0);
            }
        }

        /// <summary>
        /// Returns time information from the game.
        /// </summary>
        /// <param name="gameId">The id of the game queried</param>
        /// <param name="timeLimit">The time limit of the game</param>
        /// <param name="startTime">The time that the game started</param>
        public void GetTime(string gameId, out int timeLimit, out long startTime, SqlConnection conn, SqlTransaction trans)
        {
            using (SqlCommand command = new SqlCommand("SELECT TimeLimit, StartTime FROM Games WHERE GameID = @GameID",
                conn,
                trans))
            {
                command.Parameters.AddWithValue("@GameID", gameId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    timeLimit = (int)reader["TimeLimit"];
                    startTime = (long)reader["StartTime"];
                }
            }
        }

        /// <summary>
        /// Returns a List of strings that contains the played words of a player
        /// </summary>
        /// <param name="gameId">Id of game</param>
        /// <param name="userToken">Token of player</param>
        /// <returns></returns>
        public List<WordPair> GetWords(string gameId, string userToken, SqlConnection conn, SqlTransaction trans)
        {
            string script = "SELECT Word FROM Words WHERE GameID = @GameID AND Player = @UserId";

            using (SqlCommand command = new SqlCommand(script, conn, trans))
            {
                command.Parameters.AddWithValue("@GameID", gameId);
                command.Parameters.AddWithValue("@UserId", userToken);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                List<WordPair> list = new List<WordPair>();
                
                DataTable table = new DataTable();
                adapter.Fill(table);
                foreach (DataRow row in table.Rows)
                {
                    WordPair pair = new WordPair();
                    pair.Word = row["Word"].ToString();
                    int score;
                    int.TryParse(row["Score"].ToString(), out score);
                    pair.Score = score;
                    list.Add(pair);
                }
                return list;
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
            //var game = games[gameId];

            //if (userToken == game.player1UserToken)
            //{
            //    game.player1Score = score;
            //}
            //else if (userToken == game.player2UserToken)
            //{
            //    game.player2Score = score;
            //}
            // No longer necessary because of new database!
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
            //BoggleGame game = games[gameId];
            //game.player2UserToken = player2Token;
            //game.timeLimit = (game.timeLimit + player2TimeLimit) / 2;
            //game.startTime = startTime;
            //game.board = board;
        }

        public bool GameExists(string gameId, SqlConnection conn, SqlTransaction trans)
        {
            using (SqlCommand command = new SqlCommand("SELECT TimeLimit, StartTime FROM Games WHERE GameID = @GameID",
                    conn,
                    trans))
            {
                command.Parameters.AddWithValue("@GameID", gameId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public bool PlayerExists(string userToken)
        {
            //return players.ContainsKey(userToken);
            return true;
        }

        public string CreateGame()
        {
            //lastGameId++;
            //string gameId = lastGameId.ToString();

            //BoggleGame boggleGame = new BoggleGame();
            //boggleGame.gameId = gameId;

            //games[gameId] = boggleGame;

            //return gameId;
            return "temp";
        }

        public string GetLastGameId()
        {
            //return lastGameId.ToString();
            return "123";
        }
    }
}
