using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFacturaMvc.Reportes.Espanol
{
    public partial class frmReporteEs : System.Web.UI.Page
    {
        public static SqlConnection con;
        SqlCommand comando;
        SqlDataAdapter adapter;
        SqlParameter param;
        string idVenta;


        protected void Page_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=192.185.6.136;Initial Catalog=clouderp_conceptoselectronics;User ID=clouderp_master;Password=Slim1989!;MultipleActiveResultSets=True");
            if (!IsPostBack)
            {
                renderReport();

            }
        }

        public void renderReport()
        {
            DateTime fechaActual = DateTime.Today;
            string fechaQuote = string.Format("{0}{1}{2}", fechaActual.Month, fechaActual.Day, fechaActual.Year);

            idVenta = Request.QueryString.Get("IdVenta");

            DataTable dt = cargar(idVenta);
            ReportDataSource rds = new ReportDataSource("DataSet1", dt);
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.ReportPath = "Reportes/Espanol/rptFactura.rdlc";
            ReportViewer1.LocalReport.DisplayName = "Quote "+idVenta.ToString()+ fechaQuote;
            //parameters
            ReportParameter[] rptParams = new ReportParameter[]
            {
                new ReportParameter("idVenta",idVenta.ToString())
        };
            ReportViewer1.LocalReport.Refresh();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        public static DataTable cargar(string codigoventa)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection("Data Source=192.185.6.136;Initial Catalog=clouderp_conceptoselectronics;User ID=clouderp_master;Password=Slim1989!;MultipleActiveResultSets=True"))
            {

                SqlCommand cmd = new SqlCommand("sp_reporte_venta", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idVenta", SqlDbType.Int).Value = codigoventa;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
            }
            return dt;

        }
    }
}