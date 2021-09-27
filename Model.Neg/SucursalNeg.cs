using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Dao;
using Model.Entity;


namespace Model.Neg
{
    public class SucursalNeg
    {
        
        public SucursalNeg()
        {
            
        }
        //Obtener la información de todas las sucursales
        public List<Sucursal> consulta()
        {
            SucursalDao s = new SucursalDao();
            return s.consulta();
        }


    }
}
