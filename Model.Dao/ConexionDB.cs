using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
   public class ConexionDB
    {


        private static ConexionDB objConexionDB=null;
        private SqlConnection con;

        private ConexionDB()
        {
            con = new SqlConnection("Data Source=den1.mssql8.gear.host; Initial Catalog=crmconceptose; User ID=crmconceptose; Password=Cj999l~!3ZA2;");
        }

        public static ConexionDB saberEstado()
        {
            if (objConexionDB == null)
            {
                objConexionDB = new ConexionDB();

            }
            return objConexionDB;
        }

        public SqlConnection getCon()
        {
            return con;
        }

        public void closeDB()
        {
            objConexionDB = null;
        }
    }
}
