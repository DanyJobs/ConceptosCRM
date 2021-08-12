using Microsoft.AspNet.Identity;
using Model.Entity;
using Model.Neg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebFacturaMvc.Datos;
using Model.Dao;

namespace WebFacturaMvc.Controllers
{
    public class CompraController : Controller
    {
        
        private ClienteNeg objClienteNeg;
        private ProductoNeg objProductoNeg;        
        private static int Paso = 0;
        
                
        public CompraController()
        {           
            objClienteNeg = new ClienteNeg();
            objProductoNeg = new ProductoNeg();            
        }
        //Para acceder a la tabla proveedores usando el Model Entity
        private crmconceptoseEntities1 db = new crmconceptoseEntities1();        

        [HttpGet]
        public ActionResult ObtenerProveedores()
        {

            List<proveedor> lista = db.proveedor.ToList();
            
            return View(lista);
        }        
            [HttpPost]//para buscar clientes            
            public ActionResult ObtenerProveedores(string txtnombre, string txtappaterno, string txtid, long txtcliente = -1)
            {
                if (txtnombre == "")
                {
                    txtnombre = "-1";
                }
                if (txtappaterno == "")
                {
                    txtappaterno = "-1";
                }
                if (txtid == "")
                {
                    txtid = "-1";
                }
            int cID = int.Parse(txtid);
            //Consulta LINQ para obtener el proveedor
            var cProveedor = (from mProveedor in db.proveedor
                              where mProveedor.nombre == txtnombre || mProveedor.apellido == txtappaterno || mProveedor.idProveedor == cID
                              select mProveedor);            
            List<proveedor> mP = cProveedor.AsEnumerable<proveedor>().ToList();
                return View(mP);
            }


            [HttpPost]
            public ActionResult Seleccionar(string idProducto)
            {
                Producto objProducto = new Producto(idProducto);
                objProductoNeg.find(objProducto);
                return Json(objProducto, JsonRequestBehavior.AllowGet);

            }
            public ActionResult PruebaJson()
            {  // escribir la url directa  para ver el formato      
                List<Producto> lista = objProductoNeg.findAll();
                return Json(lista, JsonRequestBehavior.AllowGet);

            }

            public void cargarSucursales()
            {
            //Consulta LINQ para obtener las sucursales
            var cSucursal = (from mProveedor in db.sucursal                              
                              select mProveedor);
            List<sucursal> sC = cSucursal.AsEnumerable<sucursal>().ToList();            
                SelectList lista = new SelectList(sC, "idSucursal", "descripcion");
                ViewBag.ListaSucursal = lista;
            }
        public void cargarProductocmb()
        {
            List<Producto> data = objProductoNeg.findAll();
            SelectList lista = new SelectList(data, "idProducto", "nombre");
            ViewBag.ListaProducto = lista;
        }

        public ActionResult Index()
            {
            cargarProductocmb();
                cargarSucursales();
                return View();
            }
        [HttpPost]
        public ActionResult GuardarCompra(string Fecha, string IdProveedor, string Total, string Sucursal, List<compraDetalle> ListadoDetalle)
        {
            string mensaje = "";
            double iva = 18;
            string idVendedor = User.Identity.GetUserId();
            int codigoPago = 0;
            int codigoSucursal = 0;
            int codigoProveedor = 0;
            decimal total = 0;

            if (Fecha == "" || IdProveedor == "" || Total == "" || Sucursal == "")
            {
                if (Fecha == "") mensaje = "ERROR EN EL CAMPO FECHA";                
                if (IdProveedor == "") mensaje = "ERROR CON EL CODIGO DEL CLIENTE";
                if (Total == "") mensaje = "ERROR EN EL CAMPO TOTAL";
                if (Sucursal == "") mensaje = "ERROR EN EL CAMPO SUCURSAL";
            }
            else if (Sucursal == null)
            {
                mensaje = "ERROR EN EL CAMPO SUCURSAL";
            }
            else
            {
                Paso = 1;
                codigoProveedor = int.Parse(IdProveedor);
                total = Convert.ToDecimal(Total);
                codigoSucursal = int.Parse(Sucursal);
                //REGISTRO DE COMPRA
                //Datos del objeto
                Compra objCompra = new Compra();
                //Para saber el total de registros de compra
                /*var obj = (from o in db.compra                          
                          select o);
                int cantidad = obj.Count();*/
                //Se le pasan los datos al objeto                
                objCompra.IdProveedor = codigoProveedor;
                objCompra.IdSucursal = codigoSucursal;
                objCompra.Fecha = Convert.ToDateTime(Fecha);
                objCompra.Total = total;                
                //Insercion
                try
                {
                    CompraDao nCompra = new CompraDao();
                    nCompra.create(objCompra);
                    mensaje = "LA COMPRA FUE REGISTRADA CON ÉXITO";
                }
                catch(Exception ex)
                {
                    mensaje = ex.Message.ToString();
                }
                //Obtener el registro que se acaba de ingresar
                int pIdCompra = db.compra.Max(x => x.idCompra);
                //Objeto Dao
                CompraDetalleDao cDetalle = new CompraDetalleDao();
                            foreach (var data in ListadoDetalle)
                            {
                                string idProducto = data.idProducto.ToString();
                                int pCantidad = Convert.ToInt32(data.cantidad.ToString());                                
                                decimal precio = Convert.ToDecimal(data.precio.ToString());
                                CompraDetalle objCompraDetalle = new CompraDetalle(pIdCompra, idProducto,pCantidad,precio);
                                cDetalle.create(objCompraDetalle);
                            }
                            //mensaje = "VENTA GUARDADA CON EXITO...";
                        }
                                               
            return Json(mensaje);
            }            
            /*public ActionResult DetallesVenta(long id)
            {
                DetalleCotizacion objDetalleVenta = new DetalleCotizacion();
                objDetalleVenta.IdVenta = id;
                List<DetalleCotizacion> lista = objDetalleVentaNeg.detallesPorIdVenta(objDetalleVenta);
                return View(lista);
            }           */

        }
    }
