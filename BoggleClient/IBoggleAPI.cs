using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoggleClient
{
    interface IBoggleAPI
    {
        Task<int> playWord();

        Task<string> createUser(string nickName);

        Task<int> joinGame(string userToken, int timeLimit);

        Task cancelJoinRequest(string userToken);

        Task<int> playWord(string userToken, string word);

        string Title { set; };

        void DoClose();


    }
}
