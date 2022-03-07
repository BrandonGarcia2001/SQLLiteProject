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
using System.IO;

namespace BGarciaACP2_2
{

    public partial class frmMain : Form
    {
        public static SQLiteConnection sqlite_conn;
        public static string myInfo;
        static string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "teams.db");
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            frmSplash frmSplash = new frmSplash();
            frmSplash.ShowDialog();

            sqlite_conn = CreateConnection();
        }

        private void btnCreateTable_Click(object sender, EventArgs e)
        {
            try
            {
                SQLiteCommand cmd;
                cmd = sqlite_conn.CreateCommand();

                cmd.CommandText = "DROP TABLE IF EXISTS sports";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE TABLE sports(TeamID INTEGER PRIMARY KEY AUTOINCREMENT, Team TEXT, PCT REAL , PA INTEGER, PF INTEGER, Wins INTEGER, Loses INTEGER)";

                cmd.ExecuteNonQuery();

                MessageBox.Show("Table Created");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        static SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqlite_conn;

            sqlite_conn = new SQLiteConnection("Data Source="+path);

            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return sqlite_conn;
        }

        private void btnInsertData_Click(object sender, EventArgs e)
        {
            SQLiteCommand cmd;

            cmd = sqlite_conn.CreateCommand();

            // 1
            cmd.CommandText = "INSERT INTO sports(Team,PCT,PA,PF,Wins,Loses) VALUES ('Bills',.647,289,483,11,6)";
            cmd.ExecuteNonQuery();
            // 2
            cmd.CommandText = "INSERT INTO sports(Team,PCT,PA,PF,Wins,Loses) VALUES ('Patriots',.588,303,362,10,7)";
            cmd.ExecuteNonQuery();
            // 3
            cmd.CommandText = "INSERT INTO sports(Team,PCT,PA,PF,Wins,Loses) VALUES ('Dolphins',.529,373,341,9,8)";
            cmd.ExecuteNonQuery();
            // 4
            cmd.CommandText = "INSERT INTO sports(Team,PCT,PA,PF,Wins,Loses) VALUES ('Jets',.235,504,310,4,13)";
            cmd.ExecuteNonQuery();
            // 5
            cmd.CommandText = "INSERT INTO sports(Team,PCT,PA,PF,Wins,Loses) VALUES ('Chiefs',.706,364,480,12,5)";
            cmd.ExecuteNonQuery();
            // 6
            cmd.CommandText = "INSERT INTO sports(Team,PCT,PA,PF,Wins,Loses) VALUES ('Raiders',.588,439,374,10,7)";
            cmd.ExecuteNonQuery();
            // 7
            cmd.CommandText = "INSERT INTO sports(Team,PCT,PA,PF,Wins,Loses) VALUES ('Chargers',.529,459,474,9,8)";
            cmd.ExecuteNonQuery();
            // 8
            cmd.CommandText = "INSERT INTO sports(Team,PCT,PA,PF,Wins,Loses) VALUES ('Broncos',.412,322,335,7,10)";
            cmd.ExecuteNonQuery();
            // 9
            cmd.CommandText = "INSERT INTO sports(Team,PCT,PA,PF,Wins,Loses) VALUES ('Bengals',.588,376,460,10,7)";
            cmd.ExecuteNonQuery();
            // 10
            cmd.CommandText = "INSERT INTO sports(Team,PCT,PA,PF,Wins,Loses) VALUES ('Steelers',.559,398,343,9,7)";
            cmd.ExecuteNonQuery();
            // 11
            cmd.CommandText = "INSERT INTO sports(Team,PCT,PA,PF,Wins,Loses) VALUES ('Browns',.471,371,349,8,9)";
            cmd.ExecuteNonQuery();
            // 12
            cmd.CommandText = "INSERT INTO sports(Team,PCT,PA,PF,Wins,Loses) VALUES ('Ravens',.471,392,387,8,9)";
            cmd.ExecuteNonQuery();
            // 13
            cmd.CommandText = "INSERT INTO sports(Team,PCT,PA,PF,Wins,Loses) VALUES ('Titans',.706,354,419,12,5)";
            cmd.ExecuteNonQuery();
            // 14
            cmd.CommandText = "INSERT INTO sports(Team,PCT,PA,PF,Wins,Loses) VALUES ('Colts',.529,365,451,9,8)";
            cmd.ExecuteNonQuery();
            // 15
            cmd.CommandText = "INSERT INTO sports(Team,PCT,PA,PF,Wins,Loses) VALUES ('Texans',.235,452,280,3,14)";
            cmd.ExecuteNonQuery();

            MessageBox.Show("Data Inserted");
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            frmDisplay frmDisplay = new frmDisplay();
            frmDisplay.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            frmAbout frmAbout = new frmAbout();
            frmAbout.ShowDialog();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            PrintReport(GenerateReport());
        }
        private StringBuilder GenerateReport()
        {
            StringBuilder html = new StringBuilder();
            StringBuilder css = new StringBuilder();

            css.Append("<style>");
            css.Append("td {padding:5px;text-align:center;font-weight:bold;text-align-center;font-family: Times New Roman, Times, serif;}");
            css.Append("h1{color: red;font-family: Times New Roman, Times, serif;}");
            css.Append("</style>");

            html.Append("<html");
            html.Append("<a href=#><img id=logo width=100 height=100 src=https://upload.wikimedia.org/wikipedia/en/thumb/a/a2/National_Football_League_logo.svg/800px-National_Football_League_logo.svg.png></a>");
            html.Append($"<head>{css}<title>{"Sport Teams"}</title></head>");
            html.Append("<body>");
            html.Append($"<h1>{"Sports Teams"}</h1>");

            

            SQLiteDataReader dr;

            SQLiteCommand cmd;

            cmd = sqlite_conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM sports";

            dr = cmd.ExecuteReader();


            html.Append("<table cellspacing=25>");


            html.Append("<tr><td colspan=10><hr/></td></tr>");

            html.Append("<style>");
            html.Append("th,{border: 1px solid black;}");
            html.Append("</style>");

            html.Append("<tr>");
            html.Append("<th><u> ID </u></th>");
            html.Append("<th><u> Team </u></th>");
            html.Append("<th><u> PCT </u></th>");
            html.Append("<th><u> PA </u></th>");
            html.Append("<th><u> PF </u></th>");
            html.Append("<th><u> Wins </u></th>");
            html.Append("<th><u> Loses </u></th>");
            html.Append("</tr>");


            html.Append("<tr>");

            while (dr.Read())
            {
                frmMain.myInfo = dr.GetInt32(0) + "\t" + dr.GetString(1) + "\t" + dr.GetDecimal(2) + "\t" + dr.GetInt32(3) + "\t" + dr.GetInt32(4) + "\t" + dr.GetInt32(5) + "\t" + dr.GetInt32(6);
                html.Append($"<td>{dr.GetInt32(0)}</td>");
                html.Append($"<td>{dr.GetString(1)}</td>");
                html.Append($"<td>{dr.GetDecimal(2)}</td>");
                html.Append($"<td>{dr.GetInt32(3)}</td>");
                html.Append($"<td>{dr.GetInt32(4)}</td>");
                html.Append($"<td>{dr.GetInt32(5)}</td>");
                html.Append($"<td>{dr.GetInt32(6)}</td>");

                html.Append("</tr>");
            }
            html.Append("<tr><td colspan=10><hr/></td></tr>");
            html.Append("</table>");

            html.Append("<b>Created By: Brandon Garcia</b>");
            html.Append("</body></html>");

            return html;
        }

        private void PrintReport(StringBuilder html)
        {
            try
            {
                using (StreamWriter wr = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Report.html"))
                {
                    wr.WriteLine(html);
                }
                System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Report.html");

            }
            catch (Exception)
            {
                MessageBox.Show("You dont have write privileges", "Error System", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //DateTime today = DateTime.Now;

            //using (StreamWriter wr = new StreamWriter($"{today.ToString("yy-MM-dd-HHmmss")} - Report.html"))
            //{
            //    wr.WriteLine(html);
            //}

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            sqlite_conn.Close();
        }
    }
}
