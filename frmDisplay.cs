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

namespace BGarciaACP2_2
{
    public partial class frmDisplay : Form
    {
        public frmDisplay()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDisplay_Load(object sender, EventArgs e)
        {
            SQLiteDataReader dr;

            SQLiteCommand cmd;

            cmd = frmMain.sqlite_conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM sports";

            dr = cmd.ExecuteReader();

            lbxTeams.Items.Add("ID\tTeam\tPCT\tPA\tPF\tWins\tLoses");

            while (dr.Read())
            {
                frmMain.myInfo = dr.GetInt32(0) + "\t" + dr.GetString(1) + "\t" + dr.GetDecimal(2) + "\t" + dr.GetInt32(3) + "\t" + dr.GetInt32(4) +"\t"+ dr.GetInt32(5) +"\t" +dr.GetInt32(6);
                lbxTeams.Items.Add(frmMain.myInfo);
            }
        }
    }
}
