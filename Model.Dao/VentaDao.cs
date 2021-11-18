using Model.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Model.Dao
{
    public class VentaDao
    {
        private ConexionDB objConexinDB;
        private SqlCommand comando;
        private SqlDataReader reader;
        public VentaDao()
        {
            objConexinDB = ConexionDB.saberEstado();

        }
        //Para la parte de mostrar las cotizaciones y generar la venta
        //Trae las cotizaciones sin necesitar ningun parametro
        public List<Cotizacion> buscar()
        {
            List<Cotizacion> listaVentas = new List<Cotizacion>();
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_consultaVentas";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("pMonth", "");
            command.Parameters.AddWithValue("pYear", "");
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtCotizacion = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtCotizacion);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            //Se llena la lista
            for (int i = 0; i < dtCotizacion.Rows.Count; i++)
            {
                Cotizacion c = new Cotizacion();
                c.IdVenta = int.Parse(dtCotizacion.Rows[i]["idVenta"].ToString());
                c.Total = Convert.ToDouble(dtCotizacion.Rows[i]["total"].ToString());
                c.Cliente = dtCotizacion.Rows[i]["Cliente"].ToString();
                c.FechaCotizacion = Convert.ToDateTime(dtCotizacion.Rows[i]["fecha"].ToString());
                c.Iva = Convert.ToDouble(dtCotizacion.Rows[i]["IVA"].ToString());
                c.estatus = dtCotizacion.Rows[i]["estatusCotizacion"].ToString();
                listaVentas.Add(c);
            }
            //Se regresa el objeto            
            return listaVentas;
        }
        //Trae las cotizaciones según el año y mes
        public List<Cotizacion> buscar(string Month, string Year)
        {
            List<Cotizacion> listaVentas = new List<Cotizacion>();
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_consultaVentas";
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("pMonth", Month);
            command.Parameters.AddWithValue("pYear", Year);
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtCotizacion = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtCotizacion);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            //Se llena la lista
            for (int i = 0; i < dtCotizacion.Rows.Count; i++)
            {
                Cotizacion c = new Cotizacion();
                c.IdVenta = int.Parse(dtCotizacion.Rows[i]["idVenta"].ToString());
                c.Total = Convert.ToDouble(dtCotizacion.Rows[i]["total"].ToString());
                c.Cliente = dtCotizacion.Rows[i]["Cliente"].ToString();
                c.FechaCotizacion = Convert.ToDateTime(dtCotizacion.Rows[i]["fecha"].ToString());
                c.Iva = Convert.ToDouble(dtCotizacion.Rows[i]["IVA"].ToString());
                c.estatus = dtCotizacion.Rows[i]["estatusCotizacion"].ToString();
                listaVentas.Add(c);
            }
            //Se regresa el objeto            
            return listaVentas;
        }
        //Trae la información del cliente y el total de la venta
        public DataTable consulta_VC(int IdCotizacion)
        {
            List<Cotizacion> listaVentas = new List<Cotizacion>();
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_consultaVC";
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("IdCotizacion", IdCotizacion);            
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtCotizacion = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtCotizacion);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();            
            //Se regresa el objeto            
            return dtCotizacion;
        }
        //Trae el nombre del Sales Person
        public DataTable consulta_SalesPerson(int IdCotizacion)
        {
            
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_consultaSalesPerson";
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("IdCotizacion", IdCotizacion);
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtCotizacion = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtCotizacion);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();            
            //Se regresa el objeto            
            return dtCotizacion;
        }
        //Procedimiento para guardar la información en la tabla de ventas
        public void create(Venta objVenta)
        {                   
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_guardarVenta";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("IdCotizacion", objVenta.IdCotizacion);
            command.Parameters.AddWithValue("IdCliente", objVenta.IdCliente);
            command.Parameters.AddWithValue("CP", objVenta.CP);
            command.Parameters.AddWithValue("IdCiudad", objVenta.IdCiudad);
            command.Parameters.AddWithValue("Calle", objVenta.Calle);
            command.Parameters.AddWithValue("NumExt", objVenta.NumExterior);
            command.Parameters.AddWithValue("NumInt", objVenta.NumInterior);
            command.Parameters.AddWithValue("Colonia", objVenta.Colonia);
            command.Parameters.AddWithValue("Telefono", objVenta.Telefono);
            command.Parameters.AddWithValue("Archivo", objVenta.Archivo);
            command.Parameters.AddWithValue("Fecha", objVenta.Fecha);
            command.Parameters.AddWithValue("Vendedor", objVenta.Vendedor);
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se ejecuta el comando
            command.ExecuteNonQuery();
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();

        }
        //Trae las ventas realizadas a partir de una cotizacion N que se convirtiern en W o G SIN PARAMETROS
        public List<Venta> consulta_Ventas()
        {
            List<Venta> listVentas = new List<Venta>();

            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_consultaVT";
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("pMonth", "");
            command.Parameters.AddWithValue("pYear", "");
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtVenta = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtVenta);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            //Se llena la lista
            for (int i = 0; i < dtVenta.Rows.Count; i++)
            {
                Venta v = new Venta();
                v.IdVenta = int.Parse(dtVenta.Rows[i]["idVenta"].ToString());
                v.IdCotizacion= int.Parse(dtVenta.Rows[i]["idCotizacion"].ToString());
                v.Vendedor= dtVenta.Rows[i]["nombre"].ToString();
                v.Total = dtVenta.Rows[i]["total"].ToString();
                v.NombreCliente = dtVenta.Rows[i]["Cliente"].ToString();
                v.Fecha = Convert.ToDateTime(dtVenta.Rows[i]["fecha"]);                                
                listVentas.Add(v);
            }
            //Se regresa el objeto            
            return listVentas;
        }
        //Trae las ventas realizadas a partir de una cotizacion N que se convirtiern en W o G CON PARAMETROS
        public List<Venta> consulta_Ventas(string Month, string Year)
        {
            List<Venta> listVentas = new List<Venta>();

            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_consultaVT";
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("pMonth", Month);
            command.Parameters.AddWithValue("pYear", Year);
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtVenta = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtVenta);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            //Se llena la lista
            for (int i = 0; i < dtVenta.Rows.Count; i++)
            {
                Venta v = new Venta();
                v.IdVenta = int.Parse(dtVenta.Rows[i]["idVenta"].ToString());
                v.IdCotizacion = int.Parse(dtVenta.Rows[i]["idCotizacion"].ToString());
                v.Vendedor = dtVenta.Rows[i]["nombre"].ToString();
                v.Total = dtVenta.Rows[i]["total"].ToString();
                v.NombreCliente = dtVenta.Rows[i]["Cliente"].ToString();
                v.Fecha = Convert.ToDateTime(dtVenta.Rows[i]["fecha"]);
                listVentas.Add(v);
            }
            //Se regresa el objeto            
            return listVentas;
        }
        //Trae los bytes del archivo de la orden de compra
        public Venta VerOrdenCompra(int idVenta)
        {
            List<Venta> listVentas = new List<Venta>();

            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_VerOrdenCompra";
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("IdVenta", idVenta);            
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtVenta = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtVenta);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            //Se llena el objeto
            Venta v = new Venta();
            for (int i = 0; i < dtVenta.Rows.Count; i++)
            {                
                v.IdVenta = int.Parse(dtVenta.Rows[i]["idVenta"].ToString());
                v.Archivo = Encoding.ASCII.GetBytes(dtVenta.Rows[i]["archivo"].ToString());   
                v.ArchivoTemporal= dtVenta.Rows[i]["archivo"].ToString();
        }
            //Se regresa el objeto            
            return v;
        }
        //Trae los bytes del archivo de la orden de compra
        public byte[] traerBytes(int idVenta)
        {
            List<Venta> listVentas = new List<Venta>();

            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_traerBytes";
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("IdVenta", idVenta);
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtVenta = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtVenta);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            //Se llena el objeto
                        
            //Se regresa el objeto            
            return dtVenta.Rows[0]["archivo"] as byte[];
        }
        //Trae la información de una venta realizada
        public Venta VerVenta(int idVenta)
        {
            
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_consultaVI";
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("IdVenta", idVenta);
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtVenta = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtVenta);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            //Se llena el objeto
            Venta v = new Venta();
            for (int i = 0; i < dtVenta.Rows.Count; i++)
            {
                v.IdCotizacion= int.Parse(dtVenta.Rows[i]["idCotizacion"].ToString());
                v.IdVenta = int.Parse(dtVenta.Rows[i]["idVenta"].ToString());
                v.Vendedor = dtVenta.Rows[i]["Nombre"].ToString();
                v.Fecha = Convert.ToDateTime(dtVenta.Rows[i]["fecha"].ToString());
                v.NombreCliente = dtVenta.Rows[i]["Cliente"].ToString();
                v.IdCliente = int.Parse(dtVenta.Rows[i]["idCliente"].ToString());
                v.Email= dtVenta.Rows[i]["email"].ToString();
                v.CP= dtVenta.Rows[i]["codigoPostal"].ToString();
                v.IdCiudad = int.Parse(dtVenta.Rows[i]["idCiudad"].ToString());
                v.Colonia= dtVenta.Rows[i]["colonia"].ToString();
                v.Calle= dtVenta.Rows[i]["calle"].ToString();
                v.NumExterior= dtVenta.Rows[i]["numeroExt"].ToString();
                v.NumInterior= dtVenta.Rows[i]["numeroInt"].ToString();
                v.Telefono= dtVenta.Rows[i]["telefonoContacto"].ToString();
                v.Total= dtVenta.Rows[i]["Total"].ToString();
                v.IdEstado= int.Parse(dtVenta.Rows[i]["idEstado"].ToString());
                v.IdPais = int.Parse(dtVenta.Rows[i]["idPais"].ToString());
                v.ArchivoTemporal = dtVenta.Rows[i]["Archivo"].ToString();
            }
            //Se regresa el objeto            
            return v;
        }
        //Elimina una Venta
        public void eliminarVenta(int idVenta)
        {
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_eliminarVenta";
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("IdVenta", idVenta);
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();            
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se ejecuta el comando
            command.ExecuteNonQuery();
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            //Se llena el objeto
                       
        }
        //Cambia el estatus de cotización a G
        public void cambiarEstatus(int idCotizacion)
        {
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_cotizacionG";
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("IdCotizacion", idCotizacion);
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se ejecuta el comando
            command.ExecuteNonQuery();
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            //Se llena el objeto

        }
        //Procedimiento para editar la información en la tabla de ventas
        public void editar(Venta objVenta)
        {
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_editarVenta";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("IdVenta", objVenta.IdVenta);            
            command.Parameters.AddWithValue("CP", objVenta.CP);
            command.Parameters.AddWithValue("IdCiudad", objVenta.IdCiudad);
            command.Parameters.AddWithValue("Calle", objVenta.Calle);
            command.Parameters.AddWithValue("NumExt", objVenta.NumExterior);
            command.Parameters.AddWithValue("NumInt", objVenta.NumInterior);
            command.Parameters.AddWithValue("Colonia", objVenta.Colonia);
            command.Parameters.AddWithValue("Telefono", objVenta.Telefono);
            command.Parameters.AddWithValue("Archivo", objVenta.Archivo);            
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
