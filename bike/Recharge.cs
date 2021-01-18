using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace bike
{
    public partial class Recharge : Form
    {
        private string username;
        private double bl;
        public Recharge(string user)
        {
            InitializeComponent();
            this.TopMost = true;
            this.username = user;
            var SQLiteConnection = new SQLiteConnection(connstr);
            SQLiteConnection.Open();
            var cmd = new SQLiteCommand(SQLiteConnection);
            cmd.CommandText = "SELECT * FROM finance WHERE username=@username";
            cmd.Parameters.AddWithValue("@username", this.username);
            SQLiteDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                this.bl = rdr.GetDouble(1);
                label6.Text = bl.ToString();

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 4)
            {
                textBox2.Focus();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 4)
            {
                textBox3.Focus();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Length == 4)
            {
                textBox4.Focus();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text.Length == 2)
            {
                textBox7.Focus();
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }


        private static string connstr = ConfigService.ConnectionString;
        private bool rechargeprocess()
        {
            try
            {
                var SQLiteConnection = new SQLiteConnection(connstr);
                SQLiteConnection.Open();
                var cmd = new SQLiteCommand(SQLiteConnection);
                cmd.CommandText = "UPDATE finance SET BALANCE =@balance WHERE username=@username";
                cmd.Parameters.AddWithValue("@username", this.username);
                double balance = Double.Parse(textBox9.Text.Trim());
                cmd.Parameters.AddWithValue("@balance", this.bl+balance);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {

            }
            return false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (rechargeprocess())
            {
                Msg msg = new Msg("recharge successfully");
                msg.BringToFront();
                this.TopMost = false;
                this.Hide();
                msg.Show();
                
            }
            else
            {
                Msg warning = new Msg("Recharge failed");
                warning.BringToFront();
                this.TopMost = false;
                this.Hide();
                warning.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {

            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 46){
                e.Handled = true;
            }          
        }
    }
}
