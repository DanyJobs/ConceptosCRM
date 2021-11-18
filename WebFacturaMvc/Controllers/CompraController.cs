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
using System.Data;

namespace WebFacturaMvc.Controllers
{
    [Authorize(Roles = "ADMIN,STANDARD")]
    public class CompraController : Controller
    {
        
        private ClienteNeg objClienteNeg;
        private ProductoNeg objProductoNeg;        
        private static int Paso = 0;
        CategoriaNeg objCategoriaNeg = new CategoriaNeg();
        MarcaNeg objMarcaNeg = new MarcaNeg();
        CompraDetalleNeg cdNeg = new CompraDetalleNeg();


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

        public void cargarCategoria()
        {
            List<Categoria> data = objCategoriaNeg.findAll();
            SelectList lista = new SelectList(data, "idCategoria", "nombre");
            ViewBag.ListaCategoria = lista;
        }
        public void cargarMarca()
        {
            List<Marca> data = objMarcaNeg.findAll();
            SelectList lista = new SelectList(data, "idMarca", "descripcion");
            ViewBag.ListaMarca = lista;
        }
        public void cargarProductocmb()
        {
            List<Producto> data = objProductoNeg.findAll();
            SelectList lista = new SelectList(data, "idProducto", "nombre");
            ViewBag.ListaProducto = lista;
        }
        [HttpGet]
        public ActionResult ObtenerProductos()
        {
            List<Producto> lista = objProductoNeg.findAll();
            cargarCategoria();
            cargarMarca();
            return View(lista);
        }
        [HttpPost]//para buscar clientes
        public ActionResult ObtenerProductos(string txtcodigo, string txtnombre, string txtCategoria, string txtMarca)
        {

            if (txtcodigo == "")
            {
                txtcodigo = null;
            }
            if (txtnombre == "")
            {
                txtnombre = null;
            }
            if (txtCategoria == "")
            {
                txtCategoria = "-1";
            }
            if (txtMarca == "")
            {
                txtMarca = "-1";
            }
            Producto objProducto = new Producto();
            objProducto.IdProducto = txtcodigo;
            objProducto.Nombre = txtnombre;
            objProducto.Categoria = txtCategoria;
            objProducto.Marca = txtMarca;
            List<Producto> ListaProducto = objProductoNeg.findAllProductosCotizacion(objProducto);
            cargarCategoria();
            cargarMarca();
            //System.Diagnostics.Debug.WriteLine(objProducto.IdProducto +"    " +objProducto.Nombre+"    " +objProducto.Categoria+ "     "+objProducto.Marca);
            return View(ListaProducto);
        }
        [HttpPost]
        public ActionResult Seleccionar(string idProducto)
        {
            Producto objProducto = new Producto(idProducto);
            objProductoNeg.find(objProducto);
            return Json(objProducto, JsonRequestBehavior.AllowGet);
        }
        private void cargarSucursales()
            {
            //Consulta LINQ para obtener las sucursales
            /*var cSucursal = (from mProveedor in db.sucursal                              
                              select mProveedor);*/
            SucursalNeg sn = new SucursalNeg();
            List<Sucursal> list = sn.consulta();
            //IEnumerable<SelectListItem> lst = (IEnumerable<SelectListItem>)list;
                SelectList lista = new SelectList(list, "idSucursal", "descripcion");
                ViewBag.ListaSucursal = lista;
            }        
        public ActionResult Index()
            {
                cargarProductocmb();
                cargarSucursales();
                return View();
            }
        [HttpPost]
        public ActionResult GuardarCompra(string Fecha, string IdProveedor, string Total, string Sucursal, string Seccion, List<compraDetalle> ListadoDetalle)
        {
            string mensaje = "";
            double iva = 18;
            string idVendedor = User.Identity.GetUserId();
            int codigoPago = 0;
            int codigoSucursal = 0;
            int codigoProveedor = 0;
            decimal total = 0;
            string seccionAlmacen = "";

            if (Fecha == "" || IdProveedor == "" || Total == "" || Sucursal == "" || Seccion=="")
            {
                if (Fecha == "") mensaje = "ERROR EN EL CAMPO FECHA";                
                if (IdProveedor == "") mensaje = "ERROR CON EL CODIGO DEL CLIENTE";
                if (Total == "") mensaje = "ERROR EN EL CAMPO TOTAL";
                if (Sucursal == "") mensaje = "ERROR EN EL CAMPO SUCURSAL";
                if (Seccion == "") mensaje = "ERROR EN EL CAMPO SECCIÓN ALMACÉN";
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
                seccionAlmacen = Seccion;
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
                    CompraDetalle objCompraDetalle = new CompraDetalle(pIdCompra, idProducto, pCantidad, precio,seccionAlmacen);
                    try
                    {                        
                        cDetalle.create(objCompraDetalle);
                    }
                    catch(Exception ex)
                    {
                        mensaje = ex.Message.ToString();
                    }
                               
                            }
                            //mensaje = "VENTA GUARDADA CON EXITO...";
                        }
                                               
