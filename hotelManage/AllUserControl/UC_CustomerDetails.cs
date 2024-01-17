using System;
using System.Collections;
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
    public partial class UC_CustomerDetails : UserControl
    {
        functionConnect fn = new functionConnect();
        String query;
        // design interface
        public UC_CustomerDetails()
        {
            InitializeComponent();
        }
        
        private void txtSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {   
            if (txtSearchBy.SelectedIndex == 0)
            {
                query = "select customer.cid, customer.cname, " +
                    "customer.passport, customer.mobile, customer.gender, " +
                    "customer.idproof, customer.checkin,customer.checkoutday," +
                    " rooms.roomNo, rooms.roomType, rooms.bed, rooms.price from customer inner" +
                    " join rooms on customer.roomid = rooms.roomid";
                getRecord(query);
            }
            else if (txtSearchBy.SelectedIndex == 1)
            {
                query = "select customer.cid, customer.cname, " +
                    "customer.passport, customer.mobile, customer.gender," +
                    " customer.idproof, customer.checkin, customer.checkoutday," +
                    "rooms.roomNo, rooms.roomType, rooms.bed, rooms.price from customer inner join" +
                    " rooms on customer.roomid = rooms.roomid where checkout is null";
                getRecord(query);
            }
            else if (txtSearchBy.SelectedIndex == 2)
            {
                query = "select customer.cid, customer.cname, customer.passport, " +
                    "customer.mobile, customer.gender, customer.idproof, " +
                    "customer.checkin,customer.checkoutday, rooms.roomNo," +
                    " rooms.roomType, rooms.bed, rooms.price from customer inner join" +
                    " rooms on customer.roomid = rooms.roomid where checkout is not null";
                getRecord(query);
            }
        }
        private void getRecord(String query)
        {
            DataSet ds = fn.getData(query);
            guna2DataGridView1.DataSource = ds.Tables[0];
        }
    }
}
