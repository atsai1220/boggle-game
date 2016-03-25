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
    /// The initial gui.
    /// Lets you input domainname, nickname and game duration.
    /// </summary>
    public partial class StartForm : Form
    {
        /// <summary>
        /// Constructor for StartForm.
        /// </summary>
        public StartForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event triggered when start game button is pressed.
        /// </summary>
        public event Action<string, string, string> startGameEvent;

        /// <summary>
        /// Event triggered when cancle button is pressed.
        /// </summary>
        public event Action cancelEvent;

        /// <summary>
        /// This is called when the start button is clicked.
        /// </summary>
        private void startButton_Click(object sender, EventArgs e)
        {
            if(startGameEvent != null)
            {
                startGameEvent(domainBox.Text, nicknameBox.Text, durationBox.Text);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (cancelEvent != null)
            {
                cancelEvent();
            }
        }

        /// <summary>
        /// This is used to enable or disable the start button.
        /// </summary>
        public void setStartButtonEnabled(bool enable)
        {
            startButton.Enabled = enable;
        }

        /// <summary>
        /// This is used to enable or disable the cancel button.
        /// </summary>
        public void setCancelButtonEnabled(bool enable)
        {
            cancelButton.Enabled = enable;
        }

        /// <summary>
        /// Loads the start form.
        /// </summary>
        private void StartForm_Load(object sender, EventArgs e)
        {

        }
    }
}
