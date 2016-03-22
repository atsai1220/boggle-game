using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoggleClient
{
    class IBoggleView
    {
        event Action<string> closeEvent;

        event Action<string> registerPlayerEvent;
        event Action joinGameEvent;
        event Action joinCanceledEvent;


    }
}
