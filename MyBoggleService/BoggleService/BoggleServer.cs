using Boggle;
using CustomNetworking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace SimpleWebServer
{
    /// <summary>
    /// Server for boggle service.
    /// </summary>
    public class WebServer
    {
        /// <summary>
        /// Entry point for boggle service.
        /// </summary>
        public static void Main()
        {
            new WebServer();
            Console.Read();
        }

        /// <summary>
        /// Listens for new connections
        /// </summary>
        private TcpListener server;

        /// <summary>
        /// Constructor for web server.
        /// 
        /// Binds a TcpListener on port 60000
        /// </summary>
        public WebServer()
        {
            server = new TcpListener(IPAddress.Any, 60000);
            server.Start();
            server.BeginAcceptSocket(ConnectionRequested, null);
        }

        /// <summary>
        /// Create a socket for this connection
        /// And begin accepting another connection
        /// </summary>
        private void ConnectionRequested(IAsyncResult ar)
        {
            Socket s = server.EndAcceptSocket(ar);
            server.BeginAcceptSocket(ConnectionRequested, null);
            new HttpRequest(new StringSocket(s, new UTF8Encoding()));
        }
    }

    /// <summary>
    /// Class that handles a connection's actual request through a StringSocket
    /// </summary>
    class HttpRequest
    {
        /// <summary>
        /// String socket connection for the request
        /// </summary>
        private StringSocket ss;

        /// <summary>
        /// How many lines have been sent so far.
        /// Used to get the url and method from the first line.
        /// </summary>
        private int lineCount;

        /// <summary>
        /// The length of the content of the request.
        /// </summary>
        private int contentLength;

        /// <summary>
        /// The method of the HttpRequest
        /// </summary>
        private string method;

        /// <summary>
        /// The url of the HttpRequest.
        /// </summary>
        private string url;

        /// <summary>
        /// Regex for matching the url for a gameStatus request.
        /// </summary>
        private const string gameStatusRegex = @"BoggleService\.svc\/games\/([^\/?]+)(?:\?(?:(?:[Bb]rief=)?(.*)))?";

        /// <summary>
        /// Regex for matching the url for a playWord request.
        /// </summary>
        private const string playWordRegex = @"BoggleService\.svc\/games\/([^\/]+)";

        /// <summary>
        /// Constructor for HttpRequest
        /// </summary>
        public HttpRequest(StringSocket stringSocket)
        {
            this.ss = stringSocket;
            ss.BeginReceive(LineReceived, null);    // where we start reading the content of the request
        }

        /// <summary>
        /// Called whenever the string socket for this request recieves a line.
        /// </summary>
        private void LineReceived(string s, Exception e, object payload)
        {
            lineCount++;
            Console.WriteLine(s);
            if (s != null)
            {
                if (lineCount == 1)
                {
                    Regex r = new Regex(@"^(\S+)\s+(\S+)");
                    Match m = r.Match(s);
                    Console.WriteLine("Method: " + m.Groups[1].Value);
                    Console.WriteLine("URL: " + m.Groups[2].Value);

                    method = m.Groups[1].Value;
                    url = m.Groups[2].Value;
                }
                if (s.StartsWith("Content-Length:"))
                {
                    contentLength = Int32.Parse(s.Substring(16).Trim());
                }
                if (s == "\r")
                {
                    StringSocket.ReceiveCallback apiCall;

                    Object apiPayload = null;

                    if (method == "POST" && url == "/BoggleService.svc/users")
                    {
                        apiCall = HandleCreateUser;
                    }
                    else if (method == "POST" && url == "/BoggleService.svc/games")
                    {
                        apiCall = HandleJoinGame;
                    }
                    else if (method == "PUT" && url == "/BoggleService.svc/games")
                    {
                        apiCall = HandleCancelJoinRequest;
                    }
                    else if (method == "PUT" && Regex.IsMatch(url, playWordRegex))
                    {
                        Regex r = new Regex(playWordRegex);
                        Match m = r.Match(url);

                        string gameId = m.Groups[1].Value;

                        apiPayload = gameId;
                        apiCall = HandlePlayWord;
                    }
                    else if (method == "GET" && Regex.IsMatch(url, gameStatusRegex))
                    {
                        // There is no additional content to recieve.
                        HandleGameStatus(url);
                        return;
                    }
                    else
                    {
                        HandleBadRequest();
                        return;
                    }

                    ss.BeginReceive(apiCall, apiPayload, contentLength);
                }
                else
                {
                    ss.BeginReceive(LineReceived, null);
                }
            }
        }

        /// <summary>
        /// Callback to handle CreateUser api call
        /// </summary>
        private void HandleCreateUser(string content, Exception e, object payload)
        {
            if (e == null)
            {
                CreateUserBody body = JsonConvert.DeserializeObject<CreateUserBody>(content);

                BoggleService boggleService = new BoggleService();
                var contract = boggleService.CreateUser(body);

                string result = JsonConvert.SerializeObject(contract);
                Console.Write(result);
                sendResult(boggleService.GetHttpStatus(), result);
            }
        }

        /// <summary>
        /// Callback method for joining a game
        /// </summary>
        /// <param name="content"></param>
        /// <param name="e"></param>
        /// <param name="payload"></param>
        private void HandleJoinGame(string content, Exception e, object payload)
        {
            // If the Exception is non-null, it is the Exception that caused the receive attempt to fail.
            if (e == null)
            {
                //Console.Write("Join Game:");

                JoinGameBody body = JsonConvert.DeserializeObject<JoinGameBody>(content);

                BoggleService boggleService = new BoggleService();
                var contract = boggleService.JoinGame(body);

                string result = JsonConvert.SerializeObject(contract);

                sendResult(boggleService.GetHttpStatus(), result);
            }
        }

        /// <summary>
        /// Callback to handle JoinRequest api call
        /// </summary>
        private void HandleCancelJoinRequest(string content, Exception e, object payload)
        {
            if (e == null)
            {
                CancelJoinRequestBody body = JsonConvert.DeserializeObject<CancelJoinRequestBody>(content);

                BoggleService boggleService = new BoggleService();
                boggleService.CancelJoinRequest(body);

                string result = "";

                sendResult(boggleService.GetHttpStatus(), result);
            }
        }

        /// <summary>
        /// Callback method for playing a word
        /// </summary>
        /// <param name="content">UserToken and Word; need to JsonConvert.Deserialize</param>
        /// <param name="e"></param>
        /// <param name="payload">Contains the gameId</param>
        private void HandlePlayWord(string content, Exception e, object payload)
        {
            // If the Exception is non-null, it is the Exception that caused the receive attempt to fail
            if (e == null)
            {
                Console.Write("Player Word: ");
                PlayWordBody body = JsonConvert.DeserializeObject<PlayWordBody>(content);
                Console.Write(body.Word);

                BoggleService boggleService = new BoggleService();
                var contract = boggleService.PlayWord(body, payload.ToString());

                string result = JsonConvert.SerializeObject(contract);
                sendResult(boggleService.GetHttpStatus(), result);
            }
        }

        /// <summary>
        /// Callback to handle GameStatus api call
        /// </summary>
        /// <param name="url"></param>
        private void HandleGameStatus(string url)
        {
            Match match = Regex.Match(url, gameStatusRegex);

            string gameId = match.Groups[1].Value;
            string queryString = match.Groups[2].Value;

            BoggleService boggleService = new BoggleService();
            var contract = boggleService.GameStatus(gameId, queryString);

            JsonSerializerSettings settings = new JsonSerializerSettings();
            // This ignores null fields in The game status contract.
            settings.NullValueHandling = NullValueHandling.Ignore;

            string result = JsonConvert.SerializeObject(contract, settings);

            sendResult(boggleService.GetHttpStatus(), result);
        }

        /// <summary>
        /// Callback to handle bad requests
        /// 
        /// Called if request doesn't match any of the api calls.
        /// </summary>
        private void HandleBadRequest()
        {
            sendResult(HttpStatusCode.BadRequest, "");
        }

        /// <summary>
        /// Helper method to send the result of a request.
        /// </summary>
        private void sendResult(HttpStatusCode status, string result)
        {
            ss.BeginSend(String.Format("HTTP/1.1 {0} {1}\r\n", (int)status, status.ToString()), Ignore, null);
            ss.BeginSend("Content-Type: application/json\r\n", Ignore, null);
            ss.BeginSend("Content-Length: " + result.Length + "\r\n", Ignore, null);
            ss.BeginSend("\r\n", Ignore, null);
            ss.BeginSend(result, (ex, py) => { ss.Shutdown(); }, null);
        }

        /// <summary>
        /// Dummy method
        /// </summary>
        private void Ignore(Exception e, object payload)
        {
        }
    }
}