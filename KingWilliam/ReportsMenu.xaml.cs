using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KingWilliam
{
    /// <summary>
    /// Interaction logic for ReportsMenu.xaml
    /// </summary>
    public partial class ReportsMenu : Window
    {
        public ReportsMenu()
        {
            InitializeComponent();
        }

        private void button_Reservations_Click(object sender, RoutedEventArgs e)
        {
            frmReservationsReport form = new frmReservationsReport();
            form.Show();
        }

        private void button_RoomAvailability_Click(object sender, RoutedEventArgs e)
        {
            
            cnvRooms.Visibility = Visibility.Visible;
            cnvInvoice.Visibility = Visibility.Collapsed;
            btnRooms.BorderBrush = Brushes.Black;
            btnRooms.BorderThickness = new Thickness(2, 2, 2, 2);
            btnInvoice.BorderThickness = new Thickness(0, 0, 0, 0);
        }

        private void dpiStartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dpiEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnInvoice_Click(object sender, RoutedEventArgs e)
        {
            cnvRooms.Visibility = Visibility.Collapsed;
            cnvInvoice.Visibility = Visibility.Visible;
            btnInvoice.BorderThickness = new Thickness(2, 2, 2, 2);
            btnRooms.BorderThickness = new Thickness(0,0,0,0);
            btnInvoice.BorderBrush = Brushes.Black;
        }

        private void btnGenerateRooms_Click(object sender, RoutedEventArgs e)
        {
            if(dpiStartDate.Text == "" || dpiEndDate.Text == "")
            {
                MessageBox.Show("Start and End date must be entered!");
            }
            else
            {
                frmRoomAvailabilityReport form = new frmRoomAvailabilityReport(dpiStartDate.Text,dpiEndDate.Text);
                form.Show();
            }
        }

        private void btnGenerateInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (txtFind.Text == "")
            {
                MessageBox.Show("Please enter Reservation ID!");
                txtFind.Focus();
            }
            else
            {
                frmInvoiceReport form = new frmInvoiceReport(txtFind.Text);
                form.Show();
            }
        }
    }
}
