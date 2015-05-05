using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Luna.Core.Internal.Lib.UI
{
    public partial class InputDialog : Form
    {
        public string Qeustion { get; set; }
        public string Data { get; set; }

        public InputDialog()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Data = textBox1.Text;
            this.Close();
        }

        private void InputDialog_Load(object sender, EventArgs e)
        {
            label1.Text = Qeustion;
        }
    }
}
