﻿using Model.Dao;
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
        public List<Cotizacion> findAll()
        {
            return objCotizacionDao.findAll();
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
        public List<Cotizacion> buscarConEstatus(string Month, string Year,string Estatus)
        {
            return objCotizacionDao.buscarConEstatus(Month, Year,Estatus);
        }
        public List<Cotizacion> buscarConEstatus()
        {
            return objCotizacionDao.buscarConEstatus();
        }
        public List<Historial> findHistorial(Historial historial)
        {
            return objDetalleCotizacionDao.findHistorial(historial);
        }

        public List<Historial> findAllHistorial()
        {
            return objDetalleCotizacionDao.findAllHistorial();
        }


    }
}
