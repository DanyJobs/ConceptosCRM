using Lenguaje;
using Microsoft.AspNet.Identity;
using Microsoft.Reporting.WebForms;
using MimeKit;
using Model.Dao;
using Model.Entity;
using Model.Neg;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.WebPages;
using WebFacturaMvc.Datos;
using WebFacturaMvc.Reportes.Espanol;

namespace WebFacturaMvc.Controllers
{
    [Authorize(Roles = "ADMIN,STANDARD")]
    [OverrideAuthentication]
    public class CotizacionController : Controller
    {
        private CotizacionNeg objCotizacionNeg;
        private ClienteNeg objClienteNeg;
        private ProductoNeg objProductoNeg;
        //private ProductoDao objProductoDao;
        private ModoPagoNeg objModoPagoNeg;
        private FacturaNeg objFacturaNeg;
        private CategoriaNeg objCategoriaNeg;
        private MarcaNeg objMarcaNeg;
        private static int Paso = 0;
        private DetalleCotizacionNeg objDetalleVentaNeg;
        private static string idVentaMail;
        private cotizacion objCotizacion;
        private EmailMarketing objMarketing;
        private Model.Entity.RFQItem objRFQItem;
        private PlantillaCorreoNeg objPlantillaCorreoNeg;

        private crmconceptoseEntities1 db = new crmconceptoseEntities1();
        public CotizacionController()
        {
            objCotizacion = new cotizacion();
            objMarketing = new EmailMarketing();
            objMarcaNeg = new MarcaNeg();
            objCategoriaNeg = new CategoriaNeg();
            objCotizacionNeg = new CotizacionNeg();
            objClienteNeg = new ClienteNeg();
            objProductoNeg = new ProductoNeg();
            objModoPagoNeg = new ModoPagoNeg();
            objFacturaNeg = new FacturaNeg();
            objDetalleVentaNeg = new DetalleCotizacionNeg();
            objPlantillaCorreoNeg = new PlantillaCorreoNeg();

        }

