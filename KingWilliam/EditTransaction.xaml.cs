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
    public partial class EditTransaction : Window
    {
        SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=KingWilliamHotelDB;Integrated Security=True");
        public static int transactionid = 0;
        public EditTransaction()
        {
            InitializeComponent();
            cnvButton.Visibility = Visibility.Hidden;
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
            cnvButton.Visibility = Visibility.Hidden;

            cnvButton.Margin = new Thickness(502, 420, 485.2, 334.2);
            
            
        }

        private void rdbService_Checked(object sender, RoutedEventArgs e)
        {
            cnvService.Visibility = Visibility.Hidden;
            cnvButton.Visibility = Visibility.Hidden;
            cnvButton.Margin = new Thickness(502, 501, 485.2, 415.2);
            
            

            
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if(txtRoomID.Text == "" || txtAmount.Text=="")
            {
                MessageBox.Show("Enter all the information");
            }
            else
            {
                

                if (!checkReservationID())
                {
                    MessageBox.Show("Reservation not found!");

                }
                else
                {
                    conn.Open();
                    if (rdbRestaurant.IsChecked == true)
                    {
                        SqlCommand insert = new SqlCommand("update tblRestaurantTransactions set cost='" + txtAmount.Text + "' where  OrderID = '" + transactionid + "'", conn);
                        int r = insert.ExecuteNonQuery();
                        if(r > 0)
                        {
                            MessageBox.Show("Transaction updated successfully!");
                            MainWindow n = new MainWindow();
                            n.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Transaction unsuccessfully!!!");
                            MainWindow n = new MainWindow();
                            n.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        
                            int service = cmbService.SelectedIndex + 1;
                            SqlCommand insert = new SqlCommand("update tblServicesTransactions set Amount = '" + txtAmount.Text + "' , ServiceID='"+ service +"' where ServiceTransactionID = '" + transactionid+ "'", conn);

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
                    conn.Close();
                }

               
            }

        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int serviceId = cmbService.SelectedIndex + 1;
            if(conn == null || conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }
            

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

        private void btnTransactions_Click(object sender, RoutedEventArgs e)
        {
            int sid;
            if (txtRoomID.Text !="")
            {
                if (int.TryParse(txtRoomID.Text, out sid))
                {
                    if (checkReservationID())
                    {
                        if (rdbRestaurant.IsChecked == true)
                        {

                            Transactions tr = new Transactions(txtRoomID.Text, "r");
                            tr.ShowDialog();
                       
                            if(transactionid!=0)
                            {
                                rdbRestaurant_Checked(sender, e);
                                loadForm(transactionid);
                                
                            }
                        
                        }
                        else if (rdbService.IsChecked == true)
                        {
                            Transactions tr = new Transactions(txtRoomID.Text, "s");
                            tr.ShowDialog();
                        
                            if (transactionid != 0)
                            {
                                rdbService_Checked(sender, e);
                                loadForm(transactionid);
                                
                            }
                        }
                        else
                        {
                            MessageBox.Show("Select either reservation or service type");
                            rdbRestaurant.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Reservation not found!");
                        txtRoomID.SelectAll();
                        txtRoomID.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Reservation ID can be numeric only!");
                    txtRoomID.SelectAll();
                    txtRoomID.Focus();
                }

            }
            else
            {
                MessageBox.Show("Reservation ID cannot be empty");
                txtRoomID.Focus();
            }
            
            
        }

        private void loadForm(int id)
        {
            int index = -1;
            string amount = "";
            conn.Open();
            if (rdbRestaurant.IsChecked == true)
            {
                cnvButton.Visibility = Visibility.Visible;
                cnvService.Visibility = Visibility.Collapsed;
                SqlCommand select = new SqlCommand("select cost from tblRestaurantTransactions where orderID = '" + transactionid + "'",conn);
                var a = select.ExecuteScalar();
                if(a != null)
                {
                    txtAmount.Text = a.ToString();
                }
            }
            else if (rdbService.IsChecked == true)
            {
                cnvButton.Visibility = Visibility.Visible;
                cnvService.Visibility = Visibility.Visible;
                SqlDataReader rdr = null;
                SqlCommand select = new SqlCommand("select serviceID, amount from tblServicesTransactions where ServiceTransactionID = '" + transactionid + "'", conn);
                rdr = select.ExecuteReader();
                while (rdr.Read())
                {
                    amount = rdr["Amount"].ToString();
                   index = int.Parse(rdr["serviceID"].ToString()) - 1;
                    
                }
                rdr.Close();
                cmbService.SelectedIndex = index;
                txtAmount.Text = amount;

            }

            
            conn.Close();

        }

        private bool checkReservationID()
        {
            
            bool o = false;

            if (conn == null || conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }

                SqlCommand find = new SqlCommand("Select ReservationID from tblReservations where ReservationID = '" + txtRoomID.Text + "'", conn);
                SqlDataReader rdr = find.ExecuteReader();
                while (rdr.Read())
                {
                    o = true;
                }

            conn.Close();
           
            return o;
           
        }

        
    }
}
