using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using System.Data.SqlClient;

namespace KingWilliam
{
    public partial class frmRoomAvailabilityReport : Form
    {
        string start = "";
        string end = "";
        ReportDocument cry = new ReportDocument();
        public frmRoomAvailabilityReport(string start, string end)
        {
            InitializeComponent();
            this.start = start;
            this.end = end;
        }

        
        //Data object declarations
        //dataset object
        //private KingWilliamHotelDBDataSet RoomAvailabilityReportDataset;
        ////table adapter objects
        //private KingWilliamHotelDBDataSetTableAdapters.tblRoomsTableAdapter roomTableAdapter;
        //private KingWilliamHotelDBDataSetTableAdapters.tblRoomStatusesTableAdapter roomStatusTableAdapter;

        private void frmRoomAvailabilityReport_Load(object sender, EventArgs e)
        {
            ////Declare report object for use at runtime
            //RoomAvailability aRoomAvailabilityReport;
            ////Instantiate the report
            //aRoomAvailabilityReport = new RoomAvailability();

            //try
            //{
            //    //Instantiate the dataset and table adapters
            //    RoomAvailabilityReportDataset = new KingWilliamHotelDBDataSet();
            //    roomTableAdapter = new KingWilliamHotelDBDataSetTableAdapters.tblRoomsTableAdapter();
            //    roomStatusTableAdapter = new KingWilliamHotelDBDataSetTableAdapters.tblRoomStatusesTableAdapter();

            //    //Fill the dataset using the table adapters
            //    //Fill with rooms
            //    roomTableAdapter.Fill(RoomAvailabilityReportDataset.tblRooms);
            //    //Fill with room statuses
            //    roomStatusTableAdapter.Fill(RoomAvailabilityReportDataset.tblRoomStatuses);

            //    //Assign the filled dataset as the datsource for the report
            //    aRoomAvailabilityReport.SetDataSource(RoomAvailabilityReportDataset);

            //    //Set up the report viewer object to show the runtime report object
            //    crystalReportViewer1.ReportSource = aRoomAvailabilityReport;
            //}
            //catch (Exception dataException)
            //{
            //    //Catch any exception thrown
            //    MessageBox.Show("Data Error Encountered: " + dataException.Message);
            //}

            cry.Load(@"E:\Durham\Sem 5\DBAS\KingWilliam2.0\KingWilliamf\KingWilliam\Rooms.rpt");
            SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=KingWilliamHotelDB;Integrated Security=True");
            //String sql = "select tblRooms.RoomID, tblRooms.RoomFloor,tblRoomStatuses.StatusDescription from tblRooms inner join tblRoomStatuses on tblRoomStatuses.StatusID = tblRooms.StatusID  where tblRooms.RoomID not in (select RoomID from  tblReservations where ReservationStartDate < CONVERT (date, SYSDATETIME()) and ReservationEndDate > CONVERT (date, SYSDATETIME()))";
            SqlDataAdapter sda = new SqlDataAdapter("stpRooms",conn);
            sda.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.AddWithValue("@start", start);
            sda.SelectCommand.Parameters.AddWithValue("@end", end);
            DataSet st = new System.Data.DataSet();
            sda.Fill(st, "dtsRooms");
            cry.SetDataSource(st.Tables[0]);
            crystalReportViewer1.ReportSource = cry;


        }
    }
}
