using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using System.Security.Cryptography;
using System.Net;

namespace hotelManage
{
    public partial class UC_CustomerRes : UserControl
    {
        functionConnect fn = new functionConnect();
        String query;
        // design interface 
        public UC_CustomerRes()
        {
            InitializeComponent();
        }
        public void setComboBox(String query, ComboBox combo)
        {
            SqlDataReader sdr = fn.getForCombo(query);
            while (sdr.Read())
            {
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    combo.Items.Add(sdr.GetString(i));
                }
            }
            sdr.Close();
        }
        //appears the room has not been booked
        private void txtRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRoomNo.Items.Clear();
            query = "select roomNo from rooms where bed = '" + txtBed.Text + "' and roomType = '" + txtRoom.Text + "' and booked = 'NO'";
            setComboBox(query, txtRoomNo);
        }
        private void txtBed_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRoom.SelectedIndex = -1;
            txtRoomNo.Items.Clear();
            txtPrice.Clear();
        }
        int rid;
        // when choose the room has not been booked will appear price and roomid 
        private void txtRoomNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            query = "select price, roomid from rooms where roomNo = '" + txtRoomNo.Text + "'";
            DataSet ds = fn.getData(query);
            txtPrice.Text = ds.Tables[0].Rows[0][0].ToString();
            rid = int.Parse(ds.Tables[0].Rows[0][1].ToString());
        }
        // button book that room
        private void btnAllotCustomer_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtPhoneNumber.Text != ""  && txtGender.Text != ""
                && txtId.Text != "" && txtPassportNumber.Text != "" && txtCheckin.Text != ""
                && txtCheckOut.Text !=" " && txtPrice.Text != "")
            {
                String name = txtName.Text;
                Int64 mobile = Int64.Parse(txtPhoneNumber.Text);
                String passport = txtPassportNumber.Text;
                String gender = txtGender.Text;
                String idproof = txtId.Text;
                String checkin = txtCheckin.Text;
                String checkoutday = txtCheckOut.Text;

                query = "insert into customer (cname, mobile, passport, gender, idproof, checkin,checkoutday, roomid) " +
                    "values ('" + name + "'," + mobile + ", '" + passport + "','" + gender + "','" + idproof + "','" +
                    checkin + "','"+ checkoutday + "'," + rid + ") update rooms set booked = 'YES' where roomNo = '" 
                    + txtRoomNo.Text + "'";
                fn.setData(query, " Room number " + txtRoomNo.Text + "Registered sucessfully!!!.");
                clearAll();
            }
            else
            {
                MessageBox.Show("Please fill all the information!!!.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
         
        }
        // clear when out 
        public void clearAll()
        {
            txtName.Clear();
            txtPhoneNumber.Clear();
            txtGender.SelectedIndex = -1;
            txtPassportNumber.Clear();
            txtId.Clear();
            txtCheckin.ResetText();
            txtBed.SelectedIndex = -1;
            txtRoom.SelectedIndex = -1;
            txtRoomNo.Items.Clear();
            txtPrice.Clear();
        }
        private void UC_CustomerRes_Leave(object sender, EventArgs e)
        {
            clearAll();
        }
    }
}
