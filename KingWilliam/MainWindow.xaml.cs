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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KingWilliam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
           
           NewReservation newReservationForm = new NewReservation();
           newReservationForm.Show();
            this.Close();


        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            EditTransaction form = new EditTransaction();
            form.Show();
            this.Close();

        }

        private void btnPricing_Click(object sender, RoutedEventArgs e)
        {
            RoomPricing form = new RoomPricing();
            form.Show();
            this.Close();
        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            RoomServices form = new RoomServices();
            form.Show();
            this.Close();
        }

        private void button_Copy4_Click(object sender, RoutedEventArgs e)
        {
            EmployeeInformation form = new EmployeeInformation();
            form.Show();
            this.Close();
        }

        private void button_Copy5_Click(object sender, RoutedEventArgs e)
        {
            GuestInformation form = new GuestInformation();
            form.Show();
            this.Close();
        }

        private void button_Copy2_Click(object sender, RoutedEventArgs e)
        {
            ReportsMenu form = new ReportsMenu();
            form.ShowDialog();
            
        }

        private void btnExistingTransaction_Click(object sender, RoutedEventArgs e)
        {
            ExistingReservation form = new ExistingReservation();
            form.ShowDialog();
            this.Close();
        }
    }
}
