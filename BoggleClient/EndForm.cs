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
    /// The form that displays at the end of the game
    /// </summary>
    public partial class EndForm : Form
    {
        /// <summary>
        /// Constructor for the form
        /// </summary>
        public EndForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Methods for receiving words and scores from this game
        /// </summary>
        /// <param name="_list1"></param>
        /// <param name="_list2"></param>
        public void receiveText(List<string> _list1, List<string> _list2)
        {
            richTextBox1.Text = "";
            richTextBox2.Text = "";

            foreach (string item in _list1)
            {
                richTextBox1.Text += item + "\n";
            }

            foreach (string item in _list2)
            {
                richTextBox2.Text += item + "\n";
            }
        }

        /// <summary>
        /// Method for receiving player names
        /// </summary>
        /// <param name="name1"></param>
        /// <param name="name2"></param>
        public void receiveNames(string name1, string name2)
        {
           this.Name1.Text = name1;
           this.Name2.Text = name2;
        }

        /// <summary>
        /// Method for receiving player scores
        /// </summary>
        /// <param name="_score1"></param>
        /// <param name="_score2"></param>
        public void receiveScores(string _score1, string _score2)
        {
            this.textBox1.Text = _score1;
            this.textBox2.Text = _score2;
        }

        /// <summary>
        /// Methods for closing the end game form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Done_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
