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
    public class CompraDetalleDao
    {
        private ConexionDB objConexinDB;

        public CompraDetalleDao()
        {
            objConexinDB = ConexionDB.saberEstado();

        }
        //Registrar compra
        public void create(CompraDetalle objCompraDetalle)
        {            
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_CompraDetalleAlta";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("idcompra", objCompraDetalle.IdCompra);
            command.Parameters.AddWithValue("idproducto", objCompraDetalle.IdProducto);
            command.Parameters.AddWithValue("cantidad", objCompraDetalle.Cantidad);
            command.Parameters.AddWithValue("precio", objCompraDetalle.Precio);
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se ejecuta el comando
            command.ExecuteNonQuery();
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();

        }
        //Obtener las comprasDetalle
        public DataTable consultar(int idCompra)
        {
            Compra c = new Compra();
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_CompraDetalleConsulta";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se le pasan los parametros            
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
    }
}
