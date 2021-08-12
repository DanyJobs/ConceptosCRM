using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;

namespace Model.Dao
{
    public class ExistenciaDao
    {
        private ConexionDB objConexinDB;
        private SqlDataReader reader;

        public ExistenciaDao()
        {
            objConexinDB = ConexionDB.saberEstado();

        }
        //Consultar la información de existencias
        public DataTable consultar()
        {                      
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_consultaExistencias";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtExistencias = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtExistencias);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            //Se regresa la tabla llena            
            return dtExistencias;                                    
        }
        //Consultar la información de existencias por sucursal
        public List<ExistenciaT> findAllExistencia(string txtProducto, int txtSucursal)
        {
            List<ExistenciaT> listaExistencia = new List<ExistenciaT>();
            try
            {
             
                //string findAll = "select*from cliente where nombre='" + objCLiente.Nombre + "' or dni='" + objCLiente.Dni + "' or idCliente=" + objCLiente.IdCliente + " or apPaterno='" + objCLiente.Appaterno + "'";
                SqlCommand cmd = new SqlCommand("sp_obtenerexistencia", objConexinDB.getCon());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@producto", txtProducto);
                cmd.Parameters.AddWithValue("@sucursal", txtSucursal);
                objConexinDB.getCon().Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ExistenciaT objExistenciaT = new ExistenciaT();
                    objExistenciaT.Nombre = reader[0].ToString();
                    objExistenciaT.Sucursal = reader[1].ToString();
                    objExistenciaT.Cantidad = Convert.ToInt32(reader[2].ToString());
                    objExistenciaT.Seccion = reader[3].ToString();
                    listaExistencia.Add(objExistenciaT);

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaExistencia;
        }
    }
}
