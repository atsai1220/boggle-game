using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;

namespace BoggleClient
{
    /// <summary>
    /// Code to perform most of the decisions of the boggle client.
    /// </summary>
    class Controller
    {
        /// <summary>
        /// Httpclient used to make api calls.
        /// </summary>
        private HttpClient client;

        /// <summary>
        /// The main gui for the game.
        /// </summary>
        private BoggleGUI boggleWindow;

        /// <summary>
        /// Initial window that pops up.
        /// </summary>
        private StartForm boggleStart;

        /// <summary>
        /// Represents the state of the boggle
        /// client.
        /// </summary>
        private Model boggleModel;

        /// <summary>
        /// This is set to true when cancel button is pressed.
        /// Used to cancel joining a game.
        /// </summary>
        private bool cancel;

        /// <summary>
        /// Constructor for controller.
        /// 
        /// Takes in an instance of StartForm
        /// </summary>
        public Controller(StartForm boggleStart) : base()
        {
            this.boggleStart = boggleStart;

            boggleModel = new Model();

            boggleStart.startGameEvent += HandleGameStartRequest;
            boggleStart.cancelEvent += HandleCancelJoinRequest;

            boggleWindow = new BoggleGUI();

            boggleWindow.joinCanceledEvent += HandleCancelJoinRequest;
            boggleWindow.closeEvent += HandleCloseEvent;
            boggleWindow.helpEvent += HandleHelpEvent;
            boggleWindow.wordEnteredEvent += HandleWordEnteredEvent;
        }

        /// <summary>
        /// Called when start game is pressed.
        /// </summary>
        /// <param name="domain">The domain name</param>
        /// <param name="nickname">The nickname</param>
        /// <param name="duration">The desired duration of the game.</param>
        private async void HandleGameStartRequest(string domain, string nickname, string duration)
        {
            if(domain.Equals(""))
            {
                return;
            }

            if(nickname.Equals(""))
            {
                return;
            }

            if(!Regex.IsMatch(duration, @"^\d+$"))
            {
                return;
            }

            int dur = int.Parse(duration);
            if(dur < 5 || dur > 120)
            {
                return;
            }

            cancel = false;

            boggleStart.setStartButtonEnabled(false);
            boggleStart.setCancelButtonEnabled(true);

            boggleModel.domain = domain;

            CreateClient();

            try
            {
                await registerPlayer(nickname);
            }
            catch(HttpRequestException)
            {
                boggleStart.setStartButtonEnabled(true);
                boggleStart.setCancelButtonEnabled(false);

                return;
            }

            await joinGame(dur);

            if (cancel)
            {
                boggleStart.setStartButtonEnabled(true);
                boggleStart.setCancelButtonEnabled(false);

                return;
            }

            boggleStart.Hide();
            boggleWindow.ShowDialog(boggleStart);
            boggleStart.Show();

            boggleStart.setStartButtonEnabled(true);
            boggleStart.setCancelButtonEnabled(false);
        }

        /// <summary>
        /// Create HttpClient to communicate with server
        /// </summary>
        /// <returns></returns>
        private void CreateClient()
        {
            HttpClient client = new HttpClient();

            // TODO Controller must update boggleModel.domain
            client.BaseAddress = new Uri(boggleModel.domain);


            // Tell the server that the client will accept this particular type of response data
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            this.client = client;

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
        private async Task registerPlayer(string nickName)
        {
            // TODO implement Player to get token
            dynamic player = new ExpandoObject();
            //player.Nickname = boggleModel.GetName();
            player.Nickname = nickName;

            // To send a POST request, we must include the serialized parameter object
            // in the body of the request.
            StringContent content = new StringContent(JsonConvert.SerializeObject(player), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("/BoggleService.svc/users", content);

            if (response.IsSuccessStatusCode)
            {
                // TODO get and set user token to player
                String result = response.Content.ReadAsStringAsync().Result;
                dynamic serverResponse = JsonConvert.DeserializeObject(result);
                Console.WriteLine(serverResponse);

                boggleModel.UserToken = serverResponse.UserToken;
            }
            else
            {
                // TODO change
                handleMessagePopUpEvent("If Nickname is null, or is empty when trimmed, responds with status 403 (Forbidden).");
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
        private async Task joinGame(int timeLimit)
        {
            dynamic data = new ExpandoObject();

            data.UserToken = boggleModel.UserToken;
            data.TimeLimit = timeLimit;

            StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("/BoggleService.svc/games", content);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                dynamic responseObject = JsonConvert.DeserializeObject(result);

                boggleModel.GameId = responseObject.GameID;

                previousGameState = "pending";

                gameOn = true;

                checkGameStatus();

                while(previousGameState.Equals("pending") && !cancel)
                {
                    Task wait = new Task(() => Thread.Sleep(250));
                    wait.Start();
                    await wait;
                }
            }
            else
            {
                // TODO Display error message
            }
        }

        /// <summary>
        /// The last game state recieved from the server.
        /// Transitouns between game states are important
        /// for knowing what to to.
        /// </summary>
        private string previousGameState;

        /// <summary>
        /// boolean checked in gamestate polling loop.
        /// When it is false checkGameStatus will stop.
        /// </summary>
        private bool gameOn;

        /// <summary>
        /// Repeatedly checks the status of the current game.
        /// </summary>
        private async Task checkGameStatus()
        {
            while (gameOn && !cancel)
            {
                string url = String.Format("/BoggleService.svc/games/{0}", boggleModel.GameId);

                //  Request the short version
                if (previousGameState.Equals("active"))
                {
                    url += "?yes";
                }

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();

                    dynamic gameStatus = JsonConvert.DeserializeObject(result);

                    Console.WriteLine(gameStatus.GameState);

                    string gameState = gameStatus.GameState;

                    if (gameState.Equals("pending"))
                    {
                        // Wait
                    }
                    if (gameState.Equals("active"))
                    {
                        if (previousGameState.Equals("pending"))
                        {
                            HandleGameStarted(gameStatus);
                        }
                        else if (previousGameState.Equals("active"))
                        {
                            HandleGameStateUpdate(gameStatus);
                        }
                    }
                    if (gameState.Equals("completed"))
                    {
                        if (previousGameState.Equals("active"))
                        {
                            HandleGameStateUpdate(gameStatus);
                        }
                        if (previousGameState.Equals("completed"))
                        {
                            HandleGameEndEvent(gameStatus);

                            return;
                        }
                    }

                    previousGameState = gameState;
                }
                else
                {
                    // TODO display error message.
                }

                Task wait = new Task(() => Thread.Sleep(1000));
                wait.Start();
                await wait;
            }
        }
        
        /// <summary>
        /// Cancel a pending request to join a game.
        /// </summary>
        private async void HandleCancelJoinRequest()
        {
            //boggleStart.setStartButtonEnabled(true);
            boggleStart.setCancelButtonEnabled(false);

            cancel = true;

            dynamic data = new ExpandoObject();
            data.UserToken = boggleModel.UserToken;

            StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync("/BoggleService.svc/games", content);

            if (response.IsSuccessStatusCode)
            {

            }
            else
            {
                // TODO display error message
            }
        }

        /// <summary>
        /// Handles Close event
        /// </summary>
        private void HandleCloseEvent()
        {
            gameOn = false;
            boggleWindow.DoClose();
        }

        /// <summary>
        /// Displays the help message.
        /// </summary>
        private void HandleHelpEvent()
        {
            boggleWindow.MessagePopUp(
                @"How to:
1) Register your account with a nickname
2) Join a game with a specified time
3) Wait till another player joins
4) Start!

Rules:
Create strings that are legal words for points!
< 3 characters 0 pt
3 - 4 characters 1 pt
5 characters 2 pts
6 characters 3 pts
7 characters 5 pts
> 7 characters 11 pts

otherwise, -1 pt");
        }
        