        public string firma(configuracion objConfiguracion) {
            string stCuerpoHTML="";
            stCuerpoHTML += "<table cellpadding = '0' cellspacing = '0' class='sc-gPEVay eQYmiW'";
            stCuerpoHTML += "style ='vertical-align: -webkit-baseline-middle; font-size: medium; font-family: Arial; min-width: 450px; text-align: center;''>";
            stCuerpoHTML += "<tbody>";
            stCuerpoHTML += "<tr>";
            stCuerpoHTML += "<td>";
            stCuerpoHTML += "<table cellpadding = '0' cellspacing= '0' class='sc-gPEVay eQYmiW'";
            stCuerpoHTML += " style='vertical-align: -webkit-baseline-middle; font-size: medium; font-family: Arial; width: 100%;'>";
            stCuerpoHTML += "<tbody>";
            stCuerpoHTML += "<tr>";
            stCuerpoHTML += "<td height = '30' ></td> ";
            stCuerpoHTML += "</tr> ";
            stCuerpoHTML += "<tr>";
            stCuerpoHTML += "<td color='#5669e2' direction='horizontal' height='1' class='sc-jhAzac hmXDXQ'";
            stCuerpoHTML += " style='width: 100%; border-bottom: 1px solid rgb(86, 105, 226); border-left: none; display: block;'>";
            stCuerpoHTML += "</td>";
            stCuerpoHTML += "</tr>";
            stCuerpoHTML += "<tr>";
            stCuerpoHTML += "<td height = '30' ></td>";
            stCuerpoHTML += "</tr>";
            stCuerpoHTML += "</tbody>";
            stCuerpoHTML += "</table> ";
            stCuerpoHTML += "<table cellpadding='0' cellspacing='0' class='sc-gPEVay eQYmiW'";
            stCuerpoHTML += " style='vertical-align: -webkit-baseline-middle; font-size: medium; font-family: Arial; width: 100%;'>";
            stCuerpoHTML += "<tbody>";
            stCuerpoHTML += "<tr style = 'text-align: center;' >";
            stCuerpoHTML += "<td>";
            stCuerpoHTML += "<img src='cid:Firma' alt='' width='200'  style='width:50px; height:50px; border-radius:150px; text-align:center;'/>";
            stCuerpoHTML += "<h3 color='#000000' class='sc-fBuWsC eeihxG' style='margin: 0px; font-size: 18px; color: rgb(0, 0, 0);'>";
            stCuerpoHTML += "<span>" + objConfiguracion.nombre;
            stCuerpoHTML += "</span> ";
            stCuerpoHTML += "</h3>";
            stCuerpoHTML += "<p color = '#000000' font-size='medium' class='sc-fMiknA bxZCMx'";
            stCuerpoHTML += "style='margin: 0px; color: rgb(0, 0, 0); font-size: 14px; line-height: 22px;'><span>" + objConfiguracion.puesto;
            stCuerpoHTML += " </span></p>";
            stCuerpoHTML += "<p color = '#000000' font-size= 'medium' class='sc-dVhcbM fghLuF'";
            stCuerpoHTML += "style='margin: 0px; font-weight: 500; color: rgb(0, 0, 0); font-size: 14px; line-height: 22px;'>";
            stCuerpoHTML += "<span>Conceptos Electronics</span>";
            stCuerpoHTML += "</p>";
            stCuerpoHTML += "</td>";
            stCuerpoHTML += "</tr>";
            stCuerpoHTML += "<tr style = 'vertical-align: middle;'>";
            stCuerpoHTML += "<td>";
            stCuerpoHTML += "<table cellpadding='0' cellspacing='0' class='sc-gPEVay eQYmiW'";
            stCuerpoHTML += " style='vertical-align: -webkit-baseline-middle; font-size: medium; font-family: Arial;'>";
            stCuerpoHTML += "<tbody>";
            stCuerpoHTML += "<tr height = '25' style='width:500px;vertical-align: middle;'>";
            stCuerpoHTML += "<td width = '30' style='vertical-align: middle;'>";
            stCuerpoHTML += "<table cellpadding = '0' cellspacing='0' class='sc-gPEVay eQYmiW'";
            stCuerpoHTML += " style='vertical-align: -webkit-baseline-middle; font-size: medium; font-family: Arial;'>";
            stCuerpoHTML += "<tbody>";
            stCuerpoHTML += "<tr>";
            stCuerpoHTML += "<td style = 'vertical-align: bottom;' ><span color='#5669e2'";
            stCuerpoHTML += " width='11' class='sc-jlyJG bbyJzT'";
            stCuerpoHTML += " style='display: block; background-color: rgb(86, 105, 226);'><img";
            stCuerpoHTML += " src = 'https://cdn2.hubspot.net/hubfs/53/tools/email-signature-generator/icons/phone-icon-2x.png'";
            stCuerpoHTML += " color='#5669e2' width='13'";
            stCuerpoHTML += " class='sc-iRbamj blSEcj'";
            stCuerpoHTML += " style='display: block; background-color: rgb(86, 105, 226);'></span>";
            stCuerpoHTML += "</td>";
            stCuerpoHTML += "</tr>";
            stCuerpoHTML += "</tbody>";
            stCuerpoHTML += "</table>";
            stCuerpoHTML += "</td>";
            stCuerpoHTML += "<td style = 'text-align: left;padding: 0px; color: rgb(0, 0, 0);' ><a href='tel:" + objConfiguracion.telefono + "'";
            stCuerpoHTML += " color='#000000' class='sc-gipzik iyhjGb'";
            stCuerpoHTML += " style='text-decoration: none; color: rgb(0, 0, 0); font-size: 12px;'><span>" + objConfiguracion.telefono + "</span></a>";
            stCuerpoHTML += "  | <a href = 'tel:" + objConfiguracion.celular + "' color='#000000' class='sc-gipzik iyhjGb'";
            stCuerpoHTML += "  style='text-decoration: none; color: rgb(0, 0, 0); font-size: 12px;'><span>" + objConfiguracion.celular + "</span></a>";
            stCuerpoHTML += "</td>";
            stCuerpoHTML += "</tr>";
            stCuerpoHTML += "<tr height = '25' style='text-align: left;vertical-align: middle;'>";
            stCuerpoHTML += "<td width = '30' style='text-align: left;vertical-align: middle;'>";
            stCuerpoHTML += "<table cellpadding = '0' cellspacing='0' class='sc-gPEVay eQYmiW'";
            stCuerpoHTML += " style='vertical-align: -webkit-baseline-middle; font-size: medium; font-family: Arial;'>";
            stCuerpoHTML += "<tbody>";
            stCuerpoHTML += "<tr>";
            stCuerpoHTML += "<td style = 'text-align: left;vertical-align: bottom;' ><span color='#5669e2'";
            stCuerpoHTML += " width='11' class='sc-jlyJG bbyJzT'";
            stCuerpoHTML += " style='display: block; background-color: rgb(86, 105, 226);'><img";
            stCuerpoHTML += " src = 'https://cdn2.hubspot.net/hubfs/53/tools/email-signature-generator/icons/email-icon-2x.png'";
            stCuerpoHTML += " color='#5669e2' width='13'";
            stCuerpoHTML += "  class='sc-iRbamj blSEcj'";
            stCuerpoHTML += " style='display: block; background-color: rgb(86, 105, 226);'></span>";
            stCuerpoHTML += "</td>";
            stCuerpoHTML += "</tr>";
            stCuerpoHTML += "</tbody>";
            stCuerpoHTML += "</table>";
            stCuerpoHTML += "</td>";
            stCuerpoHTML += "<td style = 'text-align: left;padding: 0px;' ><a";
            stCuerpoHTML += "  href='mailto:" + objConfiguracion.email + "' color='#000000'";
            stCuerpoHTML += " class='sc-gipzik iyhjGb'";
            stCuerpoHTML += " style='text-align: left;text-decoration: none; color: rgb(0, 0, 0); font-size: 12px;'><span>" + objConfiguracion.email + "</span></a>";
            stCuerpoHTML += "</td>";
            stCuerpoHTML += "</tr>";
            stCuerpoHTML += "<tr height = '25' style= 'text-align: left;vertical-align: middle;' > ";
            stCuerpoHTML += "<td width= '30' style= 'text-align: left;vertical-align: middle;' > ";
            stCuerpoHTML += "<table cellpadding= '0' cellspacing= '0' class='sc-gPEVay eQYmiW'";
            stCuerpoHTML += " style='vertical-align: -webkit-baseline-middle; font-size: medium; font-family: Arial;'>";
            stCuerpoHTML += "<tbody>";
            stCuerpoHTML += "<tr>";
            stCuerpoHTML += " <td style = 'vertical-align: bottom;' ><span color='#5669e2'";
            stCuerpoHTML += "  width='11' class='sc-jlyJG bbyJzT'";
            stCuerpoHTML += " style='display: block; background-color: rgb(86, 105, 226);'><img";
            stCuerpoHTML += "  src = 'https://cdn2.hubspot.net/hubfs/53/tools/email-signature-generator/icons/link-icon-2x.png'";
            stCuerpoHTML += "  color='#5669e2' width='13'";
            stCuerpoHTML += "  class='sc-iRbamj blSEcj'";
            stCuerpoHTML += "  style='display: block; background-color: rgb(86, 105, 226);'></span>";
            stCuerpoHTML += "</td>";
            stCuerpoHTML += "</tr>";
            stCuerpoHTML += "</tbody>";
            stCuerpoHTML += "</table>";
            stCuerpoHTML += "</td>";
            stCuerpoHTML += "<td style = 'padding: 0px;' ><a href='" + objConfiguracion.paginaUrl + "'";
            stCuerpoHTML += " color='#000000' class='sc-gipzik iyhjGb'";
            stCuerpoHTML += " style='text-decoration: none; color: rgb(0, 0, 0); font-size: 12px;'><span>" + objConfiguracion.paginaUrl + "</span></a>";
            stCuerpoHTML += "</td>";
            stCuerpoHTML += "</tr>";
            stCuerpoHTML += "</tbody>";
            stCuerpoHTML += "</table>";
            stCuerpoHTML += "</td>";
            stCuerpoHTML += "</tr>";
            stCuerpoHTML += "</tbody>";
            stCuerpoHTML += "</table>";
            stCuerpoHTML += "<table cellpadding = '0' cellspacing='0' class='sc-gPEVay eQYmiW'";
            stCuerpoHTML += " style='vertical-align: -webkit-baseline-middle; font-size: medium; font-family: Arial; width: 100%;'>";
            stCuerpoHTML += "<tbody>";
            stCuerpoHTML += "<tr> ";
            stCuerpoHTML += "<td color='#5669e2' direction='horizontal' height='1' class='sc-jhAzac hmXDXQ'";
            stCuerpoHTML += " style='width: 100%; border-bottom: 1px solid rgb(86, 105, 226); border-left: none; display: block;'>";
            stCuerpoHTML += "</td>";
            stCuerpoHTML += "</tr>";
            stCuerpoHTML += "</tbody> ";
            stCuerpoHTML += "</table> ";
            stCuerpoHTML += "</td> ";
            stCuerpoHTML += "</tr> ";
            stCuerpoHTML += "<tr> ";
            stCuerpoHTML += "<td style='text-align: center;'></td>";
            stCuerpoHTML += "</tr>";
            stCuerpoHTML += "</tbody>";
            stCuerpoHTML += "</table>";
            return (stCuerpoHTML);
        }



        public ActionResult Historial()
        {
            Llenar();
            List<Cotizacion> lista = objCotizacionNeg.buscarConEstatus();
            cargarFechas();
            return View(lista);
        }

