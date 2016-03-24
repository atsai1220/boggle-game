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
        public string Nickname
        {
            set
            {
                //throw new NotImplementedException();
            }
        }

        public string Player2Nickname
        {
            set
            {
                //throw new NotImplementedException();
            }
        }

        public int Player1Score
        {
            set
            {
                //throw new NotImplementedException();
            }
        }

        public int Player2Score
        {
            set
            {
                //throw new NotImplementedException();
            }
        }

        public int TimeRemaining
        {
            set
            {
                textBox3.Text = value.ToString();
            }
        }

        public string BoardString
        {
            set
            {
                for(int i = 0; i < 16; i++)
                {
                    int row = i / 4;
                    int col = i % 4;

                    Button cube = (Button) cubeLayoutPanel.GetControlFromPosition(col, row);
                    cube.Text = value[i] + "";
                }
            }
        }

        public BoggleGUI()
        {
            InitializeComponent();
        }
            

        public event Action AboutEvent;
        public event Action closeEvent;
        public event Action helpEvent;
        public event Action joinCanceledEvent;
        public event Action<int> joinGameEvent;
        public event Action<string, bool> programStartEvent;
        public event Action<string> registerPlayerEvent;
        public event Action<string> MessagePopUpEvent;
        public event Action<string> messagePopUpEvent;
        public event Action aboutEvent;
        public event Action<string> domainNameEntered;
        public event Action<string> wordEnteredEvent;

        /// <summary>
        /// Close current window
        /// </summary>
        public void DoClose()
        {
            Close();
        }

        /// <summary>
        /// Shows a pop-up with message
        /// </summary>
        /// <param name="_message"></param>
        public void MessagePopUp(string _message)
        {
            MessageBox.Show(_message);
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

        public void AddWord(string word, int score)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Clicking "Help --> How to Play
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void howToPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (helpEvent != null)
            {
                helpEvent();
            }
        }

        /// <summary>
        /// Clicking and selecting a domain
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void domainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (domainForm domainForm = new domainForm())
            {
                if (domainForm.ShowDialog() == DialogResult.OK)
                {
                    if (domainNameEntered != null)
                    {
                        domainNameEntered(domainForm.TheValue);
                    }
                }
            }
        }

        private void joinNewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (joinGameEvent != null)
            {
                // TODO get this from somewhere.
                joinGameEvent(100);
            }
        }

        private void setNicknameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (registerPlayerEvent != null)
            {
                // TODO Query user for nickname.
                registerPlayerEvent("asdf");
            }
        }
    }
}
