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
    /// Interaction logic for NewReservation.xaml
    /// </summary>
    public partial class NewReservation : Window
    {
        //string hotelConnectionString = "Data Source=PARTH-LAPPY;Initial Catalog=KingWilliamHotelDB;Integrated Security=True";
        SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=KingWilliamHotelDB;Integrated Security=True");


        public NewReservation()
        {
            InitializeComponent();
            rdbExistingGuest.IsChecked = true;
            SqlDataReader reader = null;
            dpiStartDate.DisplayDateStart = DateTime.Now;
            dpiEndDate.DisplayDateStart = DateTime.Now;
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
            MainWindow form = new MainWindow();
            form.Show();
            this.Close();
        }

        private void rdbNewGuest_Checked(object sender, RoutedEventArgs e)
        {
            cnvGuest.Visibility = Visibility.Visible;
            btnCheck.Visibility = Visibility.Hidden;
            cnvReservation.Margin= new Thickness(366, 450, 432.2, 182.2);
            conn.Open();
            SqlDataReader rdr = null;
            SqlCommand cmd = new SqlCommand("SELECT TOP 1 GuestID FROM tblGuests ORDER BY GuestID DESC", conn);
            rdr = cmd.ExecuteReader();
            string guestid = "";
            while (rdr.Read())
            {
                guestid = rdr["GuestID"].ToString();
            }

            int id = int.Parse(guestid.Substring(1)) + 1;
            guestid = "G" + id.ToString().PadLeft(5, '0'); ;
            txtGuestID.Text = guestid;
            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            txtGuestID.IsEnabled = false;
        }

        private void rdbExistingGuest_Checked(object sender, RoutedEventArgs e)
        {
            cnvGuest.Visibility = Visibility.Hidden;
            btnCheck.Visibility = Visibility.Visible;
            cnvReservation.Margin = new Thickness(366, 300, 432.2, 342.2);
            txtGuestID.Clear();
            txtGuestID.IsEnabled = true;

        }

        private void cmbRoomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbRoomNumber.Items.Clear();

            lblMessage.Content = "";
            if(dpiStartDate.Text !="" && dpiEndDate.Text !="")
            {
                reloadRoom();
            }
            else
            {
                lblMessage.Content = "Starting and Ending date must be selected";
                cmbRoomType.SelectedIndex=-1;
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (rdbExistingGuest.IsChecked == true)
            {
                string error = "";
                conn.Open();
                if(txtGuestID.Text!="")
                {
                    if (isUserExist(txtGuestID.Text) > 0)
                    {
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
                            SqlCommand insertReservation = new SqlCommand("INSERT INTO tblReservations (GuestID,RoomID,EmployeeID,DateMade, ReservationStartDate,ReservationEndDate) VALUES (@guest,@room,@emp,CONVERT (date, SYSDATETIME()),@start,@end)", conn);
                            insertReservation.Parameters.Add(new SqlParameter("guest", txtGuestID.Text));
                            insertReservation.Parameters.Add(new SqlParameter("room", cmbRoomNumber.Text));
                            insertReservation.Parameters.Add(new SqlParameter("emp", txtEmployeeID.Text));
                            insertReservation.Parameters.Add(new SqlParameter("start", dpiStartDate.Text));
                            insertReservation.Parameters.Add(new SqlParameter("end", dpiEndDate.Text));
                            int r = insertReservation.ExecuteNonQuery();
                            if (r == 0)
                            {
                                MessageBox.Show("Cannot insert reservation!");
                            }
                            else
                            {
                                MessageBox.Show("Reservation Made Successfully!!");
                                NewReservation rs = new NewReservation();
                                rs.Show();
                                this.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show(error);
                        }
                        if (conn != null)
                        {
                            conn.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("\nGuest with - " + txtGuestID.Text + " is not found\n");
                        txtGuestID.Focus();
                        txtGuestID.SelectAll();

                    }
                }
                else
                {
                    MessageBox.Show("Guest ID is required!");
                }
                


                conn.Close();

            }
            else if(rdbNewGuest.IsChecked == true)
            {
                string error = "";
                conn.Open();
                if (cmbRoomType.SelectedItem == null || cmbRoomNumber.SelectedItem == null)
                {
                    error += "\n Room needs to be selected!!";
                }
                if (dpiStartDate.Text == "" || dpiEndDate.Text == "")
                {
                    error += "\n Dates must be selected!!";
                }
                if (txtFirstName.Text == "" || txtLastName.Text == "")
                {
                    error += "\nFirst Name and Last Name both are required !!";
                }
                if (txtPhone.Text == "")
                {
                    error += "\nPhone Number is required !!";
                }
                if (txtPhone.Text.Length != 10)
                {
                    error += "\nPhone Number needs to be of 10 characters!!";
                }
                if (txtAddress.Text == "")
                {
                    error += "\nAddress is required !!";
                }
                if (txtPostalCode.Text == "")
                {
                    error += "\nPostal Code is required !!";
                }

                if (txtPostalCode.Text.Length != 6)
                {
                    error += "\nPostal Code needs to be of 6 characters without space !!";
                }

                if (error == "")
                {
                    SqlCommand insertGuest = new SqlCommand("INSERT INTO tblGuests (GuestID,FirstName,LastName,GuestAddress, PostalCode,Phone,EmailAddress) VALUES (@guest,@first,@last,@address,@postal,@phone,@email)", conn);

                    insertGuest.Parameters.Add(new SqlParameter("guest", txtGuestID.Text));
                    insertGuest.Parameters.Add(new SqlParameter("first", txtFirstName.Text));
                    insertGuest.Parameters.Add(new SqlParameter("last", txtLastName.Text));
                    insertGuest.Parameters.Add(new SqlParameter("address", txtAddress.Text));
                    insertGuest.Parameters.Add(new SqlParameter("postal", (txtPostalCode.Text).ToUpper()));
                    insertGuest.Parameters.Add(new SqlParameter("phone", txtPhone.Text));
                    insertGuest.Parameters.Add(new SqlParameter("email", txtEmail.Text));
                    int result = insertGuest.ExecuteNonQuery();
                    if (result == 0)
                    {
                        MessageBox.Show("Cannot insert Guest!");
                    }
                    else
                    {
                        SqlCommand insertReservation = new SqlCommand("INSERT INTO tblReservations (GuestID,RoomID,EmployeeID,DateMade, ReservationStartDate,ReservationEndDate) VALUES (@guest,@room,@emp,CONVERT (date, SYSDATETIME()),@start,@end)", conn);
                        insertReservation.Parameters.Add(new SqlParameter("guest", txtGuestID.Text));
                        insertReservation.Parameters.Add(new SqlParameter("room", cmbRoomNumber.Text));
                        insertReservation.Parameters.Add(new SqlParameter("emp", txtEmployeeID.Text));
                        insertReservation.Parameters.Add(new SqlParameter("start", dpiStartDate.Text));
                        insertReservation.Parameters.Add(new SqlParameter("end", dpiEndDate.Text));
                        int r = insertReservation.ExecuteNonQuery();
                        if (r == 0)
                        {
                            MessageBox.Show("Cannot insert reservation!");
                        }
                        else
                        {
                            MessageBox.Show("Reservation Made Successfully!!");
                            NewReservation rs = new NewReservation();
                            rs.Show();
                            this.Close();
                        }
                    }


                }
                else
                {
                    MessageBox.Show(error);
                }
                if (conn != null)
                {
                    conn.Close();
                }



                conn.Close();

            }
        }

        private void txtGuestID_LostFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void dpiStartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            lblMessage.Content = "";
            dpiEndDate.DisplayDateStart = dpiStartDate.SelectedDate;
            dpiEndDate.Text = "";
            
        }

        private void dpiEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            lblMessage.Content = "";
            if(cmbRoomType.SelectedIndex != -1)
            {
                reloadRoom();
            }
            //
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
                //and StatusID = 1
                SqlCommand cmdRoomNumber = new SqlCommand("select RoomID from tblRooms inner join tblRoomType on tblRooms.RoomTypeID = tblRoomType.RoomTypeID where tblRooms.RoomTypeID = '" + type + "'  and (tblRooms.RoomID not in (select RoomID from tblReservations where ReservationEndDate >= '" + dpiStartDate.Text + "') or tblRooms.RoomID not in (select RoomID from tblReservations where ReservationStartDate <= '" + dpiEndDate.Text + "'))", conn);
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

        private int isUserExist(string id)
        {
            
            SqlCommand checkUser = new SqlCommand("select count(*) from tblGuests where GuestID = '" + id + "'", conn);
            int userExist = (int)checkUser.ExecuteScalar();
                      

            return userExist;
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            Window check = new CheckPreviousRoom(txtGuestID.Text);
            check.ShowDialog();
        }

        protected int checkRoomStatus(string roomID)
        {
           int output = 0;
            try
            {
                conn.Open();
                SqlCommand check = new SqlCommand("select statusid from tblRooms where roomID = '" + roomID + "'", conn);
                var status = check.ExecuteScalar();
                output = int.Parse(status.ToString());
                
            }
            finally
            {
                if(conn != null)
                {
                    conn.Close();
                }
            }


            return output;
        }

        private void cmbRoomNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int status = checkRoomStatus(cmbRoomNumber.SelectedValue.ToString());
            if(status == 2)
            {
                lblMessage.Content = "Room " + cmbRoomNumber.SelectedValue.ToString() + " needs cleaning!";
            }
            else if(status == 3)
            {
                lblMessage.Content = "Room " + cmbRoomNumber.SelectedValue.ToString() + " needs maintenance!";
            }
            else
            {
                lblMessage.Content = "";
            }
        }
    }
}
