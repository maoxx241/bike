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
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace bike
{
    public partial class MainForm : Form
    {
        private string username;
        private bool p;
        private static string connstr = ConfigService.ConnectionString;
        private SQLiteConnection SQLiteConnection;
        private SQLiteDataAdapter adp;
        private DataTable dt;
        private int bikeid=-1;
        private PointLatLng point= new PointLatLng(0,0);

        public MainForm(string user,bool proxy)
        {
            InitializeComponent();
            this.username = user;
            this.p = proxy;
            this.SQLiteConnection = new SQLiteConnection(connstr);
            this.SQLiteConnection.Open();
            this.dt = new DataTable();
            this.adp = new SQLiteDataAdapter("SELECT * from bike Where work=0 and dmg=0", this.SQLiteConnection);
            this.adp.Fill(this.dt);
            this.dataGridView1.DataSource = dt;
            this.dataGridView1.MultiSelect = false;

        }

        private void proxy(bool flag)
        {
            if(flag){
                GMap.NET.MapProviders.GMapProvider.IsSocksProxy = true;
                GMap.NET.MapProviders.GMapProvider.WebProxy = new System.Net.WebProxy("127.0.0.1", 1080);
            }
            else
            {
                GMap.NET.MapProviders.GMapProvider.IsSocksProxy = false;
            }
        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {
            this.proxy(this.p);
            this.gMapControl1.MapProvider = GMap.NET.MapProviders.GMapProviders.GoogleMap;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            this.gMapControl1.Position = new GMap.NET.PointLatLng(55.8642, -4.2518);
            this.gMapControl1.DragButton = MouseButtons.Left;
            this.gMapControl1.ShowCenter = false;

        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("exit?","system", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                e.Cancel = false;
                System.Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private int returnprocess()
        {
            if(point==new PointLatLng(0, 0))
            {
                return -1;
            }
            
            try
            {
                var cmd = new SQLiteCommand(this.SQLiteConnection);
                cmd.CommandText = "SELECT * FROM rent WHERE username=@username";
                cmd.Parameters.AddWithValue("@username", username);
                SQLiteDataReader rdr = cmd.ExecuteReader();
                
                if (!rdr.HasRows)
                {
                    return 0;
                }

                while (rdr.Read())
                {
                    string us = rdr.GetString(0);
                    int id = rdr.GetInt32(1);
                    double la = rdr.GetDouble(2);
                    double lo = rdr.GetDouble(3);
                    string time = rdr.GetString(4);
                    DateTime timebefore = Convert.ToDateTime(time);
                    DateTime timeafter = DateTime.Now;
                    TimeSpan span = timeafter - timebefore;
                    double sec = (double)span.TotalMinutes;

                    var cmd3 = new SQLiteCommand(this.SQLiteConnection);
                    cmd3.CommandText = "SELECT * FROM finance WHERE username=@username";
                    cmd3.Parameters.AddWithValue("@username", username);
                    SQLiteDataReader rdr2 = cmd3.ExecuteReader();
                    while (rdr2.Read())
                    {
                        double bl = rdr2.GetDouble(1);
                        if (bl < sec)
                        {
                            return 2;
                        }
                    }


                    var cmd1 = new SQLiteCommand(this.SQLiteConnection);
                    cmd1.CommandText = "INSERT INTO history VALUES(null,@username,@bikeid,@Latitude_0,@Longitude_0,@Latitude_1,@Longitude_1,@time,@duration)";
                    cmd1.Parameters.AddWithValue("@username", username);
                    cmd1.Parameters.AddWithValue("@bikeid", id);
                    cmd1.Parameters.AddWithValue("@Latitude_0", la);
                    cmd1.Parameters.AddWithValue("@Longitude_0", lo);
                    cmd1.Parameters.AddWithValue("@Latitude_1", point.Lat);
                    cmd1.Parameters.AddWithValue("@Longitude_1", point.Lng);
                    cmd1.Parameters.AddWithValue("@time", timeafter.ToString());
                    cmd1.Parameters.AddWithValue("@duration", sec);
                    cmd1.Prepare();
                    cmd1.ExecuteNonQuery();

                    var cmd2 = new SQLiteCommand(this.SQLiteConnection);
                    cmd1.CommandText = "DELETE FROM rent WHERE username=@username";
                    cmd1.Parameters.AddWithValue("@username", username);
                    cmd1.Prepare();
                    cmd1.ExecuteNonQuery();

                    var cmd4 = new SQLiteCommand(this.SQLiteConnection);
                    cmd4.CommandText = "UPDATE bike SET Latitude=@Latitude,Longitude=@Longitude,work=@work WHERE id=@id";
                    cmd4.Parameters.AddWithValue("@id", this.bikeid);
                    cmd4.Parameters.AddWithValue("@Latitude", point.Lat);
                    cmd4.Parameters.AddWithValue("@Longitude", point.Lng);
                    cmd4.Parameters.AddWithValue("@work", 0);
                    cmd4.ExecuteNonQuery();



                }

            }
            catch
            {
                return 0;
            }
            return 1;
        }

        //return button
        private void button1_Click(object sender, EventArgs e)
        {
            string str="";
            switch (returnprocess())
            {
                case -1:
                    str = "click map to select a point to return";
                    break;
                case 1:
                    str="return successfully";
                    break;
                case 0:
                    str="you need rent first";
                    break;
                case 2:
                    str = "please recharge";
                    break;

            }

            Msg msg = new Msg(str);
            msg.BringToFront();
            this.TopMost = false;
            msg.Show();

            dt.Clear();
            this.adp = new SQLiteDataAdapter("SELECT * from bike Where work=0 and dmg=0", this.SQLiteConnection);
            this.adp.Fill(this.dt);
        }


        
        //recharge button
        private void button3_Click(object sender, EventArgs e)
        {
            Recharge recharge = new Recharge(this.username);
            recharge.BringToFront();
            recharge.Activate();
            recharge.Show();
        }

        private int rentprocess()
        {
            //if(dataGridView1)
            try
            {
                var cmd = new SQLiteCommand(this.SQLiteConnection);
                cmd.CommandText = "SELECT * FROM rent WHERE username=@username";
                cmd.Parameters.AddWithValue("@username", username);
                SQLiteDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    return 0;
                }

                if (bikeid==-1)
                {
                    return -1;
                }
                var cmd1 = new SQLiteCommand(this.SQLiteConnection);
                cmd1.CommandText = "UPDATE bike SET work=@work WHERE id=@id";
                cmd1.Parameters.AddWithValue("@id", this.bikeid);
                cmd1.CommandText = "SELECT * FROM bike WHERE id=@id";
                cmd1.Parameters.AddWithValue("@id", bikeid);
                SQLiteDataReader rdr2 = cmd1.ExecuteReader();
                double la = 0;
                double lo = 0;
                while (rdr.Read())
                {
                    la = rdr.GetDouble(2);
                    lo = rdr.GetDouble(3);
                }

                var cmd2 = new SQLiteCommand(SQLiteConnection);
                cmd2.CommandText = "INSERT INTO rent VALUES(@username,@bikeid,@Latitude,@Longitude,@time)";
                cmd2.Parameters.AddWithValue("@username", this.username);
                cmd2.Parameters.AddWithValue("@bikeid", bikeid);
                cmd2.Parameters.AddWithValue("@Latitude", la);
                cmd2.Parameters.AddWithValue("@Longitude", lo);
                cmd2.Parameters.AddWithValue("@time", DateTime.Now.ToString());
                cmd2.Prepare();
                cmd2.ExecuteNonQuery();

                var cmd4 = new SQLiteCommand(this.SQLiteConnection);
                cmd4.CommandText = "UPDATE bike SET work=@work WHERE id=@id";
                cmd4.Parameters.AddWithValue("@id", this.bikeid);
                cmd4.Parameters.AddWithValue("@work", 1);
                cmd4.ExecuteNonQuery();


            }
            catch
            {

            }
            return 1;
        }

        //rent button
        private void button4_Click(object sender, EventArgs e)
        {
            string str = "";
            switch (rentprocess())
            {
                case 1:
                    str="rent successfully";
                    break;
                case 0:
                    str = "you cannot rent two bike";
                    break;
                case -1:
                    str = "select bike to rent first";
                    break;
            }

            Msg msg = new Msg(str);
            msg.BringToFront();
            this.TopMost = false;
            msg.Show();

            dt.Clear();
            this.adp = new SQLiteDataAdapter("SELECT * from bike Where work=0 and dmg=0", this.SQLiteConnection);
            this.adp.Fill(this.dt);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = dataGridView1.CurrentRow.Index;
            this.bikeid = Int32.Parse(dataGridView1.Rows[rowindex].Cells[0].Value.ToString());

        }

        private int reportprocess()
        {
            try
            {
                if (this.bikeid == -1)
                {
                    return -1;
                }
                var cmd = new SQLiteCommand(this.SQLiteConnection);
                cmd.CommandText = "UPDATE bike SET dmg=@dmg WHERE id=@id";
                cmd.Parameters.AddWithValue("@id", this.bikeid);
                cmd.Parameters.AddWithValue("@dmg", 1);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                return 0;
            }
            

            return 1;
        }

        //report button
        private void button2_Click(object sender, EventArgs e)
        {
            string str="";
            switch (reportprocess())
            {
                case 1:
                    str = "report successfully";
                    break;
                case 0:
                    str = "report failed";
                    break;
                case -1:
                    str = "select bike to report first";
                    break;
            }

            Msg msg = new Msg(str);
            msg.BringToFront();
            this.TopMost = false;
            msg.Show();

            dt.Clear();
            this.adp = new SQLiteDataAdapter("SELECT * from bike Where work=0 and dmg=0", this.SQLiteConnection);
            this.adp.Fill(this.dt);
        }

        private void gMapControl1_MouseClick(object sender, MouseEventArgs e)
        {
            point = this.gMapControl1.FromLocalToLatLng(e.X, e.Y);
        }
    }
}
