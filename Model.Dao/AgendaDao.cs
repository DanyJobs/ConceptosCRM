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
    public class AgendaDao
    {
        private ConexionDB objConexinDB;

        public AgendaDao()
        {
            objConexinDB = ConexionDB.saberEstado();
        }
        //Carga los eventos de la agenda del día actual
        public List<Agenda> cargarAgendaHoy(string idUsuario,string fecha)
        {

            List<Agenda> listeventos = new List<Agenda>();
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_cargarAgendaHoy";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            command.Parameters.AddWithValue("IdUsuario", idUsuario);
            command.Parameters.AddWithValue("Fecha", fecha);            
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtEventos = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtEventos);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            for (int i = 0; i < dtEventos.Rows.Count; i++)
            {
                Agenda a = new Agenda();
                a.IdEvento = int.Parse(dtEventos.Rows[i]["idEvento"].ToString());
                a.IdUsuario = dtEventos.Rows[i]["idUsuario"].ToString();
                a.Titulo= dtEventos.Rows[i]["titulo"].ToString();                
                a.Fecha = Convert.ToDateTime(dtEventos.Rows[i]["fecha"].ToString());
                a.Hora= dtEventos.Rows[i]["hora"].ToString();                
                listeventos.Add(a);
            }
            //Se regresa el objeto            
            return listeventos;
        }
        //Carga la información de un evento en particular
        public Agenda cargarEvento(int idEvento)
        {
            
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_cargarEventoAgenda";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            command.Parameters.AddWithValue("IdEvento", idEvento);            
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtEventos = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtEventos);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            Agenda a = new Agenda();
            for (int i = 0; i < dtEventos.Rows.Count; i++)
            {
                
                a.IdEvento = int.Parse(dtEventos.Rows[i]["idEvento"].ToString());
                a.IdUsuario = dtEventos.Rows[i]["idUsuario"].ToString();
                a.Titulo = dtEventos.Rows[i]["titulo"].ToString();
                a.Descripcion = dtEventos.Rows[i]["descripcion"].ToString();
                a.Fecha = Convert.ToDateTime(dtEventos.Rows[i]["fecha"].ToString());
                a.Hora = dtEventos.Rows[i]["hora"].ToString();
                a.Link = dtEventos.Rows[i]["link_reunion"].ToString();   
                a.Direccion= dtEventos.Rows[i]["direccion"].ToString();
            }
            //Se regresa el objeto            
            return a;
        }
        //Agrega la información de un evento a la tabla agenda
        public void agregarEvento(Agenda evento)
        {

            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_agregarEventoAgenda";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();            
            command.Parameters.AddWithValue("Usuario", evento.IdUsuario);
            command.Parameters.AddWithValue("Titulo", evento.Titulo);
            command.Parameters.AddWithValue("Descripcion", evento.Descripcion);
            command.Parameters.AddWithValue("Fecha", evento.Fecha);
            command.Parameters.AddWithValue("Hora", evento.Hora);
            command.Parameters.AddWithValue("Link", evento.Link);
            command.Parameters.AddWithValue("Direccion", evento.Direccion);
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtEventos = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtEventos);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();            
        }
        //Verifica si existe en evento a la misma fecha y hora que otro
        public List<Agenda> verificarEvento(DateTime fecha, string hora, string usuario)
        {
            List <Agenda> lista = new List<Agenda>();
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_verificarEvento";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();            
            command.Parameters.AddWithValue("Fecha", fecha);
            command.Parameters.AddWithValue("Hora", hora);
            command.Parameters.AddWithValue("Usuario", usuario);            
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtEventos = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtEventos);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            Agenda a = new Agenda();
            for (int i = 0; i < dtEventos.Rows.Count; i++)
            {

                a.IdEvento = int.Parse(dtEventos.Rows[i]["idEvento"].ToString());
                a.IdUsuario = dtEventos.Rows[i]["idUsuario"].ToString();
                lista.Add(a);

            }
            //Se regresa el objeto            
            return lista;
        }
        //Verifica si existe en evento a la misma fecha y hora que otro pero para la tabla editar
        public List<Agenda> verificarEvento2(Agenda evento)
        {
            List<Agenda> lista = new List<Agenda>();
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_verificarEvento2";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            command.Parameters.AddWithValue("Fecha", evento.Fecha);
            command.Parameters.AddWithValue("Hora", evento.Hora);
            command.Parameters.AddWithValue("Usuario", evento.IdUsuario);
            command.Parameters.AddWithValue("IdEvento", evento.IdEvento);
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtEventos = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtEventos);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            Agenda a = new Agenda();
            for (int i = 0; i < dtEventos.Rows.Count; i++)
            {
                a.IdEvento = int.Parse(dtEventos.Rows[i]["idEvento"].ToString());
                a.IdUsuario = dtEventos.Rows[i]["idUsuario"].ToString();
                lista.Add(a);
            }
            //Se regresa el objeto            
            return lista;
        }
        //Editar la información de un evento a la tabla agenda
        public void editarEvento(Agenda evento)
        {

            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_editarEventoAgenda";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            command.Parameters.AddWithValue("IdEvento", evento.IdEvento);
            command.Parameters.AddWithValue("Titulo", evento.Titulo);
            command.Parameters.AddWithValue("Descripcion", evento.Descripcion);
            command.Parameters.AddWithValue("Fecha", evento.Fecha);
            command.Parameters.AddWithValue("Hora", evento.Hora);
            command.Parameters.AddWithValue("Link", evento.Link);
            command.Parameters.AddWithValue("Direccion", evento.Direccion);
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtEventos = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtEventos);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
        }
        //Elimina un evento de la agenda
        public void eliminarEvento(Agenda evento)
        {

            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_eliminarEventoAgenda";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            command.Parameters.AddWithValue("IdEvento", evento.IdEvento);         
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtEventos = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtEventos);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
        }
        //Carga los eventos según un periodo de fecha
        public List<Agenda> filtrarEventos(string Day, string Month, string Year, string Usuario)
        {

            List<Agenda> listeventos = new List<Agenda>();
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_filtrarEventos";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            command.Parameters.AddWithValue("pMonth", Month);
            command.Parameters.AddWithValue("pYear", Year);
            command.Parameters.AddWithValue("pDay", Day);
            command.Parameters.AddWithValue("pUsuario", Usuario);
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtEventos = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtEventos);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            for (int i = 0; i < dtEventos.Rows.Count; i++)
            {
                Agenda a = new Agenda();
                a.IdEvento = int.Parse(dtEventos.Rows[i]["idEvento"].ToString());
                a.IdUsuario = dtEventos.Rows[i]["idUsuario"].ToString();
                a.Titulo = dtEventos.Rows[i]["titulo"].ToString();
                a.Fecha = Convert.ToDateTime(dtEventos.Rows[i]["fecha"].ToString());
                a.Hora = dtEventos.Rows[i]["hora"].ToString();
                listeventos.Add(a);
            }
            //Se regresa el objeto            
            return listeventos;
        }
    }
}
