using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Neg;
using Model.Entity;
using Microsoft.AspNet.Identity;
using System.Diagnostics;
using System.Net;
using WebFacturaMvc.Datos;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net.Mail;
using System.Text;
using System.Net.Mime;
using System.IO;

namespace WebFacturaMvc.Controllers
{
    public class AgendaController : Controller
    {
        private crmconceptoseEntities1 db = new crmconceptoseEntities1();
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
            //Días 01-31
            List<SelectListItem> listDays = new List<SelectListItem>();
            for (int i = 1; i < 32; i++)
            {
                if (i < 10)
                {
                    string numero = "0"+i.ToString();
                    listDays.Add(new SelectListItem() { Text = numero, Value = numero });
                }
                else
                {
                    string numero = i.ToString();
                    listDays.Add(new SelectListItem() { Text = numero, Value = numero });
                }                
            }
            ViewBag.ListaDays = listDays;

        }
        // GET: Agenda
        public ActionResult Index()
        {
            cargarFechas();
            AgendaNeg agenda = new AgendaNeg();
            List<Agenda> listEventos = new List<Agenda>();
            string idUsuario = User.Identity.GetUserId();            
            string fechaHoy = DateTime.Now.ToString("yyyy-MM-dd");            
            listEventos = agenda.cargarAgendaHoy(idUsuario, fechaHoy);
            return View(listEventos);
        }
        [HttpPost]
        public ActionResult Index(string txtDay, string txtMonth, string txtYear)
        {
            string month = "", year = "";
            string vyear = "";
            string day = "";
            int condicion = 0;
            if (txtMonth == "")
            {
                txtMonth = null;
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
            if (txtDay == "")
            {
                day = null;
            }
            //Para meses 10-12
            if (txtMonth != null)
            {
                condicion = int.Parse(txtMonth);
                if (condicion >= 10)
                {
                    month = txtMonth;
                }
                //Para meses 1-9
                else
                {
                    month = "0" + txtMonth;
                }
            }
            //Para el año
            if (vyear != null)
            {
                year = txtYear;
            }            
            AgendaNeg agenda = new AgendaNeg();
            List<Agenda> listEventos = new List<Agenda>();
            string idUsuario = User.Identity.GetUserId();
            Debug.Write("Día: "+txtDay);
            Debug.Write("Mes: " + txtMonth);
            Debug.Write("Año: " + txtYear);
            listEventos = agenda.filtrarEventos(txtDay, txtMonth, txtYear, idUsuario);
            cargarFechas();
            return View(listEventos);
        }
        public ActionResult VerDetalles(int idEvento)
        {            
            AgendaNeg agenda = new AgendaNeg();
            Agenda evento = new Agenda();
            evento = agenda.cargarEvento(idEvento);
            //DateTime fechaHoy = Convert.ToDateTime(DateTime.Now.ToString("yyyy-dd-MM"));
            ViewData["Evento"] = idEvento;
            ViewData["Fecha"] = evento.Fecha.ToString("dd-MM-yyyy");
            ViewData["Titulo"] = evento.Titulo;
            ViewData["Descripcion"] = evento.Descripcion;
            ViewData["Link"] = evento.Link;
            ViewData["Hora"] = evento.Hora;
            ViewData["Direccion"] = evento.Direccion;
            return View();
        }
        public ActionResult AgregarEvento()
        {            
            return View();
        }
        [HttpPost]
        public ActionResult GuardarEvento(string Fecha, string Hora, string Titulo, string Descripcion, string Link, string Direccion)
        {
            string idUsuario = User.Identity.GetUserId();
            string mensaje = "";
            Agenda a = new Agenda();            
            if (Fecha == "" || Fecha == null)
            {
                mensaje = "Error, debe seleccionar una fecha";
            }
            else if(Hora=="" || Hora == null)
            {
                mensaje = "Error, debe seleccionar una hora";
            }
            else if(Titulo=="" || Titulo == null)
            {
                mensaje = "Error, debe colocar un título";
            }
            else if(Descripcion=="" || Descripcion==null)
            {
                mensaje = "Error, debe colocar una descripción al evento";
            }            
            else
            {                
                a.Fecha = Convert.ToDateTime(Fecha);
                a.Hora = Hora;
                a.Titulo = Titulo;
                a.Descripcion = Descripcion;
                a.Link = Link;
                a.Direccion = Direccion;
                a.IdUsuario = idUsuario;
                AgendaNeg metodo = new AgendaNeg();
                try
                {
                    metodo.agregarEvento(a);
                    mensaje = "Evento agregado correctamente!";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message.ToString();
                }
            }            
            
            return Json(mensaje);
        }
        public ActionResult EditarEvento(string idEvento)
        {
            AgendaNeg agenda = new AgendaNeg();
            Agenda evento = new Agenda();
            evento = agenda.cargarEvento(int.Parse(idEvento));
            //DateTime fechaHoy = Convert.ToDateTime(DateTime.Now.ToString("yyyy-dd-MM"));
            ViewData["Evento"] = idEvento;
            ViewData["Fecha"] = evento.Fecha.ToString("yyyy-MM-dd");
            ViewData["Titulo"] = evento.Titulo;
            ViewData["Descripcion"] = evento.Descripcion;
            ViewData["Link"] = evento.Link;
            ViewData["Hora"] = evento.Hora;
            ViewData["Direccion"] = evento.Direccion;
            return View();
        }
        [HttpPost]
        public ActionResult Editar(string IdEvento, string Fecha, string Hora, string Titulo, string Descripcion, string Link, string Direccion)
        {
            string idUsuario = User.Identity.GetUserId();
            string mensaje = "";
            Agenda a = new Agenda();
            if (Fecha == "" || Fecha == null)
            {
                mensaje = "Error, debe seleccionar una fecha";
            }
            else if (Hora == "" || Hora == null)
            {
                mensaje = "Error, debe seleccionar una hora";
            }
            else if (Titulo == "" || Titulo == null)
            {
                mensaje = "Error, debe colocar un título";
            }
            else if (Descripcion == "" || Descripcion == null)
            {
                mensaje = "Error, debe colocar una descripción al evento";
            }
            else
            {
                a.IdEvento = int.Parse(IdEvento);
                a.Fecha = Convert.ToDateTime(Fecha);
                a.Hora = Hora;
                a.Titulo = Titulo;
                a.Descripcion = Descripcion;
                a.Link = Link;
                a.Direccion = Direccion;
                a.IdUsuario = idUsuario;
                AgendaNeg metodo = new AgendaNeg();
                try
                {
                    metodo.editarEvento(a);
                    mensaje = "Evento editado correctamente!";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message.ToString();
                }
            }

            return Json(mensaje);
        }
        public ActionResult EliminarEvento(string idEvento)
        {
            AgendaNeg agenda = new AgendaNeg();
            Agenda evento = new Agenda();
            evento = agenda.cargarEvento(int.Parse(idEvento));
            //DateTime fechaHoy = Convert.ToDateTime(DateTime.Now.ToString("yyyy-dd-MM"));
            ViewData["Evento"] = idEvento;
            ViewData["Fecha"] = evento.Fecha.ToString("yyyy-MM-dd");
            ViewData["Titulo"] = evento.Titulo;
            ViewData["Descripcion"] = evento.Descripcion;
            ViewData["Link"] = evento.Link;
            ViewData["Hora"] = evento.Hora;
            ViewData["Direccion"] = evento.Direccion;
            return View();
        }
        [HttpPost]
        public ActionResult Eliminar(string IdEvento)
        {
                string mensaje = "";
                Agenda a = new Agenda();
                a.IdEvento = int.Parse(IdEvento);

            AgendaNeg metodo2 = new AgendaNeg();
                try
                {
                metodo2.eliminarEvento(a);
                    mensaje = "Evento eliminado correctamente!";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message.ToString();
                }            

            return Json(mensaje);
        }
        static void NEVER_EAT_POISON_Disable_CertificateValidation()
        {
            // Disabling certificate validation can expose you to a man-in-the-middle attack
            // which may allow your encrypted message to be read by an attacker
            // https://stackoverflow.com/a/14907718/740639
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
        public string firma(configuracion objConfiguracion)
        {
            string stCuerpoHTML = "";
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
        //Correo que se envia al agregar el numero de guia
        public ActionResult enviar(Agenda a, string nombreDestinatario, string correoDestinatario)
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
            string ubicacion = "";

            DateTime fechaActual = DateTime.Today;
            string fechaQuote = string.Format("{0}{1}{2}", fechaActual.Month, fechaActual.Day, fechaActual.Year);
            //Para saber si tiene link o direccion
            if(a.Link==null || a.Link == "")
            {
                ubicacion = "The address of the meeting is: " + a.Direccion;
                
            }
            if(a.Direccion==null || a.Direccion == "")
            {
                ubicacion = "The meeting link is: " + a.Link;
            }

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from, displayName);
            mail.To.Add(correoDestinatario);
            mail.Subject = "Invitation to Event "+a.IdEvento;
            mail.IsBodyHtml = true;
            string stCuerpoHTML = "<!DOCTYPE html>";
            stCuerpoHTML += "<html lang='en' xmlns='http://www.w3.org/1999/xhtml' xmlns:o='urn:schemas-microsoft-com:office:office'>";
            stCuerpoHTML += "<head> <meta charset='UTF-8'><meta name='viewport' content='width=device-width,initial-scale=1'><meta name='x-apple-disable-message-reformatting'>";
            stCuerpoHTML += "<title> Invitation to Event " + a.IdEvento + "</title>";
            stCuerpoHTML += "<style> table, td, div, h1, p {font-family: Arial, sans-serif;}p.centrado{text-align:center;}.imgRedonda{width: 300px; height: 300px; border - radius:150px;}</style>";
            stCuerpoHTML += "</head>";
            stCuerpoHTML += "<body style='margin:0;padding:0;'><table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;background:#ffffff;'><tr>";
            stCuerpoHTML += "<td align='center' style='padding:0;'><table role='presentation' style='width:390px;border-collapse:collapse;border:1px solid #cccccc;border-spacing:0;text-align:left;'><tr><td align='center' style='padding:0px 0 0px 0;background:white;'>";
            stCuerpoHTML += "<img src='cid:Fondo' alt='' width='520' style='height:auto;display:block;' /></td></tr><tr>";
            stCuerpoHTML += "<td style='padding:36px 30px 42px 30px;'><table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;'><tr><td style='padding:0 0 36px 0;color:#153643;'>";
            stCuerpoHTML += "<h1 style='font-size:24px;margin:0 0 20px 0;font-family:Arial,sans-serif;'>Hello " + nombreDestinatario + ".</h1>";
            stCuerpoHTML += "<p style='margin:0 0 12px 0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'><br /> <br /> I hope you are well. <br /><br /> You were invited to the following event: <br /> <br /> Title: " + a.Titulo + "<br /> <br />Date: " + a.Fecha.ToString("dd/MM/yyyy") + "<br /> <br />Time: " + a.Hora + "<br /> <br />" + ubicacion+ "<br /> <br />Best Regards.  <br /><br /> ";
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
            mail.IsBodyHtml = true;
            SmtpClient client = new SmtpClient(objConfiguracion.servidorSmtp, objConfiguracion.puerto);//Aquí debes sustituir tu servidor SMTP y el puerto
            client.Credentials = new NetworkCredential(from, EncriptacionSha.DesEncriptar(objConfiguracion.contrasena));
            //client.UseDefaultCredentials = true;                
            client.EnableSsl = true;//En caso de que tu servidor de correo no utilice cifrado SSL,poner en false
            NEVER_EAT_POISON_Disable_CertificateValidation();
            client.Send(mail);
            msge = "¡Invitation sent!";
            TempData["msg"] = "<script>alert('¡Correo enviado exitosamente!');</script>";

            return Json(msge);
        }
        public ActionResult EnviarInvitacion(string idEvento)
        {
            AgendaNeg agenda = new AgendaNeg();
            Agenda evento = new Agenda();
            evento = agenda.cargarEvento(int.Parse(idEvento));            
            ViewData["Evento"] = idEvento;            
            ViewData["Titulo"] = evento.Titulo;            
            return View();
        }
        [HttpPost]
        public ActionResult Enviar(string idEvento, string nombreDestinatario, string correoDestinatario)
        {
            AgendaNeg agenda = new AgendaNeg();
            Agenda evento = new Agenda();
            evento = agenda.cargarEvento(int.Parse(idEvento));
            string mensaje = "";
            if (nombreDestinatario == "" || nombreDestinatario == null)
            {
                mensaje = "Error, debe introducir el nombre del destinatario";
            }
            else if (correoDestinatario == null || correoDestinatario == "")
            {
                mensaje = "Error, debe introducir el correo del destinatario";
            }            
            else
            {
                try
                {
                    enviar(evento, nombreDestinatario, correoDestinatario);
                    mensaje = "Invitation sent";
                }
                catch (Exception error)
                {
                    mensaje = "Error, ha ocurrido algo en el mensaje: " + error.Message.ToString();
                }
            }
            return Json(mensaje);
        }

    }
}