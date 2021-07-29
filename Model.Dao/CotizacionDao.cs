using Model.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Model.Dao
{
    public class CotizacionDao
    {
        private ConexionDB objConexinDB;
        private SqlCommand comando;

        public CotizacionDao()
        {
            objConexinDB = ConexionDB.saberEstado();

        }

        public string create(Cotizacion objVenta)
        {
            string idVenta = "";
            string create = "insert into cotizacion(total,idCliente,idVendedor,fecha,IVA) values('" + objVenta.Total + "','" + objVenta.IdCliente + "','" + objVenta.IdVendedor + "','" + objVenta.Fecha + "','" + objVenta.Iva + "') SELECT SCOPE_IDENTITY();";
            try
            {
                comando = new SqlCommand(create, objConexinDB.getCon());
                objConexinDB.getCon().Open();

                //RECUPERAR EL CODIGO AUTOGENERADO
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    idVenta = reader[0].ToString();
                }

            }
            catch (Exception e)
            {
                objVenta.Estado = 1;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return idVenta;

        }
        public void update(Cotizacion objVenta)
        {
            string update = "update cotizacion set total='" + objVenta.Total + "',idCliente='" + objVenta.IdCliente + "',idVendedor='" + objVenta.IdVendedor + "',fecha='" + objVenta.Fecha + "',IVA='" + objVenta.Iva + "' where idVenta='" + objVenta.IdVenta + "'";
            try
            {
                comando = new SqlCommand(update, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                objVenta.Estado = 1;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
        }
        public void delete(Cotizacion objVenta)
        {
            string delete = "delete from cotizacion where idVenta='" + objVenta.IdVenta + "'";
            try
            {
                comando = new SqlCommand(delete, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                objVenta.Estado = 1;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

        }
        public bool find(Cotizacion objVenta)
        {
            bool hayRegistros;
            string find = "select*from cotizacion where idVenta='" + objVenta.IdVenta + "'";
            try
            {
                comando = new SqlCommand(find, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                hayRegistros = reader.Read();
                if (hayRegistros)
                {
                    objVenta.Total = Convert.ToDouble(reader[1].ToString());
                    objVenta.IdCliente = Convert.ToInt64(reader[2].ToString());
                    objVenta.IdVendedor = reader[3].ToString();
                    objVenta.Fecha = reader[4].ToString();
                    objVenta.Iva = Convert.ToDouble(reader[5].ToString());
                    objVenta.Estado = 99;

                }
                else
                {
                    objVenta.Estado = 1;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return hayRegistros;
        }
        public List<Cotizacion> findAll()
        {
            List<Cotizacion> listaVentas = new List<Cotizacion>();

            try
            {
                comando = new SqlCommand("SP_Buscar_Cotizacion", objConexinDB.getCon());
                comando.CommandType = CommandType.StoredProcedure;
                objConexinDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Cotizacion objVenta = new Cotizacion();
                    objVenta.IdVenta = Convert.ToInt64(reader[0].ToString());
                    objVenta.NombreCliente = reader[1].ToString();
                    objVenta.IdVendedor = reader[2].ToString();
                    objVenta.Iva = Convert.ToDouble(reader[3].ToString());
                    objVenta.Total = Convert.ToDouble(reader[4].ToString());
                    objVenta.FechaCotizacion = reader.GetDateTime(5);
                    listaVentas.Add(objVenta);

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

            return listaVentas;
        }
        public bool findVentaPorClienteId(Cotizacion objVenta)
        {
            bool hayRegistros;
            string find = "select*from cotizacion where idCliente='" + objVenta.IdCliente + "'";
            try
            {
                comando = new SqlCommand(find, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                hayRegistros = reader.Read();
                if (hayRegistros)
                {
                    objVenta.Estado = 99;
                }
                else
                {
                    objVenta.Estado = 1;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return hayRegistros;
        }
        public bool findVentaPorVendedorId(Cotizacion objVenta)
        {
            bool hayRegistros;
            string find = "select*from cotizacion where idVendedor='" + objVenta.IdVendedor + "'";
            try
            {
                comando = new SqlCommand(find, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                hayRegistros = reader.Read();
                if (hayRegistros)
                {
                    objVenta.Estado = 99;
                }
                else
                {
                    objVenta.Estado = 1;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return hayRegistros;
        }

        public void sp_reporteVenta(Cotizacion objVenta)
        {
            string create = "sp_reporte_venta" + objVenta.IdVenta;
            try
            {
                comando = new SqlCommand(create, objConexinDB.getCon());
                objConexinDB.getCon().Open();
                comando.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                objVenta.Estado = 1;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

        }


    }
}
