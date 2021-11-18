using System.Data.SqlClient;

namespace Model.Dao
{
    public class ConexionDB
    {


        private static ConexionDB objConexionDB = null;
        private SqlConnection con;

        private ConexionDB()
        {
            con = new SqlConnection("Data Source=192.185.6.136;Initial Catalog=clouderp_conceptoselectronics;User ID=clouderp_master;Password=Slim1989!;MultipleActiveResultSets=True");
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
