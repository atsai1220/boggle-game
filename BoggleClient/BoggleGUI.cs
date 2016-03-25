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
    /// <summary>
    /// Class for Boggle game GUI
    /// </summary>
    public partial class BoggleGUI : Form, IBoggleView
    {

        /// <summary>
        /// Boggle GUI constructor
        /// </summary>
        public BoggleGUI()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// Close game event
        /// </summary>
        public event Action closeEvent;

        /// <summary>
        /// When the help button is pressed
        /// </summary>
        public event Action helpEvent;

        /// <summary>
        /// When Cancel button is pressed
        /// </summary>
        public event Action joinCanceledEvent;

        /// <summary>
        /// When game is joined
        /// </summary>
        public event Action<int> joinGameEvent;

        /// <summary>
        /// When registering for user 
        /// </summary>
        public event Action<string> registerPlayerEvent;

        /// <summary>
        /// When word is entered. 
        /// Key: Enter
        /// </summary>
        public event Action<string> wordEnteredEvent;

        /// <summary>
        /// Close current window
        /// </summary>
        public void DoClose()
        {
            Close();
        }

        public void ClearBoxes()
        {
            wordBox.Text = "";
            wordCountBox.Text = "";
            player1NameLabel.Text = "";
            player2NameLabel.Text = "";
        }

        /// <summary>
        /// Holds the nickname for player 1
        /// </summary>
        public string Nickname
        {
            set
            {
                player1NameLabel.Text = value;
            }
        }

        /// <summary>
        /// Holds the nickname for player 2
        /// </summary>
        public string Player2Nickname
        {
            set
            {
                player2NameLabel.Text = value;
            }
        }

        /// <summary>
        /// Holds player 1's score
        /// </summary>
        public int Player1Score
        {
            set
            {
                player1ScoreLabel.Text = value.ToString();
            }
        }

        /// <summary>
        /// Holds player 2's score
        /// </summary>
        public int Player2Score
        {
            set
            {
                player2ScoreLabel.Text = value.ToString();
            }
        }

        /// <summary>
        /// Holds the time remaining
        /// </summary>
        public int TimeRemaining
        {
            set
            {
                textBox3.Text = value.ToString();
            }
        }

        /// <summary>
        /// Displays the Boggle board
        /// </summary>
        public string BoardString
        {
            set
            {
                for (int i = 0; i < 16; i++)
                {
                    int row = i / 4;
                    int col = i % 4;

                    Button cube = (Button)cubeLayoutPanel.GetControlFromPosition(col, row);

                    if (value[i] == 'Q')
                    {
                        cube.Text = "Qu";
                    }
                    else
                    {
                        cube.Text = value[i] + "";
                    }
                }
            }
        }

        /// <summary>
        /// Shows a pop-up with message
        /// </summary>
        /// <param name="_message"></param>
        public void MessagePopUp(string _message)
        {
            MessageBox.Show(_message);
        }

        /// <summary>
        /// Method for submitting a word to the word record
        /// </summary>
        /// <param name="word">The word being recorded</param>
        /// <param name="score">The score of this.word</param>
        public void AddWord(string word, int score)
        {
            // update word count
            int _wordCount;
            int.TryParse(this.wordCountBox.Text, out _wordCount);
            _wordCount++;
            this.wordCountBox.Text = _wordCount.ToString();

            // update word list
            wordBox.Text += word + "\t" + score.ToString() + "\n";
        }

        /// <summary>
        /// Creates the end game window given the game Brief.
        /// </summary>
        /// <param name="_list1">Player 1's words and scores</param>
        /// <param name="_list2">Player 2's words and scores</param>
        public void endGameWindow(List<string> _list1, List<string> _list2)
        {
            // update word list
            using (EndForm endForm = new EndForm())
            {
                endForm.receiveText(_list1, _list2);
                endForm.receiveScores(player1ScoreLabel.Text, player2ScoreLabel.Text);
                endForm.receiveNames(player1NameLabel.Text, player2NameLabel.Text);
                // Display form
                endForm.ShowDialog();

                this.wordCountBox.Text = "0";
                wordBox.Text = "";
            }
        }

        /// <summary>
        /// Loads BoggleGUI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// When Enter is pressed when submitting player's word
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wordEntry_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;

                wordEnteredEvent(wordEntry.Text);
                wordEntry.Text = "";

            }
        }
    }
}