        /// <summary>
        /// dispay end game with list of words and score
        /// </summary>
        private async void HandleGameEndEvent(dynamic gameStatus)
        {
            List<string> player1List = new List<string>();
            List<string> player2List = new List<string>();

            boggleModel.wordsPlayed = 0;
            

            HttpResponseMessage response = await client.GetAsync("/BoggleService.svc/games/" + boggleModel.GameId + "?yes");
            String result = response.Content.ReadAsStringAsync().Result;
            dynamic serverResponse = JsonConvert.DeserializeObject(result);

            // Make recorded words into list
            foreach (var data in serverResponse.Player1.WordsPlayed)
            {
                string _score = data.Score;
                string _word = data.Word;
                player1List.Add(_score + "\t" + _word);
                
            }

            foreach (var data in serverResponse.Player2.WordsPlayed)
            {
                string _score = data.Score;
                string _word = data.Word;
                player2List.Add(_score + "\t" + _word);

            }

            boggleWindow.endGameWindow(player1List, player2List);
        }
        
        /// <summary>
        /// populate cubes and set timer
        /// </summary>
        private void HandleGameStarted(dynamic gameStatus)
        {
            boggleWindow.ClearBoxes();
            boggleWindow.BoardString = gameStatus.Board;

            boggleWindow.TimeRemaining = gameStatus.TimeLeft;

            boggleWindow.Nickname = gameStatus.Player1.Nickname;
            boggleWindow.Player1Score = gameStatus.Player1.Score;

            boggleWindow.Player2Nickname = gameStatus.Player2.Nickname;
            boggleWindow.Player2Score = gameStatus.Player2.Score;
        }

        /// <summary>
        /// Updates the time remaining and the player scores.
        /// </summary>
        private void HandleGameStateUpdate(dynamic gameStatus)
        {
            boggleWindow.TimeRemaining = gameStatus.TimeLeft;

            boggleWindow.Player1Score = gameStatus.Player1.Score;
            boggleWindow.Player2Score = gameStatus.Player2.Score;
        }
        
        /// <summary>
        /// send to server for points
        /// </summary>
        private async void HandleWordEnteredEvent(string _wordEntered)
        { 

            dynamic word = new ExpandoObject();
            word.UserToken = boggleModel.UserToken;
            word.Word = _wordEntered;

            // To send a POST request, we must include the serialized parameter object
            // in the body of the request.
            //     PUT /BoggleService.svc/games/:GameID
            StringContent content = new StringContent(JsonConvert.SerializeObject(word), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync("/BoggleService.svc/games/" + boggleModel.GameId, content);
            

            if (response.IsSuccessStatusCode)
            {
                // TODO get and set user token to player
                String result = response.Content.ReadAsStringAsync().Result;
                dynamic serverResponse = JsonConvert.DeserializeObject(result);
                Console.WriteLine(serverResponse);
                boggleModel.wordsPlayed++;
                int intScore;
                string _score = serverResponse.Score;
                int.TryParse(_score, out intScore);
                boggleWindow.AddWord(_wordEntered, intScore);

                boggleModel.wordRecord.Add(_wordEntered + "\t" + intScore.ToString());

            }
            // If Word is null or empty when trimmed, or if GameID or UserToken is missing or invalid, 
            // or if UserToken is not a player in the game identified by GameID, responds with response 
            // code 403 (Forbidden).
            else
            {
                handleMessagePopUpEvent("Check Word/GameID/UserToken");
            }
        }
    }

    
}
