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
    /// Interaction logic for RoomPricing.xaml
    /// </summary>
    public partial class RoomPricing : Window
    {
        SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=KingWilliamHotelDB;Integrated Security=True");

        public RoomPricing()
        {
            InitializeComponent();
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
            MainWindow form = new MainWindow();
            form.Show();
            this.Close();
        }

        private void cmbRoomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            conn.Open();
            SqlDataReader reader = null;
            if (cmbRoomType.SelectedValue.ToString() == "Suite")
            {
                txtPrice.Text = "";
                cmbRoomNumber.Items.Clear();
                cnvRoomNumber.Visibility = Visibility.Visible;
                cnvPricing.Margin = new Thickness(451, 509, 533.2, 215.2);
                
                SqlCommand number = new SqlCommand("select roomID from tblRooms inner join tblRoomType on tblRooms.RoomTypeID = tblRoomType.RoomTypeID where TypeDescription ='Suite'", conn);
                reader = number.ExecuteReader();

                while (reader.Read())
                {
                    cmbRoomNumber.Items.Add(reader["roomID"].ToString());
                }
                reader.Close();
                btnUpdate.Visibility = Visibility.Collapsed;
            }
            else
            {
                
               
                double money = 0;
                cnvRoomNumber.Visibility = Visibility.Collapsed;
                cnvPricing.Margin = new Thickness(451, 421, 533.2, 303.2);
                SqlCommand type = new SqlCommand("SELECT TOP 1 Cost FROM tblRooms inner join tblRoomType on tblRooms.RoomTypeID = tblRoomType.RoomTypeID where TypeDescription ='" + cmbRoomType.SelectedValue.ToString() + "'", conn);
                var cost = type.ExecuteScalar();
                if(cost!= null)
                {
                    money = double.Parse(cost.ToString());
                     
                }
                txtPrice.Text = money.ToString("#.##");
                btnUpdate.Visibility = Visibility.Visible;
            }
           
            conn.Close();
        }

        private void cmbRoomNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbRoomNumber.SelectedIndex != -1)
            {
                conn.Open();
                SqlCommand num = new SqlCommand("Select cost from tblRooms where RoomID = '" + cmbRoomNumber.SelectedValue.ToString() + "'", conn);
                double result = 0;
                var output = num.ExecuteScalar();
                if (output != null)
                {
                    result = double.Parse(output.ToString());
                }
                txtPrice.Text = result.ToString("#.##");
                btnUpdate.Visibility = Visibility.Visible;
                conn.Close();
            }
            else
            {
                btnUpdate.Visibility = Visibility.Collapsed;
            }
            
            
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            double price = 0;
            if(double.TryParse(txtPrice.Text,out price) && price > 0)
            {
                conn.Open();

                if (cmbRoomType.SelectedValue.ToString() == "Suite")
                {
                    SqlCommand update = new SqlCommand("UPDATE tblRooms SET Cost='" + txtPrice.Text + "' where RoomID = '" + cmbRoomNumber.Text + "'", conn);
                    int r = update.ExecuteNonQuery();
                    if (r > 0)
                    {
                        MessageBox.Show("Reservation Updated successfully!");
                        MainWindow main = new MainWindow();
                        main.Show();
                        this.Close();
                    }
                }
                else
                {
                    SqlCommand update = new SqlCommand("Update tblrooms set cost = '" + txtPrice.Text + "' where RoomTypeID = ( select top 1 tblrooms.roomtypeid from tblrooms inner join tblroomtype on tblrooms.RoomTypeID = tblroomtype.RoomTypeID where Typedescription = '" + cmbRoomType.Text + "')", conn);
                    int r = update.ExecuteNonQuery();
                    if (r > 0)
                    {
                        MessageBox.Show("Reservation Updated successfully!");
                        MainWindow main = new MainWindow();
                        main.Show();
                        this.Close();
                    }

                }
            }
            else
            {
                MessageBox.Show("Enter numbers for price and greater than 0");
            }
            

        }
    }
}
