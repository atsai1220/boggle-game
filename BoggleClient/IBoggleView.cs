using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoggleClient
{
    interface IBoggleView
    {
        event Action closeEvent;

        event Action<string> registerPlayerEvent;
        event Action joinGameEvent;
        event Action joinCanceledEvent;
        event Action<string, bool> programStartEvent;
        event Action gameStartEvent;
        event Action AboutEvent;
        event Action helpEvent;
        event Action<string> messagePopUpEvent;


        void DoClose();

    }
}
