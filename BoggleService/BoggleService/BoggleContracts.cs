using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Boggle
{
    [DataContract]
    public class BoggleGameContract
    {
        [DataMember]
        public string GameState { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Board { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string TimeLimit { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string TimeLeft { get; set; }

        [DataMember]
        public Player Player1;

        [DataMember]
        public Player Player2;
    }

    [DataContract]
    public struct Player
    {
        [DataMember(EmitDefaultValue = false)]
        public string Nickname { get; set; }

        [DataMember]
        public int Score { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<WordPair> WordsPlayed { get; set; }
    }

    [DataContract]
    public struct WordPair
    {
        [DataMember]
        public string Word { get; set; }

        [DataMember]
        public int Score { get; set; }
    }

    [DataContract]
    public class UserTokenContract
    {
        [DataMember]
        public string UserToken { get; set; }
    }

    [DataContract]
    public class GameIdContract
    {
        [DataMember]
        public string GameID { get; set; }
    }

    [DataContract]
    public class PlayWordContract
    {
        [DataMember]
        public string Score { get; set; }
    }
}