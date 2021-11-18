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
    public class CiudadDao
    {
        private ConexionDB objConexinDB;

        public CiudadDao()
        {
            objConexinDB = ConexionDB.saberEstado();

        }
        //Carga las ciudades disponibles a partir del ID del estado
        public List<Ciudad> cargarCiudades(int IdEstado)
        {

            List<Ciudad> listCiudades = new List<Ciudad>();
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_cargarCiudades";
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("IdEstado", IdEstado);
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtCiudades = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtCiudades);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            for (int i = 0; i < dtCiudades.Rows.Count; i++)
            {
                Ciudad c = new Ciudad();
                c.NombreCiudad = dtCiudades.Rows[i]["nombre"].ToString();
                c.IdCiudad = int.Parse(dtCiudades.Rows[i]["idCiudad"].ToString());
                c.IdEstado = int.Parse(dtCiudades.Rows[i]["idEstado"].ToString());
                c.IdPais = int.Parse(dtCiudades.Rows[i]["Pais"].ToString());
                listCiudades.Add(c);
            }

            //Se regresa el objeto            
            return listCiudades;
        }
        //Trae una sola ciudad para poder regresar el Estado Index con el IdPais
        public Ciudad cargarCiudadPais(int IdEstado)
        {
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_cargarCiudades";
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("IdEstado", IdEstado);
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtCiudades = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtCiudades);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            Ciudad c = new Ciudad();
            if (dtCiudades.Rows.Count != 0)
            {
                c.NombreCiudad = dtCiudades.Rows[0]["nombre"].ToString();
                c.IdCiudad = int.Parse(dtCiudades.Rows[0]["idCiudad"].ToString());
                c.IdEstado = int.Parse(dtCiudades.Rows[0]["idEstado"].ToString());
                c.IdPais = int.Parse(dtCiudades.Rows[0]["Pais"].ToString());
            }
            else
            {
                EstadoDao es = new EstadoDao();
                Estado e = new Estado();
                e = es.cargarEstado(IdEstado);
                c.IdPais = e.IdPais;
            }            
            //Se regresa el objeto            
            return c;
        }
        //Carga las ciudades filtradas disponibles a partir del ID del estado
        public List<Ciudad> cargarCiudades(int IdEstado, string Parametro)
        {

            List<Ciudad> listCiudades = new List<Ciudad>();
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_filtrarCiudades";
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("Parametro", Parametro);
            command.Parameters.AddWithValue("IdEstado", IdEstado);
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtCiudades = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtCiudades);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            for (int i = 0; i < dtCiudades.Rows.Count; i++)
            {
                Ciudad c = new Ciudad();
                c.NombreCiudad = dtCiudades.Rows[i]["nombre"].ToString();
                c.IdCiudad = int.Parse(dtCiudades.Rows[i]["idCiudad"].ToString());
                c.IdEstado = int.Parse(dtCiudades.Rows[i]["idEstado"].ToString());
                c.IdPais = int.Parse(dtCiudades.Rows[i]["Pais"].ToString());
                listCiudades.Add(c);
            }

            //Se regresa el objeto            
            return listCiudades;
        }
        //Agrega una Ciudad nueva
        public void agregarCiudad(Ciudad c)
        {
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_agregarCiudad";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se le pasan los parametros
            command.Parameters.AddWithValue("IdEstado",c.IdEstado);
            command.Parameters.AddWithValue("Nombre", c.NombreCiudad);
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se ejecuta el comando
            command.ExecuteNonQuery();
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
        }
        //Trae una sola ciudad a partir del IdCiudad
        public Ciudad cargarCiudad(int IdCiudad)
        {
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_cargarCiudad";
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("IdCiudad", IdCiudad);
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtCiudades = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtCiudades);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            Ciudad c = new Ciudad();            
                c.NombreCiudad = dtCiudades.Rows[0]["nombre"].ToString();
                c.IdCiudad = int.Parse(dtCiudades.Rows[0]["idCiudad"].ToString());
                c.IdEstado = int.Parse(dtCiudades.Rows[0]["idEstado"].ToString());                
            //Se regresa el objeto            
            return c;
        }
        //Edita una Ciudad
        public void editarCiudad(Ciudad c)
        {
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_editarCiudad";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se le pasan los parametros
            command.Parameters.AddWithValue("IdCiudad", c.IdCiudad);
            command.Parameters.AddWithValue("Nombre", c.NombreCiudad);
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se ejecuta el comando
            command.ExecuteNonQuery();
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
        }
        //Comprueba si hay una venta con una ciudad activa
        public bool hayCiudad(Ciudad c)
        {
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_hayCiudad";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se le pasan los parametros
            command.Parameters.AddWithValue("IdCiudad", c.IdCiudad);
            command.Parameters.AddWithValue("IdEstado", "");
            command.Parameters.AddWithValue("IdPais", "");
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtCiudades = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtCiudades);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            bool respuesta;
            if (dtCiudades.Rows.Count != 0)
            {
                respuesta = true;
            }
            else
            {
                respuesta = false;
            }
            return respuesta;                        
        }
        //Elimina una ciudad
        public void eliminarCiudad(Ciudad c)
        {
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_eliminarCiudad";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se le pasan los parametros
            command.Parameters.AddWithValue("IdCiudad", c.IdCiudad);            
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se ejecuta el comando
            command.ExecuteNonQuery();
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
        }
    }
}
