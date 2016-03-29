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
        private enum GameState { Pending, Active, Completed }

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

        /// <summary>
        /// Joins a game
        /// </summary>
        /// <param name="body">Contains UserToken and TimeLimit</param>
        /// <returns></returns>
        public string JoinGame(JoinGameBody body)
        {
            string player1Id;
            string player2Id;

            BoggleState boggleState = BoggleState.getBoggleState();
            // If UserToken is invalid, TimeLimit < 5, or TimeLimit > 120, responds with status 403 (Forbidden).
            if (body.TimeLimit < 5 || body.TimeLimit > 120)
            {
                SetStatus(Forbidden);
                return null;
            }
            
            // LastGameId always contain a pending game
            boggleState.GetPlayers(boggleState.LastGameId.ToString(), out player1Id, out player2Id);   
                     
            // If UserToken is already a player in the pending game
            if (body.UserToken.Equals(player1Id))
            {
                SetStatus(Conflict);
                return null;
            }

            // if there is already one player in the pending game
            else if (player1Id.Length > 0)
            {
                BoggleBoard board = new BoggleBoard();
                long startTime = DateTime.UtcNow.Ticks;
                boggleState.StartGame(boggleState.LastGameId.ToString(), body.UserToken, body.TimeLimit, startTime, board.ToString());
                int oldGameId = boggleState.LastGameId;
                boggleState.LastGameId++;
                SetStatus(Created);
                return oldGameId.ToString();
            }

            // UserToken is the first player of a new game
            else
            {
                boggleState.AddGame(boggleState.LastGameId.ToString(), body.UserToken, body.TimeLimit);
                SetStatus(Accepted);
                return boggleState.LastGameId.ToString();
            }
        }

        public void CancelJoinRequest(CancelJoinRequestBody body)
        {
            throw new NotImplementedException();
        }

        public int PlayWord(PlayWordBody body, string gameId)
        {
            throw new NotImplementedException();
        }

        public BoggleGameContract GameStatus(string gameId, bool brief)
        {
            throw new NotImplementedException();
        }

        private GameState getGameState(string gameId)
        {
            BoggleState boggleState = BoggleState.getBoggleState();

            string player1Id;
            string player2Id;
            boggleState.GetPlayers(gameId, out player1Id, out player2Id);

            if (player2Id == "")
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
