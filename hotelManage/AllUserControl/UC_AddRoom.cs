﻿using System;
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

    public partial class UC_AddRoom : UserControl
    {
        functionConnect fn = new functionConnect();
        String query;
        public UC_AddRoom()
        {
            InitializeComponent();
        }
        private void UC_AddRoom_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            // add data in dataGridView1
            try
            {
                query = "select * from rooms";
                DataSet ds = fn.getData(query);
                dataGridView1.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Exception",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            // button create room 
            if (txtRoomNo.Text != "" 
                && txtRoomType.Text != "" 
                && txtBed.Text != ""
                && txtPrice.Text != "")
            {

                String roomno = txtRoomNo.Text;
                String type = txtRoomType.Text;
                String bed = txtBed.Text;
                Int64 price = Int64.Parse(txtPrice.Text);

                query = "insert into rooms (roomNo, roomType, bed, price) values" +
                    " ('" + roomno + "','" + type + "','" + bed + "', " + price + ")";

                fn.setData(query, "Room added!!!");
                LoadData();
                clearAll();
            }

            else
            {
                MessageBox.Show("Please fill out all information!!!", "Warning !", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void clearAll()
        {
            // will clear all when out 
            txtRoomNo.Clear();
            txtRoomType.SelectedIndex = -1;
            txtBed.SelectedIndex = -1;
            txtPrice.Clear();
        }

        private void UC_AddRoom_Leave(object sender, EventArgs e)
        {
            clearAll();
        }

        private void UC_AddRoom_Enter(object sender, EventArgs e)
        {
            UC_AddRoom_Load(this, null);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
