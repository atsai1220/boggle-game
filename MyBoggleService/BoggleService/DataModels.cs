using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Boggle
{
    public class CreateUserBody
    {
        public string Nickname { get; set; }
    }

    public class JoinGameBody
    {
        public string UserToken { get; set; }
        public int TimeLimit { get; set; }
    }
    public class CancelJoinRequestBody
    {
        public string UserToken { get; set; }
    }

    public class PlayWordBody
    {
        public string UserToken { get; set; }
        public string Word { get; set; }
    }
}