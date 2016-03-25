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
    public partial class EndForm : Form
    {
        public EndForm()
        {
            InitializeComponent();
        }

        public void receiveText(List<string> _list1, List<string> _list2)
        {
            foreach (string item in _list1)
            {
                richTextBox1.Text += item + "\n";
            }

            foreach (string item in _list2)
            {
                richTextBox2.Text += item + "\n";
            }
        }

        public void receiveNames(string name1, string name2)
        {
           this.Name1.Text = name1;
           this.Name2.Text = name2;
        }

        public void receiveScores(string _score1, string _score2)
        {
            this.textBox1.Text = _score1;
            this.textBox2.Text = _score2;
        }

        private void Done_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Score2Label_Click(object sender, EventArgs e)
        {

        }
    }
}
