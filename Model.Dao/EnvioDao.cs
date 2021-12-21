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
    public class EnvioDao
    {
        private ConexionDB objConexinDB;

        public EnvioDao()
        {
            objConexinDB = ConexionDB.saberEstado();

        }
        //Se actualiza la información del envio cuando se agrega el número de guia
        public void agregarGuia(Envio e)
        {            
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_agregarGuia";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("IdEnvio", e.IdEnvio);
            command.Parameters.AddWithValue("NumeroGuia", e.NumeroGuia);
            command.Parameters.AddWithValue("Fecha", e.Fecha);
            command.Parameters.AddWithValue("Paqueteria", e.IdPaqueteria);
            command.Parameters.AddWithValue("Usuario", e.UsuarioEnvio);
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se ejecuta el comando
            command.ExecuteNonQuery();
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
        }
        //Carga los envios que no han enviado el número de guía
        public List<Envio> cargarEnviosN()
        {

            List<Envio> listEnvios = new List<Envio>();
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_consultaEnviosN";                        
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();

            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtEnvios = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtEnvios);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            for (int i = 0; i < dtEnvios.Rows.Count; i++)
            {
                Envio e = new Envio();
                e.IdEnvio = int.Parse(dtEnvios.Rows[i]["idEnvio"].ToString());
                e.IdVenta= int.Parse(dtEnvios.Rows[i]["idVenta"].ToString());
                e.NombreCliente = dtEnvios.Rows[i]["Cliente"].ToString();
                e.Correo = dtEnvios.Rows[i]["Email"].ToString();
                listEnvios.Add(e);
            }
            //Se regresa el objeto            
            return listEnvios;
        }        
        //Trae la información del envio para el email
        public Envio datosEmail(int idEnvio)
        {
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_datosEmail";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            command.Parameters.AddWithValue("IdEnvio", idEnvio);
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtEnvios = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtEnvios);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            Envio e = new Envio();
            for (int i = 0; i < dtEnvios.Rows.Count; i++)
            {
                e.IdEnvio = int.Parse(dtEnvios.Rows[i]["idEnvio"].ToString());
                e.IdVenta = int.Parse(dtEnvios.Rows[i]["idVenta"].ToString());
                e.NombreCliente = dtEnvios.Rows[i]["nombre"].ToString();
                e.Correo = dtEnvios.Rows[i]["email"].ToString();
                e.NumeroGuia = dtEnvios.Rows[i]["numeroGuia"].ToString();
                e.Paqueteria = dtEnvios.Rows[i]["paqueteria"].ToString();
                e.Link= dtEnvios.Rows[i]["link"].ToString();
            }
            //Se regresa el objeto            
            return e;
        }
        //Carga los envios que se han enviado
        public List<Envio> cargarEnvios()
        {

            List<Envio> listEnvios = new List<Envio>();
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_consultaEnvios";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            command.Parameters.AddWithValue("IdEnvio", "");
            command.Parameters.AddWithValue("pMonth", "");
            command.Parameters.AddWithValue("pYear", "");
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtEnvios = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtEnvios);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            for (int i = 0; i < dtEnvios.Rows.Count; i++)
            {
                Envio e = new Envio();
                e.IdEnvio = int.Parse(dtEnvios.Rows[i]["idEnvio"].ToString());
                e.IdVenta = int.Parse(dtEnvios.Rows[i]["idVenta"].ToString());
                e.NombreCliente = dtEnvios.Rows[i]["Cliente"].ToString();
                e.Correo = dtEnvios.Rows[i]["email"].ToString();
                e.Fecha = Convert.ToDateTime(dtEnvios.Rows[i]["fecha"].ToString());
                e.Paqueteria= dtEnvios.Rows[i]["paqueteria"].ToString();
                e.NumeroGuia= dtEnvios.Rows[i]["numeroGuia"].ToString();
                e.UsuarioEnvio= dtEnvios.Rows[i]["Usuario"].ToString();
                listEnvios.Add(e);
            }
            //Se regresa el objeto            
            return listEnvios;
        }
        //Carga los envios que se han enviado filtrados por mes y año
        public List<Envio> cargarEnvios(string mes, string year)
        {

            List<Envio> listEnvios = new List<Envio>();
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_consultaEnvios";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            command.Parameters.AddWithValue("IdEnvio", "");
            command.Parameters.AddWithValue("pMonth", mes);
            command.Parameters.AddWithValue("pYear", year);
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtEnvios = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtEnvios);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            for (int i = 0; i < dtEnvios.Rows.Count; i++)
            {
                Envio e = new Envio();
                e.IdEnvio = int.Parse(dtEnvios.Rows[i]["idEnvio"].ToString());
                e.IdVenta = int.Parse(dtEnvios.Rows[i]["idVenta"].ToString());
                e.NombreCliente = dtEnvios.Rows[i]["Cliente"].ToString();
                e.Correo = dtEnvios.Rows[i]["email"].ToString();
                e.Fecha = Convert.ToDateTime(dtEnvios.Rows[i]["fecha"].ToString());
                e.Paqueteria = dtEnvios.Rows[i]["paqueteria"].ToString();
                e.NumeroGuia = dtEnvios.Rows[i]["numeroGuia"].ToString();
                e.UsuarioEnvio = dtEnvios.Rows[i]["Usuario"].ToString();
                listEnvios.Add(e);
            }
            //Se regresa el objeto            
            return listEnvios;
        }
        //Carga la información de un envio para el botón eliminar
        public Envio cargarEnvios(int IdEnvio)
        {            
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_consultaEnvios";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            command.Parameters.AddWithValue("IdEnvio", IdEnvio);
            command.Parameters.AddWithValue("pMonth", "");
            command.Parameters.AddWithValue("pYear", "");
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtEnvios = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtEnvios);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            Envio e = new Envio();                            
                e.IdEnvio = int.Parse(dtEnvios.Rows[0]["idEnvio"].ToString());
                e.IdVenta = int.Parse(dtEnvios.Rows[0]["idVenta"].ToString());
                e.NombreCliente = dtEnvios.Rows[0]["Cliente"].ToString();
                e.Correo = dtEnvios.Rows[0]["email"].ToString();
                e.Fecha = Convert.ToDateTime(dtEnvios.Rows[0]["fecha"].ToString());
                e.Paqueteria = dtEnvios.Rows[0]["paqueteria"].ToString();
                e.IdPaqueteria= int.Parse(dtEnvios.Rows[0]["numeroPaqueteria"].ToString());
                e.NumeroGuia = dtEnvios.Rows[0]["numeroGuia"].ToString();
                e.UsuarioEnvio = dtEnvios.Rows[0]["Usuario"].ToString();                            
            //Se regresa el objeto            
            return e;
        }
        //Carga la información de un envio para el botón eliminar
        public Envio cargarEnvio(int IdEnvio)
        {
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_consultaEnvio";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            command.Parameters.AddWithValue("IdEnvio", IdEnvio);
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtEnvios = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtEnvios);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            Envio e = new Envio();
            e.IdEnvio = int.Parse(dtEnvios.Rows[0]["idEnvio"].ToString());
            e.IdVenta = int.Parse(dtEnvios.Rows[0]["idVenta"].ToString());
            e.NombreCliente= dtEnvios.Rows[0]["Cliente"].ToString();
            e.Paqueteria = dtEnvios.Rows[0]["paqueteria"].ToString();
            e.NumeroGuia = dtEnvios.Rows[0]["numeroGuia"].ToString();            
            //Se regresa el objeto            
            return e;
        }
        public void eliminarEnvio(Envio e)
        {
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_eliminarEnvio";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("IdEnvio", e.IdEnvio);                                                
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se ejecuta el comando
            command.ExecuteNonQuery();
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
        }
        //Se actualiza la información del envio
        public void editarEnvio(Envio e)
        {
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_editarEnvio";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("IdEnvio", e.IdEnvio);
            command.Parameters.AddWithValue("NumeroGuia", e.NumeroGuia);            
            command.Parameters.AddWithValue("Paqueteria", e.IdPaqueteria);            
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
