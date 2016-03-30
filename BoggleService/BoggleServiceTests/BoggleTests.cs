using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Dynamic;
using static System.Net.HttpStatusCode;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Boggle
{
    /// <summary>
    /// Provides a way to start and stop the IIS web server from within the test
    /// cases.  If something prevents the test cases from stopping the web server,
    /// subsequent tests may not work properly until the stray process is killed
    /// manually.
    /// </summary>
    public static class IISAgent
    {
        // Reference to the running process
        private static Process process = null;

        /// <summary>
        /// Starts IIS
        /// </summary>
        public static void Start(string arguments)
        {
            if (process == null)
            {
                ProcessStartInfo info = new ProcessStartInfo(Properties.Resources.IIS_EXECUTABLE, arguments);
                info.WindowStyle = ProcessWindowStyle.Minimized;
                info.UseShellExecute = false;
                process = Process.Start(info);
            }
        }

        /// <summary>
        ///  Stops IIS
        /// </summary>
        public static void Stop()
        {
            if (process != null)
            {
                process.Kill();
            }
        }
    }
    [TestClass]
    public class BoggleTests
    {
        /// <summary>
        /// This is automatically run prior to all the tests to start the server
        /// </summary>
        [ClassInitialize()]
        public static void StartIIS(TestContext testContext)
        {
            IISAgent.Start(@"/site:""BoggleService"" /apppool:""Clr4IntegratedAppPool"" /config:""..\..\..\.vs\config\applicationhost.config""");
        }

        /// <summary>
        /// This is automatically run when all tests have completed to stop the server
        /// </summary>
        [ClassCleanup()]
        public static void StopIIS()
        {
            IISAgent.Stop();
        }

        private RestTestClient client = new RestTestClient("http://localhost:60000/");

        [TestMethod]
        public void TestMethod1()
        {
            Response r = client.DoGetAsync("/numbers?length={0}", "5").Result;
            Assert.AreEqual(OK, r.Status);
            Assert.AreEqual(5, r.Data.Count);
            r = client.DoGetAsync("/numbers?length={0}", "-5").Result;
            Assert.AreEqual(Forbidden, r.Status);
        }

        [TestMethod]
        public void TestMethod2()
        {
            List<int> list = new List<int>();
            list.Add(15);
            Response r = client.DoPostAsync("/first", list).Result;
            Assert.AreEqual(OK, r.Status);
            Assert.AreEqual(15, r.Data);
        }

        /// <summary>
        /// Tests CreateUser
        /// </summary>
        [TestMethod]
        public void CreateUserTest1()
        {
            dynamic player = new ExpandoObject();
            player.Nickname = "Andrew";
            Response r = client.DoPostAsync("/users", player).Result;
            Assert.AreEqual(Created, r.Status);
        }

        /// <summary>
        /// Tests CreateUser for empty Nickname
        /// </summary>
        [TestMethod]
        public void CreateuserTest2()
        {
            dynamic player = new ExpandoObject();
            player.Nickname = "";
            Response r = client.DoPostAsync("/users", player).Result;
            Assert.AreEqual(Forbidden, r.Status);
        }

        /// <summary>
        /// Tests CreateUser for null Nickname
        /// </summary>
        [TestMethod]
        public void CreateuserTest3()
        {
            dynamic player = new ExpandoObject();
            player.Nickname = null;
            Response r = client.DoPostAsync("/users", player).Result;
            Assert.AreEqual(Forbidden, r.Status);
        }

        /// <summary>
        /// Tests JoinGame
        /// </summary>
        [TestMethod]
        public void JoinGameTest1()
        {
            dynamic player = new ExpandoObject();
            player.Nickname = "Andrew";
            Response playerR = client.DoPostAsync("/users", player).Result;
            string userToken = playerR.Data;

            dynamic data = new ExpandoObject();
            data.UserToken = userToken;
            data.TimeLimit = 25;

            Response dataR = client.DoPostAsync("/games", data).Result;
            Assert.AreEqual(Accepted, dataR.Status);

            CancelGameTest1(userToken);
        }

        // tests cancel 
        [TestMethod]
        public void CancelGameTest1(string userToken)
        {
            dynamic data = new ExpandoObject();
            data.UserToken = "thisSHOULDNTwork";

            Response dataR = client.DoPutAsync("/games", data).Result;
            Assert.AreEqual(Forbidden, dataR);

            data.UserToken = userToken;
            dataR = client.DoPutAsync("/games", data).Result;
            Assert.AreEqual(OK, dataR);
        }

        /// <summary>
        /// Test player 2 joining a pending game
        /// </summary>
        public void Player2JoinGame()
        {
            // Player 1
            dynamic player1 = new ExpandoObject();
            player1.Nickname = "Andrew";
            Response player1R = client.DoPostAsync("/users", player1).Result;
            string userToken1 = player1R.Data;

            dynamic data1 = new ExpandoObject();
            data1.UserToken = userToken1;
            data1.TimeLimit = 5;

            Response data1R = client.DoPostAsync("/games", data1).Result;
            Assert.AreEqual(Accepted, data1R.Status);

            // Player 2
            dynamic player2 = new ExpandoObject();
            player2.Nickname = "Sam";
            Response player2R = client.DoPostAsync("/users", player2).Result;
            string userToken2 = player2R.Data;

            dynamic data2 = new ExpandoObject();
            data2.UserToken = userToken2;
            data2.TimeLimit = 5;

            Response dataR = client.DoPostAsync("/games", data2).Result;
            Assert.AreEqual(Created, dataR.Status);
        }


    }
}
