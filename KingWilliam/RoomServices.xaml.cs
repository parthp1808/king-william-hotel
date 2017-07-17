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
    /// Interaction logic for RoomServices.xaml
    /// </summary>
    public partial class RoomServices : Window
    {
        SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=KingWilliamHotelDB;Integrated Security=True");

        public RoomServices()
        {
            InitializeComponent();
            rdbRestaurant.IsChecked = true;
            SqlDataReader rdr = null;
            try
            {

                conn.Open();

                SqlCommand service = new SqlCommand("Select ServiceDescription from tblServices", conn);
                rdr = service.ExecuteReader();
                while (rdr.Read())
                {
                    cmbService.Items.Add(rdr["ServiceDescription"].ToString());
                }



            }
            finally
            {

                if (rdr != null)
                {
                    rdr.Close();
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

        private void rdbRestaurant_Checked(object sender, RoutedEventArgs e)
        {
            cnvService.Visibility = Visibility.Hidden;
            cnvButton.Margin = new Thickness(364, 340, 623.2, 414.2);
           
        }

        private void rdbService_Checked(object sender, RoutedEventArgs e)
        {
            cnvService.Visibility = Visibility.Visible;
            cnvButton.Margin = new Thickness(364, 376, 623.2, 378.2);            
            

            
        }

        private void btnCharge_Click(object sender, RoutedEventArgs e)
        {
            if(txtRoomID.Text == "" || txtAmount.Text=="")
            {
                MessageBox.Show("Enter all the information");
            }
            else
            {
                conn.Open();
                SqlCommand id = new SqlCommand("select reservationID from tblReservations where RoomID = '" + txtRoomID.Text + "' and ReservationEndDate > CONVERT (date, SYSDATETIME()) and ReservationStartDate <= CONVERT (date, SYSDATETIME())", conn);
                var rid = id.ExecuteScalar();
                if(rid == null)
                {
                    MessageBox.Show("Room number - " + txtRoomID.Text + " is not booked by anyone. Please check room number");

                }
                else
                {
                    if(rdbRestaurant.IsChecked == true)
                    {
                        SqlCommand insert = new SqlCommand("insert into tblRestaurantTransactions (ReservationID,cost,TransactionDate) values ('" + rid.ToString() + "','" + txtAmount.Text + "',CONVERT (DATE,SYSDATETIME()))",conn);
                        int r = insert.ExecuteNonQuery();
                        if(r > 0)
                        {
                            MessageBox.Show("Transaction done successfully!");
                            MainWindow n = new MainWindow();
                            n.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Transaction unsuccessfully!!!");
                        }
                    }
                    else
                    {
                        
                            int service = cmbService.SelectedIndex;
                            SqlCommand insert = new SqlCommand("insert into tblServicesTransactions (ReservationID,ServiceID,ServiceDate) values ('" + rid.ToString() + "','" + service++ + "',CONVERT (DATE,SYSDATETIME()))", conn);

                            int r = insert.ExecuteNonQuery();
                            if (r > 0)
                            {
                                MessageBox.Show("Transaction done successfully!");
                            }
                            else
                            {
                                MessageBox.Show("Transaction unsuccessfully!!!");
                            }
                        
                    }
                }

                conn.Close();
            }

        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int serviceId = cmbService.SelectedIndex + 1;
            conn.Open();

            SqlCommand cost = new SqlCommand("select Cost from tblServices where ServiceID = '" + serviceId + "'", conn);
            var result = cost.ExecuteScalar();
            double price = 0;
            if(result!=null)
            {
                price = double.Parse(result.ToString());
            }
            txtAmount.Text = price.ToString("#.##");


            conn.Close();
        }
    }
}
