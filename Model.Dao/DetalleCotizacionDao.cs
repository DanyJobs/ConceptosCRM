﻿using Model.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Model.Dao
{
    public class DetalleCotizacionDao
    {
        private ConexionDB objConexionDB;
        private SqlCommand comando;
        private SqlDataReader reader;
        public DetalleCotizacionDao()
        {
            objConexionDB = ConexionDB.saberEstado();
        }

        public void create(DetalleCotizacion objDetalleVenta)
        {
            string create = "insert into detalleCotizacion values('" + objDetalleVenta.NumFacura + "','" + objDetalleVenta.IdVenta + "','" + objDetalleVenta.SubTotal + "','" + objDetalleVenta.IdProducto + "','" + objDetalleVenta.Descuento + "','" + objDetalleVenta.Cantidad + "')";
            try
            {
                comando = new SqlCommand(create, objConexionDB.getCon());
                objConexionDB.getCon().Open();
                comando.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                objDetalleVenta.Estado = 1;
            }
            finally
            {
                objConexionDB.getCon().Close();
                objConexionDB.closeDB();
            }
        }
        public void delete(DetalleCotizacion objDetalleVenta)
        {
            string delete = "delete from detalleCotizacion where idVenta='" + objDetalleVenta.IdVenta + "'and numFactura='" + objDetalleVenta.NumFacura + "'";
            try
            {
                comando = new SqlCommand(delete, objConexionDB.getCon());
                objConexionDB.getCon().Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                objDetalleVenta.Estado = 1000;
            }
            finally
            {
                objConexionDB.getCon().Close();
                objConexionDB.closeDB();
            }

        }
        public bool findPorNumFactura(DetalleCotizacion objDetalleVenta)
        {
            bool hayRegistros;
            string find = "select*from detalleCotizacion where numFactura='" + objDetalleVenta.NumFacura + "'";
            try
            {
                comando = new SqlCommand(find, objConexionDB.getCon());
                objConexionDB.getCon().Open();
                reader = comando.ExecuteReader();
                hayRegistros = reader.Read();
                if (hayRegistros)
                {
                    objDetalleVenta.Estado = 99;
                }
                else
                {
                    objDetalleVenta.Estado = 1;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                objConexionDB.getCon().Close();
                objConexionDB.closeDB();
            }

            return hayRegistros;
        }
        public bool find(DetalleCotizacion objDetalleVenta)
        {
            bool hayRegistros;
            string find = "select*from detalleCotizacion where idDetalle='" + objDetalleVenta.IdDetalleVenta + "'";
            try
            {
                comando = new SqlCommand(find, objConexionDB.getCon());
                objConexionDB.getCon().Open();
                reader = comando.ExecuteReader();
                hayRegistros = reader.Read();
                if (hayRegistros)
                {
                    objDetalleVenta.NumFacura = Convert.ToInt64(reader[1].ToString());
                    objDetalleVenta.IdVenta = Convert.ToInt64(reader[2].ToString());
                    objDetalleVenta.SubTotal = Convert.ToDouble(reader[3].ToString());
                    objDetalleVenta.IdProducto = reader[4].ToString();
                    objDetalleVenta.Descuento = Convert.ToDouble(reader[5].ToString());
                    objDetalleVenta.Cantidad = Convert.ToInt32(reader[6].ToString());

                    objDetalleVenta.Estado = 99;
                }
                else
                {
                    objDetalleVenta.Estado = 1;
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                objConexionDB.getCon().Close();
                objConexionDB.closeDB();
            }

            return hayRegistros;
        }
        public List<DetalleCotizacion> findAll()
        {
            List<DetalleCotizacion> lista = new List<DetalleCotizacion>();
            string findAll = "select*from detalleCotizacion";
            try
            {
                comando = new SqlCommand(findAll, objConexionDB.getCon());
                objConexionDB.getCon().Open();
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    DetalleCotizacion objDetalleVenta = new DetalleCotizacion();
                    objDetalleVenta.IdDetalleVenta = Convert.ToInt64(reader[0].ToString());
                    objDetalleVenta.NumFacura = Convert.ToInt64(reader[1].ToString());
                    objDetalleVenta.IdVenta = Convert.ToInt64(reader[2].ToString());
                    objDetalleVenta.SubTotal = Convert.ToDouble(reader[3].ToString());
                    objDetalleVenta.IdProducto = reader[4].ToString();
                    objDetalleVenta.Descuento = Convert.ToDouble(reader[5].ToString());
                    objDetalleVenta.Cantidad = Convert.ToInt32(reader[6].ToString());
                    lista.Add(objDetalleVenta);
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                objConexionDB.getCon().Close();
                objConexionDB.closeDB();
            }

            return lista;
        }
        public bool findPorIdVenta(DetalleCotizacion objDetalleVenta)
        {
            bool hayRegistros;
            string find = "select*from detalleCotizacion where idVenta='" + objDetalleVenta.IdVenta + "'";
            try
            {
                comando = new SqlCommand(find, objConexionDB.getCon());
                objConexionDB.getCon().Open();
                reader = comando.ExecuteReader();
                hayRegistros = reader.Read();
                if (hayRegistros)
                {
                    objDetalleVenta.Estado = 99;
                }
                else
                {
                    objDetalleVenta.Estado = 1;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                objConexionDB.getCon().Close();
                objConexionDB.closeDB();
            }

            return hayRegistros;
        }
        public bool findDetalleVentaPorProductoId(DetalleCotizacion objDetalleVenta)
        {
            bool hayRegistros;
            string find = "select*from detalleCotizacion where idProducto='" + objDetalleVenta.IdProducto + "'";
            try
            {
                comando = new SqlCommand(find, objConexionDB.getCon());
                objConexionDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                hayRegistros = reader.Read();
                if (hayRegistros)
                {
                    objDetalleVenta.Estado = 99;
                }
                else
                {
                    objDetalleVenta.Estado = 1;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                objConexionDB.getCon().Close();
                objConexionDB.closeDB();
            }
            return hayRegistros;
        }
        public List<DetalleCotizacion> detallesPorIdVenta(DetalleCotizacion objDetalleVenta)
        {
            List<DetalleCotizacion> lista = new List<DetalleCotizacion>();


            string find = "select*from detalleCotizacion where idVenta='" + objDetalleVenta.IdVenta + "'";
            try
            {
                comando = new SqlCommand(find, objConexionDB.getCon());
                objConexionDB.getCon().Open();
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    DetalleCotizacion objDetalleVentas = new DetalleCotizacion();
                    objDetalleVentas.IdDetalleVenta = Convert.ToInt64(reader[0].ToString());
                    objDetalleVentas.NumFacura = Convert.ToInt64(reader[1].ToString());
                    objDetalleVentas.IdVenta = Convert.ToInt64(reader[2].ToString());
                    objDetalleVentas.SubTotal = Convert.ToDouble(reader[3].ToString());
                    objDetalleVentas.IdProducto = reader[4].ToString();
                    objDetalleVentas.Descuento = Convert.ToDouble(reader[5].ToString());
                    objDetalleVentas.Cantidad = Convert.ToInt32(reader[6].ToString());

                    lista.Add(objDetalleVentas);

                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                objConexionDB.getCon().Close();
                objConexionDB.closeDB();
            }

            return lista;
        }
        public List<Historial> findHistorial(Historial historial)
        {
            List<Historial> listaHistorial = new List<Historial>();
            //string find = "select*from producto order by nombre asc";
            try
            {
                comando = new SqlCommand("SP_COTIZACION_HISTORIAL", objConexionDB.getCon());
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@idProducto", SqlDbType.VarChar).Value = historial.IdProducto;
                objConexionDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Historial objHistorial = new Historial();
                    objHistorial.FechaCotizacion = reader.GetDateTime(0);
                    objHistorial.Cliente = reader[1].ToString();
                    objHistorial.NumCotizacion = reader[2].ToString();
                    objHistorial.Producto = reader[3].ToString();
                    objHistorial.PrecioUnitario = reader[4].ToString();
                    listaHistorial.Add(objHistorial);
                }
            }
            catch (Exception)
            {
                Historial objHistorial2 = new Historial();
                objHistorial2.Estado = 1000;
            }
            finally
            {
                objConexionDB.getCon().Close();
                objConexionDB.closeDB();
            }
            return listaHistorial;
        }
        public List<Historial> findAllHistorial()
        {
            List<Historial> listaHistorial = new List<Historial>();
            //string find = "select*from producto order by nombre asc";
            try
            {
                comando = new SqlCommand("SP_COTIZACION_HISTORIAL_TODO", objConexionDB.getCon());
                comando.CommandType = CommandType.StoredProcedure;
                objConexionDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Historial objHistorial = new Historial();
                    objHistorial.Cliente = reader[0].ToString();
                    objHistorial.NumCotizacion = reader[1].ToString();
                    objHistorial.Producto = reader[2].ToString();
                    objHistorial.PrecioUnitario = reader[3].ToString();
                    listaHistorial.Add(objHistorial);
                }
            }
            catch (Exception)
            {
                Historial objHistorial2 = new Historial();
                objHistorial2.Estado = 1000;
            }
            finally
            {
                objConexionDB.getCon().Close();
                objConexionDB.closeDB();
            }
            return listaHistorial;
        }

        //Trae la información del Detalle cotizacion
        public List<DetalleCotizacion> VerDetalles(int IdVenta)
        {
            List<DetalleCotizacion> listaCotizaciones = new List<DetalleCotizacion>();


            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_CotizacionDetalleConsulta";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexionDB.getCon();
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("IdVenta", IdVenta);
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtCotizacion = new DataTable();
            //Se abre la conexión
            objConexionDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtCotizacion);
            //Se cierra la conexión
            objConexionDB.getCon().Close();
            command.Connection.Close();
            //Se llena la lista
            for (int i = 0; i < dtCotizacion.Rows.Count; i++)
            {
                DetalleCotizacion c = new DetalleCotizacion();
                c.IdVenta = int.Parse(dtCotizacion.Rows[i]["idVenta"].ToString());
                c.SubTotal = Convert.ToDouble(dtCotizacion.Rows[i]["subTotal"].ToString());
                c.IdProducto = dtCotizacion.Rows[i]["nombre"].ToString();
                c.Descuento = Convert.ToDouble(dtCotizacion.Rows[i]["descuento"].ToString());
                c.Cantidad = int.Parse(dtCotizacion.Rows[i]["cantidad"].ToString());
                listaCotizaciones.Add(c);
            }
            //Se regresa el objeto            
            return listaCotizaciones;
        }
    }
}
