using Model.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class PlantillaCorreoDao
    {
        private ConexionDB objConexionDB;
        private SqlCommand comando;
        private SqlDataReader reader;
        public PlantillaCorreoDao()
        {
            objConexionDB = ConexionDB.saberEstado();

        }
        public List<plantillaCorreo> findAll()
        {
            List<plantillaCorreo> listaPlantilla = new List<plantillaCorreo>();
            string findAll = "select * from PlantillaCorreo";
            try
            {
                comando = new SqlCommand(findAll, objConexionDB.getCon());
                if (objConexionDB.getCon().State == ConnectionState.Closed)
                {
                    objConexionDB.getCon().Open();
                }
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    plantillaCorreo objPlantilla = new plantillaCorreo();
                    objPlantilla.idPlantilla = Convert.ToInt32(reader[0].ToString());
                    objPlantilla.DescpPlantilla = reader[1].ToString();
                    objPlantilla.plantilla = reader[2].ToString();
                    listaPlantilla.Add(objPlantilla);
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                objConexionDB.getCon().Close();
                objConexionDB.closeDB();
            }

            return listaPlantilla;

        }

    }
}
