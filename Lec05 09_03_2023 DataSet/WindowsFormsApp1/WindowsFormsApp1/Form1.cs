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
    public partial class Form1 : Form
    {
        string conStr = @"Data Source=LAB-G700;Initial Catalog=DBUsers;Integrated Security=True";
        SqlConnection con = null;
        SqlDataAdapter adtr = null;

        DataSet ds = new DataSet();

        DataTable users = null;

        public Form1()
        {
            InitializeComponent();
            con = new SqlConnection(conStr);
            adtr = new SqlDataAdapter(
            " SELECT *" +
            " FROM TBUsers" +
            " Order By Name desc", con);

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            ds.Clear();
            adtr.Fill(ds, "T1");
            lblRes.Text = ds.Tables["T1"].Rows[0]["Name"].ToString();
            users = ds.Tables["T1"];

            dataGridView1.DataSource = users;

            //string output = "";
            //foreach (DataRow row in users.Rows)
            //{
            //    output += row["ID"] + "--" + row["Name"] + "--" + row["Family"] + "\n";
            //}
            //lblRes.Text = output;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            DataRow dr = users.NewRow();

            dr["ID"] = 7;
            dr["Name"] = txtName.Text;
            dr["Family"] = txtFamily.Text;

            users.Rows.Add(dr);   
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            foreach (DataRow row in users.Rows)
            {
                if (row.RowState != DataRowState.Deleted &&  ((int)row["ID"]).ToString() == txtId.Text)
                {
                    row["Name"] = txtName.Text;
                    row["Family"] = txtFamily.Text;
                }
            }
        }

        private void btnUpdateDB_Click(object sender, EventArgs e)
        {
            new SqlCommandBuilder(adtr);
            adtr.Update(ds, "T1");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (DataRow row in users.Rows)
            {
                if (((int)row["ID"]).ToString() == txtId.Text)
                {
                    row.Delete();
                }
            }
        }
    }
}
