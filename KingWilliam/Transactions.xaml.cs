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
using System.Data;

namespace KingWilliam
{
    /// <summary>
    /// Interaction logic for Transactions.xaml
    /// </summary>
    public partial class Transactions : Window
    {
        string id = "";
        string type = "";
        string sql = "";
        SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=KingWilliamHotelDB;Integrated Security=True");

        public Transactions()
        {
            InitializeComponent();
        }
        public Transactions(string id,string type)
        {
            this.type = type;
            if(type == "s")
            {
                sql = "select serviceTransactionID as'Transaction ID', servicedescription as 'Description', servicedate as Date,amount as Amount from tblServicesTransactions inner join tblServices on tblServicesTransactions.ServiceID = tblServices.ServiceId where ReservationID = '" + id + "'";

            }
            else
            {
                sql = "select orderID as'Transaction ID', Transactiondate as Date,Cost as Amount from tblRestaurantTransactions where ReservationID = '" + id + "'";

            }
            InitializeComponent();
            this.id = id;
             SqlCommand tr = new SqlCommand(sql,conn);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(tr); //c.con is the connection string

            DataTable dtRecord = new DataTable();
            dataAdapter.Fill(dtRecord);
            dtgTransactions.ItemsSource = dtRecord.DefaultView;
            
        }

        private void dtgTransactions_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
        private void Edit(object sender, RoutedEventArgs e)
        {
            int id = getID(sender, e);
            if(id > 0)
            {
                EditTransaction.transactionid = id;
                this.Close();
            }


        }

        private int getID(object sender, RoutedEventArgs e)
        {
            int s = 0;
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
            {
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    int i = row.GetIndex();
                    DataRowView v = (DataRowView)dtgTransactions.Items[i];  // this give you access to the row
                    s = (int)v[0];
                    break;
                }
            }
            return s;
        }
        private void Delete(object sender, RoutedEventArgs e)
        {
            int id = getID(sender, e);
            SqlCommand delete = null;
            MessageBoxResult result = MessageBox.Show("Are you sure about deleting transaction?", "Deleting Transaction", MessageBoxButton.YesNo);
            conn.Open();
            if(result == MessageBoxResult.Yes)
            {
                if (id > 0)
                {
                    if (type == "s")
                    {
                        delete = new SqlCommand("delete from tblServicesTransactions where serviceTransactionID = '" + id + "'",conn);
                        
                    }
                    else
                    {
                        delete = new SqlCommand("delete from tblRestaurantTransactions where OrderID = '" + id + "'",conn);
                       
                    }

                    int r = delete.ExecuteNonQuery();
                    if (r > 0)
                    {
                        MessageBox.Show("Transaction Deleted Successfully!");
                       
                        this.Close();
                    }
                }
            }
            conn.Close();
            
            
            
        }
    }
}
