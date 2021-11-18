using Model.Dao;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Data;

namespace Model.Neg
{
    public class VentaNeg
    {
        VentaDao objVentaDao = new VentaDao();
        //Para la parte de mostrar las cotizaciones y editar
        public List<Cotizacion> buscar()
        {
            return objVentaDao.buscar();
        }
        //Para filtrar por año y mes
        public List<Cotizacion> buscar(string Month, string Year)
        {
            return objVentaDao.buscar(Month, Year);
        }
        //Trae la información del cliente y el total de la venta
        public DataTable consulta_VC(int IdCotizacion)
        {
            return objVentaDao.consulta_VC(IdCotizacion);
        }
        //Trae el nombre del Sales Person
        public DataTable consulta_SalesPerson(int IdCotizacion)
        {
            return objVentaDao.consulta_SalesPerson(IdCotizacion);
        }
        //Procedimiento para guardar la información en la tabla de ventas
        public void create(Venta objVenta)
        {
            objVentaDao.create(objVenta);
        }
        //Trae las ventas realizadas a partir de una cotizacion N que se convirtiern en W o G SIN PARAMETROS
        public List<Venta> consulta_Ventas()
        {
            List<Venta> listVentas = objVentaDao.consulta_Ventas();
            return listVentas;
        }
        //Trae las ventas realizadas a partir de una cotizacion N que se convirtiern en W o G CON PARAMETROS
        public List<Venta> consulta_Ventas(string Month, string Year)
        {
            List<Venta> listVentas = objVentaDao.consulta_Ventas(Month, Year);
            return listVentas;
        }
        //Trae los bytes del archivo de la orden de compra
        public Venta VerOrdenCompra(int idVenta)
        {
            return objVentaDao.VerOrdenCompra(idVenta);
        }
        //Trae los bytes del archivo de la orden de compra
        public byte[] traerBytes(int idVenta)
        {
            return objVentaDao.traerBytes(idVenta);
        }
        //Trae la información de una venta realizada
        public Venta VerVenta(int idVenta)
        {
            return objVentaDao.VerVenta(idVenta);
        }
        //Elimina una Venta
        public void eliminarVenta(int idVenta)
        {
            objVentaDao.eliminarVenta(idVenta);
        }
        //Cambia el estatus de cotización a G
        public void cambiarEstatus(int idCotizacion)
        {
            objVentaDao.cambiarEstatus(idCotizacion);
        }
        //Procedimiento para editar la información en la tabla de ventas
        public void editar(Venta objVenta)
        {
            objVentaDao.editar(objVenta);
        }


        }
}