            return Json(mensaje);
            }
        
        private List<Compra> cargarCompras()
        {
            //Dao
            CompraDao c = new CompraDao();
            //Tabla
            DataTable dtCompras = c.consultar();
            //Lista
            List<Compra> listCompras = new List<Compra>();
            for(int i = 0; i < dtCompras.Rows.Count; i++)
            {
                //Entity
                Compra compras = new Compra();
                compras.IdCompra = int.Parse(dtCompras.Rows[i]["idCompra"].ToString());
                compras.Total = Convert.ToDecimal(dtCompras.Rows[i]["total"].ToString());
                compras.Fecha = Convert.ToDateTime(dtCompras.Rows[i]["fechaCompra"].ToString());
                compras.NombreSucursal = dtCompras.Rows[i]["descripcion"].ToString();
                compras.NombreProveedor = dtCompras.Rows[i]["nombre"].ToString();
                listCompras.Add(compras);
            }
            return listCompras;
        }
        private List<Compra> cargarCompras(string month, string year)
        {
            //Dao
            CompraDao c = new CompraDao();
            //Tabla
            DataTable dtCompras = c.filtrar(month, year);
            //Lista
            List<Compra> listCompras = new List<Compra>();
            for (int i = 0; i < dtCompras.Rows.Count; i++)
            {
                //Entity
                Compra compras = new Compra();
                compras.IdCompra = int.Parse(dtCompras.Rows[i]["idCompra"].ToString());
                compras.Total = Convert.ToDecimal(dtCompras.Rows[i]["total"].ToString());
                compras.Fecha = Convert.ToDateTime(dtCompras.Rows[i]["fechaCompra"].ToString());
                compras.NombreSucursal = dtCompras.Rows[i]["descripcion"].ToString();
                compras.NombreProveedor = dtCompras.Rows[i]["nombre"].ToString();
                listCompras.Add(compras);
            }
            return listCompras;
        }
        private void cargarFechas()
        {
            //Arreglo de meses
            string[] meses = {"Enero", "Febrero", "Marzo", "Abril", "Mayo","Junio","Julio","Agosto","Septiembre",
            "Octubre","Noviembre","Diciembre"};            
            List<SelectListItem> listMeses = new List<SelectListItem>();

            for (int i = 0; i < 12; i++)
            {                
                    int j = i + 1;
                string numero = j.ToString();              
                listMeses.Add(new SelectListItem() {Text=meses[i], Value = numero });
               // prueba = numero;
            }
            ViewBag.ListaMeses = listMeses;
            //Años 2000-2099
            List<SelectListItem> listYears = new List<SelectListItem>();
            for (int i = 2000; i < 2100; i++)
            {                
                string numero = i.ToString();
                listYears.Add(new SelectListItem() { Text =numero, Value = numero });
                
            }
            ViewBag.ListaYears = listYears;
                        
        }
        //public static string prueba;
        
        [HttpGet]
        public ActionResult Consulta()
        {
            cargarFechas();
           // System.Diagnostics.Debug.WriteLine(prueba);
            return View(cargarCompras());
        }

