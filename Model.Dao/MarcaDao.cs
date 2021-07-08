using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;
using System.Data.SqlClient;


namespace Model.Dao
{
    public class MarcaDao:Obligatorio<Marca>
    {
        private ConexionDB objConexionDB;
        private SqlCommand comando;
        private SqlDataReader reader;
        public MarcaDao()
        {
            objConexionDB = ConexionDB.saberEstado();
        }
        public void create(Marca objMarca)
        {
            string create = "insert into marca values('" + objMarca.IdMarca + "','" + objMarca.Descripcion + "')";
            try
            {
                comando = new SqlCommand(create, objConexionDB.getCon());
                objConexionDB.getCon().Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                objMarca.Estado = 1000;
            }
            finally
            {
                objConexionDB.getCon().Close();
                objConexionDB.closeDB();
            }
        }

        public void delete(Marca objMarca)
        {
            string delete = "delete from Marca where idMarca='" + objMarca.IdMarca + "'";
            try
            {
                comando = new SqlCommand(delete, objConexionDB.getCon());
                objConexionDB.getCon().Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                objMarca.Estado = 1000;
            }
            finally
            {
                objConexionDB.getCon().Close();
                objConexionDB.closeDB();
            }
        }

        public bool find(Marca objMarca)
        {
            bool hayRegistros; 
            string find = "select * from Marca where idMarca='" + objMarca.IdMarca + "'";
            try
            {
                comando = new SqlCommand(find, objConexionDB.getCon());
                objConexionDB.getCon().Open();
                reader = comando.ExecuteReader();
                hayRegistros = reader.Read();
                if (hayRegistros)
                {
                    objMarca.Descripcion = reader[1].ToString();
                    objMarca.Estado = 99;
                }
                else
                {
                    objMarca.Estado = 1;
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

            return hayRegistros;
        }

        public List<Marca> findAll()
        {
            List<Marca> listaMarca = new List<Marca>();
            string find = "select * from Marca";
            try
            {
                comando = new SqlCommand(find, objConexionDB.getCon());
                objConexionDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Marca objMarca = new Marca();
                    objMarca.IdMarca = reader[0].ToString();
                    objMarca.Descripcion = reader[1].ToString();
                    listaMarca.Add(objMarca);
                }

            }
            catch (Exception)
            {
                //objMarca.Estado = 1000;
            }
            finally
            {
                objConexionDB.getCon().Close();
                objConexionDB.closeDB();
            }

            return listaMarca;
        }

        public void update(Marca objMarca)
        {
            string update = "update Marca set  descripcion='" + objMarca.Descripcion + "' where idMarca='" + objMarca.IdMarca + "'";
            try
            {
                comando = new SqlCommand(update, objConexionDB.getCon());
                objConexionDB.getCon().Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                objMarca.Estado = 1000;
            }
            finally
            {
                objConexionDB.getCon().Close();
                objConexionDB.closeDB();
            }
        }
    }
}
