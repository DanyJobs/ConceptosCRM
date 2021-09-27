using Model.Dao;
using Model.Entity;
using System;
using System.Collections.Generic;

namespace Model.Neg
{
    public class CompraDetalleNeg
    {
        //Obtener los productos de comprasDetalle
        public List<CompraDetalle> consultarDetalles(int idCompra)
        {
            CompraDetalleDao cd = new CompraDetalleDao();
            return cd.consultarDetalles(idCompra);
        }
    }
}
