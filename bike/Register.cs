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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private static string connstr = ConfigService.ConnectionString;
        private bool registerprocess()
        {
            string gender = "";
            if (genderbutton.Checked)
            {
                gender = "male";
            }
            else
            {
                gender = "female";
            }

            int.TryParse(agebox.Text.Trim(), out int num);

            var SQLiteConnection = new SQLiteConnection(connstr);

            SQLiteConnection.Open();
            try
            {

                var cmd = new SQLiteCommand(SQLiteConnection);
                cmd.CommandText = "INSERT INTO person VALUES(@username,@password,@operator)";
                cmd.Parameters.AddWithValue("@username", usernamebox.Text.Trim());
                cmd.Parameters.AddWithValue("@password", passwordbox1.Text.Trim());
                if (adminbox.Text.Trim() == "Qi")
                {
                    cmd.Parameters.AddWithValue("@operator", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@operator", 0);
                }
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                Msg warning = new Msg("Username is already taken");
                warning.BringToFront();
                warning.Show();
                return false;
            }

            var cmd1 = new SQLiteCommand(SQLiteConnection);
            cmd1.CommandText = "INSERT INTO user VALUES(@username,@firstname,@lastname,@gender,@age)";
            cmd1.Parameters.AddWithValue("@username", usernamebox.Text.Trim());
            cmd1.Parameters.AddWithValue("@firstname", firstnamebox.Text.Trim());
            cmd1.Parameters.AddWithValue("@lastname", lastnamebox.Text.Trim());
            cmd1.Parameters.AddWithValue("@gender", gender);
            cmd1.Parameters.AddWithValue("@age", num);
            cmd1.Prepare();
            cmd1.ExecuteNonQuery();

            var cmd2 = new SQLiteCommand(SQLiteConnection);
            cmd2.CommandText = "INSERT INTO finance VALUES(@username,@balance)";
            cmd2.Parameters.AddWithValue("@username", usernamebox.Text.Trim());
            cmd2.Parameters.AddWithValue("@balance", 0);
            cmd2.Prepare();
            cmd2.ExecuteNonQuery();

            return true;
            

        }
        private void button1_Click(object sender, EventArgs e)
        {

            bool check = true;
            bool succ = false;
            if(usernamebox.Text.Trim()=="" || passwordbox1.Text.Trim()==""|| passwordbox2.Text.Trim()==""||
                lastnamebox.Text.Trim()=="" || firstnamebox.Text.Trim()==""|| agebox.Text.Trim()=="")
            {
                Msg warning = new Msg("Please fill in all information");
                warning.BringToFront();
                warning.Show();
                check = false;
            }
            if(passwordbox1.Text.Trim() != passwordbox2.Text.Trim())
            {
                Msg warning = new Msg("password does not match");
                warning.BringToFront();
                warning.Show();
                check = false;
            }
            if (check)
            {
                succ=registerprocess();
            }

            if (succ)
            {
                this.Hide();
            }


        }

        private void agebox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }

        private void adminbox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
