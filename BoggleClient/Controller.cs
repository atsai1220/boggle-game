using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BoggleClient
{
    class  Controller
    {
        private IBoggleAPI boggleWindow;
        private Model boggleModel;
        
        /// <summary>
        /// Begin controlling boggleWindow
        /// </summary>
        public Controller()
        {
            boggleWindow = new BoggleGUI();

        }

        /// <summary>
        /// Registers the player with the boggle server.
        /// Sets the id of the player in the model.
        /// </summary>
        /// <param name="nickName">Desired name of the player.</param>
        private void registerPlayer(string nickName)
        {
            // POST /BoggleService.svc/users
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri()

        }

        /// <summary>
        /// Creates an HttpClient for communicating with GitHub.  The GitHub API requires specific information
        /// to appear in each request header.
        /// </summary>
        public static HttpClient CreateClient()
        {
            // Create a client whose base address is the GitHub server
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.github.com/");

            // Tell the server that the client will accept this particular type of response data
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");

            // This is an authorization token that you can create by logging in to your GitHub account.
            client.DefaultRequestHeaders.Add("Authorization", "token " + TOKEN);

            // When an http request is made from a browser, the user agent describes the browser.
            // Github requires the email address of the authenticated user.
            client.DefaultRequestHeaders.UserAgent.Clear();
            client.DefaultRequestHeaders.Add("User-Agent", Uri.EscapeDataString(EMAIL));

            // There is more client configuration to do, depending on the request.
            return client;
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
    }
}
