using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;


namespace WebFacturaMvc.Reportes.Ingles
{
    public partial  class frmReporteEn : System.Web.UI.Page
    {
        private static SqlConnection con;
        SqlCommand comando;
        SqlDataAdapter adapter;
        SqlParameter param;
        string idVenta;
        string NoCotizacion;


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
           
            idVenta = Request.QueryString.Get("IdVenta");
            DataTable dt = cargar(idVenta);
            ReportDataSource rds = new ReportDataSource("DataSet1", dt);
            Reporte1.LocalReport.DataSources.Add(rds);
            Reporte1.LocalReport.ReportPath = "Reportes/Ingles/rptFacturaEn.rdlc";
            NoCotizacion = dt.Rows[0]["NoCotizacion"].ToString();
            Reporte1.LocalReport.DisplayName = NoCotizacion;
            //parameters
            ReportParameter[] rptParams = new ReportParameter[]
            {
                new ReportParameter("idVenta",idVenta.ToString())
        };
            Reporte1.LocalReport.Refresh();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
       
        public static DataTable cargar(string codigoventa)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection("Data Source=192.185.6.136;Initial Catalog=clouderp_conceptoselectronics;User ID=clouderp_master;Password=Slim1989!;MultipleActiveResultSets=True"))
            {

                SqlCommand cmd = new SqlCommand("sp_reporte_venta", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idVenta", SqlDbType.Int).Value = codigoventa;

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
            }
            return dt;

        }
    }
}