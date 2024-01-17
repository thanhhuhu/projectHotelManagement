using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hotelManage
{
    public partial class UC_CheckOut : UserControl
    {
        functionConnect fn = new functionConnect();
        String query;
        public UC_CheckOut()
        {
            InitializeComponent();
        }
        // choose the customer has been not checked out 
        private void UC_CheckOut_Load(object sender, EventArgs e)
        {
            query = "SELECT customer.cid, customer.cname, customer.mobile," +
                " customer.passport, customer.gender, customer.idproof, " +
                "customer.checkin, rooms.roomNo, rooms.roomType, rooms.bed, " +
                "rooms.price FROM customer INNER JOIN rooms ON customer.roomid = rooms.roomid" +
                " WHERE chekout = 'NO'";
            DataSet ds = fn.getData(query);
            guna2DataGridView1.DataSource = ds.Tables[0];
        }
        // when click the name on dataGridView will appear the Name customer on the txtName
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            query = "SELECT customer.cid, customer.cname, customer.mobile, " +
                "customer.passport, customer.gender, customer.idproof, " +
                "customer.checkin, rooms.roomNo, rooms.roomType, rooms.bed, rooms.price" +
                " FROM customer INNER JOIN rooms ON customer.roomid = rooms.roomid WHERE cname LIKE '" 
                + txtName.Text + "%' AND chekout = 'NO'";
            DataSet ds = fn.getData(query);
            guna2DataGridView1.DataSource = ds.Tables[0];
        }
        int id;
        // when click the name on dataGridView
        private void guna2DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value 
                != null)
            {
                id = int.Parse(guna2DataGridView1.Rows[e.RowIndex].Cells["cid"].Value.ToString());
                txtCName.Text = guna2DataGridView1.Rows[e.RowIndex].Cells["cname"].Value.ToString();
                txtRoom.Text = guna2DataGridView1.Rows[e.RowIndex].Cells["roomNo"].Value.ToString();
            }
        }
        // button check out room and message box
        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            if (txtCName.Text != "")
            {
                if (MessageBox.Show("Are you sure?", "Confirmed!!",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    String cdate = txtCheckOutDate.Text;

                    // Update customer table
                    string customerUpdateQuery = $"UPDATE customer SET chekout = 'YES'," +
                        $" checkout = '{cdate}' WHERE cid = {id}";
                    fn.setData(customerUpdateQuery, "Successfully checked out customer");

                    // Update rooms table
                    string roomsUpdateQuery = $"UPDATE rooms SET booked = 'NO'" +
                        $" WHERE roomNo = '{txtRoom.Text}'";
                    fn.setData(roomsUpdateQuery, "Successfully updated room status");

                    UC_CheckOut_Load(this, null);
                    clearAll();
                }
            }
            else
            {
                MessageBox.Show("No customer to choose", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        // clear when out 
        public void clearAll()
        {
            txtCName.Clear();
            txtName.Clear();
            txtRoom.Clear();
            txtCheckOutDate.ResetText();
        }

        private void UC_CheckOut_Leave(object sender, EventArgs e)
        {
            clearAll();
        }
    }
}
