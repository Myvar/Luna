using Luna.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Luna
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        Engine en ;

        private void MainForm_Load(object sender, EventArgs e)
        {
            en = new Engine(MainHost);
            en.OpenApp("http://myvarhd.github.io/Luna/");
            en.InvokeMain();

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
