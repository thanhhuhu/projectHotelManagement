using Guna.UI2.WinForms;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Xceed.Document.NET;
using Xceed.Words.NET;
using NPOI.XWPF.UserModel;
using NPOI.HSSF.Record.Chart;


namespace hotelManage
{
    public partial class UC_Receipt : UserControl
    {
        private functionConnect fn = new functionConnect();


        String query;
        public UC_Receipt()
        {
            InitializeComponent();
            LoadData();
            AutoSelectCustomerRoom();
            UpdateRoomPrice();
            LoadRooms();
        }

        private void LoadData()
        {
            LoadCustomers();
            AutoSelectCustomerRoom();
            UpdateRoomPrice();
            LoadRooms();
        }
        private void LoadCustomers()
        {
            string query = "SELECT * FROM customer";
            DataSet ds = fn.getData(query);

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = ds.Tables[0];

            txtName.DataSource = bindingSource;
            txtName.DisplayMember = "cname";
            txtName.ValueMember = "cid";

            txtName.SelectedIndexChanged += (sender, e) => AutoSelectCustomerRoom();
        }

        // when choose name customer will appear room price, room number and passport number
        private void AutoSelectCustomerRoom()
        {
            if (txtName.SelectedIndex >= 0)
            {
                int selectedCustomerId = Convert.ToInt32(txtName.SelectedValue);

                string getCustomerInfoQuery = $"SELECT price, roomNo, passport FROM rooms r INNER JOIN customer c ON r.roomid = c.roomid WHERE c.cid = {selectedCustomerId}";
                DataSet customerInfoDs = fn.getData(getCustomerInfoQuery);

                if (customerInfoDs.Tables[0].Rows.Count > 0)
                {
                    string bookedRoomNo = customerInfoDs.Tables[0].Rows[0]["roomNo"].ToString();
                    string passportNumber = customerInfoDs.Tables[0].Rows[0]["passport"].ToString();
                    string price = customerInfoDs.Tables[0].Rows[0]["price"].ToString();
                    txtRoomNo.Text = bookedRoomNo;
                    txtPassport.Text = passportNumber;
                    txtRoomPrice.Text = price;
                    UpdateRoomPrice();
                }
            }
        }

        private void LoadRooms()
        {
          

            if (txtName.SelectedIndex >= 0)
            {
                int selectedCustomerId = Convert.ToInt32(txtName.SelectedValue);
                string getCustomerRoomQuery = $"SELECT roomNo FROM rooms r INNER JOIN customer c ON r.roomid = c.roomid WHERE c.cid = {selectedCustomerId}";
                DataSet roomDs = fn.getData(getCustomerRoomQuery);

                if (roomDs.Tables[0].Rows.Count > 0)
                {
                    txtRoomNo.Text = roomDs.Tables[0].Rows[0]["roomNo"].ToString();
                }
                else
                {
                    txtRoomNo.Text = string.Empty;
                }

                UpdateRoomPrice();
            }
        }
        
        private void UpdateRoomPrice()
        {
            if (int.TryParse(txtRoomNo.Text, out int selectedRoomId))
            {
                string getPriceQuery = $"SELECT price FROM rooms WHERE roomid = {selectedRoomId}";
                DataSet priceDs = fn.getData(getPriceQuery);

                if (priceDs.Tables[0].Rows.Count > 0)
                {
                    double roomPrice = Convert.ToDouble(priceDs.Tables[0].Rows[0]["price"]);

                    string formattedPrice = roomPrice.ToString("F0");

                    formattedPrice = formattedPrice.Replace(",", "");

                    txtRoomPrice.Text = formattedPrice;
                }
            }
            else
            {
                txtRoomPrice.Text = string.Empty;
            }
        }

        // check out penalty
        private double CalculateLateCheckoutPenalty(DateTime expectedCheckOutDate, DateTime actualCheckOutDate)
        {
            if (actualCheckOutDate > expectedCheckOutDate)
            {
                double penaltyPerDay = 500.0; 
                int daysLate = (int)(actualCheckOutDate.Date - expectedCheckOutDate.Date).TotalDays;

                double totalPenalty;
                if (daysLate > 0)
                {
                    totalPenalty = daysLate * penaltyPerDay;
                }
                else
                {
                    totalPenalty = 0.0;
                }


                return totalPenalty;
            }
            else
            {
                return 0.0;
            }
        }
        private void bt_Reload_Click(object sender, EventArgs e)
        {
            LoadData();

        }
        private int GetRoomId()
        {
            int selectedCustomerId = Convert.ToInt32(txtName.SelectedValue);
            string getCustomerRoomQuery =
                $"SELECT * FROM rooms r INNER JOIN customer c ON r.roomid = c.roomid WHERE c.cid = " +
                $"{selectedCustomerId}";
            DataSet roomid = fn.getData(getCustomerRoomQuery);
            var idroom = 0;
            if (roomid.Tables[0].Rows.Count > 0)
            {
                idroom = int.Parse(roomid.Tables[0].Rows[0]["roomid"].ToString());
                return idroom;
            }
            return idroom;
        }
        
        private int CalculateDaysOfStay(DateTime bookingDate, DateTime checkoutDate)
        {
            return (checkoutDate.Date - bookingDate.Date).Days;
        }
        private void Clear()
        {
            LoadData();
        }
        private void AddReceipt_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtName.Text.Trim())
                || string.IsNullOrEmpty(txtRoomNo.Text)
                || string.IsNullOrEmpty(txtRoomPrice.Text) 
                || string.IsNullOrEmpty(txtPassport.Text) 
                || string.IsNullOrEmpty(dtDateCheckout.Value.ToString())
                || string.IsNullOrEmpty(dtDateCheckin.Value.ToString())
                || string.IsNullOrEmpty(dtDateCheckouted.Value.ToString())
                
            )
            {
                MessageBox.Show("All field is required!");
                return;
            } else
            {
                int daysOfStay = CalculateDaysOfStay(dtDateCheckin.Value, dtDateCheckouted.Value);
                double roomFeePerDay = double.Parse(txtRoomPrice.Text.Trim());
                double totalRoomFee = roomFeePerDay * daysOfStay;

                var lateCheckoutPenalty = CalculateLateCheckoutPenalty(dtDateCheckout.Value,
                    dtDateCheckouted.Value);
                var totalPrice = totalRoomFee  + lateCheckoutPenalty;
                var idR = GetRoomId();

                if (idR == 0)
                {
                    MessageBox.Show("Not have roomid!");
                    return;
                } else
                {
                    query = $"Insert into Receipts(RoomID,CustomerName,Passport,BookingDate,ExpectedCheckOutDate,ActualCheckOutDate,RoomPrice,TotalPrice,LateCheckoutPenalty) values({idR},N'{txtName.Text}','{txtPassport.Text}', '{dtDateCheckin.Value}', '{dtDateCheckout.Value}','{dtDateCheckouted.Value}',{totalRoomFee}, {totalPrice}, {lateCheckoutPenalty})";
                    fn.setData(query, $"Add bill for {txtName.Text} successfully!");
                    Clear();
                    return;
                }

            
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Bill formbill = new Bill();
            formbill.ShowDialog();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
