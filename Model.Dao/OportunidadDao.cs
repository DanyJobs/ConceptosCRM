using Model.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class OportunidadDao
    {
        private ConexionDB objConexionDB;
        private SqlDataReader reader;
        public OportunidadDao()
        {
            objConexionDB = ConexionDB.saberEstado();
        }
        public List<Oportunidad> findAll()
        {
            List<Oportunidad> objListaOportunidad = new List<Oportunidad>();
            //try
            //{
                //string findAll = "select*from cliente where nombre='" + objCLiente.Nombre + "' or dni='" + objCLiente.Dni + "' or idCliente=" + objCLiente.IdCliente + " or apPaterno='" + objCLiente.Appaterno + "'";
                SqlCommand cmd = new SqlCommand("sp_consultaOportunidad", objConexionDB.getCon());
                cmd.CommandType = CommandType.StoredProcedure;               
                if (objConexionDB.getCon().State == ConnectionState.Closed)
                {
                    objConexionDB.getCon().Open();
                }
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Oportunidad objOportunidadT = new Oportunidad();
                    objOportunidadT.idOportunidad = Convert.ToInt32(reader[0].ToString());
                    objOportunidadT.cotizacion = Convert.ToInt32(reader[1].ToString());
                    objOportunidadT.idUsuario = reader[2].ToString();
                    objOportunidadT.notas = reader[3].ToString();
                    objOportunidadT.acciones = reader[4].ToString();
                if (reader[5].ToString()=="") {
                    objOportunidadT.venta = 0;
                }
                else {
                    objOportunidadT.venta = Convert.ToDecimal(reader[5].ToString());                    
                }
                if (reader[6].ToString() == "")
                {              
                    objOportunidadT.costo = 0;
                }
                else
                {
                    objOportunidadT.costo = Convert.ToDecimal(reader[6].ToString());
                }

               
                    objOportunidadT.porcentaje = Convert.ToInt32(reader[7].ToString());
                    objOportunidadT.estatus = reader[8].ToString(); 
                    objListaOportunidad.Add(objOportunidadT);
                }
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            //finally
            //{
                objConexionDB.getCon().Close();
                objConexionDB.closeDB();
            //}
            return objListaOportunidad;
        }
    }
}
