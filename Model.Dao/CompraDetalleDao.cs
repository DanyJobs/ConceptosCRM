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
            command.Parameters.AddWithValue("seccion", objCompraDetalle.Seccion);
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
        //Obtener los productos de comprasDetalle
        public List<CompraDetalle> consultarDetalles(int idCompra)
        {
           
            //Lista 
            List<CompraDetalle> list = new List<CompraDetalle>();
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_CompraConsultaEditar2";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("IdCompra", idCompra);
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
            //Se llena la lista
            for(int i = 0; i < dtCompras.Rows.Count; i++)
            {
                //Objeto individual
                CompraDetalle cd = new CompraDetalle();
                cd.IdCompra = int.Parse(dtCompras.Rows[i]["idCompra"].ToString());
                cd.Cantidad= int.Parse(dtCompras.Rows[i]["cantidad"].ToString());
                cd.IdProducto= dtCompras.Rows[i]["idProducto"].ToString();
                cd.Precio= Convert.ToDecimal(dtCompras.Rows[i]["precio"].ToString());
                cd.NombreProducto= dtCompras.Rows[i]["nombre"].ToString();
                cd.IdSucursal= int.Parse(dtCompras.Rows[i]["idSucursal"].ToString());
                cd.NombreSucursal = dtCompras.Rows[i]["descripcion"].ToString();
                cd.Descripcion= dtCompras.Rows[i]["idCompra"].ToString();
                cd.Seccion= dtCompras.Rows[i]["seccion"].ToString();
                list.Add(cd);
            }

            //Se regresa el objeto            
            return list;

        }
        //Eliminar los registros de compraDetalle
        public void eliminar(int idCompra)
        {
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_CompraDetalleEliminar";
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("IdCompra", idCompra);
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
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
