using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using static System.Net.HttpStatusCode;

namespace Boggle
{
    public class BoggleService : IBoggleService
    {
        /// <summary>
        /// Enumeration to represent the game state.
        /// 
        /// A game is invalid if it hasn't been created yet.
        /// </summary>
        private enum GameState { Invalid, Pending, Active, Completed }

        private static HashSet<string> dictionary = new HashSet<string>(File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "dictionary.txt"));

        //private static HashSet<string> dictionary = new HashSet<string>();

        /// <summary>
        /// The most recent call to SetStatus determines the response code used when
        /// an http response is sent.
        /// </summary>
        /// <param name="status"></param>
        private static void SetStatus(HttpStatusCode status)
        {
            WebOperationContext.Current.OutgoingResponse.StatusCode = status;
        }

        /// <summary>
        /// Returns a Stream version of index.html.
        /// </summary>
        /// <returns></returns>
        public Stream API()
        {
            SetStatus(OK);
            WebOperationContext.Current.OutgoingResponse.ContentType = "text/html";
            return File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "index.html");
        }

        public UserTokenContract CreateUser(CreateUserBody body)
        {
            using (SqlConnection conn = new SqlConnection(BoggleState.boggleDB))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    // TODO Consider putting an upperbound on nickname length.
                    if (body.Nickname == null || body.Nickname.Trim().Length == 0)
                    {
                        SetStatus(Forbidden);
                        return null;
                    }
                    else
                    {
                        string userToken = Guid.NewGuid().ToString();

                        BoggleState.getBoggleState().CreateUser(body.Nickname.Trim(), userToken, conn, trans);
                        SetStatus(Created);

                        UserTokenContract userTokenContract = new UserTokenContract();
                        userTokenContract.UserToken = userToken;

                        trans.Commit();
                        return userTokenContract;
                    }
                }
            }
        }

        /// <summary>
        /// Joins a game
        /// </summary>
        /// <param name="body">Contains UserToken and TimeLimit</param>
        /// <returns></returns>
        public GameIdContract JoinGame(JoinGameBody body)
        {
            using (SqlConnection conn = new SqlConnection(BoggleState.boggleDB))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    // If UserToken is invalid, TimeLimit < 5, or TimeLimit > 120, responds with status 403 (Forbidden).
                    if (body.TimeLimit < 5 || body.TimeLimit > 120)
                    {
                        SetStatus(Forbidden);
                        return null;
                    }
                    BoggleState boggleState = BoggleState.getBoggleState();

                    if (!boggleState.PlayerExists(body.UserToken, conn, trans))
                    {
                        SetStatus(Forbidden);
                        return null;
                    }

                    string gameId = boggleState.GetLastGameId(conn, trans);

                    if (getGameState(gameId) == GameState.Pending)
                    {
                        string player1Id;
                        string player2Id;

                        boggleState.GetPlayers(gameId, out player1Id, out player2Id, conn, trans);

                        // If UserToken is already a player in the pending game
                        if (body.UserToken.Equals(player1Id))
                        {
                            SetStatus(Conflict);
                            return null;
                        }

                        BoggleBoard board = new BoggleBoard();
                        long startTime = DateTime.UtcNow.Ticks;
                        boggleState.StartGame(gameId, body.UserToken, body.TimeLimit, startTime, board.ToString(), conn, trans);

                        SetStatus(Created);
                    }
                    else
                    {
                        gameId = boggleState.AddGame(body.UserToken, body.TimeLimit, conn, trans);

                        SetStatus(Accepted);
                    }

                    GameIdContract GameIdContract = new GameIdContract();
                    GameIdContract.GameID = gameId;

                    trans.Commit();
                    return GameIdContract;
                }
            }
        }

        public void CancelJoinRequest(CancelJoinRequestBody body)
        {
            throw new NotImplementedException();
            /*
            lock (sync)
            {
                BoggleState boggleState = BoggleState.getBoggleState();

                string gameId = boggleState.GetLastGameId();

                if (getGameState(gameId) == GameState.Pending)
                {
                    string player1UserToken, player2UserToken;
                    boggleState.GetPlayers(gameId, out player1UserToken, out player2UserToken);

                    if (player1UserToken == body.UserToken)
                    {
                        boggleState.CancelGame(gameId);

                        SetStatus(OK);

                        return;
                    }
                }

                SetStatus(Forbidden);

                return;
            }
            */
        }

        /// <summary>
        /// Plays a word
        /// </summary>
        /// <param name="body"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public PlayWordContract PlayWord(PlayWordBody body, string gameId)
        {
            throw new NotImplementedException();
            /*
            lock (sync)
            {
                BoggleState _boggleState = BoggleState.getBoggleState();
                int tmp;

                // if Word is null or empty when trimmed
                if (body.Word == null || body.Word.Trim().Equals(""))
                {
                    SetStatus(Forbidden);
                    return null;
                }
                // if  UserToken is null or empty
                else if (body.UserToken == null || body.UserToken.Trim().Equals(""))
                {
                    SetStatus(Forbidden);
                    return null;
                }
                // if gameId is missing
                else if (gameId == null)
                {
                    SetStatus(Forbidden);
                    return null;
                }
                else if (gameId.Trim() == "")
                {
                    SetStatus(Forbidden);
                    return null;
                }
                // If gameId is invalid
                else if (!int.TryParse(gameId, out tmp))
                {
                    SetStatus(Forbidden);
                    return null;
                }
                else if (!_boggleState.GameExists(gameId))
                {
                    SetStatus(Forbidden);
                    return null;
                }
                // If game state is anything other than "active" -> 409 (Conflict)
                // TODO does this work? (GameState.Active)
                else if (getGameState(gameId) != GameState.Active)
                {
                    SetStatus(Conflict);
                    return null;
                }

                // Make sure that the player is in the game.
                string userToken1;
                string userToken2;
                _boggleState.GetPlayers(gameId, out userToken1, out userToken2);
                if (body.UserToken != userToken1 && body.UserToken != userToken2)
                {
                    SetStatus(Forbidden);
                    return null;
                }
                // Record trimmed Word by UserToken
                else
                {

                    WordPair pair = GetScore(body, gameId);
                    // Add to word record
                    _boggleState.AddWord(gameId, body.UserToken, pair.Word, pair.Score);
                    // Update player total score
                    int currentScore = _boggleState.GetScore(gameId, body.UserToken);
                    currentScore += pair.Score;
                    _boggleState.SetScore(gameId, body.UserToken, currentScore);

                    SetStatus(OK);
                    PlayWordContract PlayWordContract = new PlayWordContract();
                    PlayWordContract.Score = pair.Score.ToString();

                    return PlayWordContract;
                }
            }*/
        }

        public BoggleGameContract GameStatus(string gameId, string brief)
        {
            using (SqlConnection conn = new SqlConnection(BoggleState.boggleDB))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    GameState gameState = getGameState(gameId);

                    if (gameState == GameState.Invalid)
                    {
                        SetStatus(Forbidden);
                        return null;
                    }

                    BoggleState boggleState = BoggleState.getBoggleState();

                    BoggleGameContract game = new BoggleGameContract();

                    if (gameState == GameState.Pending)
                    {
                        game.GameState = "pending";
                    }
                    else
                    {
                        if (gameState == GameState.Active)
                        {
                            game.GameState = "active";
                        }
                        else if (gameState == GameState.Completed)
                        {
                            game.GameState = "completed";
                        }

                        int timeLimit;
                        long startTime;
                        boggleState.GetTime(gameId, out timeLimit, out startTime, conn, trans);

                        int timeLeft = timeLimit - (int)((DateTime.UtcNow.Ticks - startTime) / (long)1e7);

                        if (timeLeft < 0)
                        {
                            timeLeft = 0;
                        }

                        game.TimeLeft = timeLeft.ToString();

                        game.Player1 = new Player();
                        game.Player2 = new Player();

                        string player1UserToken, player2UserToken;
                        boggleState.GetPlayers(gameId, out player1UserToken, out player2UserToken, conn, trans);

                        game.Player1.Score = boggleState.GetScore(gameId, player1UserToken, conn, trans);
                        game.Player2.Score = boggleState.GetScore(gameId, player2UserToken, conn, trans);

                        if (brief != "yes")
                        {
                            game.Board = boggleState.GetBoard(gameId, conn, trans);

                            game.TimeLimit = timeLimit.ToString();

                            game.Player1.Nickname = boggleState.GetNickname(player1UserToken, conn, trans);
                            game.Player2.Nickname = boggleState.GetNickname(player2UserToken, conn, trans);

                            if (gameState == GameState.Completed)
                            {
                                game.Player1.WordsPlayed = boggleState.GetWords(gameId, player1UserToken, conn, trans);
                                game.Player2.WordsPlayed = boggleState.GetWords(gameId, player2UserToken, conn, trans);
                            }
                        }
                    }

                    SetStatus(OK);

                    trans.Commit();
                    return game;
                }
            }
        }

        private GameState getGameState(string gameId)
        {
            using (SqlConnection conn = new SqlConnection(BoggleState.boggleDB))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    BoggleState boggleState = BoggleState.getBoggleState();

                    if (!boggleState.GameExists(gameId, conn, trans))
                    {
                        trans.Commit();
                        return GameState.Invalid;
                    }

                    string player1UserToken, player2UserToken;
                    boggleState.GetPlayers(gameId, out player1UserToken, out player2UserToken, conn, trans);

                    if (player2UserToken == "")
                    {
                        trans.Commit();
                        return GameState.Pending;
                    }

                    int timeLimit;
                    long startTime;
                    boggleState.GetTime(gameId, out timeLimit, out startTime, conn, trans);

                    long endTime = startTime + timeLimit * (long)1e7;

                    if (DateTime.UtcNow.Ticks < endTime)
                    {
                        trans.Commit();
                        return GameState.Active;
                    }
                    else
                    {
                        trans.Commit();
                        return GameState.Completed;
                    }
                }
            }
        }

        private WordPair GetScore(PlayWordBody body, string gameId)
        {
            using (SqlConnection conn = new SqlConnection(BoggleState.boggleDB))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    BoggleState _boggleState = BoggleState.getBoggleState();
                    BoggleBoard board = new BoggleBoard(_boggleState.GetBoard(gameId, conn, trans));
                    Predicate<WordPair> repeatFinder = (WordPair p) => { return p.Word == body.Word.Trim(); };

                    List<WordPair> pairs = _boggleState.GetWords(gameId, body.UserToken, conn, trans);
                    WordPair pair = new WordPair();
                    pair.Word = body.Word.Trim();
                    int wordLength = body.Word.Length;

                    if (pairs.Exists(repeatFinder))
                    {
                        pair.Score = 0;
                    }
                    // Determine word score first!
                    // Determine if word is legal or not
                    else if (board.CanBeFormed(pair.Word) && dictionary.Contains(pair.Word.ToUpper()))
                    {
                        if (wordLength < 3)
                        {
                            pair.Score = 0;
                        }
                        else if (wordLength == 3 || wordLength == 4)
                        {
                            pair.Score = 1;
                        }
                        else if (wordLength == 5)
                        {
                            pair.Score = 2;
                        }
                        else if (wordLength == 6)
                        {
                            pair.Score = 3;
                        }
                        // SOMETIMES THE BOARD DOES NOT HAVE A WORD THIS LONG
                        else if (wordLength == 7)
                        {
                            pair.Score = 5;
                        }
                        // SOMETIMES THE BOARD DOES NOT HAVE A WORD THIS LONG
                        else if (wordLength > 7)
                        {
                            pair.Score = 11;
                        }
                    }
                    // Not legal
                    else
                    {
                        pair.Score = -1;
                    }

                    trans.Commit();
                    return pair;
                }
            }
        }
    }
}