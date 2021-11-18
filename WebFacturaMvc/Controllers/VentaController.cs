using Microsoft.AspNet.Identity;
using Microsoft.Reporting.WebForms;
using Model.Entity;
using Model.Neg;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using WebFacturaMvc.Datos;
using WebFacturaMvc.Reportes.Espanol;
using System.Diagnostics;
using System.Web;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace WebFacturaMvc.Controllers
{
    [Authorize(Roles = "ADMIN,STANDARD")]
    public class VentaController : Controller
    {
        private CotizacionNeg objCotizacionNeg = new CotizacionNeg();
        private VentaNeg objVentaNeg = new VentaNeg();
        // GET: Venta
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
                listMeses.Add(new SelectListItem() { Text = meses[i], Value = numero });
                // prueba = numero;
            }
            ViewBag.ListaMeses = listMeses;
            //Años 2000-2099
            List<SelectListItem> listYears = new List<SelectListItem>();
            for (int i = 2000; i < 2100; i++)
            {
                string numero = i.ToString();
                listYears.Add(new SelectListItem() { Text = numero, Value = numero });

            }
            ViewBag.ListaYears = listYears;

        }
        //Pagina principal
        public ActionResult Index()
        {
            List<Cotizacion> lista = objVentaNeg.buscar();
            cargarFechas();
            return View(lista);
        }
        //Filtrar
        [HttpPost]
        public ActionResult Index(string txtMes, string txtyear)
        {
            string month = "", year = "";
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
            if (vyear != null)
            {
                year = txtyear;
            }
            cargarFechas();
            List<Cotizacion> lista = objVentaNeg.buscar(month, year);
            return View(lista);
        }
        //Pantalla que mostrará la información del cliente, venta y permitirá guardar la orden de compra
        public ActionResult HacerVenta(int idCotizacion)
        {
            cargarPaises();
            //Trae la información del cliente
            VentaNeg objVenta = new VentaNeg();
            DataTable dtCliente = objVenta.consulta_VC(idCotizacion);
            ViewData["NombreCliente"] = dtCliente.Rows[0]["Cliente"].ToString();
            ViewData["Cliente"] = dtCliente.Rows[0]["idCliente"].ToString();
            ViewData["Email"] = dtCliente.Rows[0]["email"].ToString();
            ViewData["Cotizacion"] = dtCliente.Rows[0]["idVenta"].ToString();
            CotizacionNeg cn = new CotizacionNeg();
            Cotizacion objCotizacion = cn.buscarIdVenta(int.Parse(dtCliente.Rows[0]["idVenta"].ToString()));
            //para obtener el total            
            ViewData["TotalVendido"] = objCotizacion.Total.ToString();
            //Obtener Salesperson y ID
            DataTable dtSP = objVenta.consulta_SalesPerson(idCotizacion);            
            ViewData["SalesPerson"] = dtSP.Rows[0]["Nombre"].ToString();
            return View();
        }
        public void cargarPaises()
        {            
            List<Pais> listPaises = new List<Pais>();
            PaisNeg p = new PaisNeg();
            listPaises = p.cargarPaises();
            SelectList lista = new SelectList(listPaises, "IdPais", "NombrePais");
            ViewBag.Paises = lista;
        }
        [HttpPost]
        public JsonResult cargarEstados(string IdPais)
        {
            System.Diagnostics.Debug.WriteLine("Pais: "+IdPais);
            List<Estado> listEstados = new List<Estado>();
            EstadoNeg e = new EstadoNeg();
            Estado es = new Estado();
            if (IdPais == null || IdPais == "0" || IdPais=="")
            {
                es.IdEstado = 0;
                es.NombreEstado = "Please select a state";
                listEstados.Add(es);
            }
            else
            {
                es.IdEstado = 0;
                es.NombreEstado = "Please select a state";
                listEstados.Add(es);
                List<Estado> preEstados = new List<Estado>();
                preEstados= e.cargarEstados(int.Parse(IdPais));
                foreach(var state in preEstados)
                {
                    listEstados.Add(state);
                }
            }                        
            SelectList lista = new SelectList(listEstados, "IdEstado", "NombreEstado");
            return Json(new SelectList(lista, "Value", "Text"));
        }
        [HttpPost]
        public JsonResult cargarCiudades(string IdEstado)
        {
            System.Diagnostics.Debug.WriteLine("Estado: "+IdEstado);
            List<Ciudad> listCiudades = new List<Ciudad>();
            CiudadNeg c = new CiudadNeg();
            Ciudad ci = new Ciudad();
            if (IdEstado == null || IdEstado == "0" || IdEstado == "")
            {
                ci.IdCiudad = 0;
                ci.NombreCiudad = "Please select a city";
                listCiudades.Add(ci);
            }
            else
            {
                listCiudades = c.cargarCiudades(int.Parse(IdEstado));
            }
            SelectList lista = new SelectList(listCiudades, "IdCiudad", "NombreCiudad");
            return Json(new SelectList(lista, "Value", "Text"));
        }
        [HttpPost]
        public ActionResult ListaProductos(string idVenta)
        {
            List<Producto> listProductos = new List<Producto>();
            //Para traer los productos del detalle cotizacion de la base de datos
            DetalleCotizacionNeg dc = new DetalleCotizacionNeg();
            List<DetalleCotizacion> listDetalles = dc.VerProductos(int.Parse(idVenta));

            //Asignar los productos
            foreach (var item in listDetalles)
            {

                Producto objProducto = new Producto();
                //LINQ para traer el producto                
                DetalleCotizacionNeg dcn = new DetalleCotizacionNeg();
                objProducto = dcn.VerProducto(int.Parse(item.IdProducto));
                listProductos.Add(objProducto);
                //Debug
                //Debug.WriteLine("Descuento#"+i+": "+objProducto.Descuento);

            }
            return Json(listProductos, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult GuardarVenta(string Fecha, string IdCotizacion, string IdCliente, string CP, string IdCiudad, string Colonia,string Calle, string NumExt, string NumInt, string Telefono, string OrdenCompra)
        {

            string mensaje = "";
            
            if (CP=="" || CP==null)
            {
                mensaje = "Código Postal vacío o inválido";
            }
            else if (IdCiudad == null || IdCiudad == "" || IdCiudad=="0")
            {
                mensaje = "Seleccione una ciudad";
            }
            else if (Colonia == null || Colonia == "")
            {
                mensaje = "Campo colonia vacío";
            }
            else if (Calle == null || Calle == "")
            {
                mensaje = "Campo calle vacío";
            }
            else if (NumExt == null || NumExt == "")
            {
                mensaje = "El número exterior es necesario";
            }
            else if (Telefono == null || Telefono == "")
            {
                mensaje = "Campo télefono vacío";
            }
            else if (OrdenCompra == null)
            {
                mensaje = "No se ha seleccionado una orden de compra";
            }
            else
            {
               try
               {
                    //Obtener Salesperson y ID
                    VentaNeg objVentaNeg = new VentaNeg();
                    DataTable dtSP = objVentaNeg.consulta_SalesPerson(int.Parse(IdCotizacion));                    
                    Venta objVenta = new Venta();
                    objVenta.Vendedor = dtSP.Rows[0]["Id"].ToString();
                    objVenta.IdCotizacion = int.Parse(IdCotizacion);
                    objVenta.IdCliente = int.Parse(IdCliente);
                    objVenta.CP = CP.ToString();
                //Convertir IdCiudad a int
                    int ciudad = int.Parse(IdCiudad);
                    objVenta.IdCiudad = ciudad;
                    objVenta.Calle = Calle.ToString();
                    objVenta.NumExterior = NumExt.ToString();
                    objVenta.NumInterior = NumInt.ToString();
                    objVenta.Colonia = Colonia.ToString();
                    objVenta.Telefono = Telefono.ToString();
                    //Transformar archivo a bytes
                    byte [] archivoData = null;
                    if (OrdenCompra == "Vacio" || OrdenCompra == null)
                    {
                        objVenta.Archivo = archivoData;
                    }
                    else
                    {
                        archivoData = Convert.FromBase64String(OrdenCompra);
                        objVenta.Archivo = archivoData;
                    }                    
                    //objVenta.ArchivoTemporal = OrdenCompra;
                    objVenta.Fecha = Convert.ToDateTime(Fecha);
                    objVentaNeg.create(objVenta);
                    //Cambia el estado de cotizaicon a G (ganada)
                    objVentaNeg.cambiarEstatus(objVenta.IdCotizacion);
                    mensaje = "Venta registrada correctamente";
               }
               catch(Exception ex)
               {
                    mensaje = "Error, ha ocurrido algo: " + ex.Message.ToString();
                }
            }
            return Json(mensaje);
        }
        //Trae las ventas disponibles
        public ActionResult Consulta()
        {
            List<Venta> lista = objVentaNeg.consulta_Ventas();
            cargarFechas();
            return View(lista);
        }

        //Trae las ventas disponibles filtradas
        [HttpPost]
        public ActionResult Consulta(string txtMes, string txtyear)
        {
            string month = "", year = "";
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
            if (vyear != null)
            {
                year = txtyear;
            }
            cargarFechas();
            List<Venta> lista = objVentaNeg.consulta_Ventas(month, year);
            return View(lista);
        }
        
        
        public ActionResult VerOrdenCompra(int idVenta)
        {
            
                Venta v = objVentaNeg.VerOrdenCompra(idVenta);
            
            //Este es el bueno por fin traerBytes()
            byte[] byteArray = objVentaNeg.traerBytes(idVenta);
            
            MemoryStream pdfStream = new MemoryStream();            
                pdfStream.Write(byteArray, 0, byteArray.Length);
                pdfStream.Position = 0;
                return new FileStreamResult(pdfStream, "application/pdf");
            
            //string pdf = Convert.ToBase64String(v.Archivo);
            //System.Diagnostics.Debug.WriteLine("Archivo: " + v.Archivo.Length);
            
        }
        //Para el ver Venta
        public void cargarEstado(string idPais)
        {
            List<Estado> listEstados = new List<Estado>();
            EstadoNeg e = new EstadoNeg();
            listEstados = e.cargarEstados(int.Parse(idPais));
            SelectList lista = new SelectList(listEstados, "IdEstado", "NombreEstado");
            ViewBag.Estados = lista;
        }
        public void cargarCiudad(string idEstado)
        {
            List<Ciudad> listCiudades = new List<Ciudad>();
            CiudadNeg c = new CiudadNeg();
            listCiudades = c.cargarCiudades(int.Parse(idEstado));
            SelectList lista = new SelectList(listCiudades, "IdCiudad", "NombreCiudad");
            ViewBag.Ciudades = lista;
        }
        //Ver Venta
        public ActionResult VerVenta(int idVenta)
        {
            //Obtener información
            VentaNeg objVenta = new VentaNeg();
            Venta v = objVenta.VerVenta(idVenta);
            //Mostrar información
            ViewData["Vendedor"] = v.Vendedor;
            ViewData["IDC"] = v.IdCotizacion;
            ViewData["FechaVenta"] = v.Fecha.ToString("MM/dd/yyyy");
            ViewData["NC"] = v.NombreCliente;
            ViewData["Mail"] = v.Email;
            ViewData["IDV"] = v.IdVenta;
            ViewData["codigoP"] = v.CP;
            ViewData["colonia"] = v.Colonia;
            ViewData["calle"] = v.Calle;
            ViewData["numExt"] = v.NumExterior;
            ViewData["numInt"] = v.NumInterior;
            ViewData["telefono"] = v.Telefono;
            ViewData["Pais"] = v.IdPais;
            ViewData["Estado"] = v.IdEstado;
            ViewData["Ciudad"] = v.IdCiudad;
            ViewData["TotalVenta"] = v.Total;
            if(v.ArchivoTemporal==null || v.ArchivoTemporal=="")
            {
                ViewData["Archivo"] = "Vacio";
            }
            else
            {
                ViewData["Archivo"] = "Contiene archivo";
            }
            cargarPaises();
            cargarEstado(Convert.ToString(v.IdPais));
            cargarCiudad(Convert.ToString(v.IdEstado));
            return View();
        }
        //Ver Venta
        public ActionResult EditarVenta(int idVenta)
        {
            //Obtener información
            VentaNeg objVenta = new VentaNeg();
            Venta v = objVenta.VerVenta(idVenta);
            //Mostrar información
            ViewData["Vendedor"] = v.Vendedor;
            ViewData["IDC"] = v.IdCotizacion;
            ViewData["FechaVenta"] = v.Fecha.ToString("MM/dd/yyyy");
            ViewData["NC"] = v.NombreCliente;
            ViewData["Mail"] = v.Email;
            ViewData["IDV"] = v.IdVenta;
            ViewData["codigoP"] = v.CP;
            ViewData["colonia"] = v.Colonia;
            ViewData["calle"] = v.Calle;
            ViewData["numExt"] = v.NumExterior;
            ViewData["numInt"] = v.NumInterior;
            ViewData["telefono"] = v.Telefono;
            ViewData["Pais"] = v.IdPais;
            ViewData["Estado"] = v.IdEstado;
            ViewData["Ciudad"] = v.IdCiudad;
            ViewData["TotalVenta"] = v.Total;
            cargarPaises();
            cargarEstado(Convert.ToString(v.IdPais));
            cargarCiudad(Convert.ToString(v.IdEstado));
            return View();
        }
        //Ver PreView de la venta
        public ActionResult EliminarPreview(int idVenta)
        {
            //Obtener información
            VentaNeg objVenta = new VentaNeg();
            Venta v = objVenta.VerVenta(idVenta);
            List<Venta> lista = new List<Venta>();
            lista.Add(v);
            return View(lista);
        }
        //Eliminar la Venta
        public ActionResult Eliminar(int idVenta)
        {
            VentaNeg objVenta = new VentaNeg();
            objVentaNeg.eliminarVenta(idVenta);
            return View();
        }
        [HttpPost]
        public ActionResult EditarVenta(string IdVenta, string CP, string IdCiudad, string Colonia, string Calle, string NumExt, string NumInt, string Telefono, string OrdenCompra)
        {

            string mensaje = "";

            if (CP == "" || CP == null)
            {
                mensaje = "Código Postal vacío o inválido";
            }
            else if (IdCiudad == null || IdCiudad == "" || IdCiudad == "0")
            {
                mensaje = "Seleccione una ciudad";
            }
            else if (Colonia == null || Colonia == "")
            {
                mensaje = "Campo colonia vacío";
            }
            else if (Calle == null || Calle == "")
            {
                mensaje = "Campo calle vacío";
            }
            else if (NumExt == null || NumExt == "")
            {
                mensaje = "El número exterior es necesario";
            }
            else if (Telefono == null || Telefono == "")
            {
                mensaje = "Campo télefono vacío";
            }           
            else
            {
                try
                {
                    //Obtener Salesperson y ID
                    VentaNeg objVentaNeg = new VentaNeg();                    
                    Venta objVenta = new Venta();                    
                    objVenta.CP = CP.ToString();
                    //Convertir IdCiudad a int
                    int ciudad = int.Parse(IdCiudad);
                    objVenta.IdVenta = int.Parse(IdVenta);
                    objVenta.IdCiudad = ciudad;
                    objVenta.Calle = Calle.ToString();
                    objVenta.NumExterior = NumExt.ToString();
                    objVenta.NumInterior = NumInt.ToString();
                    objVenta.Colonia = Colonia.ToString();
                    objVenta.Telefono = Telefono.ToString();
                    //Transformar archivo a bytes
                    byte[] archivoData = null;
                    if (OrdenCompra == "Vacio" || OrdenCompra == null)
                    {
                        objVenta.Archivo = archivoData;
                    }
                    else
                    {
                        archivoData = Convert.FromBase64String(OrdenCompra);
                        objVenta.Archivo = archivoData;
                    }                                        
                    
                    objVentaNeg.editar(objVenta);                    
                    mensaje = "Venta editada correctamente";
                }
                catch (Exception ex)
                {
                    mensaje = "Error, ha ocurrido algo: " + ex.Message.ToString();
                }
            }
            return Json(mensaje);
        }

    }

    }
