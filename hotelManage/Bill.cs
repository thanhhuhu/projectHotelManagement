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
    public partial class Bill : Form
    {
        functionConnect fn = new functionConnect();
        String query;
        public Bill()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            query = "Select CustomerName, RoomID, BookingDate, ExpectedCheckOutDate, ActualCheckOutDate, RoomPrice, LateCheckoutPenalty ,TotalPrice from Receipts";
            var data = fn.getData(query);
            guna2DataGridView1.DataSource = data.Tables[0];
        }

        private void Bill_Load(object sender, EventArgs e)
        {

        }
    }
}