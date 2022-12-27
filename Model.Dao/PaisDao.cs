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
    public class PaisDao
    {
        private ConexionDB objConexinDB;

        public PaisDao()
        {
            objConexinDB = ConexionDB.saberEstado();

        }
        //Carga los paises disponibles
        public List<Pais> cargarPaises()
        {
            
            List<Pais> listPais = new List<Pais>();
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_cargarPaises";            
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtPaises = new DataTable();
            //Se abre la conexión
            if (command.Connection.State == ConnectionState.Open)
            {
                command.Connection.Close();
            }
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtPaises);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close(); 
            for(int i = 0; i < dtPaises.Rows.Count; i++)
            {
                Pais p = new Pais();
                p.NombrePais = dtPaises.Rows[i]["nombre"].ToString();
                p.IdPais = int.Parse(dtPaises.Rows[i]["idPais"].ToString());
                listPais.Add(p);
            }
                    
            //Se regresa el objeto            
            
            return listPais;
        }
        //Carga los paises disponibles FILTRADOS
        public List<Pais> cargarPaises(string parametro)
        {

            List<Pais> listPais = new List<Pais>();
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_filtrarPaises";
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("Parametro", parametro);
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtPaises = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtPaises);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            for (int i = 0; i < dtPaises.Rows.Count; i++)
            {
                Pais p = new Pais();
                p.NombrePais = dtPaises.Rows[i]["nombre"].ToString();
                p.IdPais = int.Parse(dtPaises.Rows[i]["idPais"].ToString());
                listPais.Add(p);
            }

            //Se regresa el objeto            

            return listPais;
        }
        //Agrega un pais nuevo
        public void agregarPais(Pais p)
        {            
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_agregarPais";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("Nombre", p.NombrePais);            
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se ejecuta el comando
            command.ExecuteNonQuery();
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
        }
        //Carga la información de un pais
        public Pais cargarPais(int IdPais)
        {            
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_cargarPais";
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("IdPais", IdPais);
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtPaises = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtPaises);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();            
                Pais p = new Pais();
                p.NombrePais = dtPaises.Rows[0]["nombre"].ToString();
                p.IdPais = int.Parse(dtPaises.Rows[0]["idPais"].ToString());                           
            //Se regresa el objeto            
            return p;
        }
        //Agrega un pais nuevo
        public void editarPais(Pais p)
        {
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_editarPais";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("IdPais", p.IdPais);
            command.Parameters.AddWithValue("Nombre", p.NombrePais);
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se ejecuta el comando
            command.ExecuteNonQuery();
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
        }
        //Comprueba si hay una venta con un pais activo
        public bool hayPais(Pais p)
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
            command.Parameters.AddWithValue("IdEstado", "");
            command.Parameters.AddWithValue("IdPais", p.IdPais);
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
        //Comprueba si hay una venta con un pais activo
        public bool hayEstadoPais(Pais p)
        {
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_hayEstadoPais";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se le pasan los parametros                        
            command.Parameters.AddWithValue("IdPais", p.IdPais);
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
        //Eliminar un pais
        public void eliminarPais(Pais p)
        {
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_eliminarPais";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("IdPais", p.IdPais);            
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
