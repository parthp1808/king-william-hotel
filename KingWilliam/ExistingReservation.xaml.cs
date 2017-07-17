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
    /// Interaction logic for ExistingReservation.xaml
    /// </summary>
    public partial class ExistingReservation : Window
    {
        SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=KingWilliamHotelDB;Integrated Security=True");
       
        
        public ExistingReservation()
        {

            InitializeComponent();
            

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow form = new MainWindow();
            form.Show();
            this.Close();
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            if(txtFind.Text !="")
            {
                conn.Open();
                SqlCommand reservation = new SqlCommand("select * from tblReservations inner join tblGuests on tblReservations.GuestID = tblGuests.GuestID where reservationid =  '" + txtFind.Text + "'", conn);
                SqlDataReader rdr = null;
                rdr = reservation.ExecuteReader();
                bool found = false;
                while(rdr.Read())
                {
                    cnvResult.Visibility = Visibility.Visible;
                    txtGuestID.Text = rdr["GuestID"].ToString();
                    txtEmployeeID.Text = rdr["EmployeeID"].ToString();
                    
                    lblStartDate.Content = rdr["ReservationStartDate"];
                    lblEndDate.Content = rdr["ReservationEndDate"];
                    lblRoomNumber.Content = rdr["RoomID"].ToString();
                    txtFirstName.Text = rdr["FirstName"].ToString();
                    txtLastName.Text = rdr["LastName"].ToString();
                    txtPhone.Text = rdr["Phone"].ToString();
                    txtAddress.Text = rdr["GuestAddress"].ToString();
                    txtEmail.Text = rdr["EmailAddress"].ToString();
                    txtPostalCode.Text = rdr["PostalCode"].ToString();
                    found = true;
                    
                    
                }

                if(found==false)
                {
                    cnvResult.Visibility = Visibility.Collapsed;
                    MessageBox.Show("Reservation ID not found. Please try again!");
                   
                }
                rdr.Close();
                conn.Close();




            }
            else
            {
                MessageBox.Show("Enter Reservation ID to find Reservation details");
                txtFind.Focus();
            }

            
        }
        
        private void txtGuestID_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void dpiStartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dpiEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Window choose = new ChangeRoom(txtGuestID.Text );
            var result = choose.ShowDialog();
            if(result == true)
            {
                lblStartDate.Content = ChangeRoom.startDate;
                lblEndDate.Content = ChangeRoom.endDate;
                lblRoomNumber.Content = ChangeRoom.roomNumber;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if(txtFirstName.Text=="" || txtLastName.Text =="" || txtAddress.Text=="" || txtPhone.Text =="" ||txtEmail.Text==""||txtPostalCode.Text=="")
            {
                MessageBox.Show("No field can be empty. Fill all the required textboxes.");
            }
            else
            {
                conn.Open();
                SqlCommand update = new SqlCommand("UPDATE tblGuests SET FirstName='" + txtFirstName.Text + "' ,LastName='" + txtLastName.Text + "' ,GuestAddress='" + txtAddress.Text + "' ,PostalCode='" + txtPostalCode.Text + "', Phone='" + txtPhone.Text + "' ,EmailAddress = '" + txtEmail.Text + "' where GuestID = '"+txtGuestID.Text+"'", conn);
                int r = update.ExecuteNonQuery();
                if(r > 0)
                {
                    SqlCommand updateR = new SqlCommand("UPDATE tblReservations SET RoomID='" + lblRoomNumber.Content + "', ReservationStartDate='" + lblStartDate.Content + "', ReservationEndDate='" + lblEndDate.Content + "' where GuestID = '" + txtGuestID.Text + "'",conn);
                    int s = updateR.ExecuteNonQuery();
                    if(s >0)
                    {
                        MessageBox.Show("Reservation Updated successfully!");
                        MainWindow main = new MainWindow();
                        main.Show();
                        this.Close();
                    }
                }

                conn.Close();

            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            conn.Open();
            SqlCommand update = new SqlCommand("DELETE FROM tblReservations where reservationID = '" + txtFind.Text + "'", conn);
            int r = update.ExecuteNonQuery();
            if(r > 0)
            {
                MessageBox.Show("Reservation Deleted Successfully!");
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
        }
    }
}
