using Model.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace Model.Dao
{
    
    public class CompraDao
    {        
        private ConexionDB objConexinDB;        

        public CompraDao()
        {
            objConexinDB = ConexionDB.saberEstado();

        }
        //Registrar compra
        public void create(Compra objCompra)
        {                   
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_CompraAlta";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("total", objCompra.Total);
            command.Parameters.AddWithValue("fechaCompra", objCompra.Fecha);
            command.Parameters.AddWithValue("idSucursal", objCompra.IdSucursal);
            command.Parameters.AddWithValue("idProveedor", objCompra.IdProveedor);            
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se ejecuta el comando
            command.ExecuteNonQuery();
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();

        }
        //Obtener las compras
        public DataTable consultar()
        {
            Compra c = new Compra();
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_CompraConsulta";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtCompras = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtCompras);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();            
            //Se regresa el objeto            
            return dtCompras;
        }
        //Obtener la info de una compra
        public DataTable consultar(int idCompra)
        {
            Compra c = new Compra();
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_CompraConsultaIndividual";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se pasa el parametro
            command.Parameters.AddWithValue("idCompra", idCompra);
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtCompras = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtCompras);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            //Se regresa el objeto            
            return dtCompras;

        }
        //Obtener las compras filtradas por fecha
        public DataTable filtrar(string month, string year)
        {
            Compra c = new Compra();
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_CompraFiltrado";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se pasa el parametro
            command.Parameters.AddWithValue("pMonth", month);
            command.Parameters.AddWithValue("pYear", year);
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtCompras = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtCompras);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            //Se regresa el objeto            
            return dtCompras;

        }
        //Eliminar compra
        public void eliminar(int idCompra)
        {
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_CompraEliminar";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("idCompra", idCompra);            
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se ejecuta el comando
            command.ExecuteNonQuery();
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();

        }
        //Obtener la información de la compra apartir de su id
        public Compra consultaE(int idCompra)
        {
            Compra c = new Compra();
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_CompraConsultaEditar";
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("IdCompra", idCompra);
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtCompras = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtCompras);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            c.IdCompra = idCompra;
            c.Fecha = Convert.ToDateTime(dtCompras.Rows[0]["fechaCompra"].ToString());
            c.IdProveedor=int.Parse(dtCompras.Rows[0]["idProveedor"].ToString());
            c.NombreProveedor = dtCompras.Rows[0]["NP"].ToString();
            c.IdSucursal= int.Parse(dtCompras.Rows[0]["idSucursal"].ToString());
            c.Total = Convert.ToDecimal(dtCompras.Rows[0]["total"].ToString());
            //Se regresa el objeto            
            return c;
        }
        //Actualiza al información del registro de la tabla de compra
        public void editar(int idCompra, decimal total, string fecha, int idSucursal, int idProveedor)
        {
            Compra c = new Compra();
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_CompraEditar";
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("idCompra", idCompra);
            command.Parameters.AddWithValue("total", total);
            command.Parameters.AddWithValue("fechaCompra", fecha);
            command.Parameters.AddWithValue("idSucursal", idSucursal);
            command.Parameters.AddWithValue("idProveedor", idProveedor);
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
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
