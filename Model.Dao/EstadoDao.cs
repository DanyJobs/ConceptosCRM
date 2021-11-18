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
    public class EstadoDao
    {
        private ConexionDB objConexinDB;

        public EstadoDao()
        {
            objConexinDB = ConexionDB.saberEstado();

        }
        //Carga los estados disponibles a partir del ID del pais
        public List<Estado> cargarEstados(int IdPais)
        {

            List<Estado> listEstados = new List<Estado>();
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_cargarEstados";
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("IdPais", IdPais);
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtEstados = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtEstados);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            for (int i = 0; i < dtEstados.Rows.Count; i++)
            {
                Estado e = new Estado();
                e.NombreEstado= dtEstados.Rows[i]["nombre"].ToString();
                e.IdEstado = int.Parse(dtEstados.Rows[i]["idEstado"].ToString());
                e.IdPais = int.Parse(dtEstados.Rows[i]["idPais"].ToString());
                listEstados.Add(e);
            }

            //Se regresa el objeto            
            return listEstados;
        }
        //Carga los estados disponibles a partir del ID del pais y el nombre del estado
        public List<Estado> cargarEstados(int IdPais, string txtparametro)
        {

            List<Estado> listEstados = new List<Estado>();
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_filtrarEstados";
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("Parametro", txtparametro);
            command.Parameters.AddWithValue("IdPais", IdPais);
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtEstados = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtEstados);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            for (int i = 0; i < dtEstados.Rows.Count; i++)
            {
                Estado e = new Estado();
                e.NombreEstado = dtEstados.Rows[i]["nombre"].ToString();
                e.IdEstado = int.Parse(dtEstados.Rows[i]["idEstado"].ToString());
                e.IdPais = int.Parse(dtEstados.Rows[i]["idPais"].ToString());
                listEstados.Add(e);
            }

            //Se regresa el objeto            
            return listEstados;
        }
        //Agrega un Estado nuevo
        public void agregarEstado(Estado e)
        {
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_agregarEstado";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se le pasan los parametros
            command.Parameters.AddWithValue("IdPais", e.IdPais);
            command.Parameters.AddWithValue("Nombre", e.NombreEstado);            
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se ejecuta el comando
            command.ExecuteNonQuery();
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
        }
        //Carga un estado a partir del ID del estado
        public Estado cargarEstado(int IdEstado)
        {            
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_cargarEstado";
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("IdEstado", IdEstado);
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtEstados = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtEstados);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();            
                Estado e = new Estado();
                e.NombreEstado = dtEstados.Rows[0]["nombre"].ToString();
                e.IdEstado = int.Parse(dtEstados.Rows[0]["idEstado"].ToString());
                e.IdPais = int.Parse(dtEstados.Rows[0]["idPais"].ToString());                            
            //Se regresa el objeto            
            return e;
        }
        //Edita la información de un estado
        public void editarEstado(Estado e)
        {
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_editarEstado";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se le pasan los parametros
            command.Parameters.AddWithValue("IdEstado", e.IdEstado);
            command.Parameters.AddWithValue("Nombre", e.NombreEstado);
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se ejecuta el comando
            command.ExecuteNonQuery();
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
        }
        //Comprueba si hay una venta con un estado activo
        public bool hayEstado(Estado e)
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
            command.Parameters.AddWithValue("IdCiudad", "");
            command.Parameters.AddWithValue("IdEstado", e.IdEstado);
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
        //Elimina la información de un estado
        public void eliminarEstado(Estado e)
        {
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_eliminarEstado";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se le pasan los parametros
            command.Parameters.AddWithValue("IdEstado", e.IdEstado);            
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
