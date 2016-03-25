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

    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        public event Action<string, string, string> startGameEvent;
        public event Action cancelEvent;

        private void startButton_Click(object sender, EventArgs e)
        {
            if(startGameEvent != null)
            {
                startGameEvent(domainBox.Text, nicknameBox.Text, durationBox.Text);
            }
        }

        public void setStartButtonEnabled(bool enable)
        {
            startButton.Enabled = enable;
        }

        public void setCancelButtonEnabled(bool enable)
        {
            cancelButton.Enabled = enable;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (cancelEvent != null)
            {
                cancelEvent();
            }
        }

        private void StartForm_Load(object sender, EventArgs e)
        {

        }
    }
}