        [HttpPost]
        public ActionResult Historial(string txtMes, string txtYear, string txtEstatus)
        {
            Llenar();
            string month = "", year = "";
            string vyear = "";
            int condicion = 0;
            if (txtMes == "")
            {
                txtMes = null;
            }
            if (txtYear == "")
            {
                txtYear = "-1";
            }
            //Validaciones
            if (txtYear == "-1")
            {
                vyear = null;
            }

            if (txtEstatus == "")
            {
                txtEstatus = null;
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
                year = txtYear;
            }
            cargarFechas();
            List<Cotizacion> lista = objCotizacionNeg.buscarConEstatus(month, year, txtEstatus);
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

        private string EnviarCorreosMensaje(List<string> ListadoDetalle,configuracion objConfiguracion,string id) 
        {
            string msge = "";
            foreach (var item in ListadoDetalle)
            {
                objConfiguracion = db.configuracion.First(p => p.usuario == id);
                if (objConfiguracion == null)
                {          
                    RedirectToAction("HttpNotFound");
                    msge = "Error al enviar este correo. Por favor verifique los datos o intente más tarde.";
                    return (msge);
                }
                else
                {     
                    var lects = db.Database.SqlQuery<SendEmail>("sp_consultaEmailCliente @idVenta", new SqlParameter("@idVenta", item.ToString())).Single();
                    Paso = 0;
                    SendEmail email = new SendEmail();
                    email = lects;

                    msge = "Error al enviar este correo. Por favor verifique los datos o intente más tarde.";
                    string from = objConfiguracion.email;
                    string displayName = objConfiguracion.displayName;
                try
                {
                        //parameters                                     
                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress(from, displayName);
                        mail.To.Add(email.To);
                        mail.Subject = "Follow Up " + email.Subject;
                        mail.IsBodyHtml = true;
                        string stCuerpoHTML = "<!DOCTYPE html>";
                        stCuerpoHTML += "<html lang='en' xmlns='http://www.w3.org/1999/xhtml' xmlns:o='urn:schemas-microsoft-com:office:office'>";
                        stCuerpoHTML += "<head> <meta charset='UTF-8'><meta name='viewport' content='width=device-width,initial-scale=1'><meta name='x-apple-disable-message-reformatting'>";
                        stCuerpoHTML += "<title>" + "Follow Up " + email.Subject + "</title>";
                        stCuerpoHTML += "<style> table, td, div, h1, p {font-family: Arial, sans-serif;}p.centrado{text-align:center;}.imgRedonda{width: 300px; height: 300px; border - radius:150px;}</style>";
                        stCuerpoHTML += "</head>";
                        stCuerpoHTML += "<body style='margin:0;padding:0;'><table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;background:#ffffff;'><tr>";
                        stCuerpoHTML += "<td align='center' style='padding:0;'><table role='presentation' style='width:390px;border-collapse:collapse;border:1px solid #cccccc;border-spacing:0;text-align:left;'><tr><td align='center' style='padding:0px 0 0px 0;background:white;'>";
                        stCuerpoHTML += "<img src='cid:Fondo' alt='' width='520' style='height:auto;display:block;' /></td></tr><tr>";
                        stCuerpoHTML += "<td style='padding:36px 30px 42px 30px;'><table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;'><tr><td style='padding:0 0 36px 0;color:#153643;'>";
                        stCuerpoHTML += "<h1 style='font-size:24px;margin:0 0 20px 0;font-family:Arial,sans-serif;'>Hello " + email.Cliente + ".</h1>";
                        stCuerpoHTML += "<p style='margin:0 0 12px 0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'><br /> <br /> I hope you are well. <br /><br /> I wanted to know if you are still interested in the equipment. <br /> <br /> Let me know as soon as possible. <br /> <br /> Regards. </p>";
                        stCuerpoHTML += firma(objConfiguracion);
                        stCuerpoHTML += "</td></tr></table></td> </tr><tr><td style='padding:30px;background:#ee4c50;'>";
                        stCuerpoHTML += " <table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;font-size:9px;font-family:Arial,sans-serif;'><tr><td style='padding:0;width:50%;' align='left'>";
                        stCuerpoHTML += "<p style='margin:0;font-size:14px;line-height:16px;font-family:Arial,sans-serif;color:#ffffff;'>Copyright &reg; CRM Conceptos Electronics 2021<br /></p></td><td style='padding:0;width:50%;' align='right'>";
                        stCuerpoHTML += "<table role='presentation' style='border-collapse:collapse;border:0;border-spacing:0;'><tr><td style='padding:0 0 0 10px;width:38px;'><a href='http://www.twitter.com/' style='color:#ffffff;'><img src='https://assets.codepen.io/210284/tw_1.png' alt='Twitter' width='38' style='height:auto;display:block;border:0;' /></a>";
                        stCuerpoHTML += "</td><td style='padding:0 0 0 10px;width:38px;'><a href='https://www.facebook.com/Conceptos-Electronics-104154518135452' style='color:#ffffff;'><img src='https://assets.codepen.io/210284/fb_1.png' alt='Facebook' width='38' style='height:auto;display:block;border:0;' /></a>";
                        stCuerpoHTML += "</td></tr></table></td></tr></table></td></tr></table>";
                        stCuerpoHTML += "</td></tr></table>";
                        stCuerpoHTML += "</body></html>";
                        AlternateView htmlView = AlternateView.CreateAlternateViewFromString(stCuerpoHTML, Encoding.UTF8, MediaTypeNames.Text.Html);
                        string stImagen = Server.MapPath("~") + @"\Img\CRM Logo.jpg";
                        string stIdImagen = "Fondo";
                        LinkedResource img = new LinkedResource(stImagen, MediaTypeNames.Image.Jpeg);
                        LinkedResource imgFirma = new LinkedResource(new MemoryStream(objConfiguracion.imagen), MediaTypeNames.Image.Jpeg);
                        string stIdImagenFirma = "Firma";
                        imgFirma.ContentId = stIdImagenFirma;
                        img.ContentId = stIdImagen;
                        htmlView.LinkedResources.Add(img);
                        htmlView.LinkedResources.Add(imgFirma);
                        mail.AlternateViews.Add(htmlView);
                        mail.Body = stCuerpoHTML;
                        mail.IsBodyHtml = true;
                        SmtpClient client = new SmtpClient(objConfiguracion.servidorSmtp, objConfiguracion.puerto); //Aquí debes sustituir tu servidor SMTP y el puerto
                        client.Credentials = new NetworkCredential(from, EncriptacionSha.DesEncriptar(objConfiguracion.contrasena));
                        client.EnableSsl = true;//En caso de que tu servidor de correo no utilice cifrado SSL,poner en false
                         NEVER_EAT_POISON_Disable_CertificateValidation();
                        client.Send(mail);
                        msge = "¡Correo enviado exitosamente!";
                        return (msge);
                    }
                catch (Exception e)
                {
                        msge = "Error al enviar este correo. Por favor verifique los datos o intente más tarde.";
                        return (msge);
                }

                }
            }
            return (msge);
        }


        [HttpPost]
        public ActionResult EnviarCorreos(List<string> ListadoDetalle, int Dias)
        {         
            string id = User.Identity.GetUserId();
            
            List<Cotizacion> lista = objCotizacionNeg.buscarConEstatus();
            configuracion objConfiguracion = new configuracion();
            cargarFechas();
            string mensaje="Error";           

            if (!(Request.IsAuthenticated || User.IsInRole("ADMIN")))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (ListadoDetalle==null) {
                    System.Diagnostics.Debug.WriteLine("Lista vacia");
                }else{
                    Llenar();
                    cargarFechas();
                    foreach (var item in ListadoDetalle)
                    {     
                        var cotizacionItem = new cotizacion { idVenta = Convert.ToDecimal(item), diasSeguimiento = Dias, fechaComienzoSeguimiento= DateTime.Now};
                        db.cotizacion.Attach(cotizacionItem);
                        db.Entry(cotizacionItem).Property(x => x.fechaComienzoSeguimiento).IsModified = true;
                        db.Entry(cotizacionItem).Property(x => x.diasSeguimiento).IsModified = true;
                        db.SaveChanges();
                    }
                    mensaje=EnviarCorreosMensaje(ListadoDetalle, objConfiguracion, id);                    
                    return View(mensaje);
                }
                return View(mensaje);
            } 
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

        public ActionResult NuevaCotizacion()
        {
            Llenar();
            cargarModoPagocmb();
            cargarProductocmb();
            return View();
        }

        [HttpPost]
        public ActionResult GuardarCotizacion(string Fecha, string modoPago, string IdCliente, string Total,string notas, string notasCompras,string estatus, List<DetalleCotizacion> ListadoDetalle, int dias)
        {
            Llenar();
            string mensaje = "";
            double iva = 18;
            string idVendedor = User.Identity.GetUserId();
            int codigoPago = 0;
            long codigoCliente = 0;
            double total = 0;
            string nota;

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
                Cotizacion objVenta = new Cotizacion(total, codigoCliente, idVendedor, Fecha, iva,notas,notasCompras, estatus,dias,"N","A", DateTime.Now.ToString("yyyy-MM-dd"));         
                string codigoVenta = objCotizacionNeg.create(objVenta);

                if (codigoVenta == "" || codigoVenta == null)
                {
                    mensaje = "ERROR AL REGISTRAR LA VENTA";
                }
                else
                {
                    Session["idVenta"] = codigoVenta;
                    idVentaMail = codigoVenta;
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
                            if (String.IsNullOrWhiteSpace(data.notas))
                            {
                                nota = "";
                            }
                            else {
                                nota = data.notas.ToString();
                            }
                           
                            DetalleCotizacion objDetalleVenta = new DetalleCotizacion(Convert.ToInt32(codigoFactura), Convert.ToInt32(codigoVenta), idProducto, subtotal, descuento, cantidad,nota);
                            objDetalleVentaNeg.create(objDetalleVenta);
                        }
                        mensaje = "VENTA " + codigoVenta + " GUARDADA CON EXITO...";
                    }
                }
            }
            return Json(mensaje);
        }

        public ActionResult reporteActual()
        {
            if (Paso == 1)
            {
                Llenar();
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
                Llenar();
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
                Llenar();
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
                Llenar();
                cargarModoPagocmb();
                cargarProductocmb();
                TempData["msg"] = "<script>alert('Debes guardar la cotizacion');</script>";
                return View("NuevaCotizacion");
            }
        }
            

        public ActionResult ReporteCotizacion()
        {
            List<Cotizacion> lista = objCotizacionNeg.findAll(User.Identity.GetUserId());
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
            List<Cotizacion> lista = objCotizacionNeg.findAll(User.Identity.GetUserId());
            return View(lista);
        }

        public ActionResult BuscarHistorial(string idProducto)
        {
            Historial objHistorial = new Historial();
            objHistorial.IdProducto = idProducto;
            List<Historial> Cotizacion = objCotizacionNeg.findHistorial(objHistorial);
            return View(Cotizacion);
        }
        public ActionResult BuscarRFQ(string idProducto)
        {
            RFQHistorial objHistorial = new RFQHistorial();
            objHistorial.idProducto = idProducto;
            List<RFQHistorial> Cotizacion = objCotizacionNeg.findRFQ(objHistorial);
            return View(Cotizacion);
        }

        public ActionResult SendEmailFactura(decimal IdVenta)
        {       //string id = Session["idVenta"].ToString();
                var lects = db.Database.SqlQuery<SendEmail>("sp_consultaEmailCliente @idVenta",new SqlParameter("@idVenta", IdVenta)).Single();
                Paso = 0;
                SendEmail email = new SendEmail();
                email = lects;
                //lects.NoCotizacion
                return View("SendEmailFactura", lects);       
        }

        [HttpPost]
        public ActionResult SendEmailFactura(SendEmail objSendEmail)
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
   
                using (var viewer = new LocalReport())
                {
             
                    string fechaQuote = objSendEmail.NoCotizacion;

                    DataTable dt = frmReporteEs.cargar(objSendEmail.idCotizacion.ToString());
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
                    mail.IsBodyHtml = true;
                    string stCuerpoHTML = "<!DOCTYPE html>";
                    stCuerpoHTML += "<html lang='en' xmlns='http://www.w3.org/1999/xhtml' xmlns:o='urn:schemas-microsoft-com:office:office'>";
                    stCuerpoHTML += "<head> <meta charset='UTF-8'><meta name='viewport' content='width=device-width,initial-scale=1'><meta name='x-apple-disable-message-reformatting'>";
                    stCuerpoHTML += "<title>"  + objSendEmail.Subject + "</title>";
                    stCuerpoHTML += "<style> table, td, div, h1, p {font-family: Arial, sans-serif;}p.centrado{text-align:center;}.imgRedonda{width: 300px; height: 300px; border - radius:150px;}</style>";
                    stCuerpoHTML += "</head>";
                    stCuerpoHTML += "<body style='margin:0;padding:0;'><table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;background:#ffffff;'><tr>";
                    stCuerpoHTML += "<td align='center' style='padding:0;'><table role='presentation' style='width:390px;border-collapse:collapse;border:1px solid #cccccc;border-spacing:0;text-align:left;'><tr><td align='center' style='padding:0px 0 0px 0;background:white;'>";
                    stCuerpoHTML += "<img src='cid:Fondo' alt='' width='520' style='height:auto;display:block;' /></td></tr><tr>";
                    stCuerpoHTML += "<td style='padding:36px 30px 42px 30px;'><table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;'><tr><td style='padding:0 0 36px 0;color:#153643;'>";
                    stCuerpoHTML += "<h1 style='font-size:24px;margin:0 0 20px 0;font-family:Arial,sans-serif;'>Hello " + objSendEmail.Cliente + ".</h1>";
                    stCuerpoHTML += "<p style='margin:0 0 12px 0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'><br /> <br /> I hope you are well. <br /><br /> Here is the quote you requested. <br /> <br /> Let me know as soon as you can. <br /> <br />Best Regards.  <br /><br /> ";
                    if (String.IsNullOrWhiteSpace(objSendEmail.Body)){
                        stCuerpoHTML += "</p>";
                    }else {                   
                    stCuerpoHTML += "Note: " + objSendEmail.Body + "</p>";               
                    }
                    stCuerpoHTML += firma(objConfiguracion);
                    stCuerpoHTML += "</td></tr></table></td> </tr><tr><td style='padding:30px;background:#ee4c50;'>";
                    stCuerpoHTML += " <table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;font-size:9px;font-family:Arial,sans-serif;'><tr><td style='padding:0;width:50%;' align='left'>";
                    stCuerpoHTML += "<p style='margin:0;font-size:14px;line-height:16px;font-family:Arial,sans-serif;color:#ffffff;'>Copyright &reg; CRM Conceptos Electronics 2021<br /></p></td><td style='padding:0;width:50%;' align='right'>";
                    stCuerpoHTML += "<table role='presentation' style='border-collapse:collapse;border:0;border-spacing:0;'><tr><td style='padding:0 0 0 10px;width:38px;'><a href='http://www.twitter.com/' style='color:#ffffff;'><img src='https://assets.codepen.io/210284/tw_1.png' alt='Twitter' width='38' style='height:auto;display:block;border:0;' /></a>";
                    stCuerpoHTML += "</td><td style='padding:0 0 0 10px;width:38px;'><a href='https://www.facebook.com/Conceptos-Electronics-104154518135452' style='color:#ffffff;'><img src='https://assets.codepen.io/210284/fb_1.png' alt='Facebook' width='38' style='height:auto;display:block;border:0;' /></a>";
                    stCuerpoHTML += "</td></tr></table></td></tr></table></td></tr></table>";
                    stCuerpoHTML += "</td></tr></table></body></html>";
                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(stCuerpoHTML, Encoding.UTF8, MediaTypeNames.Text.Html);
                string stImagen = Server.MapPath("~") + @"\Img\CRM Logo.jpg";
                string stIdImagen = "Fondo";
                LinkedResource img = new LinkedResource(stImagen, MediaTypeNames.Image.Jpeg);    
                LinkedResource imgFirma = new LinkedResource(new MemoryStream(objConfiguracion.imagen), MediaTypeNames.Image.Jpeg);
                string stIdImagenFirma = "Firma";
                imgFirma.ContentId = stIdImagenFirma;
                img.ContentId = stIdImagen;
                htmlView.LinkedResources.Add(img);
                htmlView.LinkedResources.Add(imgFirma);
                mail.AlternateViews.Add(htmlView);
                mail.Body = HttpUtility.HtmlEncode(stCuerpoHTML);
                mail.Attachments.Add(new Attachment(new MemoryStream(bytes), fechaQuote + ".pdf"));
                mail.IsBodyHtml = true;
                SmtpClient client = new SmtpClient(objConfiguracion.servidorSmtp, objConfiguracion.puerto);//Aquí debes sustituir tu servidor SMTP y el puerto
                client.Credentials = new NetworkCredential(from, EncriptacionSha.DesEncriptar(objConfiguracion.contrasena));
                //client.UseDefaultCredentials = true;                
                client.EnableSsl = true;//En caso de que tu servidor de correo no utilice cifrado SSL,poner en false
                NEVER_EAT_POISON_Disable_CertificateValidation();
                client.Send(mail);
                msge = "¡Correo enviado exitosamente! Pronto te contactaremos.";
                TempData["msg"] = "<script>alert('¡Correo enviado exitosamente!');</script>";
            }
                return RedirectToAction("VentaFactura", "Cotizacion");     
        }
      
        static void NEVER_EAT_POISON_Disable_CertificateValidation()
        {         
            ServicePointManager.ServerCertificateValidationCallback =
                delegate (
                    object s,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors
                ) {
                    return true;
                };
        }

        public ActionResult SendEmail()
        {
            Llenar();
            if (Paso == 1)
            {
                if (Session["idVenta"].ToString() != null)
                {
                    string idVenta = Session["idVenta"].ToString();
                    var lects = db.Database.SqlQuery<SendEmail>("sp_consultaEmailCliente @idVenta",new SqlParameter("@idVenta", idVenta)).Single();
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
                    ReportParameter[] rptParams = new ReportParameter[]{new ReportParameter("idVenta",idVentaMail)};
                    viewer.Refresh();
                    var bytes = viewer.Render("PDF");
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(from, displayName);              
                    mail.To.Add(objSendEmail.To);
                    mail.Subject = objSendEmail.Subject;
                    mail.IsBodyHtml = true;
                    string stCuerpoHTML = "<!DOCTYPE html>";
                    stCuerpoHTML += "<html lang='en' xmlns='http://www.w3.org/1999/xhtml' xmlns:o='urn:schemas-microsoft-com:office:office'>";
                    stCuerpoHTML += "<head> <meta charset='UTF-8'><meta name='viewport' content='width=device-width,initial-scale=1'><meta name='x-apple-disable-message-reformatting'>";
                    stCuerpoHTML += "<title>" + objSendEmail.Subject + "</title>";
                    stCuerpoHTML += "<style> table, td, div, h1, p {font-family: Arial, sans-serif;}p.centrado{text-align:center;}.imgRedonda{width: 300px; height: 300px; border - radius:150px;}</style>";
                    stCuerpoHTML += "</head>";
                    stCuerpoHTML += "<body style='margin:0;padding:0;'><table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;background:#ffffff;'><tr>";
                    stCuerpoHTML += "<td align='center' style='padding:0;'><table role='presentation' style='width:390px;border-collapse:collapse;border:1px solid #cccccc;border-spacing:0;text-align:left;'><tr><td align='center' style='padding:0px 0 0px 0;background:white;'>";
                    stCuerpoHTML += "<img src='cid:Fondo' alt='' width='520' style='height:auto;display:block;' /></td></tr><tr>";
                    stCuerpoHTML += "<td style='padding:36px 30px 42px 30px;'><table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;'><tr><td style='padding:0 0 36px 0;color:#153643;'>";
                    stCuerpoHTML += "<h1 style='font-size:24px;margin:0 0 20px 0;font-family:Arial,sans-serif;'>Hello " + objSendEmail.Cliente + ".</h1>";
                    stCuerpoHTML += "<p style='margin:0 0 12px 0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'><br /> <br /> I hope you are well. <br /><br /> Here is the quote you requested. <br /> <br /> Let me know as soon as you can. <br /> <br />Best Regards.  <br /><br /> ";
                    if (String.IsNullOrWhiteSpace(objSendEmail.Body))
                    {
                        stCuerpoHTML += "</p>";
                    }
                    else
                    {
                        stCuerpoHTML += "Note: " + objSendEmail.Body + "</p>";
                    }
                    stCuerpoHTML += firma(objConfiguracion);
                    stCuerpoHTML += "</td></tr></table></td> </tr><tr><td style='padding:30px;background:#ee4c50;'>";
                    stCuerpoHTML += " <table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;font-size:9px;font-family:Arial,sans-serif;'><tr><td style='padding:0;width:50%;' align='left'>";
                    stCuerpoHTML += "<p style='margin:0;font-size:14px;line-height:16px;font-family:Arial,sans-serif;color:#ffffff;'>Copyright &reg; CRM Conceptos Electronics 2021<br /></p></td><td style='padding:0;width:50%;' align='right'>";
                    stCuerpoHTML += "<table role='presentation' style='border-collapse:collapse;border:0;border-spacing:0;'><tr><td style='padding:0 0 0 10px;width:38px;'><a href='http://www.twitter.com/' style='color:#ffffff;'><img src='https://assets.codepen.io/210284/tw_1.png' alt='Twitter' width='38' style='height:auto;display:block;border:0;' /></a>";
                    stCuerpoHTML += "</td><td style='padding:0 0 0 10px;width:38px;'><a href='https://www.facebook.com/Conceptos-Electronics-104154518135452' style='color:#ffffff;'><img src='https://assets.codepen.io/210284/fb_1.png' alt='Facebook' width='38' style='height:auto;display:block;border:0;' /></a>";
                    stCuerpoHTML += "</td></tr></table></td></tr></table></td></tr></table>";
                    stCuerpoHTML += "</td></tr></table></body></html>";

                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(stCuerpoHTML, Encoding.UTF8, MediaTypeNames.Text.Html);
                    string stImagen = Server.MapPath("~") + @"\Img\CRM Logo.jpg";
                    string stIdImagen = "Fondo";
                    LinkedResource img = new LinkedResource(stImagen, MediaTypeNames.Image.Jpeg);
                    LinkedResource imgFirma = new LinkedResource(new MemoryStream(objConfiguracion.imagen), MediaTypeNames.Image.Jpeg);
                    string stIdImagenFirma = "Firma";
                    imgFirma.ContentId = stIdImagenFirma;
                    img.ContentId = stIdImagen;
                    htmlView.LinkedResources.Add(img);
                    htmlView.LinkedResources.Add(imgFirma);
                    mail.AlternateViews.Add(htmlView);
                    mail.Body = HttpUtility.HtmlEncode(stCuerpoHTML);
                    mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Quote " +idVentaMail+fechaQuote + ".pdf"));
                    mail.IsBodyHtml = true;
                    SmtpClient client = new SmtpClient(objConfiguracion.servidorSmtp, objConfiguracion.puerto); //Aquí debes sustituir tu servidor SMTP y el puerto
                    client.Credentials = new NetworkCredential(from, EncriptacionSha.DesEncriptar(objConfiguracion.contrasena));
                    client.EnableSsl = true;//En caso de que tu servidor de correo no utilice cifrado SSL,poner en false
                    client.Send(mail);
                    msge = "¡Correo enviado exitosamente! Pronto te contactaremos.";
                    TempData["msg"] = "<script>alert('¡Correo enviado exitosamente!');</script>";
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

        [HttpGet]
        public ActionResult ObtenerProductos()
        {
            Producto objProducto = new Producto();
            objProducto.IdProducto = null;
            objProducto.Nombre = null;
            objProducto.Categoria = "-1";
            objProducto.Marca = "-1";
            List<Producto> ListaProducto = objProductoNeg.findAllProductosCotizacion(objProducto);
            cargarCategoria();
            cargarMarca();
            return View(ListaProducto);
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
            

        [HttpPost]
        public ActionResult EditarEstatusSeguimiento(string idVenta)
        {
            int codigo = Convert.ToInt32(idVenta);           
            string id = User.Identity.GetUserId();
            List<Cotizacion> lista = objCotizacionNeg.buscarConEstatus();
            configuracion objConfiguracion = new configuracion();
            cargarFechas();
            string mensaje = "Error";
            try
            {
                    var cotizacionItem = new cotizacion {idVenta = codigo, estatusSeguimiento = "C"};
                    //System.Diagnostics.Debug.WriteLine("Cotizacion: "+cotizacionItem.idVenta+" "+ cotizacionItem.estatusSeguimiento);
                    db.cotizacion.Attach(cotizacionItem);
                    db.Entry(cotizacionItem).Property(x => x.estatusSeguimiento).IsModified = true;
                    db.SaveChanges();
                    mensaje ="¡Seguimiento cancelado correctamente!";
            }
            catch (Exception e){
                    mensaje = "Error: "+ e.ToString();
                    throw;
             }
            return Json(mensaje);            
        }

        private string EnviarCorreosMarketing(List<string> ListadoDetalle, configuracion objConfiguracion)
        {
            string id = User.Identity.GetUserId();
            string msge = "";
            foreach (var item in ListadoDetalle)
            {
                objConfiguracion = db.configuracion.First(p => p.usuario == id);
                if (objConfiguracion == null)
                {
                    RedirectToAction("HttpNotFound");
                    msge = "Error al enviar este correo. Por favor verifique los datos o intente más tarde.";
                    return (msge);
                }
                else
                {                    
                    var lects = db.Database.SqlQuery<SendEmailMarketing>("sp_correosEnvioMarketing @idMarketing", new SqlParameter("@idMarketing", Convert.ToDecimal(item.ToString()))).Single();
                    Paso = 0;
                    SendEmailMarketing email = new SendEmailMarketing();
                    email = lects;

                    msge = "Error al enviar este correo. Por favor verifique los datos o intente más tarde.";
                    string from = objConfiguracion.email;
                    string displayName = objConfiguracion.displayName;
                    try
                    {
                        //parameters                                     
                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress(from, displayName);
                        mail.To.Add(email.To);
                        mail.Subject = "Conceptos Electronics - Test Equipment";
                        mail.IsBodyHtml = true;
                        string stCuerpoHTML = "<!DOCTYPE html>";
                        stCuerpoHTML += "<html lang='en' xmlns='http://www.w3.org/1999/xhtml' xmlns:o='urn:schemas-microsoft-com:office:office'>";
                        stCuerpoHTML += "<head> <meta charset='UTF-8'><meta name='viewport' content='width=device-width,initial-scale=1'><meta name='x-apple-disable-message-reformatting'>";
                        stCuerpoHTML += "<title>Conceptos Electronics</title>";
                        stCuerpoHTML += "<style> table, td, div, h1, p {font-family: Arial, sans-serif;}p.centrado{text-align:center;}.imgRedonda{width: 300px; height: 300px; border - radius:150px;}</style>";
                        stCuerpoHTML += "</head>";
                        stCuerpoHTML += "<body style='margin:0;padding:0;'><table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;background:#ffffff;'><tr>";
                        stCuerpoHTML += "<td align='center' style='padding:0;'><table role='presentation' style='width:390px;border-collapse:collapse;border:1px solid #cccccc;border-spacing:0;text-align:left;'><tr><td align='center' style='padding:0px 0 0px 0;background:white;'>";
                        stCuerpoHTML += "<img src='cid:Fondo' alt='' width='520' style='height:auto;display:block;' /></td></tr><tr>";
                        stCuerpoHTML += "<td style='padding:36px 30px 42px 30px;'><table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;'><tr><td style='padding:0 0 36px 0;color:#153643;'>";
                        if (String.IsNullOrWhiteSpace(email.Cliente))
                        {
                            stCuerpoHTML += "<h1 style='font-size:24px;margin:0 0 20px 0;font-family:Arial,sans-serif;'>Hello.</h1>";
                        }
                        else{
                            stCuerpoHTML += "<h1 style='font-size:24px;margin:0 0 20px 0;font-family:Arial,sans-serif;'>Hello " + email.Cliente + ".</h1>";
                        }                        
                        stCuerpoHTML += "<p style='margin:0 0 12px 0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'>I hope you are well. <br /><br /> I am an " + objConfiguracion.puesto + " with Conceptos Electronics. <br /> <br /> We buy and sell reconditioned and new test equipment. <br /> <br /> We have over 20 years experience and can help you with your application specific needs.";
                        stCuerpoHTML += "<br/> <br />We also can train, install, and service your equipment.<br/><br/>We have an accredited test laboratory.<br/><br/>Please take a look at our website and please let me know how we can help.<br/><br/>I look forward to working with you.<br/><br/> <center><a href='https://conceptoselectronics.com/reconditioned/'>Test Equipment Reconditioned</a><center/></p>";
                        stCuerpoHTML += firma(objConfiguracion);
                        stCuerpoHTML += "</td></tr></table></td> </tr><tr><td style='padding:30px;background:#ee4c50;'>";
                        stCuerpoHTML += " <table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;font-size:9px;font-family:Arial,sans-serif;'><tr><td style='padding:0;width:50%;' align='left'>";
                        stCuerpoHTML += "<p style='margin:0;font-size:14px;line-height:16px;font-family:Arial,sans-serif;color:#ffffff;'>Copyright &reg; CRM Conceptos Electronics 2021<br /></p></td><td style='padding:0;width:50%;' align='right'>";
                        stCuerpoHTML += "<table role='presentation' style='border-collapse:collapse;border:0;border-spacing:0;'><tr><td style='padding:0 0 0 10px;width:38px;'><a href='http://www.twitter.com/' style='color:#ffffff;'><img src='https://assets.codepen.io/210284/tw_1.png' alt='Twitter' width='38' style='height:auto;display:block;border:0;' /></a>";
                        stCuerpoHTML += "</td><td style='padding:0 0 0 10px;width:38px;'><a href='https://www.facebook.com/Conceptos-Electronics-104154518135452' style='color:#ffffff;'><img src='https://assets.codepen.io/210284/fb_1.png' alt='Facebook' width='38' style='height:auto;display:block;border:0;' /></a>";
                        stCuerpoHTML += "</td></tr></table></td></tr></table></td></tr></table>";
                        stCuerpoHTML += "</td></tr></table>";
                        stCuerpoHTML += "</body></html>";
                        AlternateView htmlView = AlternateView.CreateAlternateViewFromString(stCuerpoHTML, Encoding.UTF8, MediaTypeNames.Text.Html);
                        string stImagen = Server.MapPath("~") + @"\Img\CRM Logo.jpg";
                        string stIdImagen = "Fondo";
                        LinkedResource img = new LinkedResource(stImagen, MediaTypeNames.Image.Jpeg);
                        LinkedResource imgFirma = new LinkedResource(new MemoryStream(objConfiguracion.imagen), MediaTypeNames.Image.Jpeg);
                        string stIdImagenFirma = "Firma";
                        imgFirma.ContentId = stIdImagenFirma;
                        img.ContentId = stIdImagen;
                        htmlView.LinkedResources.Add(img);
                        htmlView.LinkedResources.Add(imgFirma);
                        mail.AlternateViews.Add(htmlView);
                        mail.Body = stCuerpoHTML;
                        mail.IsBodyHtml = true;
                        SmtpClient client = new SmtpClient(objConfiguracion.servidorSmtp, objConfiguracion.puerto); //Aquí debes sustituir tu servidor SMTP y el puerto
                        client.Credentials = new NetworkCredential("AKIATTRE76YXUR7SQAEH", "BMnIbAvS6XQZuNhiySqTRCKuQweYr/um6uz6UF52DcdZ");
                        client.EnableSsl = true;//En caso de que tu servidor de correo no utilice cifrado SSL,poner en false   
                        try
                        {                           
                            client.Send(mail);                           
                            System.Diagnostics.Debug.WriteLine("Email sent!");                         
                        }
                        catch (Exception ex)
                        {                           
                            System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                            return (ex.Message.ToString());
                        }
              
                    }
                    catch (Exception e)
                {
                        RedirectToAction("HttpNotFound");
                        msge = "Error al enviar este correo. Por favor verifique los datos o intente más tarde.";
                    return (msge);
                }
            }
            }
            return (msge);
        }

        private string EnviarCorreosMarketing2(List<string> ListadoDetalle, configuracion objConfiguracion)
        {
            // Replace sender@example.com with your "From" address. 
            // This address must be verified with Amazon SES.
            String FROM = "daniel.conceptoselectronics@cloudserviceserp.site";
            String FROMNAME = "Daniel";

            // Replace recipient@example.com with a "To" address. If your account 
            // is still in the sandbox, this address must be verified.
            String TO = "juan100mata@gmail.com";

            // Replace smtp_username with your Amazon SES SMTP user name.
            String SMTP_USERNAME = "AKIATTRE76YXUR7SQAEH";

            // Replace smtp_password with your Amazon SES SMTP password.
            String SMTP_PASSWORD = "BMnIbAvS6XQZuNhiySqTRCKuQweYr/um6uz6UF52DcdZ";

            // (Optional) the name of a configuration set to use for this message.
            // If you comment out this line, you also need to remove or comment out
            // the "X-SES-CONFIGURATION-SET" header below.
            //String CONFIGSET = "ConfigSet";

            // If you're using Amazon SES in a region other than US West (Oregon), 
            // replace email-smtp.us-west-2.amazonaws.com with the Amazon SES SMTP  
            // endpoint in the appropriate AWS Region.
            String HOST = "email-smtp.us-east-1.amazonaws.com";

            // The port you will connect to on the Amazon SES SMTP endpoint. We
            // are choosing port 587 because we will use STARTTLS to encrypt
            // the connection.
            int PORT = 587;

            // The subject line of the email
            String SUBJECT =
                "Amazon SES test (SMTP interface accessed using C#)";

            // The body of the email
            String BODY =
                "<h1>Amazon SES Test</h1>" +
                "<p>This email was sent through the " +
                "<a href='https://aws.amazon.com/ses'>Amazon SES</a> SMTP interface " +
                "using the .NET System.Net.Mail library.</p>";

            // Create and build a new MailMessage object
            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.From = new MailAddress(FROM, FROMNAME);
            message.To.Add(new MailAddress(TO));
            message.Subject = SUBJECT;
            message.Body = BODY;
            // Comment or delete the next line if you are not using a configuration set
            //message.Headers.Add("X-SES-CONFIGURATION-SET", CONFIGSET);

            using (var client = new System.Net.Mail.SmtpClient(HOST, PORT))
            {
                // Pass SMTP credentials
                client.Credentials =
                    new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);

                // Enable SSL encryption
                client.EnableSsl = true;

                // Try to send the message. Show status in console.
                try
                {
                    Console.WriteLine("Attempting to send email...");
                    client.Send(message);
                    Console.WriteLine("Email sent!");
                    System.Diagnostics.Debug.WriteLine("Email sent!");
                    return ("Email sent!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("The email was not sent.");
                    Console.WriteLine("Error message: " + ex.Message);
                    System.Diagnostics.Debug.WriteLine("Error: "+ex.Message);
                    return (ex.Message.ToString());
                }
            }
        }

        [HttpPost]
        public ActionResult EnviarCorreosMarketing(List<string> ListadoDetalle, int Dias)
        {
            string id = User.Identity.GetUserId();
            db.EmailMarketing.AsNoTracking().ToList();
            configuracion objConfiguracion = new configuracion();
            cargarFechas();
            string mensaje = "Error";

            if (!(Request.IsAuthenticated || User.IsInRole("ADMIN")))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (ListadoDetalle == null)
                {
                    System.Diagnostics.Debug.WriteLine("Lista vacia");
                }
                else
                {
                    Llenar();
                    cargarFechas();
                    foreach (var item in ListadoDetalle)
                    {
                        var marketingItem = new EmailMarketing {idMarketing = Convert.ToInt32(item), dias = Dias, fechaComienzo = DateTime.Now,idUsuario=id};
                        db.EmailMarketing.Attach(marketingItem);
                        db.Entry(marketingItem).Property(x => x.fechaComienzo).IsModified = true;
                        db.Entry(marketingItem).Property(x => x.dias).IsModified = true;
                        db.Entry(marketingItem).Property(x => x.idUsuario).IsModified = true;


                        db.SaveChanges();
                    }
                    mensaje = EnviarCorreosMarketing(ListadoDetalle, objConfiguracion);
                    return View(mensaje);
                }
                return View(mensaje);
            }
        }

        public ActionResult EnviarCorreosMarketing()
        {
            string id = User.Identity.GetUserId();
            List<EmailMarketingCorreos> lista = objCotizacionNeg.buscarEmailMarketing(id);
            return View(lista);
        }
        public ActionResult RFQEditar(string idRFQ)
        {
            Llenar();
            //CompraDao cd = new CompraDao();
            Model.Entity.RFQ registro = objCotizacionNeg.buscarListaRFQ(idRFQ);
            ViewData["IdRFQ"] = registro.idRFQ.ToString();
            ViewData["Fecha"] = registro.fecha.ToString("MM/dd/yyyy");
            ViewData["Vendedor"] = registro.idVendedor.ToString();
            //ViewData["CP"] = registro.IdProveedor.ToString();
            ViewData["EstatusSeleccionado"] = registro.estatus.ToString();
            //ViewData["TotalCompra"] = registro.Total.ToString();
            //ViewData["Seccion"] = registro.Seccion.ToString();
            return View();       
        }

        [HttpPost]
        public ActionResult RFQEditarSinEliminados(string idRFQ, string estatus, List<Model.Entity.RFQItem> ListaItems)
        {
            Model.Entity.RFQ objRFQ = new Model.Entity.RFQ();
            objRFQ.idRFQ = int.Parse(idRFQ);
            objRFQ.estatus = estatus;
            string notas;
            string mensaje = "";
            try
            {               
                objCotizacionNeg.updateRFQ(objRFQ);
                mensaje = "RFQ MODIFICADO CON ÉXITO";
            }
            catch (Exception ex)
            {
                mensaje = ex.Message.ToString();
            } 
            foreach (var data in ListaItems)
            {               
                int idRFQItem = Convert.ToInt32(data.idRFQItem);
                int idRFQLista = Convert.ToInt32(idRFQ);
                int? idProveedor = Convert.ToInt32(data.idProveedor);
                if (idProveedor==0)
                {
                    idProveedor = null;
                }
                string idProducto = data.idProducto.ToString();
                decimal precio = Convert.ToDecimal(data.precio.ToString());
                int pCantidad = Convert.ToInt32(data.cantidad.ToString());
                if (data.notas==null)
                {
                    notas = null;

                }
                else {
                    notas = data.notas.ToString();
                }
                string fecha = DateTime.Now.ToString();
                Model.Entity.RFQItem objCompraDetalle = new Model.Entity.RFQItem(idRFQItem,idRFQLista,idProveedor,idProducto,precio,pCantidad,notas,fecha);
                try
                { 
                    mensaje = objCotizacionNeg.updateRFQItem(objCompraDetalle);
                    mensaje = "Actualizado Correctamente";
            
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message.ToString();
                }
            } 
            return Json(mensaje);
        }

        [HttpPost]
        public ActionResult RFQEditarConEliminados(string idRFQ, string estatus, List<Model.Entity.RFQItem> ListaItems, List<string> ListaEliminados)
        {
            Model.Entity.RFQ objRFQ = new Model.Entity.RFQ(int.Parse(idRFQ), estatus);         
            string notas;
            string mensaje = "";
            try
            {
                objCotizacionNeg.updateRFQ(objRFQ);
                mensaje = "RFQ MODIFICADO CON ÉXITO";
            }
            catch (Exception ex)
            {
                mensaje = ex.Message.ToString();
            }
            foreach (var data in ListaItems)
            {
                int idRFQItem = Convert.ToInt32(data.idRFQItem);
                int idRFQLista = Convert.ToInt32(idRFQ);
                int? idProveedor = Convert.ToInt32(data.idProveedor);
                if (idProveedor == 0)
                {
                    idProveedor = null;
                }
                string idProducto = data.idProducto.ToString();
                decimal precio = Convert.ToDecimal(data.precio.ToString());
                int pCantidad = Convert.ToInt32(data.cantidad.ToString());
                if (data.notas == null)
                {
                    notas = null;

                }
                else
                {
                    notas = data.notas.ToString();
                }
                string fecha = DateTime.Now.ToString();
                Model.Entity.RFQItem objCompraDetalle = new Model.Entity.RFQItem(idRFQItem, idRFQLista, idProveedor, idProducto, precio, pCantidad, notas, fecha);
                try
                {
                    mensaje = objCotizacionNeg.updateRFQItem(objCompraDetalle);
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message.ToString();
                }
            }
            foreach (var data in ListaEliminados)
                {
                    int idRFQItem = Convert.ToInt32(data);
                    int idRFQLista = Convert.ToInt32(idRFQ);
                    RFQItemEliminado objRFQItemEliminados = new RFQItemEliminado(idRFQItem, idRFQLista);
                    try
                    {
                        mensaje = objCotizacionNeg.eliminadosRFQ(objRFQItemEliminados);                      
                    }
                    catch (Exception ex)
                    {
                        mensaje = ex.Message.ToString();
                    }
            }        
            return Json(mensaje);
        }
        public ActionResult RFQLista()
        {
                     
            List<Model.Entity.RFQ> registro = objCotizacionNeg.buscarListaRFQ();      
            return View(registro);
        }

        [HttpPost]
        public ActionResult ListaProductosRFQ(string idRFQ)
        {
            List<Model.Entity.RFQItem> list = new List<Model.Entity.RFQItem>();
            list = objCotizacionNeg.buscarListaProductosRFQ(idRFQ);
            return Json(list,JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ObtenerPlantillas()
        {
            List<Model.Entity.plantillaCorreo> lista = objPlantillaCorreoNeg.findAll();
            return View(lista);
        }


    }
}