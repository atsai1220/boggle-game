using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.ServiceModel.Web;
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

        public string CreateUser(CreateUserBody body)
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

                return userToken;
            }
        }

        public string JoinGame(JoinGameBody body)
        {
            string player1Id;
            string player2Id;
            int gameId;
            BoggleState boggleState = BoggleState.getBoggleState();
            // If UserToken is invalid, TimeLimit < 5, or TimeLimit > 120, responds with status 403 (Forbidden).
            if (body.TimeLimit < 5 || body.TimeLimit > 120)
            {
                SetStatus(Forbidden);
                return null;
            }
            
            // LastGameId always contain a pending game
            gameId = boggleState.LastGameId;
            boggleState.GetPlayers(gameId.ToString(), out player1Id, out player2Id);   
                     
            // If UserToken is already a player in the pending game
            if (body.UserToken.Equals)
            {
                SetStatus(Conflict);
                return null;
            }
            // if there is already one player in the pending game
            else
            {
                boggleState.AddGame()
                }
            
            // If there is already one player in the pending game
            else if ()

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

        public int PlayWord(PlayWordBody body, string gameId)
        {
            throw new NotImplementedException();
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

        private string CreateGameId()
        {

        }
    }
}
