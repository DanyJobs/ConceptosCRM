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
    public partial class frmReporteEn : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand comando;
        SqlDataAdapter adapter;
        SqlParameter param;
        string idVenta;


        protected void Page_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=den1.mssql8.gear.host;Initial Catalog=crmconceptose;User ID=crmconceptose;Password=Cj999l~!3ZA2;");
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

        public DataTable cargar(string codigoventa)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection("Data Source=den1.mssql8.gear.host;Initial Catalog=crmconceptose;User ID=crmconceptose;Password=Cj999l~!3ZA2;"))
            {

                SqlCommand cmd = new SqlCommand("sp_reporte_venta", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idVenta", SqlDbType.Int).Value = idVenta;

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
            }
            return dt;

        }
    }
}