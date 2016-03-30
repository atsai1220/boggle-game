using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            // TODO Consider putting an upperbound on nickname length.
            if (body.Nickname == null || body.Nickname.Trim().Length == 0)
            {
                SetStatus(Forbidden);
                return null;
            }
            else
            {
                string userToken = Guid.NewGuid().ToString();

                BoggleState.getBoggleState().CreateUser(body.Nickname.Trim(), userToken);
                SetStatus(Created);

                UserTokenContract userTokenContract = new UserTokenContract();
                userTokenContract.UserToken = userToken;

                //return JsonConvert.SerializeObject(response);
                return userTokenContract;
            }
        }

        /// <summary>
        /// Joins a game
        /// </summary>
        /// <param name="body">Contains UserToken and TimeLimit</param>
        /// <returns></returns>
        public string JoinGame(JoinGameBody body)
        {
            // If UserToken is invalid, TimeLimit < 5, or TimeLimit > 120, responds with status 403 (Forbidden).
            if (body.TimeLimit < 5 || body.TimeLimit > 120)
            {
                SetStatus(Forbidden);
                return null;
            }

            BoggleState boggleState = BoggleState.getBoggleState();

            string gameId = boggleState.GetLastGameId();

            if(getGameState(gameId) == GameState.Pending)
            {
                string player1Id;
                string player2Id;
                
                boggleState.GetPlayers(gameId, out player1Id, out player2Id);
                
                // If UserToken is already a player in the pending game
                if (body.UserToken.Equals(player1Id))
                {
                    SetStatus(Conflict);
                    return null;
                }

                BoggleBoard board = new BoggleBoard();
                long startTime = DateTime.UtcNow.Ticks;
                boggleState.StartGame(gameId, body.UserToken, body.TimeLimit, startTime, board.ToString());

                SetStatus(Created);
            }
            else
            {
                gameId = boggleState.CreateGame();

                boggleState.AddGame(gameId, body.UserToken, body.TimeLimit);
                SetStatus(Accepted);
            }

            return gameId;
        }

        public void CancelJoinRequest(CancelJoinRequestBody body)
        {
            BoggleState boggleState = BoggleState.getBoggleState();

            string player1UserToken, player2UserToken;
            boggleState.GetPlayers(boggleState.LastGameId.ToString(), out player1UserToken, out player2UserToken);

            if(player1UserToken == body.UserToken)
            {
                boggleState.CancelGame(boggleState.LastGameId.ToString());
                boggleState.LastGameId--;

                SetStatus(OK);
            }
            else
            {
                SetStatus(Forbidden);
            }
        }

        /// <summary>
        /// Plays a word
        /// </summary>
        /// <param name="body"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public string PlayWord(PlayWordBody body, string gameId)
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
            // If gameId is invalid
            else if (int.TryParse(gameId, out tmp) || tmp >= _boggleState.LastGameId)
            {
               SetStatus(Forbidden);
               return null;
            }
            // If game state isi anything other than "active" -> 409 (Conflict)
            // TODO does this work? (GameState.Active)
            else if (!getGameState(gameId).Equals(GameState.Active))
            {
                SetStatus(Conflict);
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
                return pair.Score.ToString();
            }
        }

        public BoggleGameContract GameStatus(string gameId, bool brief)
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
                boggleState.GetTime(gameId, out timeLimit, out startTime);

                int timeLeft = timeLimit - (int)((DateTime.Now.Ticks - startTime) / (long)1e7);

                if (timeLeft < 0)
                {
                    timeLeft = 0;
                }

                game.TimeLeft = timeLeft.ToString();

                string player1UserToken, player2UserToken;
                boggleState.GetPlayers(gameId, out player1UserToken, out player2UserToken);

                game.player1.Score = boggleState.GetScore(gameId, player1UserToken);
                game.player2.Score = boggleState.GetScore(gameId, player2UserToken);

                if (!brief)
                {
                    game.Board = boggleState.GetBoard(gameId);

                    game.TimeLimit = timeLimit.ToString();

                    game.player1.Nickname = boggleState.GetNickname(player1UserToken);
                    game.player2.Nickname = boggleState.GetNickname(player2UserToken);

                    if (gameState == GameState.Completed)
                    {
                        game.player1.WordsPlayed = boggleState.GetWords(gameId, player1UserToken);
                        game.player2.WordsPlayed = boggleState.GetWords(gameId, player2UserToken);
                    }
                }
            }

            SetStatus(OK);
            return game;
        }

        private GameState getGameState(string gameId)
        {
            BoggleState boggleState = BoggleState.getBoggleState();

            if (int.Parse(gameId) > boggleState.LastGameId)
            {
                return GameState.Invalid;
            }
            else if (int.Parse(gameId) == boggleState.LastGameId)
            {
                return GameState.Pending;
            }

            int timeLimit;
            long startTime;
            boggleState.GetTime(gameId, out timeLimit, out startTime);

            long endTime = startTime + timeLimit * (long)1e7;

            if (DateTime.UtcNow.Ticks < endTime)
            {
                return GameState.Active;
            }
            else
            {
                return GameState.Completed;
            }
        }

        private WordPair GetScore(PlayWordBody body, string gameId)
        {
            BoggleState _boggleState = BoggleState.getBoggleState();
            BoggleBoard board = new BoggleBoard(_boggleState.GetBoard(gameId));
            Predicate<WordPair> repeatFinder = (WordPair p) => { return p.Word == body.Word.Trim(); };

            List<WordPair> pairs = _boggleState.GetWords(gameId, body.UserToken);
            WordPair pair = new WordPair();
            pair.Word = body.Word.Trim();
            int wordLength = body.Word.Length;

            // Determine word score first!
            // Determine if word is legal or not
            if (board.CanBeFormed(pair.Word))
            {
                if (wordLength < 3 || !pairs.Exists(repeatFinder))
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
                else if (wordLength == 7)
                {
                    pair.Score = 5;
                }
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

            return pair;
        }
    }
}