        //Compras filtradas
        [HttpPost]
        public ActionResult Consulta(string txtMes, string txtyear)
        {
            string month="", year = "";
            string vyear = "";
            int condicion = 0;
            if (txtMes == "")
            {
                txtMes = null;
            }
            if (txtyear == "")
            {
                txtyear = "-1";
            }
            //Validaciones
            if (txtyear == "-1")
            {
                vyear = null;
            }
            //Para meses 10-12
            if (txtMes != null)
            {
                condicion = int.Parse(txtMes);
                if (condicion >= 10)
                {
                    month = txtMes;
                }
                //Para meses 1-9
                else
                {
                    month = "0" + txtMes;
                }
            }
            //Para el año
            if(vyear!=null)
            {
                year = txtyear;
            }       
            cargarFechas();       
            return View(cargarCompras(month,year));
        }
        private List<CompraDetalle> cargarComprasDetalle(int idCompra)
        {
            CompraDetalleDao c = new CompraDetalleDao();
            
            //Lista
            List<CompraDetalle> listCompras = new List<CompraDetalle>();
            listCompras = c.consultarDetalles(idCompra);            
            return listCompras;
        }
        public ActionResult CompraDetalle(int idCompra)
        {

            return View(cargarComprasDetalle(idCompra));
        }
        //Mostrar la info del archivo a eliminar
        private List<Compra> cargarCompras(int idCompra)
        {
            //Dao
            CompraDao c = new CompraDao();
            //Tabla
            DataTable dtCompras = c.consultar(idCompra);
            //Lista
            List<Compra> listCompras = new List<Compra>();
            for (int i = 0; i < dtCompras.Rows.Count; i++)
            {
                //Entity
                Compra compras = new Compra();
                compras.IdCompra = int.Parse(dtCompras.Rows[i]["idCompra"].ToString());
                compras.Total = Convert.ToDecimal(dtCompras.Rows[i]["total"].ToString());
                compras.Fecha = Convert.ToDateTime(dtCompras.Rows[i]["fechaCompra"].ToString());
                compras.NombreSucursal = dtCompras.Rows[i]["descripcion"].ToString();
                compras.NombreProveedor = dtCompras.Rows[i]["nombre"].ToString();
                listCompras.Add(compras);
            }
            return listCompras;
        }
        public ActionResult EliminarCompraPreview(int idCompra)
        {

            return View(cargarCompras(idCompra));
        }
        public ActionResult EliminarCompra(int idCompra)
        {
            //Dao
            CompraDao c = new CompraDao();
            try
            {
                //Eliminar
                c.eliminar(idCompra);
                ViewBag.MensajeEliminar = "Registro eliminado correctamente!";
            }
            catch (Exception ex)
            {
                ViewBag.MensajeEliminar = ex.ToString();
            }
                        
            return View();
        }
        //Mostrar la información actual de la compra
        //Para mostrar la información de los productos de la compra
        [HttpPost]
        public ActionResult ListaProductos(string idCompra)
        {
            List<CompraDetalle> list = new List<CompraDetalle>();            
            list = cdNeg.consultarDetalles(int.Parse(idCompra));
            return Json(list, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Editar(int idCompra)
        {
            cargarProductocmb();
            cargarSucursales();
            CompraDao cd = new CompraDao();
            Compra registro = cd.consultaE(idCompra);
            ViewData["Compra"] = registro.IdCompra.ToString();
            ViewData["Fecha"] = registro.Fecha.ToString("MM/dd/yyyy");
            ViewData["NombreP"] = registro.NombreProveedor.ToString();
            ViewData["CP"] = registro.IdProveedor.ToString();
            ViewData["Sucursal"] = registro.IdSucursal.ToString();
            ViewData["TotalCompra"] = registro.Total.ToString();
            ViewData["Seccion"] = registro.Seccion.ToString();

            return View();
        }
        //Para editar los campos de compra y de compradetalle
        [HttpPost]
        public ActionResult EditarCompra(string Fecha, string IdCompra, string IdProveedor, string Total, string Sucursal, string Seccion, List<compraDetalle> ListadoDetalle)
        {
            string mensaje = "";
            double iva = 18;
            string idVendedor = User.Identity.GetUserId();
            int codigoPago = 0;
            int codigoSucursal = 0;
            int codigoProveedor = 0;
            decimal total = 0;
            string seccionAlmacen = "";

            if (Fecha == "" || IdProveedor == "" || Total == "" || Sucursal == "" || Seccion=="")
            {
                if (Fecha == "") mensaje = "ERROR EN EL CAMPO FECHA";
                if (IdProveedor == "") mensaje = "ERROR CON EL CODIGO DEL CLIENTE";
                if (Total == "") mensaje = "ERROR EN EL CAMPO TOTAL";
                if (Sucursal == "") mensaje = "ERROR EN EL CAMPO SUCURSAL";
                if (Seccion == "") mensaje = "ERROR EN EL CAMPO SECCIÓN ALMACÉN";
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
                seccionAlmacen = Seccion;
                //REGISTRO DE COMPRA
                //Datos del objeto
                Compra objCompra = new Compra();                
                //Se le pasan los datos al objeto
                objCompra.IdCompra = int.Parse(IdCompra);
                objCompra.IdProveedor = codigoProveedor;
                objCompra.IdSucursal = codigoSucursal;                
                objCompra.Total = total;
                //Insercion
                try
                {
                    CompraDao nCompra = new CompraDao();
                    CompraDetalleDao ncd = new CompraDetalleDao();
                    ncd.eliminar(objCompra.IdCompra);
                    nCompra.editar(objCompra.IdCompra, objCompra.Total,Fecha, objCompra.IdSucursal, objCompra.IdProveedor);
                    mensaje = "LA COMPRA FUE EDITADA CON ÉXITO";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message.ToString();
                }               
                //Objeto Dao
                CompraDetalleDao cDetalle = new CompraDetalleDao();
                foreach (var data in ListadoDetalle)
                {
                    string idProducto = data.idProducto.ToString();
                    int pCantidad = Convert.ToInt32(data.cantidad.ToString());
                    decimal precio = Convert.ToDecimal(data.precio.ToString());
                    CompraDetalle objCompraDetalle = new CompraDetalle(int.Parse(IdCompra), idProducto, pCantidad, precio, seccionAlmacen);
                   try
                   {
                        cDetalle.create(objCompraDetalle);                        
                   }
                    catch (Exception e)
                    {
                        mensaje = e.Message.ToString();
                    }                                       
                }
                //mensaje = "VENTA GUARDADA CON EXITO...";
            }

            return Json(mensaje);
        }
    }
    }
