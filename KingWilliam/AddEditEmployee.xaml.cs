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
    /// Interaction logic for AddEditEmployee.xaml
    /// </summary>
    public partial class AddEditEmployee : Window
    {
        SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=KingWilliamHotelDB;Integrated Security=True");

        string id = "";
        public AddEditEmployee()
        {
            InitializeComponent();
            lblHeader.Content = "Add Employee";
            btnEdit.Content = "Add";

            conn.Open();
            SqlDataReader rdr = null;
            SqlCommand cmd = new SqlCommand("SELECT TOP 1 EmployeeID FROM tblEmployee ORDER BY EmployeeID DESC", conn);
            rdr = cmd.ExecuteReader();
            string EmployeeID = "";
            while (rdr.Read())
            {
                EmployeeID = rdr["EmployeeID"].ToString();
            }

            int id = int.Parse(EmployeeID.Substring(1)) + 1;
            EmployeeID = "E" + id.ToString().PadLeft(5, '0'); ;
            txtID.Text = EmployeeID;
            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            txtFirstName.Focus();
        }
        public AddEditEmployee(string id)
        {
            InitializeComponent();
            lblHeader.Content = "Edit Employee";
            btnEdit.Content = "Update";
            this.id = id;
            txtID.Text = id;
            conn.Open();
            SqlCommand reservation = new SqlCommand("select * from tblEmployee where employeeID =  '" + txtID.Text + "'", conn);
            SqlDataReader rdr = null;
            rdr = reservation.ExecuteReader();
            
            while (rdr.Read())
            {
                cnvEmployee.Visibility = Visibility.Visible;
                txtID.Text = rdr["EmployeeID"].ToString();
                txtFirstName.Text = rdr["FirstName"].ToString();
                txtLastName.Text = rdr["LastName"].ToString();
                txtPhone.Text = rdr["Phone"].ToString();
                txtAddress.Text = rdr["EmployeeAddress"].ToString();
                txtRate.Text = rdr["PayRate"].ToString();
                txtPosition.Text = rdr["Position"].ToString();
                txtType.Text = rdr["EmployeeType"].ToString();

            }
            conn.Close();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (id != "")
            {
                if (txtFirstName.Text == "" || txtLastName.Text == "" || txtPosition.Text == "" || txtPhone.Text == "" || txtType.Text == "" || txtAddress.Text == "" || txtRate.Text == "")
                {
                    MessageBox.Show("No field can be empty. Fill all the required textboxes.");
                }
                else
                {
                    conn.Open();
                    SqlCommand update = new SqlCommand("UPDATE tblEmployee SET FirstName='" + txtFirstName.Text + "' ,LastName='" + txtLastName.Text + "' ,EmployeeAddress='" + txtAddress.Text + "' ,EmployeeType='" + txtType.Text + "', Phone='" + txtPhone.Text + "', Position='" + txtPosition.Text + "' ,PayRate = '" + txtRate.Text + "' where EmployeeID = '" + txtID.Text + "'", conn);
                    int r = update.ExecuteNonQuery();
                    if (r > 0)
                    {
                        MessageBox.Show("Reservation Updated successfully!");
                        this.Close();
                    }
                    conn.Close();
                }
            }
            else
            {
                if (txtFirstName.Text == "" || txtLastName.Text == "" || txtPosition.Text == "" || txtPhone.Text == "" || txtType.Text == "" || txtAddress.Text == "" || txtRate.Text == "")
                {
                    MessageBox.Show("No field can be empty. Fill all the required textboxes.");
                }
                else
                {
                    conn.Open();
                    SqlCommand insertReservation = new SqlCommand("INSERT INTO tblEmployee (EmployeeID,FirstName,LastName,EmployeeAddress,Phone,EmployeeType,Position,PayRate) VALUES (@id,@first,@last,@add,@phone,@type,@pos,@rate)", conn);
                    insertReservation.Parameters.Add(new SqlParameter("id", txtID.Text));
                    insertReservation.Parameters.Add(new SqlParameter("first", txtFirstName.Text));
                    insertReservation.Parameters.Add(new SqlParameter("last", txtLastName.Text));
                    insertReservation.Parameters.Add(new SqlParameter("add", txtAddress.Text));
                    insertReservation.Parameters.Add(new SqlParameter("phone", txtPhone.Text));
                    insertReservation.Parameters.Add(new SqlParameter("type", txtType.Text));
                    insertReservation.Parameters.Add(new SqlParameter("pos", txtPosition.Text));
                    insertReservation.Parameters.Add(new SqlParameter("rate", txtRate.Text));

                    int r = insertReservation.ExecuteNonQuery();
                    if (r == 0)
                    {
                        MessageBox.Show("Cannot insert reservation!");
                    }
                    else
                    {
                        MessageBox.Show("Employee Added Successfully!!");                        
                        this.Close();
                    }
                }
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
