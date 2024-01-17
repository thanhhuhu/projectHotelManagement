using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hotelManage.AllUserControl
{
    public partial class UC_Employee : UserControl
    {
        functionConnect fn = new functionConnect();
        String query;
        //design interface
        public UC_Employee()
        {
            InitializeComponent();
        }

        private void Employee_Load(object sender, EventArgs e)
        {
            getMaxId();
        }
        // get number of employees
        public void getMaxId()
        {
            query = "select max(eid) from employee";
            DataSet ds = fn.getData(query);
            if (ds.Tables[0].Rows[0][0].ToString() != "")
            {
                Int64 num = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
                labelToSET.Text = (num + 1).ToString();
            }
        }
        // button registration employees
        private void btnRegistaion_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtMobile.Text != "" && txtGender.Text != ""
                && txtPassportNo.Text != "" && txtUsername.Text != "" 
                && txtPassword.Text != "")
            {
                String name = txtName.Text;
                Int64 mobile = Int64.Parse(txtMobile.Text);
                String passport = txtPassportNo.Text;
                String gender = txtGender.Text;
                String username = txtUsername.Text;
                String pass = txtPassword.Text;


                query = "insert into employee (ename, mobile, gender, passport, username, pass) " +
                    "values ('" + name + "', " + mobile + ", '" + gender + "','" + passport + "','" 
                    + username + "','" + pass + "')";
                fn.setData(query, "Registered successfully!!");


                clearAll();
                getMaxId();
            }
        }
        // clear when out
        public void clearAll()
        {
            txtName.Clear();
            txtMobile.Clear();
            txtGender.SelectedIndex = -1;
            txtPassportNo.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
        }
        private void tabEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabEmployee.SelectedIndex == 1)
            {
                setEmployee(guna2DataGridView1);
            }
            else if (tabEmployee.SelectedIndex == 2)
            {
                setEmployee(guna2DataGridView2);
            }
        }
        // details employee
        public void setEmployee(DataGridView dgv)
        {
            query = "select * from employee";
            DataSet ds = fn.getData(query);
            dgv.DataSource = ds.Tables[0];
        }
       
        private void UC_Employee_Leave(object sender, EventArgs e)
        {
            clearAll();
        }
        // button delete employee
        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            if (guna2DataGridView2.Rows.Count == 0)
            {
                MessageBox.Show("No data can be deleted ", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            DialogResult dr = MessageBox.Show("Are you sure deleting this employee ?",
                "Confirmed", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                string deleteQuery = "DELETE employee WHERE eid = '" 
                    + guna2DataGridView2.Rows[guna2DataGridView2.CurrentCell.RowIndex].Cells[0]
                    .Value.ToString() + "'";

                fn.setData(deleteQuery, "deleteed successfully.");

                
                query = "select * from employee";
                DataSet ds = fn.getData(query);
                guna2DataGridView2.DataSource = ds.Tables[0];
            }
            else
                return;
        }
  
    }
}
