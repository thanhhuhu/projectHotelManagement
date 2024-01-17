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

namespace hotelManage
{
    public partial class Form1 : Form
    {
        functionConnect fn = new functionConnect();
        String query;
        // design form interface
        public Form1()
        {
            InitializeComponent();
        }
        // button login

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //use username and paswword of employees
            query = "select username, pass from employee where username = '" 
                + txtUsername.Text + "' and pass = '"
                + txtPassword.Text + "'";

            DataSet ds = fn.getData(query);
            if (txtUsername.Text == "1" && txtPassword.Text == "1")
            {
                labelError.Visible = false;
                dashboard dash = new dashboard();
                this.Hide();
                dash.Show();
            }
            else if (ds.Tables[0].Rows.Count != 0)
            {
                labelError.Visible = false;
                dashboard dash = new dashboard();
                this.Hide();
                dash.Show();
            }
            else
            {
                // clear Username and Password when type wrong 
                labelError.Visible = true;
                txtUsername.Clear();
                txtPassword.Clear();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

