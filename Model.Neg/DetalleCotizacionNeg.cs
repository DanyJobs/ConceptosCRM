using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;
using Model.Dao;

namespace Model.Neg
{
   public class DetalleCotizacionNeg
    {
        private DetalleCotizacionDao objDetalleCotizacionDao;
        public DetalleCotizacionNeg()
        {
            objDetalleCotizacionDao = new DetalleCotizacionDao();
        }
        public void create(DetalleCotizacion objDetalleCotizacion)
        {
            bool verificacion = true;

            //inicio verificacion de cantidaa estado=1
            int cant = 0;
            string cantidad = objDetalleCotizacion.Cantidad.ToString();
            if (cantidad == null)
            {
                objDetalleCotizacion.Estado = 10;
                return;
            }
            else
            {
                try
                {
                    cant = Convert.ToInt32(objDetalleCotizacion.Cantidad);
                    verificacion = cant > 0 && cant < 999999;
                    if (!verificacion)
                    {
                        objDetalleCotizacion.Estado = 1;
                        return;
                    }
                }
                catch (Exception e)
                {

                    objDetalleCotizacion.Estado = 100;
                    return;
                }

            }
            //fin verificacion de cantidad
            objDetalleCotizacion.Estado = 99;
            objDetalleCotizacionDao.create(objDetalleCotizacion);
            return;

        }

        public List<DetalleCotizacion> detallesPorIdVenta(DetalleCotizacion objDetalleCotizacion)
        {
            return objDetalleCotizacionDao.detallesPorIdVenta(objDetalleCotizacion);
        }
        public void delete(DetalleCotizacion objDetalleCotizacion)
        {            
            objDetalleCotizacionDao.delete(objDetalleCotizacion);
        }
        public List<DetalleCotizacion> findAll()
        {
            return objDetalleCotizacionDao.findAll();
        }


    }
}
