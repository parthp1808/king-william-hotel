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
    /// Interaction logic for AddEditGuest.xaml
    /// </summary>
    public partial class AddEditGuest : Window
    {
        SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=KingWilliamHotelDB;Integrated Security=True");

        string id = "";
        public AddEditGuest()
        {
            InitializeComponent();
        }
        public AddEditGuest(string id)
        {
            InitializeComponent();
            this.id = id;
            txtID.Text = id;
            conn.Open();
            SqlCommand reservation = new SqlCommand("select * from tblGuests where guestID =  '" + txtID.Text + "'", conn);
            SqlDataReader rdr = null;
            rdr = reservation.ExecuteReader();

            while (rdr.Read())
            {               
                txtID.Text = rdr["GuestID"].ToString();
                txtFirstName.Text = rdr["FirstName"].ToString();
                txtLastName.Text = rdr["LastName"].ToString();
                txtPhone.Text = rdr["Phone"].ToString();
                txtAddress.Text = rdr["GuestAddress"].ToString();
                txtPostal.Text = rdr["PostalCode"].ToString();
                txtEmail.Text = rdr["EmailAddress"].ToString();
                

            }
            conn.Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (id != "")
            {
                if (txtFirstName.Text == "" || txtLastName.Text == "" || txtEmail.Text == "" || txtPhone.Text == "" || txtPostal.Text == "" || txtAddress.Text == "")
                {
                    MessageBox.Show("No field can be empty. Fill all the required textboxes.");
                }

                else
                {
                    conn.Open();
                    SqlCommand update = new SqlCommand("UPDATE tblGuests SET FirstName='" + txtFirstName.Text + "' ,LastName='" + txtLastName.Text + "' ,GuestAddress='" + txtAddress.Text + "', Phone='" + txtPhone.Text + "', PostalCode='" + txtPostal.Text + "' ,EmailAddress = '" + txtEmail.Text + "' where GuestID = '" + txtID.Text + "'", conn);
                    int r = update.ExecuteNonQuery();
                    if (r > 0)
                    {
                        MessageBox.Show("Guest Updated successfully!");
                        this.Close();
                    }
                    conn.Close();
                }

            }
            else
            {
                
                
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
