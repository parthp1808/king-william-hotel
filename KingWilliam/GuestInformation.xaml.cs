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
    /// Interaction logic for GuestInformation.xaml
    /// </summary>
    public partial class GuestInformation : Window
    {
        SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=KingWilliamHotelDB;Integrated Security=True");

        public GuestInformation()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow form = new MainWindow();
            form.Show();
            this.Close();
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            if (txtGuestID.Text != "")
            {
                conn.Open();
                SqlCommand guest = new SqlCommand("select * from tblGuests where guestID =  '" + txtGuestID.Text + "'", conn);
                SqlDataReader rdr = null;
                rdr = guest.ExecuteReader();
                bool found = false;
                while (rdr.Read())
                {
                    cnvGuest.Visibility = Visibility.Visible;
                    lblID.Content = rdr["GuestID"].ToString();
                    lblFirstName.Content = rdr["FirstName"].ToString();
                    lblLastName.Content = rdr["LastName"];
                    lblPhone.Content = rdr["Phone"];
                    lblAddress.Content = rdr["GuestAddress"].ToString();
                    lblEmail.Content = rdr["EmailAddress"].ToString();
                    lblPostal.Content = rdr["PostalCode"].ToString();                  
                    found = true;
                }

                if (found == false)
                {
                    cnvGuest.Visibility = Visibility.Collapsed;
                    MessageBox.Show("Guest ID not found. Please try again!");
                }
                rdr.Close();
                conn.Close();
            }
            else
            {
                MessageBox.Show("Enter Reservation ID to find Reservation details");
                txtGuestID.Focus();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            AddEditGuest add = new AddEditGuest(txtGuestID.Text);
            add.ShowDialog();
            btnView_Click(sender,e);
        }
    }
}
