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
            if(body.Nickname == null || body.Nickname.Trim().Length == 0)
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
            BoggleState boggleState = BoggleState.getBoggleState();
            // If UserToken is invalid, TimeLimit < 5, or TimeLimit > 120, responds with status 403 (Forbidden).
            if (body.TimeLimit < 5 || body.TimeLimit > 120)
            {
                SetStatus(Forbidden);
                return null;
            }
            // If there is a pending game
            else if (boggleState.GetGameState() == "PENDING")
            {
                // If UserToken is already a player in the pending game
                if (boggleState)
                {
                    SetStatus(Conflict);
                    return null;
                }
                // if there is already one player in the pending game
                else
                {
                    boggleState.AddGame()
                }
            }
            // If there is already one player in the pending game
            else if ()

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

            if(player2Id == "")
            {
                return GameState.Pending;
            }

            int timeLimit;
            long startTime;
            boggleState.GetTime(gameId, out timeLimit, out startTime);
            
            long endTime = startTime + timeLimit * (long) 1e7;

            if(DateTime.UtcNow.Ticks < endTime)
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
