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
    public class WebServer
    {
        public static void Main()
        {
            new WebServer();
            Console.Read();
        }

        private TcpListener server;

        /// <summary>
        /// Create server and being accepting
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
        /// <param name="ar"></param>
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
        private StringSocket ss;
        private int lineCount;
        private int contentLength;

        private string method;
        private string url;

        private const string gameStatusRegex = @"BoggleService\.svc\/games\/([^\/?]+)(?:\?(?:(?:Brief=)?(.*)))?";

        public HttpRequest(StringSocket stringSocket)
        {
            this.ss = stringSocket;
            ss.BeginReceive(LineReceived, null);    // where we start reading the content of the request
        }

        /// <summary>
        /// Determines which Callback method to call
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        /// <param name="payload"></param>
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

                    if(method == "POST" && url == "/BoggleService.svc/users")
                    {
                        apiCall = HandleCreateUser;
                    }
                    else if(false) // Join game
                    {
                        
                    }
                    else if(method == "PUT" && url == "/BoggleService.svc/games")
                    {
                        apiCall = HandleCancelJoinRequest;
                    }
                    else if(method == "POST" && Regex.IsMatch(url, gameStatusRegex)) //Play word
                    {
                        Regex r = new Regex(@"BoggleService\.svc\/games\/(\d+)");
                        Match m = r.Match(url);

                        string gameId = m.Groups[1].Value;

                        apiPayload = gameId;
                        apiCall = HandleJoinGame;
                    }
                    else if(method == "GET" && Regex.IsMatch(url, gameStatusRegex))
                    {
                        // There is no additional content to recieve.
                        HandleGameStatus(url);
                        return;
                    }
                    else
                    {
                        apiCall = HandleBadRequest;
                    }

                    ss.BeginReceive(apiCall, apiPayload, contentLength);
                }
                else
                {
                    ss.BeginReceive(LineReceived, null);
                }
            }
        }

        private void HandleCreateUser(string content, Exception e, object payload)
        {
            CreateUserBody body = JsonConvert.DeserializeObject<CreateUserBody>(content);

            BoggleService boggleService = new BoggleService();
            var contract = boggleService.CreateUser(body);

            string result = JsonConvert.SerializeObject(contract);
            Console.Write(result);
            sendResult(boggleService.GetHttpStatus(), result);
        }

        // Andrew
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
                Console.Write("Join Game:");

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
            CancelJoinRequestBody body = JsonConvert.DeserializeObject<CancelJoinRequestBody>(content);

            BoggleService boggleService = new BoggleService();
            boggleService.CancelJoinRequest(body);

            string result = "";

            sendResult(boggleService.GetHttpStatus(), result);
        }

        //Andrew
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

        //SAm
        private void HandleGameStatus(string url)
        {
            Match match = Regex.Match(url, gameStatusRegex);

            string gameId = match.Groups[1].Value;
            string queryString = match.Groups[2].Value;

            BoggleService boggleService = new BoggleService();
            var contract = boggleService.GameStatus(gameId, queryString);

            string result = JsonConvert.SerializeObject(contract);

            sendResult(boggleService.GetHttpStatus(), result);
        }

        //Amdrew
        private void HandleBadRequest(string s, Exception e, object payload)
        {

        }

        private void sendResult(HttpStatusCode status, string result)
        {
            ss.BeginSend(String.Format("HTTP/1.1 {0} {1}\n", (int)status, status.ToString()), Ignore, null);
            ss.BeginSend("Content-Type: application/json\n", Ignore, null);
            ss.BeginSend("Content-Length: " + result.Length + "\n", Ignore, null);
            ss.BeginSend("\r\n", Ignore, null);
            ss.BeginSend(result, (ex, py) => { ss.Shutdown(); }, null);
        }

        private void Ignore(Exception e, object payload)
        {
        }
    }
}