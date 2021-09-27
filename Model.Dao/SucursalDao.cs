using Model.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Model.Dao
{
    public class SucursalDao
    {
        private ConexionDB objConexinDB;

        public SucursalDao()
        {
            objConexinDB = ConexionDB.saberEstado();

        }
        //Obtener la información de todas las sucursales
        public List<Sucursal> consulta()
        {
            
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_consultaSucursales";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtSucursales = new DataTable();
            //Se abre la conexión
            objConexinDB.getCon().Open();
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtSucursales);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            //Lista de sucursales
            List<Sucursal> lstSucursales = new List<Sucursal>();
            for(int i=0; i < dtSucursales.Rows.Count; i++)
            {
                Sucursal s = new Sucursal();
                s.IdSucursal = int.Parse(dtSucursales.Rows[i]["idSucursal"].ToString());
                s.Descripcion = dtSucursales.Rows[i]["descripcion"].ToString();
                s.Calle = dtSucursales.Rows[i]["calle"].ToString();
                s.NumExt = dtSucursales.Rows[i]["numExt"].ToString();
                s.Colonia = dtSucursales.Rows[i]["colonia"].ToString();
                s.CP = dtSucursales.Rows[i]["cp"].ToString();
                s.Email = dtSucursales.Rows[i]["email"].ToString();
                s.Telefono = dtSucursales.Rows[i]["telefono"].ToString();
                lstSucursales.Add(s);
            }            
            //Se regresa el objeto            
            return lstSucursales;
        }
    }
}
