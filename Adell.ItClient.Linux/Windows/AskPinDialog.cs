using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Adell.ItClient.Linux.Windows
{
    public partial class AskPinDialog : Form
    {
        public AskPinDialog()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Pin = textBox1.Text;
            DialogResult = DialogResult.Retry;
        }

        public string Pin { get; private set; }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
