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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Register reg = new Register();
            reg.BringToFront();
            reg.Show();
        }

        private static string connstr = ConfigService.ConnectionString;
        private int loginprocess()
        {
            var SQLiteConnection = new SQLiteConnection(connstr);
            SQLiteConnection.Open();
            try
            {
                var cmd = new SQLiteCommand(SQLiteConnection);
                cmd.CommandText = "SELECT * FROM person WHERE username=@username";
                cmd.Parameters.AddWithValue("@username", usernamebox.Text.Trim());
                SQLiteDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    string us = rdr.GetString(0);
                    string pw = rdr.GetString(1);
                    int op = rdr.GetInt32(2);

                    if (us == usernamebox.Text.Trim() && pw == passwordbox.Text.Trim() && op == 1 && checkBox2.Checked)
                    {
                        return 2;
                    }

                    if (us==usernamebox.Text.Trim() && pw == passwordbox.Text.Trim())
                    {
                        return 1;
                    }
                    

                }
            }
            catch
            {

            }
            return 0;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            switch (loginprocess())
            {
                case 0:
                    Msg warning = new Msg("Wrong username or password");
                    warning.BringToFront();
                    warning.Show();
                    break;

                case 1:
                    MainForm mf = new MainForm(usernamebox.Text.Trim(), checkBox1.Checked);
                    mf.BringToFront();
                    mf.Show();
                    this.Hide();
                    break;

                case 2:
                    OperatorForm operatorForm = new OperatorForm(usernamebox.Text.Trim(), checkBox1.Checked);
                    operatorForm.BringToFront();
                    operatorForm.Show();
                    this.Hide();
                    break;
            }
            
            
        }

    }
}
