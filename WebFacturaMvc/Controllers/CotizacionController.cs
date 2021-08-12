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
using System.Web.UI;
using WebFacturaMvc.Datos;
using WebFacturaMvc.Reportes.Espanol;

namespace WebFacturaMvc.Controllers
{
    [Authorize(Roles = "ADMIN,STANDARD")]
    public class CotizacionController : Controller
    {
        private CotizacionNeg objCotizacionNeg;
        private ClienteNeg objClienteNeg;
        private ProductoNeg objProductoNeg;
        private ModoPagoNeg objModoPagoNeg;
        private FacturaNeg objFacturaNeg;
        private static int Paso=0;
        private DetalleCotizacionNeg objDetalleVentaNeg;
        private static string idVentaMail;
        private crmconceptoseEntities1 db = new crmconceptoseEntities1();
        public CotizacionController()
        {
            objCotizacionNeg = new CotizacionNeg();
            objClienteNeg = new ClienteNeg();
            objProductoNeg = new ProductoNeg();
            objModoPagoNeg = new ModoPagoNeg();
            objFacturaNeg = new FacturaNeg();
            objDetalleVentaNeg = new DetalleCotizacionNeg();
        }
        [HttpGet]
        public ActionResult ObtenerClientes()
        {
            List<Cliente> lista = objClienteNeg.findAll();
            return View(lista);
        }

        [HttpPost]//para buscar clientes
        public ActionResult ObtenerClientes(string txtnombre, string txtcliente, string txtapellido,string txtemail)
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

        public ActionResult NuevaCotizacion()
        {
            cargarModoPagocmb();
            cargarProductocmb();
            return View();
        }
        [HttpPost]
        public ActionResult GuardarCotizacion(string Fecha, string modoPago, string IdCliente, string Total,string notas, string notasCompras, List<DetalleCotizacion> ListadoDetalle)
        {
            string mensaje = "";
            double iva = 18;
            string idVendedor = User.Identity.GetUserId();
            int codigoPago = 0;
            long codigoCliente = 0;
            double total = 0;

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
                codigoCliente = Convert.ToInt64(IdCliente);
                total = Convert.ToDouble(Total);



                //REGISTRO DE VENTA
                Cotizacion objVenta = new Cotizacion(total, codigoCliente, idVendedor, Fecha, iva,notas,notasCompras);
                string codigoVenta = objCotizacionNeg.create(objVenta);
                if (codigoVenta == "" || codigoVenta == null)
                {
                    mensaje = "ERROR AL REGISTRAR LA VENTA";
                }
                else
                {
                   
                     Session["idVenta"] = codigoVenta;
                     idVentaMail= codigoVenta;
                    //REGISTRO DE FACTURA
                    Factura objFactura = new Factura(Fecha, iva, total, codigoPago);
                    string codigoFactura = objFacturaNeg.create(objFactura);
                    if (codigoFactura == "" || codigoFactura == null)
                    {
                        mensaje = "ERROR AL REGISTRAR LA FACTURA";
                    }
                    else
                    {

                        foreach (var data in ListadoDetalle)
                        {
                            string idProducto = data.IdProducto.ToString();
                            int cantidad = Convert.ToInt32(data.Cantidad.ToString());
                            double descuento = Convert.ToDouble(data.Descuento.ToString());
                            double subtotal = Convert.ToDouble(data.SubTotal.ToString());

                            DetalleCotizacion objDetalleVenta = new DetalleCotizacion(Convert.ToInt64(codigoFactura), Convert.ToInt64(codigoVenta), idProducto, subtotal, descuento, cantidad);
                            objDetalleVentaNeg.create(objDetalleVenta);

                        }
                        mensaje = "VENTA GUARDADA CON EXITO...";
                    }
                }

            }

            return Json(mensaje);
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
            else {
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
            } else{
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
                var lects = db.Database.SqlQuery<SendEmail>(
    "sp_consultaEmailCliente @idVenta",
new SqlParameter("@idVenta", idVenta)).Single();
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
            if (Paso == 1)
            {
                if (Session["idVenta"].ToString() != null)
                {
                    string idVenta = Session["idVenta"].ToString();
                    var lects = db.Database.SqlQuery<SendEmail>(
        "sp_consultaEmailCliente @idVenta",
    new SqlParameter("@idVenta", idVenta)).Single();
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
                    ReportParameter[] rptParams = new ReportParameter[]
                    {
                new ReportParameter("idVenta",idVentaMail)
                };
                    viewer.Refresh();

                    var bytes = viewer.Render("PDF");

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(from, displayName);              
                    mail.To.Add(objSendEmail.To);
                    mail.Subject = objSendEmail.Subject;
                    mail.Body = objSendEmail.Body;
                    mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Quote " +idVentaMail+fechaQuote + ".pdf"));
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
                TempData["msg"] = "<script>alert('"+ ex.Message.ToString() + "');</script>";
                msge = ex.Message + ". Por favor verifica tu conexión a internet y que tus datos sean correctos e intenta nuevamente.";
                return RedirectToAction("NuevaCotizacion", "Cotizacion"); ;
            }
        }
    }
}