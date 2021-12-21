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

namespace WebFacturaMvc.Controllers
{
    [Authorize(Roles = "ADMIN,STANDARD")]
    public class DetalleCotizacionController : Controller
    {
        private DetalleCotizacionNeg objDetalleVentaNeg;
        private FacturaNeg objFacturaNeg;
        private CotizacionNeg objVentaNeg;
        private CotizacionNeg objCotizacionNeg;        
        private ClienteNeg objClienteNeg;
        private ProductoNeg objProductoNeg;
        //private ProductoDao objProductoDao;
        private ModoPagoNeg objModoPagoNeg;        
        private CategoriaNeg objCategoriaNeg;
        private MarcaNeg objMarcaNeg;
        private static int Paso = 0;        
        private static string idVentaMail;
        private crmconceptoseEntities1 db = new crmconceptoseEntities1();
        public DetalleCotizacionController()
        {
            objMarcaNeg = new MarcaNeg();
            objCategoriaNeg = new CategoriaNeg();
            objCotizacionNeg = new CotizacionNeg();
            objClienteNeg = new ClienteNeg();
            objProductoNeg = new ProductoNeg();
            objModoPagoNeg = new ModoPagoNeg();
            objFacturaNeg = new FacturaNeg();
            objDetalleVentaNeg = new DetalleCotizacionNeg();
        }
        // GET: DetalleVenta
        public ActionResult Index()
        {
            List<Cotizacion> lista = objCotizacionNeg.buscar();
            cargarFechas();
            return View(lista);
        }

        //Para ver los detalles de la cotizacion
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
        
        public ActionResult DetalleCotizacion(int idVenta)
        {
            List<DetalleCotizacion> lista = objDetalleVentaNeg.VerDetalles(idVenta);
            return View(lista);
        }
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
            List<Cotizacion> lista = objCotizacionNeg.buscar(month, year);                        
            return View(lista);
        }
        
        
        //COSAS DEL CONTROLLER COTIZACION PRINCIPAL
        [HttpGet]
        public ActionResult ObtenerClientes()
        {
            List<Cliente> lista = objClienteNeg.findAll();
            return View(lista);
        }

