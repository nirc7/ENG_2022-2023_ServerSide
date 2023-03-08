using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string fileName = "data.txt";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSayHello_Click(object sender, EventArgs e)
        {
            lblResult.Text = "Hello " + txtName.Text;
            File.AppendAllText(fileName, "Hello " + txtName.Text + "\r\n");

        }

        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            File.Delete(fileName);
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            string[] lines = File.ReadAllLines(fileName);
            string output = "";
            foreach (var line in lines)
            {
                foreach (var word in line.Split(' '))
                {
                    output += word + "\n";
                }
            }
            MessageBox.Show(output);
        }
    }
}
