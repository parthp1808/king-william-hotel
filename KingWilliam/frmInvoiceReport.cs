using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;

namespace KingWilliam
{
    public partial class frmInvoiceReport : Form
    {
        string reservation = "";
        ReportDocument cry = new ReportDocument();

        public frmInvoiceReport(string reservation)
        {
            InitializeComponent();
            this.reservation = reservation;
        }

        private void frmInvoiceReport_Load(object sender, EventArgs e)
        {
            cry.Load(@"E:\Durham\Sem 5\DBAS\KingWilliam2.0\KingWilliamf\KingWilliam\Invoice.rpt");
            SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=KingWilliamHotelDB;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter("stpInvoice", conn);
            sda.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.AddWithValue("@reservation", reservation);
            DataSet st = new System.Data.DataSet();
            sda.Fill(st, "dtsInvoice");
            cry.SetDataSource(st.Tables[0]);


            SqlDataAdapter sd2 = new SqlDataAdapter("stpRoomInvoice", conn);
            sd2.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sd2.SelectCommand.Parameters.AddWithValue("@id", reservation);
            DataSet st2 = new System.Data.DataSet();
            sd2.Fill(st2, "dtsRoomInvoice");
            cry.OpenSubreport("RoomInvoice").SetDataSource(st2.Tables[0]);

            SqlDataAdapter sd3 = new SqlDataAdapter("stpServiceInvoice", conn);
            sd3.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sd3.SelectCommand.Parameters.AddWithValue("@id", reservation);
            DataSet st3 = new System.Data.DataSet();
            sd3.Fill(st3, "dtsServiceInvoice");
            cry.OpenSubreport("ServiceInvoice").SetDataSource(st3.Tables[0]);

            SqlDataAdapter sd4 = new SqlDataAdapter("stpResInvoice", conn);
            sd4.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sd4.SelectCommand.Parameters.AddWithValue("@id", reservation);
            DataSet st4 = new System.Data.DataSet();
            sd4.Fill(st4, "dtsRestaurantInvoice");
            cry.OpenSubreport("ResInvoice").SetDataSource(st4.Tables[0]);




            crystalReportViewer1.ReportSource = cry;
            


        }
    }
}
