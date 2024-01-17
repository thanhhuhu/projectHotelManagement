using hotelManage.AllUserControl;
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
    public partial class dashboard : Form
    {
        public dashboard()
        {
            InitializeComponent();
        }

        private void dashboard_Load(object sender, EventArgs e)
        {
            uC_AddRoom1.Visible = false;
            uC_CustomerRes1.Visible = false;
            uC_CheckOut1.Visible = false;
            btnAddRoom.PerformClick();
            uC_Receipt1.Visible = false;
            employee1.Visible = false;
        }
        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            uC_AddRoom1.Visible=true;
            uC_AddRoom1.BringToFront();
        }

        private void btnCustomerRes_Click(object sender, EventArgs e)
        {
            uC_CustomerRes1.Visible=true;
            uC_CustomerRes1.BringToFront();

        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            uC_CheckOut1.Visible=true;
            uC_CheckOut1.BringToFront();
        }

        private void btnCustomerDetail_Click(object sender, EventArgs e)
        {
            uC_CustomerDetails1.Visible = true;
            uC_CustomerDetails1.BringToFront();
        }

        private void btnReceipt_Click_1(object sender, EventArgs e)
        {
            uC_Receipt1.Visible = true;
            uC_Receipt1.BringToFront();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            employee1.Visible = true;
            employee1.BringToFront();
        }

     
    }
}
