using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stenach_app
{
    public partial class FormReply : Form
    {
        public FormReply(string user = "", string comment = "")
        {
            InitializeComponent();
            this.Text = this.Text + " " + user;
            this.textBoxOutput.Text = comment;
            this.textBoxInput.Focus();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public string getTextBoxInputValue()
        {
            return this.textBoxInput.Text;
        }

        private void FormReply_Load(object sender, EventArgs e)
        {
            this.textBoxInput.Focus();
        }
    }
}
