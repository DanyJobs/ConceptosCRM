using Model.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Model.Dao
{
    public class ClienteDao : Obligatorio<Cliente>
    {
        private ConexionDB objConexinDB;
        private SqlCommand comando;
        private SqlDataReader reader;

        public ClienteDao()
        {
            objConexinDB = ConexionDB.saberEstado();

        }
        public void create1(Cliente objCliente)
        {
            //string create = "insert into cliente(nombre,apellido,direccion,telefono,email,cuenta) values('" + objCliente.Nombre + "','" + objCliente.Apellido + "','" + objCliente.Direccion + "','" + objCliente.Telefono + "','" + objCliente.Email + "')";
            try
            {
                SqlCommand cmd = new SqlCommand("sp_agregarCliente", objConexinDB.getCon());
                cmd.CommandType = CommandType.StoredProcedure;                
                cmd.Parameters.AddWithValue("@nombre", objCliente.Nombre);
                cmd.Parameters.AddWithValue("@apellido", objCliente.Apellido);
                cmd.Parameters.AddWithValue("@direccion", objCliente.Direccion);
                cmd.Parameters.AddWithValue("@telefono", objCliente.Telefono);
                cmd.Parameters.AddWithValue("@email", objCliente.Email); 
                cmd.Parameters.AddWithValue("@cuenta", objCliente.Cuenta);          
            if (objConexinDB.getCon().State == ConnectionState.Closed)
                {
                    objConexinDB.getCon().Open();
                }
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                objCliente.Estado = 1;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

        }
        public void create(Cliente objCliente)
        {
            //string create = "sp_cliente_adc " + objCliente.IdCliente + "," + objCliente.Nombre + "," + objCliente.Appaterno + "," + objCliente.Apmaterno + "," + objCliente.Dni + "," + objCliente.Direccion + "," + objCliente.Telefono + "";
            try
            {
                //comando = new SqlCommand(create, objConexinDB.getCon());
                if (objConexinDB.getCon().State == ConnectionState.Closed)
                {
                    objConexinDB.getCon().Open();
                }
                comando.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                objCliente.Estado = 1;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

        }

        public void delete(Cliente objCliente)
        {
            string delete = "delete from cliente where idCliente='" + objCliente.IdCliente + "'";
            try
            {
                comando = new SqlCommand(delete, objConexinDB.getCon());
                if (objConexinDB.getCon().State == ConnectionState.Closed)
                {
                    objConexinDB.getCon().Open();
                }
                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                objCliente.Estado = 1;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

        }

        public bool find(Cliente objCliente)
        {
            bool hayRegistros;
            //string find = "select*from cliente where idCliente='" + objCliente.IdCliente + "'";
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ConsultaCliente", objConexinDB.getCon());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCliente", objCliente.IdCliente);

                if (objConexinDB.getCon().State == ConnectionState.Closed)
                {
                    objConexinDB.getCon().Open();
                }
                //bool hayRegistros;
                SqlDataReader reader = cmd.ExecuteReader();
                hayRegistros = reader.Read();
                if (hayRegistros)
                {
                    objCliente.Nombre = reader[1].ToString();
                    objCliente.Apellido = reader[2].ToString();
                    objCliente.Direccion = reader[3].ToString();
                    objCliente.Telefono = reader[4].ToString();
                    objCliente.Email = reader[5].ToString();
                    objCliente.Cuenta = Convert.ToInt32(reader[6].ToString());
                    objCliente.NombreCuenta = reader[7].ToString();
                    objCliente.Estado = 99;
                }
                else
                {
                    objCliente.Estado = 1;
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

        public List<Cliente> findAll()
        {
            List<Cliente> listaClientes = new List<Cliente>();
            string findAll = "select*from cliente order by nombre asc, apellido asc";
            try
            {
                comando = new SqlCommand(findAll, objConexinDB.getCon());
                if (objConexinDB.getCon().State == ConnectionState.Closed)
                {
                    objConexinDB.getCon().Open();
                }
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Cliente objCliente = new Cliente();
                    objCliente.IdCliente = Convert.ToInt64(reader[0].ToString());
                    objCliente.Nombre = reader[1].ToString();
                    objCliente.Apellido = reader[2].ToString();
                    objCliente.Direccion = reader[3].ToString();
                    objCliente.Telefono = reader[4].ToString();
                    objCliente.Email = reader[5].ToString();
                    listaClientes.Add(objCliente);

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

            return listaClientes;

        }

        public void update(Cliente objCliente)
        {
            string update = "update cliente set nombre='" + objCliente.Nombre + "',apellido='" + objCliente.Apellido + "',direccion='" + objCliente.Direccion + "',telefono='" + objCliente.Telefono + "',email='" + objCliente.Email + "',cuenta='" + objCliente.Cuenta + "' where idCliente='" + objCliente.IdCliente + "'";
            try
            {
                comando = new SqlCommand(update, objConexinDB.getCon());
                if (objConexinDB.getCon().State == ConnectionState.Closed)
                {
                    objConexinDB.getCon().Open();
                }
                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                objCliente.Estado = 1;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
        }

        public bool findClientePorEmail(Cliente objCliente)
        {
            bool hayRegistros;
            string find = "select*from cliente where Email='" + objCliente.Email + "'";
            try
            {
                comando = new SqlCommand(find, objConexinDB.getCon());
                if (objConexinDB.getCon().State == ConnectionState.Closed)
                {
                    objConexinDB.getCon().Open();
                }
                //bool hayRegistros;
                SqlDataReader reader = comando.ExecuteReader();
                hayRegistros = reader.Read();
                if (hayRegistros)
                {
                    objCliente.Nombre = reader[1].ToString();
                    objCliente.Apellido = reader[2].ToString();
                    objCliente.Direccion = reader[3].ToString();

                    objCliente.Telefono = reader[4].ToString();
                    objCliente.Email = reader[5].ToString();

                    objCliente.Estado = 99;

                }
                else
                {
                    objCliente.Estado = 1;
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

        public List<Cliente> findAllCliente(Cliente objCLiente)
        {
            List<Cliente> listaClientes = new List<Cliente>();
            //string findAll = "select*from cliente where nombre='" + objCLiente.Nombre + "' or dni='" + objCLiente.Dni + "' or idCliente=" + objCLiente.IdCliente + " or apPaterno='" + objCLiente.Appaterno + "'";
            SqlCommand cmd = new SqlCommand("sp_obtenercliente", objConexinDB.getCon());

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigo", objCLiente.IdCliente);
                cmd.Parameters.AddWithValue("@nombre", objCLiente.Nombre);
                cmd.Parameters.AddWithValue("@apellido", objCLiente.Apellido);
                cmd.Parameters.AddWithValue("@email", objCLiente.Email);

                if (objConexinDB.getCon().State == ConnectionState.Closed)
                {
                    objConexinDB.getCon().Open();
                }
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Cliente objCliente = new Cliente();
                    objCliente.IdCliente = Convert.ToInt64(reader[0].ToString());
                    objCliente.Nombre = reader[1].ToString();
                    objCliente.Apellido = reader[2].ToString();
                    objCliente.Direccion = reader[3].ToString();
                    objCliente.Telefono = reader[4].ToString();
                    objCliente.Email = reader[5].ToString();
                    listaClientes.Add(objCliente);

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

            return listaClientes;
        }
    }
}
