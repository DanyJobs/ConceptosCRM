using Model.Dao;
using Model.Entity;
using System;
using System.Collections.Generic;

namespace Model.Neg
{
    public class CotizacionNeg
    {
        private CotizacionDao objCotizacionDao;
        private DetalleCotizacionDao objDetalleCotizacionDao;

        public CotizacionNeg()
        {
            objCotizacionDao = new CotizacionDao();
            objDetalleCotizacionDao = new DetalleCotizacionDao();
        }

        public string create(Cotizacion objCotizacion)
        {
            bool verificacion = true;

            //inicio verificacion total estado=2
            string total = objCotizacion.Total.ToString();
            double total1 = 0;
            if (total == null)
            {
                objCotizacion.Estado = 20;

            }
            else
            {
                try
                {
                    total1 = Convert.ToDouble(objCotizacion.Total);
                    verificacion = total1 > 0 && total1 < 999999999999999;

                    if (!verificacion)
                    {
                        objCotizacion.Estado = 2;

                    }
                }
                catch (Exception e)
                {
                    objCotizacion.Estado = 200;

                }
            }
            //inicio verificacion total            

            //inicio verificacion fecha estado=4
            string fecha = objCotizacion.Fecha.ToString();

            if (fecha == null)
            {
                objCotizacion.Estado = 40;

            }
            else
            {
                fecha = objCotizacion.Fecha.Trim();
                verificacion = fecha.Length > 0 && fecha.Length < 30;
                if (!verificacion)
                {
                    objCotizacion.Estado = 4;

                }
            }
            //fin verificacion de fecha

            //todo bien
            objCotizacion.Estado = 99;
            return objCotizacionDao.create(objCotizacion);

        }

        public void update(Cotizacion objCotizacion)
        {
            bool verificacion = true;

            //inicio verificacion total estado=2
            string total = objCotizacion.Total.ToString();
            double total1 = 0;
            if (total == null)
            {
                objCotizacion.Estado = 20;
                return;
            }
            else
            {
                try
                {
                    total1 = Convert.ToDouble(objCotizacion.Total);
                    verificacion = total1 > 0 && total1 < 999999999999999;

                    if (!verificacion)
                    {
                        objCotizacion.Estado = 2;
                        return;
                    }
                }
                catch (Exception e)
                {
                    objCotizacion.Estado = 200;
                    return;
                }
            }
            //inicio verificacion total           

            //inicio verificacion fecha estado=4
            string fecha = objCotizacion.Fecha.ToString();

            if (fecha == null)
            {
                objCotizacion.Estado = 40;
                return;
            }
            else
            {
                fecha = objCotizacion.Fecha.Trim();
                verificacion = fecha.Length > 0 && fecha.Length < 30;
                if (!verificacion)
                {
                    objCotizacion.Estado = 4;
                    return;
                }
            }
            //fin verificacion de fecha

            //todo bien
            objCotizacion.Estado = 99;
            objCotizacionDao.update(objCotizacion);
            return;
        }

        public void delete(Cotizacion objCotizacion)
        {
            bool verificacion = true;
            //inicio verificacion de existencia
            Cotizacion objCotizacionAux = new Cotizacion();
            objCotizacionAux.IdVenta = objCotizacion.IdVenta;
            verificacion = objCotizacionDao.find(objCotizacionAux);
            if (!verificacion)
            {
                objCotizacion.Estado = 33;
                return;
            }
            //fin verificaicon de existencia de Cotizacion

            //verificaicon de existencia de Detalle
            DetalleCotizacion objDetalleCotizacion = new DetalleCotizacion();
            objDetalleCotizacion.IdVenta = objCotizacion.IdVenta;
            verificacion = !objDetalleCotizacionDao.findPorIdVenta(objDetalleCotizacion);
            if (!verificacion)
            {
                objCotizacion.Estado = 34;
                return;
            }
            //todo bien
            objCotizacion.Estado = 99;
            objCotizacionDao.delete(objCotizacion);
            return;
        }

        public bool find(Cotizacion objCotizacion)
        {
            return objCotizacionDao.find(objCotizacion);
        }
        public List<Cotizacion> findAll(string Usuario)
        {
            return objCotizacionDao.findAll(Usuario);
        }
        //Para la parte de mostrar las cotizaciones y editar
        public List<Cotizacion> buscar()
        {
            return objCotizacionDao.buscar();
        }
        //Para filtrar por año y mes
        public List<Cotizacion> buscar(string Month, string Year)
        {
            return objCotizacionDao.buscar(Month,Year);
        }
        //Para traer la ifnormación de un registro de cotizacion con el IdVenta
        public Cotizacion buscarIdVenta(int idVenta)
        {
            return objCotizacionDao.buscarIdVenta(idVenta);
        }
        public List<Historial> findHistorial(Historial historial)
        {
            return objDetalleCotizacionDao.findHistorial(historial);
        }
        public List<RFQHistorial> findRFQ(RFQHistorial rfqitem)
        {
            return objDetalleCotizacionDao.findRFQ(rfqitem);
        }

        public List<Historial> findAllHistorial()
        {
            return objDetalleCotizacionDao.findAllHistorial();
        }
        //Metodo para actualizar al información de la cotizacion y cotización detalle
        public void Actualizar(int idVenta, decimal Total, int IdCliente, string idVendedor, string fecha, decimal IVA, string Notas, string NotasCompras, string Estatus)
        {
            objCotizacionDao.Actualizar(idVenta, Total, IdCliente, idVendedor, fecha,  IVA, Notas,  NotasCompras, Estatus);
        }
        //Eliminar la cotizacion
        public void Eliminar(int idVenta)
        {
            objCotizacionDao.Eliminar(idVenta);
        }
        public List<Cotizacion> buscarConEstatus()
        {
            return objCotizacionDao.buscarConEstatus();
        }
        public List<Cotizacion> buscarConEstatus(string Month, string Year, string Estatus)
        {
            return objCotizacionDao.buscarConEstatus(Month, Year, Estatus);
        }
        public List<EmailMarketingCorreos> buscarEmailMarketing(string id)
        {
            return objCotizacionDao.buscarEmailMarketing(id);
        }
        public List<RFQItem> buscarListaProductosRFQ(string id)
        {
            return objCotizacionDao.buscarListaProductosRFQ(id);
        }
        public RFQ buscarListaRFQ(string id)
        {
            return objCotizacionDao.buscarListaRFQ(id);
        }
        public List<RFQ> buscarListaRFQ()
        {
            return objCotizacionDao.buscarListaRFQ();
        }
        
        public string agregarRFQ(string idVenta, string idVendedor)
        {
            string mensaje = "";

            if (idVendedor.Equals(null) || idVenta.Equals(null))
            {                             
                mensaje = "RFQ no se ha podido efectuar correctamente";
            }
            else
            {
                mensaje=objCotizacionDao.agregarRFQ(idVenta, idVendedor);        
            }
            return mensaje;
        }

        public string updateRFQ(RFQ objRFQ)
        {           
            return objCotizacionDao.updateRFQ(objRFQ);
        }
        public string updateRFQItem(RFQItem objRFQ)
        {
            return objCotizacionDao.updateRFQItem(objRFQ);
        }
        public string eliminadosRFQ(RFQItemEliminado objRFQ)
        {
            return objCotizacionDao.eliminadosRFQ(objRFQ);
        }      
    }
}
