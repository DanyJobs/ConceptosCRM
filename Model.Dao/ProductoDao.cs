using Model.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Model.Dao
{
    public class ProductoDao : Obligatorio<Producto>
    {
        private ConexionDB objConexionDB;
        private SqlCommand comando;
        private SqlDataReader reader;
      
        public ProductoDao()
        {
            objConexionDB = ConexionDB.saberEstado();
        }
        public void create(Producto objProducto)
        {
            string create = "insert into producto values('" + objProducto.IdProducto + "','" + objProducto.Nombre + "','" + objProducto.Descripcion + "'," + objProducto.PrecioUnitario + ",'" + objProducto.Categoria + "','" + objProducto.Marca + "','" + objProducto.BandaAncha + "'," + objProducto.Channels + ")";
            try
            {
                comando = new SqlCommand(create, objConexionDB.getCon());
                objConexionDB.getCon().Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                objProducto.Estado = 1000;
            }
            finally
            {
                objConexionDB.getCon().Close();
                objConexionDB.closeDB();
            }
        }

        public void delete(Producto objProducto)
        {
            string delete = "delete from producto where idProducto='" + objProducto.IdProducto + "'";
            try
            {
                comando = new SqlCommand(delete, objConexionDB.getCon());
                objConexionDB.getCon().Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                objProducto.Estado = 1000;
            }
            finally
            {
                objConexionDB.getCon().Close();
                objConexionDB.closeDB();
            }
        }

        public bool find(Producto objProducto)
        {
         
            bool hayRegistros;
            try
            {
                SqlCommand cmd = new SqlCommand("SP_PRODUCTO_CONSULTA_COTIZACION", objConexionDB.getCon());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idProducto", objProducto.IdProducto);

                objConexionDB.getCon().Open();

                reader = cmd.ExecuteReader();

                hayRegistros = reader.Read();
                if (hayRegistros)
                {
                    objProducto.Nombre = reader[1].ToString();
                    objProducto.Descripcion = reader[2].ToString();
                    objProducto.PrecioUnitario = Convert.ToDouble(reader[3].ToString());
                    objProducto.Categoria = reader[4].ToString();
                    objProducto.Marca = reader[5].ToString();
                    objProducto.BandaAncha = reader[6].ToString();
                    objProducto.Channels = Convert.ToInt32(reader[7].ToString());
                    objProducto.Estado = 99;
                }
                else
                {
                    objProducto.Estado = 1;
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
        public bool findProductos(Producto objProducto)
        {

            bool hayRegistros;
            try
            {
                SqlCommand cmd = new SqlCommand("SP_PRODUCTO_CONSULTA_COTIZACION_PRODUCTO", objConexionDB.getCon());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idProducto", objProducto.IdProducto);

                objConexionDB.getCon().Open();

                reader = cmd.ExecuteReader();

                hayRegistros = reader.Read();
                if (hayRegistros)
                {
                    objProducto.Nombre = reader[1].ToString();
                    objProducto.Descripcion = reader[2].ToString();
                    objProducto.PrecioUnitario = Convert.ToDouble(reader[3].ToString());
                    objProducto.Categoria = reader[4].ToString();
                    objProducto.Marca = reader[5].ToString();
                    objProducto.BandaAncha = reader[6].ToString();
                    objProducto.Channels = Convert.ToInt32(reader[7].ToString());
                    objProducto.Estado = 99;
                }
                else
                {
                    objProducto.Estado = 1;
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
        public List<Producto> findAll()
        {
            List<Producto> listaVendedores = new List<Producto>();
            //string find = "select*from producto order by nombre asc";
            try
            {
                comando = new SqlCommand("SP_PRODUCTO_CONSULTA", objConexionDB.getCon());
                comando.CommandType = CommandType.StoredProcedure;
                objConexionDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Producto objProducto = new Producto();
                    objProducto.IdProducto = reader[0].ToString();
                    objProducto.Nombre = reader[1].ToString();
                    objProducto.Descripcion = reader[2].ToString();
                    objProducto.PrecioUnitario = Convert.ToDouble(reader[3].ToString());
                    objProducto.Categoria = reader[4].ToString();
                    objProducto.Marca = reader[5].ToString();
                    objProducto.BandaAncha = reader[6].ToString();
                    objProducto.Channels = Convert.ToInt32(reader[7].ToString());
                    listaVendedores.Add(objProducto);
                }
            }
            catch (Exception)
            {
                Producto objProducto2 = new Producto();
                objProducto2.Estado = 1000;
            }
            finally
            {
                objConexionDB.getCon().Close();
                objConexionDB.closeDB();
            }
            return listaVendedores;
        }

        public void update(Producto objProducto)
        {
            string update = "update producto set  nombre='" + objProducto.Nombre + "',descripcion='" + objProducto.Descripcion + "' ,precioUnitario=" + objProducto.PrecioUnitario + ",idCategoria='" + objProducto.Categoria + "' ,idMarca='" + objProducto.Marca + "' where idProducto='" + objProducto.IdProducto + "'";
            try
            {
                comando = new SqlCommand(update, objConexionDB.getCon());
                objConexionDB.getCon().Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {

                objProducto.Estado = 1000;
            }
            finally
            {
                objConexionDB.getCon().Close();
                objConexionDB.closeDB();
            }
        }

        public bool findProductoPorCategoriaId(Producto objProducto)
        {
            bool hayRegistros;
            string find = "select*from producto where IdCategoria='" + objProducto.Categoria + "'";
            try
            {
                comando = new SqlCommand(find, objConexionDB.getCon());
                objConexionDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                hayRegistros = reader.Read();
                if (hayRegistros)
                {
                    objProducto.Estado = 99;
                }
                else
                {
                    objProducto.Estado = 1;
                }
            }
            catch (Exception u)
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

        public bool findProductoPorMarcaId(Producto objProducto)
        {
            bool hayRegistros;
            string find = "select*from producto where IdMarca='" + objProducto.Marca + "'";
            try
            {
                comando = new SqlCommand(find, objConexionDB.getCon());
                objConexionDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                hayRegistros = reader.Read();
                if (hayRegistros)
                {
                    objProducto.Estado = 99;
                }
                else
                {
                    objProducto.Estado = 1;
                }
            }
            catch (Exception u)
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



        public List<Producto> findAllProductos(Producto objProducto)
        {
            List<Producto> listaProductos = new List<Producto>();
            //string findAll = "select*from cliente where nombre='" + objCLiente.Nombre + "' or dni='" + objCLiente.Dni + "' or idCliente=" + objCLiente.IdCliente + " or apPaterno='" + objCLiente.Appaterno + "'";
            string findAll = "select* from producto where nombre like '%" + objProducto.Nombre + "%' or idProducto like '%" + objProducto.IdProducto + "%' or idCategoria like '%" + objProducto.Categoria + "%'";
            try
            {
                comando = new SqlCommand(findAll, objConexionDB.getCon());
                objConexionDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Producto objProductos = new Producto();
                    objProductos.IdProducto = reader[0].ToString();
                    objProductos.Nombre = reader[1].ToString();
                    objProductos.Descripcion = reader[2].ToString();
                    objProductos.PrecioUnitario = Convert.ToDouble(reader[3].ToString());
                    objProductos.Categoria = reader[4].ToString();
                    objProducto.Marca = reader[5].ToString();
                    objProducto.BandaAncha = reader[6].ToString();
                    objProducto.Channels = Convert.ToInt32(reader[7].ToString());
                    listaProductos.Add(objProductos);

                }
            }
            catch (Exception)
            {

                objProducto.Estado = 1000;
            }
            finally
            {
                objConexionDB.getCon().Close();
                objConexionDB.closeDB();
            }

            return listaProductos;
        }

        public List<Producto> findAllProductosCotizacion(Producto objProducto)
        {
            List<Producto> objListaProducto = new List<Producto>();
            try
            {
                //string findAll = "select*from cliente where nombre='" + objCLiente.Nombre + "' or dni='" + objCLiente.Dni + "' or idCliente=" + objCLiente.IdCliente + " or apPaterno='" + objCLiente.Appaterno + "'";
                SqlCommand cmd = new SqlCommand("SP_PRODUCTO_CONSULTA_FILTRADO", objConexionDB.getCon());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idProducto", objProducto.IdProducto);
                cmd.Parameters.AddWithValue("@nombre", objProducto.Nombre);
                cmd.Parameters.AddWithValue("@categoria", objProducto.Categoria);
                cmd.Parameters.AddWithValue("@idMarca", objProducto.Marca);
                objConexionDB.getCon().Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Producto objProductoT = new Producto();
                    objProductoT.IdProducto = reader[0].ToString();
                    objProductoT.Nombre = reader[1].ToString();
                    objProductoT.Descripcion = reader[2].ToString();
                    objProductoT.PrecioUnitario = Convert.ToDouble(reader[3].ToString());
                    objProductoT.Categoria = reader[4].ToString();
                    objProductoT.Marca = reader[5].ToString();
                    objProductoT.BandaAncha = reader[6].ToString();
                    objProductoT.Channels = Convert.ToInt32(reader[7].ToString());
                    objListaProducto.Add(objProductoT);
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
            return objListaProducto;
        }


        public List<Producto> findAllProductosPorCategoria(Producto objProducto)
        {
            List<Producto> listaProductos = new List<Producto>();
            string findAll = "select*from producto where idCategoria='" + objProducto.Categoria + "'";

            //string findAll = "sp_producto_categoria " + objProducto.Categoria;
            try
            {
                comando = new SqlCommand(findAll, objConexionDB.getCon());
                objConexionDB.getCon().Open();
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Producto objProductos = new Producto();
                    objProductos.IdProducto = reader[0].ToString();
                    objProductos.Nombre = reader[1].ToString();
                    objProductos.PrecioUnitario = Convert.ToDouble(reader[2].ToString());
                    objProductos.Categoria = reader[3].ToString();
                    objProducto.Marca = reader[4].ToString();
                    objProducto.BandaAncha = reader[5].ToString();
                    listaProductos.Add(objProductos);

                }
            }
            catch (Exception)
            {

                objProducto.Estado = 1000;
            }
            finally
            {
                objConexionDB.getCon().Close();
                objConexionDB.closeDB();
            }

            return listaProductos;

        }

        public List<Producto> findPrecioProducto(Producto objProductos)
        {
            List<Producto> listaVendedores = new List<Producto>();
            string find = "select*from producto where idProducto='" + objProductos.IdProducto + "'";
            try
            {
                comando = new SqlCommand(find, objConexionDB.getCon());
                objConexionDB.getCon().Open();
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Producto objProducto = new Producto();
                    objProducto.IdProducto = reader[0].ToString();
                    objProducto.Nombre = reader[1].ToString();
                    objProducto.PrecioUnitario = Convert.ToDouble(reader[2].ToString());
                    objProducto.Categoria = reader[3].ToString();
                    objProducto.Marca = reader[4].ToString();
                    objProducto.BandaAncha = reader[5].ToString();
                    listaVendedores.Add(objProducto);
                }

            }
            catch (Exception)
            {
                Producto objProducto2 = new Producto();
                objProducto2.Estado = 1000;
            }
            finally
            {
                objConexionDB.getCon().Close();
                objConexionDB.closeDB();
            }

            return listaVendedores;
        }

    }
}
