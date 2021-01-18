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
    public partial class OperatorForm : Form
    {

        private string username;
        private bool p;
        private static string connstr = ConfigService.ConnectionString;
        private SQLiteConnection SQLiteConnection;
        private SQLiteDataAdapter adp;
        private DataTable dt;
        private int bikeid = -1;
        private PointLatLng point = new PointLatLng(0,0);

        public OperatorForm(string str, bool proxy)
        {
            InitializeComponent();
            this.username = str;
            this.p = proxy;
            this.SQLiteConnection = new SQLiteConnection(connstr);
            this.SQLiteConnection.Open();
            this.dt = new DataTable();
            this.adp = new SQLiteDataAdapter("SELECT * from bike", this.SQLiteConnection);
            this.adp.Fill(this.dt);
            this.dataGridView1.DataSource = dt;
            this.dataGridView1.MultiSelect = false;
        }

        private void proxy(bool flag)
        {
            if (flag)
            {
                GMap.NET.MapProviders.GMapProvider.IsSocksProxy = true;
                GMap.NET.MapProviders.GMapProvider.WebProxy = new System.Net.WebProxy("127.0.0.1", 1080);
            }
            else
            {
                GMap.NET.MapProviders.GMapProvider.IsSocksProxy = false;
            }
        }

        private void OperatorForm_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("exit?", "system", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                e.Cancel = false;
                System.Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {
            this.proxy(this.p);
            this.gMapControl1.MapProvider = GMap.NET.MapProviders.GMapProviders.GoogleMap;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            this.gMapControl1.Position = new PointLatLng(55.8642, -4.2518);
            this.gMapControl1.DragButton = MouseButtons.Left;
            this.gMapControl1.ShowCenter = false;
        }

        private bool trackprocess()
        {
            if (bikeid == -1)
            {
                return false;
            }

            this.gMapControl1.Overlays.Clear();
            var cmd = new SQLiteCommand(this.SQLiteConnection);
            cmd.CommandText = "SELECT * FROM bike WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", bikeid);
            SQLiteDataReader rdr = cmd.ExecuteReader();
            double la = 0;
            double lo = 0;
            while (rdr.Read())
            {
                la = rdr.GetDouble(2);
                lo = rdr.GetDouble(3);
            }

            

            GMapOverlay markers = new GMapOverlay("markers");
            GMapMarker marker = new GMarkerGoogle(new PointLatLng(la, lo), GMarkerGoogleType.blue_pushpin);
            markers.Markers.Add(marker);
            this.gMapControl1.Overlays.Add(markers);
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!trackprocess())
            {
                Msg msg = new Msg("select bike to track first");
                msg.BringToFront();
                this.TopMost = false;
                msg.Show();
            }
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = dataGridView1.CurrentRow.Index;
            this.bikeid = Int32.Parse(dataGridView1.Rows[rowindex].Cells[0].Value.ToString());
            
        }

        private int moveprocess()
        {
            if (bikeid == -1)
            {
                return -1;
            }
            if (this.point == new PointLatLng(0, 0))
            {
                return 0;
            }
            var cmd = new SQLiteCommand(this.SQLiteConnection);
            cmd.CommandText = "SELECT * FROM bike WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", bikeid);
            SQLiteDataReader rdr = cmd.ExecuteReader();
            double la = 0;
            double lo = 0;
            while (rdr.Read())
            {
                la = rdr.GetDouble(2);
                lo = rdr.GetDouble(3);
            }
            
            var cmd1 = new SQLiteCommand(SQLiteConnection);
            cmd1.CommandText = "UPDATE bike SET Latitude=@Latitude, Longitude=@Longitude WHERE id=@id";
            cmd1.Parameters.AddWithValue("@id", this.bikeid);
            cmd1.Parameters.AddWithValue("@Latitude", this.point.Lat);
            cmd1.Parameters.AddWithValue("@Longitude", this.point.Lng);
            cmd1.ExecuteNonQuery();




            return 1;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string str = "";
            switch (moveprocess())
            {
                case 1:
                    str= "move successfully";
                    break;

                case 0:
                    str = "click the map to select move point";
                    break;

                case -1:
                    str= "select bike to track first";
                    break;
            }
            Msg msg = new Msg(str);
            msg.BringToFront();
            this.TopMost = false;
            msg.Show();

            dt.Clear();
            this.adp = new SQLiteDataAdapter("SELECT * from bike", this.SQLiteConnection);
            this.adp.Fill(this.dt);
        }

        private void gMapControl1_MouseClick(object sender, MouseEventArgs e)
        {
            point = this.gMapControl1.FromLocalToLatLng(e.X, e.Y);
        }

        private bool repairprocess()
        {
            if (bikeid == -1)
            {
                return false;
            }
            var cmd = new SQLiteCommand(this.SQLiteConnection);
            cmd.CommandText = "UPDATE bike SET dmg=@dmg WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", this.bikeid);
            cmd.Parameters.AddWithValue("@dmg", 0);
            cmd.ExecuteNonQuery();

            return true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string str = "";
            if (repairprocess())
            {
                str = "repair successfully";
            }
            else
            {
                str = "select bike to repair first";
            }
            Msg msg = new Msg(str);
            msg.BringToFront();
            this.TopMost = false;
            msg.Show();

            dt.Clear();
            this.adp = new SQLiteDataAdapter("SELECT * from bike", this.SQLiteConnection);
            this.adp.Fill(this.dt);
        }
    }
}