        [HttpPost]//para buscar clientes
        public ActionResult ObtenerClientes(string txtnombre, string txtcliente, string txtapellido, string txtemail)
        {
            if (txtnombre == "")
            {
                txtnombre = "-1";
            }
            if (txtapellido == "")
            {
                txtapellido = "-1";
            }
            if (txtcliente == "")
            {
                txtcliente = "-1";
            }
            if (txtemail == "")
            {
                txtemail = "-1";
            }
            long txtCodigoConvertido = Convert.ToInt64(txtcliente);
            Cliente objCliente = new Cliente();
            objCliente.Nombre = txtnombre;
            objCliente.IdCliente = txtCodigoConvertido;
            objCliente.Apellido = txtapellido;
            objCliente.Email = txtemail;
            List<Cliente> cliente = objClienteNeg.findAllClientes(objCliente);
            return View(cliente);
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

        public void cargarProductocmb()
        {
            List<Producto> data = objProductoNeg.findAll();
            SelectList lista = new SelectList(data, "idProducto", "nombre");
            ViewBag.ListaProducto = lista;
        }
        public void cargarModoPagocmb()
        {
            List<ModoPago> data = objModoPagoNeg.findAll();
            SelectList lista = new SelectList(data, "numPago", "nombre");
            ViewBag.ListaModoPago = lista;
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
        public ActionResult reporteActual()
        {
            if (Paso == 1)
            {
                if (Session["idVenta"].ToString() != null)
                {
                    string idVenta = Session["idVenta"].ToString();
                    Paso = 0;
                    return Redirect("~/Reportes/Espanol/frmReporteEs.aspx?IdVenta=" + idVenta);
                }
                else
                {
                    cargarModoPagocmb();
                    cargarProductocmb();
                    TempData["msg"] = "<script>alert('Debes guardar la cotizacion');</script>";
                    return View("NuevaCotizacion");
                }
            }
            else
            {
                cargarModoPagocmb();
                cargarProductocmb();
                TempData["msg"] = "<script>alert('Debes guardar la cotizacion');</script>";
                return View("NuevaCotizacion");
            }

        }

        public ActionResult reporteActualIngles()
        {
            if (Paso == 1)
            {
                if (Session["idVenta"].ToString() != null)
                {
                    string idVenta = Session["idVenta"].ToString();
                    Paso = 0;
                    return Redirect("~/Reportes/Ingles/frmReporteEn.aspx?IdVenta=" + idVenta);
                }
                else
                {
                    cargarModoPagocmb();
                    cargarProductocmb();
                    TempData["msg"] = "<script>alert('Debes guardar la cotizacion');</script>";
                    return View("NuevaCotizacion");
                }
            }
            else
            {
                cargarModoPagocmb();
                cargarProductocmb();
                TempData["msg"] = "<script>alert('Debes guardar la cotizacion');</script>";
                return View("NuevaCotizacion");
            }
        }


        public ActionResult ReporteCotizacion()
        {
            List<Cotizacion> lista = objCotizacionNeg.findAll();
            return View(lista);
        }

        public ActionResult DetallesVenta(long id)
        {
            DetalleCotizacion objDetalleVenta = new DetalleCotizacion();
            objDetalleVenta.IdVenta = id;
            List<DetalleCotizacion> lista = objDetalleVentaNeg.detallesPorIdVenta(objDetalleVenta);
            return View(lista);
        }

        public ActionResult VentaFactura()
        {
            List<Cotizacion> lista = objCotizacionNeg.findAll();
            return View(lista);
        }

        public ActionResult BuscarHistorial(string idProducto)
        {
            Historial objHistorial = new Historial();
            objHistorial.IdProducto = idProducto;
            List<Historial> Cotizacion = objCotizacionNeg.findHistorial(objHistorial);
            return View(Cotizacion);
        }

        public ActionResult SendEmailFactura(int idVenta)
        {
            if (idVenta != null)
            {
                //string id = Session["idVenta"].ToString();
                var lects = db.Database.SqlQuery<SendEmail>("sp_consultaEmailCliente @idVenta", new SqlParameter("@idVenta", idVenta)).Single();
                Paso = 0;
                SendEmail email = new SendEmail();
                email = lects;
                return View("SendEmail", lects);
            }
            else
            {
                cargarModoPagocmb();
                cargarProductocmb();
                TempData["msg"] = "<script>alert('Debes guardar la cotizacion');</script>";
                return View();
            }
        }

        public ActionResult SendEmail()
        {
            Llenar();
            if (Paso == 1)
            {
                if (Session["idVenta"].ToString() != null)
                {
                    string idVenta = Session["idVenta"].ToString();
                    var lects = db.Database.SqlQuery<SendEmail>("sp_consultaEmailCliente @idVenta", new SqlParameter("@idVenta", idVenta)).Single();
                    Paso = 0;
                    SendEmail email = new SendEmail();
                    email = lects;
                    return View(lects);
                }
                else
                {
                    cargarModoPagocmb();
                    cargarProductocmb();
                    TempData["msg"] = "<script>alert('Debes guardar la cotizacion');</script>";
                    return View();
                }
            }
            else
            {

                cargarModoPagocmb();
                cargarProductocmb();
                TempData["msg"] = "<script>alert('Debes guardar la cotizacion');</script>";
                return View("NuevaCotizacion");
            }
        }

        [HttpPost]
        public ActionResult SendEmail(SendEmail objSendEmail)
        {
            Llenar();
            configuracion objConfiguracion = new configuracion();
            if (!(Request.IsAuthenticated || User.IsInRole("ADMIN")))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                string id = User.Identity.GetUserId();
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                objConfiguracion = db.configuracion.First(p => p.usuario == id);
                if (objConfiguracion == null)
                {
                    return HttpNotFound();
                }
            }

            string msge = "Error al enviar este correo. Por favor verifique los datos o intente más tarde.";
            string from = objConfiguracion.email;
            string displayName = objConfiguracion.displayName;
            try
            {

                using (var viewer = new LocalReport())
                {

                    DateTime fechaActual = DateTime.Today;
                    string fechaQuote = string.Format("{0}{1}{2}", fechaActual.Month, fechaActual.Day, fechaActual.Year);

                    DataTable dt = frmReporteEs.cargar(idVentaMail);
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    viewer.DataSources.Add(rds);
                    viewer.ReportPath = "Reportes/Ingles/rptFacturaEn.rdlc";
                    //parameters
                    ReportParameter[] rptParams = new ReportParameter[] { new ReportParameter("idVenta", idVentaMail) };
                    viewer.Refresh();
                    var bytes = viewer.Render("PDF");
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(from, displayName);
                    mail.To.Add(objSendEmail.To);
                    mail.Subject = objSendEmail.Subject;
                    mail.Body = objSendEmail.Body;
                    mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Quote " + idVentaMail + fechaQuote + ".pdf"));
                    mail.IsBodyHtml = true;
                    SmtpClient client = new SmtpClient(objConfiguracion.servidorSmtp, objConfiguracion.puerto); //Aquí debes sustituir tu servidor SMTP y el puerto
                    client.Credentials = new NetworkCredential(from, EncriptacionSha.DesEncriptar(objConfiguracion.contrasena));
                    client.EnableSsl = true;//En caso de que tu servidor de correo no utilice cifrado SSL,poner en false
                    client.Send(mail);
                    msge = "¡Correo enviado exitosamente! Pronto te contactaremos.";
                }
                return RedirectToAction("NuevaCotizacion", "Cotizacion"); ;
            }
            catch (Exception ex)
            {
                TempData["msg"] = "<script>alert('" + ex.Message.ToString() + "');</script>";
                msge = ex.Message + "Por favor verifica tu conexión a internet y que tus datos sean correctos e intenta nuevamente.";
                return RedirectToAction("NuevaCotizacion", "Cotizacion"); ;
            }
        }

        [HttpGet]
        public ActionResult ObtenerProductos()
        {
            List<Producto> lista = objProductoNeg.findAll();
            cargarCategoria();
            cargarMarca();
            return View(lista);
        }
        public void Llenar()
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem() { Text = "Alto", Value = "A" });
            lst.Add(new SelectListItem() { Text = "Medio", Value = "M" });
            lst.Add(new SelectListItem() { Text = "Bajo", Value = "B" });
            ViewBag.Estatus = lst;
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
        //Mostrar la información actual de la cotizacion
        //Para mostrar la información de los productos de la cotizacion
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
        public ActionResult Editar(int idVenta)
        {
            Llenar();
            cargarModoPagocmb();
            cargarProductocmb();
            Cotizacion c = new Cotizacion();
            c.IdVenta = idVenta;
            CotizacionNeg cn = new CotizacionNeg();
            Cotizacion objCotizacion = cn.buscarIdVenta(idVenta);
            //para obtener al cliente

            ViewData["IdVenta"] = objCotizacion.IdVenta.ToString();
            ViewData["Cliente"] = objCotizacion.Cliente.ToString();
            ViewData["idCliente"] = objCotizacion.IdCliente.ToString();
            ViewData["Email"] = objCotizacion.Email.ToString();
            ViewData["EstatusC"] = objCotizacion.estatus.ToString();
            ViewData["Total"] = objCotizacion.Total.ToString();
            ViewData["Notas"] = objCotizacion.notas.ToString();
            ViewData["NotasCompras"] = objCotizacion.notasCompras.ToString();
            ViewData["FechaCotizacion"] = objCotizacion.FechaCotizacion.ToString("MM/dd/yyyy");
            //Para el estatus
            
            ViewData["Status"] = objCotizacion.estatus.ToString();
            string e = objCotizacion.estatus.ToString();
                                       
            return View();
        }
        [HttpPost]
        public ActionResult GuardarCotizacion(string Fecha, string modoPago, string IdCliente, string Total, string notas, string notasCompras, string estatus, string idVenta, List<DetalleCotizacion> ListadoDetalle)
        {
            Llenar();
            string mensaje = "";
            double iva = 18;
            string idVendedor = User.Identity.GetUserId();
            int codigoPago = 0;
            long codigoCliente = 0;
            decimal total = 0;
            int Venta = int.Parse(idVenta);


            if (Fecha == "" || modoPago == "" || IdCliente == "" || Total == "")
            {
                if (Fecha == "") mensaje = "ERROR EN EL CAMPO FECHA";
                if (modoPago == "") mensaje = "SELECCIONE UN MODO DE PAGO";
                if (IdCliente == "") mensaje = "ERROR CON EL CODIGO DEL CLIENTE";
                if (Total == "") mensaje = "ERROR EN EL CAMPO TOTAL";
            }
            else
            {
                Paso = 1;
                codigoPago = Convert.ToInt32(modoPago);
                codigoCliente = int.Parse(IdCliente);
                total = Convert.ToDecimal(Total);


                string codigoVenta="";
                //REGISTRO DE Actualzacion
                CotizacionNeg c = new CotizacionNeg();
                
                try
                {
                    c.Actualizar(Venta, total, int.Parse(IdCliente), idVendedor, Fecha, Convert.ToDecimal(iva), notas, notasCompras, estatus);
                    codigoVenta = "TRUE";
                    //mensaje += "Cotización registrada correctamente";
                }
                catch(Exception ex)
                {
                    mensaje += ex.Message.ToString();
                }    
                
                if (codigoVenta == "" || codigoVenta == null)
                {
                    mensaje = "ERROR AL REGISTRAR LA VENTA";
                }
                else
                {

                    Session["idVenta"] = codigoVenta;
                    idVentaMail = codigoVenta;
                    //REGISTRO DE FACTURA
                    Factura objFactura = new Factura(Fecha, iva, Convert.ToDouble(total), codigoPago);
                    string codigoFactura = objFacturaNeg.create(objFactura);
                    if (codigoFactura == "" || codigoFactura == null)
                    {
                        mensaje = "ERROR AL REGISTRAR LA FACTURA";
                    }
                    else
                    {
                        DetalleCotizacionNeg cd = new DetalleCotizacionNeg();
                        //Para eliminar primero los registros en Detalle Cotizacion
                        cd.preEliminar(Venta);
                        foreach (var data in ListadoDetalle)
                        {
                            
                            string idProducto = data.IdProducto.ToString();
                            int cantidad = Convert.ToInt32(data.Cantidad.ToString());
                            decimal descuento = Convert.ToDecimal(data.Descuento.ToString());
                            decimal subtotal = Convert.ToDecimal(data.SubTotal.ToString());
                            try
                            {
                                cd.Actualizar(Venta, subtotal, idProducto, descuento, cantidad);
                                //mensaje += "Detalle registrado correctamente";
                            }
                            catch(Exception ex)
                            {
                                mensaje += ex.Message.ToString();
                            }
                            
                        }
                        mensaje = "Venta modificada correctamente!";
                    }
                }

            }

            return Json(mensaje);
        }
        //Para Ver la Información Pre eliminar del registro a borrar
        public ActionResult PreEliminar(int idVenta)
        {
            Cotizacion c = objCotizacionNeg.buscarIdVenta(idVenta);
            List<Cotizacion> lista = new List<Cotizacion>();
            lista.Add(c);
            return View(lista);
        }
        
        //Eliminar registro
        public ActionResult Eliminar(int idVenta)
        {
            try
            {
                //Para eliminar primero los registros en Detalle Cotizacion
                DetalleCotizacionNeg cd = new DetalleCotizacionNeg();                
                cd.preEliminar(idVenta);
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            try
            {
                //Se elimina la cotizacion
                CotizacionNeg co = new CotizacionNeg();
                co.Eliminar(idVenta);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return View();
        }

        [HttpPost]
        public ActionResult AgregarRfq(string idVenta)
        {
            string id = User.Identity.GetUserId();
            List<Cotizacion> lista = objCotizacionNeg.buscarConEstatus();

            cargarFechas();
            string mensaje = "Error";
            try
            {
                mensaje = objCotizacionNeg.agregarRFQ(idVenta, id);
            }

            catch (Exception e)
            {
                mensaje = "Error: " + e.ToString();
            }
            return Json(mensaje);
        }
    }
}