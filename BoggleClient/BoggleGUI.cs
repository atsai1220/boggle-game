using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoggleClient
{
    public partial class BoggleGUI : Form, IBoggleView
    {
        public BoggleGUI()
        {
            InitializeComponent();
            programStartEvent("startPanel", true);
        }

        public event Action AboutEvent;
        public event Action closeEvent;
        public event Action gameStartEvent;
        public event Action helpEvent;
        public event Action joinCanceledEvent;
        public event Action joinGameEvent;
        public event Action<string, bool> programStartEvent;
        public event Action<string> registerPlayerEvent;

        /// <summary>
        /// Close current window
        /// </summary>
        public void DoClose()
        {
            Close();
        }

        private void BoggleGUI_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Clicking "Boggle --> Quit"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (closeEvent != null)
            {
                closeEvent();
            }
        }

        private void gameStartPanel()
        {

        }


    }
}
