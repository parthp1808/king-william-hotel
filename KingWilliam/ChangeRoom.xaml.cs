using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace KingWilliam
{
    /// <summary>
    /// Interaction logic for ChangeRoom.xaml
    /// </summary>
    public partial class ChangeRoom : Window
    {
        public static string startDate { get; set; }
        public static string endDate { get; set; }
        public static string roomNumber { get; set; }
        SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=KingWilliamHotelDB;Integrated Security=True");
        string guest = "";
        public ChangeRoom()
        {
            InitializeComponent();
        }

        public ChangeRoom(string id)
        {
            InitializeComponent();
            guest = id;
            dpiStartDate.DisplayDateStart = DateTime.Now;
            dpiEndDate.DisplayDateStart = DateTime.Now;
            SqlDataReader reader = null;
            try
            {

                conn.Open();

                SqlCommand cmdType = new SqlCommand("select * from tblRoomType", conn);

                reader = cmdType.ExecuteReader();

                while (reader.Read())
                {
                    cmbRoomType.Items.Add(reader["TypeDescription"].ToString());
                }



            }
            finally
            {

                if (reader != null)
                {
                    reader.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dpiStartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dpiEndDate.DisplayDateStart = dpiStartDate.SelectedDate;
            dpiEndDate.Text = "";
        }
        private void reloadRoom()
        {
            cmbRoomNumber.Items.Clear();
            conn.Open();
            string type = "";
            SqlDataReader reader = null;
            try
            {

                SqlCommand cmdGetRoomTypeID = new SqlCommand("select RoomTypeID from tblRoomType where TypeDescription = '" + cmbRoomType.SelectedValue.ToString() + "'", conn);
                reader = cmdGetRoomTypeID.ExecuteReader();

                while (reader.Read())
                {
                    type = reader["RoomTypeID"].ToString();
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            SqlDataReader reader2 = null;
            try
            {
                SqlCommand cmdRoomNumber = new SqlCommand("select RoomID from tblRooms inner join tblRoomType on tblRooms.RoomTypeID = tblRoomType.RoomTypeID where tblRooms.RoomTypeID = '" + type + "' and StatusID = 1 and (tblRooms.RoomID not in (select RoomID from tblReservations where ReservationEndDate >= '" + dpiStartDate.Text + "') or tblRooms.RoomID not in (select RoomID from tblReservations where ReservationStartDate <= '" + dpiEndDate.Text + "'))", conn);
                reader2 = cmdRoomNumber.ExecuteReader();
                while (reader2.Read())
                {
                    cmbRoomNumber.Items.Add(reader2["RoomID"].ToString());
                }
            }
            finally
            {
                if (reader2 != null)
                {
                    reader2.Close();
                }
            }
            if (conn != null)
            {
                conn.Close();
            }
        }

        private void dpiEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbRoomType.SelectedIndex != -1)
            {
                reloadRoom();
            }
        }

        private void cmbRoomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbRoomNumber.Items.Clear();
            if (dpiStartDate.Text != "" && dpiEndDate.Text != "")
            {
                reloadRoom();
            }
            else
            {                
                cmbRoomType.SelectedIndex = -1;
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string error = "";
            if (cmbRoomType.SelectedItem == null || cmbRoomNumber.SelectedItem == null)
            {
                error += "\n Room needs to be selected!!";
            }
            if (dpiStartDate.Text == "" || dpiEndDate.Text == "")
            {
                error += "\n Dates must be selected!!";
            }

            if (error == "")
            {
                startDate = dpiStartDate.Text;
                endDate = dpiEndDate.Text;
                roomNumber = cmbRoomNumber.Text;
                this.DialogResult = true;
            }
        }
    }
}
