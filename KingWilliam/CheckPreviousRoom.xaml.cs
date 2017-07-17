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
    /// Interaction logic for CheckPreviousRoom.xaml
    /// </summary>
    public partial class CheckPreviousRoom : Window
    {
        public CheckPreviousRoom()
        {
            InitializeComponent();
        }

        public CheckPreviousRoom(string value)
        {
            InitializeComponent();
            SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=KingWilliamHotelDB;Integrated Security=True");
            conn.Open();


            string sql = "select RoomID, CONVERT(VARCHAR(11),DateMade,106) as Date from tblReservations where GuestID = @id order by DateMade desc";
            SqlCommand com = new SqlCommand(sql, conn);
            com.Parameters.AddWithValue("@id",value);

            using (SqlDataAdapter adapter = new SqlDataAdapter(com))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dtgCheck.ItemsSource = dt.DefaultView;
            }
        }
    }
}
