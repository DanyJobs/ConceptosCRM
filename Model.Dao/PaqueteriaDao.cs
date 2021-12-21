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
    public class PaqueteriaDao
    {
        private ConexionDB objConexinDB;

        public PaqueteriaDao()
        {
            objConexinDB = ConexionDB.saberEstado();

        }
        //Carga las paqueterias disponibles
        public List<Paqueteria> cargarPaqueterias()
        {
            List<Paqueteria> listPaqueteria = new List<Paqueteria>();
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_cargarPaqueterias";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtPaqueterias = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtPaqueterias);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            for (int i = 0; i < dtPaqueterias.Rows.Count; i++)
            {
                Paqueteria p = new Paqueteria();
                p.IdPaqueteria = int.Parse(dtPaqueterias.Rows[i]["idPaqueteria"].ToString());
                p.Nombre = dtPaqueterias.Rows[i]["nombre"].ToString();
                listPaqueteria.Add(p);
            }
            //Se regresa el objeto            
            return listPaqueteria;
        }
    }
}
