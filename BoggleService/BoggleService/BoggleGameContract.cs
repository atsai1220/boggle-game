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

        [DataMember(EmitDefaultValue = false)]
        public Player player1;

        [DataMember(EmitDefaultValue = false)]
        public Player player2;
    }

    [DataContract]
    public class Player
    {
        [DataMember(EmitDefaultValue = false)]
        public string Nickname { get; set; }

        [DataMember]
        public int Score { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<WordPair> WordsPlayed { get; set; }
    }

    [DataContract]
    public class WordPair
    {
        [DataMember]
        public string Word { get; set; }

        [DataMember]
        public int Score { get; set; }
    }
}