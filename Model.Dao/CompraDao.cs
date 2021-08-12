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
        //  string idCompra = "";            
            /*string create = "insert into compra(idCompra,total,fechaCompra,idSucursal,idProveedor) values('" +compra+ objCompra.Total + "','" + objCompra.Fecha + "','" + objCompra.IdSucursal + "','" + objCompra.IdProveedor + "') SELECT SCOPE_IDENTITY();";
            try
            {
                comando = new SqlCommand(create, objConexinDB.getCon());
                objConexinDB.getCon().Open();

                //RECUPERAR EL CODIGO AUTOGENERADO
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    idCompra = reader[0].ToString();
                }

            }
            catch (Exception e)
            {
                objCompra.Estado = 1;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return idCompra;*/            
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
    }
}
