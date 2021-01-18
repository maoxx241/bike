using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bike
{
    public partial class Msg : Form
    {
        public Msg(string str)
        {
            InitializeComponent();
            this.TopMost = true;
            label1.Text = str;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            this.Hide();
        }
    }
}
