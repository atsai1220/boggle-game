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
    }
}
