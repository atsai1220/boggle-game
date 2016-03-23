using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;



namespace BoggleClient
{
    class  Controller
    {
        private IBoggleView boggleWindow;
        private Model boggleModel;
        
        /// <summary>
        /// Begin controlling boggleWindow
        /// </summary>
        public Controller()
        {
            boggleModel = new Model();
        }

        public Controller(IBoggleView _boggleWindow) : base()
        {
            boggleWindow = _boggleWindow;
            boggleWindow.registerPlayerEvent += registerPlayer;
            boggleWindow.joinGameEvent += joinGame;
            boggleWindow.joinCanceledEvent += cancelJoinRequest;
            boggleWindow.closeEvent += HandleCloseEvent;
            boggleWindow.programStartEvent += handleProgramStartEvent;
            boggleWindow.messagePopUpEvent += handleMessagePopUpEvent;
        }

        
        /// <summary>
        /// Create HttpClient to communicate with server
        /// </summary>
        /// <returns></returns>
        private static HttpClient CreateClient()
        {
            // TODO change Uri to update with input from user
            // Create a client whose base address is the GitHub server
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://bogglecs3500s16.azurewebsites.net");

            // Tell the server that the client will accept this particular type of response data
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            


            // There is more client configuration to do, depending on the request.
            return client;
        }

        

        /// <summary>
        /// Registers the player with the boggle server.
        /// Sets the id of the player in the model.
        /// 
        /// POST /BoggleService.svc/users
        /// {
        ///     "Nickname": "Joe"
        /// }
        /// </summary>
        /// <param name="nickName">Desired name of the player.</param>
        private void registerPlayer(string nickName)
        {
            // TODO implement Player to get token
            using (HttpClient client = CreateClient())
            {

                // An ExpandoObject is one to which in which we can set arbitrary properties.
                // To create a new public repository, we must send a request parameter which
                // is a JSON object with various properties of the new repo expressed as
                // properties.
                dynamic player = new ExpandoObject();
                //player.Nickname = boggleModel.GetName();
                player.Nickname = nickName;

                // To send a POST request, we must include the serialized parameter object
                // in the body of the request.
                StringContent content = new StringContent(JsonConvert.SerializeObject(player), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("/BoggleService.svc/users", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    // The deserialized response value is an object that describes the user token
                    // TODO get and set user token to player
                    String result = response.Content.ReadAsStringAsync().Result;
                    dynamic serverResponse = JsonConvert.DeserializeObject(result);
                    Console.WriteLine(serverResponse);
                }
                else
                {
                    // TODO change
                    handleMessagePopUpEvent("If Nickname is null, or is empty when trimmed, responds with status 403 (Forbidden).");
                }
            }
        }

        /// <summary>
        /// Handles a pop-up dialog with passed message
        /// </summary>
        /// <param name="_message"></param>
        private void handleMessagePopUpEvent(string _message)
        {
            boggleWindow.MessagePopUp(_message);
        }


        /// <summary>
        /// Joins a game or starts a new game.
        /// 
        /// State should go to waiting for game to start.
        /// </summary>
        private void joinGame()
        {

        }

        /// <summary>
        /// Cancel a pending request to join a game.
        /// </summary>
        private void cancelJoinRequest()
        {

        }

        /// <summary>
        /// Handles Close event
        /// </summary>
        private void HandleCloseEvent()
        {
            boggleWindow.DoClose();
        }

        private void HandleAboutEvent()
        {

        }

        private void HandleHelpEvent()
        {

        }

        /// <summary>
        /// dispay end game
        /// </summary>
        private void HandleGameEndEvent()
        {

        }

        /// <summary>
        /// populate cubes and set timer
        /// </summary>
        private void HandleGameStartEvent()
        {

        }

        /// <summary>
        /// send to server for points
        /// </summary>
        private void HandleWordEnteredEvent()
        {

        }
    }
}
