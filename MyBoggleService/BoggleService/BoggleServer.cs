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

        public WebServer()
        {
            server = new TcpListener(IPAddress.Any, 60000);
            server.Start();
            server.BeginAcceptSocket(ConnectionRequested, null);
        }

        private void ConnectionRequested(IAsyncResult ar)
        {
            Socket s = server.EndAcceptSocket(ar);
            server.BeginAcceptSocket(ConnectionRequested, null);
            new HttpRequest(new StringSocket(s, new UTF8Encoding()));
        }

    }

    class HttpRequest
    {
        private StringSocket ss;
        private int lineCount;
        private int contentLength;

        private string method;
        private string url;

        public HttpRequest(StringSocket stringSocket)
        {
            this.ss = stringSocket;
            ss.BeginReceive(LineReceived, null);
        }

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

                    if(method == "POST" && url == "/BoggleService.svc/users")
                    {
                        apiCall = HandleCreateUser;
                    }
                    else if(false) //Join game
                    {

                    }
                    else if(false) //Cancel Join
                    {

                    }
                    else if(false) //Play word
                    {
                        
                    }
                    else if(false) //Game status
                    {

                    }
                    else
                    {
                        apiCall = BadRequest;
                    }

                    ss.BeginReceive(apiCall, null, contentLength);
                }
                else
                {
                    ss.BeginReceive(LineReceived, null);
                }
            }
        }

        private void HandleCreateUser(string content, Exception e, object payload)
        {
            Console.WriteLine("Create User:");

            CreateUserBody body = JsonConvert.DeserializeObject<CreateUserBody>(content);

            BoggleService boggleService = new BoggleService();
            var contract = boggleService.CreateUser(body);

            string result = JsonConvert.SerializeObject(contract);

            sendResult(boggleService.GetHttpStatus(), result);
        }

        // Andrew
        private void HandleJoinGame(string content, Exception e, object payload)
        {

        }

        //Sam
        private void HandleCancelJoinRequest(string content, Exception e, object payload)
        {

        }

        //Andrew
        private void HandlePlayWord(string content, Exception e, object payload)
        {

        }

        //SAm
        private void HandleGameStatus(string content, Exception e, object payload)
        {

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