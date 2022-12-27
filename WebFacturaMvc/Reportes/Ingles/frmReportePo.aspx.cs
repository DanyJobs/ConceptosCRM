using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFacturaMvc.Reportes.Ingles
{
    public partial class frmReportePo : System.Web.UI.Page
    {
        public static SqlConnection con;
        SqlCommand comando;
        SqlDataAdapter adapter;
        SqlParameter param;
        string idVenta;
        string idProveedor;
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
            idVenta = Request.QueryString.Get("idVenta");
            idProveedor = Request.QueryString.Get("idProveedor");
            DataTable dt = cargar(idVenta, idProveedor);
            ReportDataSource rds = new ReportDataSource("PurchaseOrder", dt);
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.ReportPath = "Reportes/Ingles/ReportPo.rdlc";          
            ReportViewer1.LocalReport.DisplayName = idVenta;
            //parameters
            ReportParameter[] rptParams = new ReportParameter[]
            {
                new ReportParameter("idVenta",idVenta.ToString()),
                new ReportParameter("idProveedor",idProveedor.ToString())
            };
            ReportViewer1.LocalReport.Refresh();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        public static DataTable cargar(string codigoventa,string proveedor)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection("Data Source=192.185.6.136;Initial Catalog=clouderp_conceptoselectronics;User ID=clouderp_master;Password=Slim1989!;MultipleActiveResultSets=True"))
            {
                SqlCommand cmd = new SqlCommand("sp_reporte_po", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idVenta", SqlDbType.Int).Value = codigoventa;
                cmd.Parameters.Add("@idProveedor", SqlDbType.Int).Value = proveedor;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
            }
            return dt;

        }
    }
}