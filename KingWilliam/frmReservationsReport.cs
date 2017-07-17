using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KingWilliam
{
    public partial class frmReservationsReport : Form
    {
        public frmReservationsReport()
        {
            InitializeComponent();
        }

        //Data object declarations
        //dataset object
        private KingWilliamHotelDBDataSet ReservationsReportDataset;
        //table adapter objects
        private KingWilliamHotelDBDataSetTableAdapters.tblReservationsTableAdapter reservationsTableAdapter;
        private KingWilliamHotelDBDataSetTableAdapters.tblEmployeeTableAdapter employeeTableAdapter;
        private KingWilliamHotelDBDataSetTableAdapters.tblGuestsTableAdapter guestTableAdapter;
        private KingWilliamHotelDBDataSetTableAdapters.tblRoomsTableAdapter roomTableAdapter;
        private KingWilliamHotelDBDataSetTableAdapters.tblRoomTypeTableAdapter roomTypeTableAdapter;


        private void frmReservationsReport_Load(object sender, EventArgs e)
        {
            //Declare report object for use at runtime
            Reservations aReservationsReport;
            //Instantiate the report
            aReservationsReport = new Reservations();

            try
            {
                //Instantiate the dataset and table adapters
                ReservationsReportDataset = new KingWilliamHotelDBDataSet();
                employeeTableAdapter = new KingWilliamHotelDBDataSetTableAdapters.tblEmployeeTableAdapter();
                reservationsTableAdapter = new KingWilliamHotelDBDataSetTableAdapters.tblReservationsTableAdapter();
                guestTableAdapter = new KingWilliamHotelDBDataSetTableAdapters.tblGuestsTableAdapter();
                roomTableAdapter = new KingWilliamHotelDBDataSetTableAdapters.tblRoomsTableAdapter();
                roomTypeTableAdapter = new KingWilliamHotelDBDataSetTableAdapters.tblRoomTypeTableAdapter();

                //Fill the dataset using the table adapters
                //Fill with reservations
                reservationsTableAdapter.Fill(ReservationsReportDataset.tblReservations);
                //Fill with employees
                employeeTableAdapter.Fill(ReservationsReportDataset.tblEmployee);
                //Fill with guests
                guestTableAdapter.Fill(ReservationsReportDataset.tblGuests);
                //Fill with rooms
                roomTableAdapter.Fill(ReservationsReportDataset.tblRooms);
                //Fill with room types
                roomTypeTableAdapter.Fill(ReservationsReportDataset.tblRoomType);

                //Assign the filled dataset as the datsource for the report
                aReservationsReport.SetDataSource(ReservationsReportDataset);

                //Set up the report viewer object to show the runtime report object
                crystalReportViewer1.ReportSource = aReservationsReport;
            }
            catch (Exception dataException)
            {
                //Catch any exception thrown
                MessageBox.Show("Data Error Encountered: " + dataException.Message);
            }

        }
    }
}
