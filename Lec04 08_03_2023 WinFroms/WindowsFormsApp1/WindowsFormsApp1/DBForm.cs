using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class DBForm : Form
    {
        public DBForm()
        {
            InitializeComponent();
        }

        private void DBForm_Load(object sender, EventArgs e)
        {

        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            MessageBox.Show((((Button)sender).Name == "button1").ToString());
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            ExcNonQ("INSERT INTO TBUsers(Name, Family) VALUES('" +
                txtName.Text + $"', '{txtFamily.Text}')");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ExcNonQ("DELETE TBUsers WHERE Id=" + txtId.Text);
        }

        private int ExcNonQ(string commTxt)
        {
            string conStr = @"Data Source=LAB-G700;Initial Catalog=DBUsers;Integrated Security=True";
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand comm = new SqlCommand(commTxt, con);

            con.Open();
            int res = comm.ExecuteNonQuery();
            con.Close();

            if (res == 1)
            {
                MessageBox.Show("successfully ended!");
            }
            else
            {
                MessageBox.Show(":(");
            }

            RefreshTableLbl();

            return res;
        }

        private void btnUdapte_Click(object sender, EventArgs e)
        {
            ExcNonQ(
                $" UPDATE TBUsers " +
                $" Set Name='{txtName.Text}', Family='{txtFamily.Text}'" +
                $" WHERE Id={txtId.Text}");
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            RefreshTableLbl();
        }

        private void RefreshTableLbl()
        {
            string conStr = @"Data Source=LAB-G700;Initial Catalog=DBUsers;Integrated Security=True";
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand comm = new SqlCommand(
                " SELECT * " +
                " FROM TBUsers " +
                " ORDER BY Name", con);

            con.Open();
            SqlDataReader reader = comm.ExecuteReader();
            string output = "ID -- Name -- Family\n";
            while (reader.Read())
            {
                output += reader["ID"].ToString() + " -- " + reader["Name"] + " -- " + reader["Family"] + "\n";
            }
            con.Close();

            lblTable.Text = output;
        }
    }
}
