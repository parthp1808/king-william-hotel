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
    /// Interaction logic for EmployeeInformation.xaml
    /// </summary>
    public partial class EmployeeInformation : Window
    {
         SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=KingWilliamHotelDB;Integrated Security=True");
       
        
        public EmployeeInformation()
        {
            InitializeComponent();
            txtEmployeeID.Focus();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow form = new MainWindow();
            form.Show();
            this.Close();
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            if (txtEmployeeID.Text != "")
            {
                conn.Open();
                SqlCommand reservation = new SqlCommand("select * from tblEmployee where employeeID =  '" + txtEmployeeID.Text + "'", conn);
                SqlDataReader rdr = null;
                rdr = reservation.ExecuteReader();
                bool found = false;
                while (rdr.Read())
                {
                    cnvEmployee.Visibility = Visibility.Visible;
                    lblID.Content = rdr["EmployeeID"].ToString();
                    lblFirstName.Content = rdr["FirstName"].ToString();

                    lblLastName.Content = rdr["LastName"];
                    lblPhone.Content = rdr["Phone"];
                    lblAddress.Content = rdr["EmployeeAddress"].ToString();
                    lblRate.Content = rdr["PayRate"].ToString();
                    lblPosition.Content = rdr["Position"].ToString();
                    lblType.Content = rdr["EmployeeType"].ToString();
                    
                    found = true;


                }

                if (found == false)
                {
                    cnvEmployee.Visibility = Visibility.Collapsed;
                    MessageBox.Show("Employee ID not found. Please try again!");

                }
                rdr.Close();
                conn.Close();




            }
            else
            {
                MessageBox.Show("Enter Reservation ID to find Reservation details");
                txtEmployeeID.Focus();
            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditEmployee add = new AddEditEmployee();
            add.ShowDialog();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            AddEditEmployee win = new AddEditEmployee(txtEmployeeID.Text);
            win.ShowDialog();
        }
    }
}
