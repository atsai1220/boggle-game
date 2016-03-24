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
    public partial class domainForm : Form
    {
        public domainForm()
        {
            InitializeComponent();
        }

        private void domainAcceptButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.domainValue = this.domainComboBox.Text;
        }

        private void domainCancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        public string TheValue
        {
            get { return this.domainComboBox.Text; }
        }

        private string domainValue
        {
            set; get;
        }
        
    }
}
