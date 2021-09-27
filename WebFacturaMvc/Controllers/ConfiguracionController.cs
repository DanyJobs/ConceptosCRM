using Microsoft.AspNet.Identity;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebFacturaMvc.Datos;

namespace WebFacturaMvc.Controllers
{
    public class ConfiguracionController : Controller
    {
        private crmconceptoseEntities1 db = new crmconceptoseEntities1();

        // GET: Configuracion
        public ActionResult Index()
        {
            var configuracion = db.configuracion.Include(c => c.AspNetUsers).Include(c => c.Moneda1);
            return View(configuracion.ToList());
        }

        // GET: Configuracion/Details/5
        public ActionResult Details()
        {
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
                configuracion configuracionObj = db.configuracion.First(p => p.usuario == id);

                if (configuracionObj == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    if (configuracionObj.requerir.Equals("S"))
                    {
                        configuracionObj.requerir = "Si";

                    }
                    else
                    {
                        configuracionObj.requerir = "No";
                    }
                    return View(configuracionObj);
                }
            }
        }
           

        // GET: Configuracion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            configuracion configuracion = db.configuracion.Find(id);
            if (configuracion == null)
            {
                return HttpNotFound();
            }
            Llenar();
            ViewBag.usuario = new SelectList(db.AspNetUsers, "Id", "Email", configuracion.usuario);   
            ViewBag.moneda = new SelectList(db.Moneda, "abreviatura", "abreviatura", configuracion.moneda);

            return View(configuracion);
        }

        // POST: Configuracion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(configuracion objconfiguracion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //objconfiguracion.usuario = User.Identity.GetUserId();
                    //objconfiguracion.contrasena = EncriptacionSha.GetSHA256(objconfiguracion.contrasena);
                    db.configuracion.Attach(objconfiguracion);
                    db.Entry(objconfiguracion).Property(x => x.moneda).IsModified = true;
                    db.Entry(objconfiguracion).Property(x => x.requerir).IsModified = true;
                    db.Entry(objconfiguracion).Property(x => x.impuesto).IsModified = true;
                    db.SaveChanges();
                    return RedirectToAction("Details");
                }
                Llenar();
                ViewBag.usuario = new SelectList(db.AspNetUsers, "Id", "Email", objconfiguracion.usuario);
                ViewBag.moneda = new SelectList(db.Moneda, "abreviatura", "abreviatura", objconfiguracion.moneda);
                return View();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

       
        public void Llenar()
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem() { Text = "Si", Value = "S" });
            lst.Add(new SelectListItem() { Text = "No", Value = "N" });        
            ViewBag.Opciones = lst;           
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ConfigurarCorreo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            configuracion configuracion = db.configuracion.Find(id);


            if (configuracion == null)
            {
                return HttpNotFound();
            }
            Llenar();
            ViewBag.usuario = new SelectList(db.AspNetUsers, "Id", "Email", configuracion.usuario);
            ViewBag.moneda = new SelectList(db.Moneda, "abreviatura", "abreviatura", configuracion.moneda);
            return View(configuracion);
        }

        public ActionResult VerImagen(string id)
        {
            using (db)
            {
                var imagen = (from configuracion in db.configuracion
                              where configuracion.usuario == id
                              select configuracion.imagen).FirstOrDefault();
                if (imagen != null)
                {
                    return File(imagen, "Img/jpg");
                }
                else {
                    string stImagen = Server.MapPath("~") + @"\Img\noimage.jpg";
                    return File(stImagen, "Img/jpg");
                }
            }    
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfigurarCorreo(configuracion configuracion, HttpPostedFileBase upload,string check)
        {
            if (ModelState.IsValid)
            {                
                if (upload != null && upload.ContentLength>0)
                {
                    byte[] imagenData = null;
                    using (var imagen = new BinaryReader(upload.InputStream))
                    {
                        imagenData = imagen.ReadBytes(upload.ContentLength);
                    }
                    configuracion.imagen = imagenData;
                }
                configuracion.usuario = User.Identity.GetUserId();
                configuracion.contrasena = EncriptacionSha.Encriptar(configuracion.contrasena);              
                db.configuracion.Attach(configuracion);
                db.Entry(configuracion).Property(x => x.servidorSmtp).IsModified = true;
                db.Entry(configuracion).Property(x => x.puerto).IsModified = true;
                db.Entry(configuracion).Property(x => x.contrasena).IsModified = true;
                db.Entry(configuracion).Property(x => x.displayName).IsModified = true;
                db.Entry(configuracion).Property(x => x.email).IsModified = true;
                db.Entry(configuracion).Property(x => x.nombre).IsModified = true;
                db.Entry(configuracion).Property(x => x.telefono).IsModified = true;
                db.Entry(configuracion).Property(x => x.celular).IsModified = true;
                db.Entry(configuracion).Property(x => x.puesto).IsModified = true;
                db.Entry(configuracion).Property(x => x.paginaUrl).IsModified = true;
                if (String.IsNullOrWhiteSpace(check))
                {
                  db.Entry(configuracion).Property(x => x.imagen).IsModified = true;
                }
               
                              
                db.SaveChanges();
                return RedirectToAction("DetailsCorreo");
            }
            Llenar();
            ViewBag.usuario = new SelectList(db.AspNetUsers, "Id", "Email", configuracion.usuario);
            ViewBag.moneda = new SelectList(db.Moneda, "abreviatura", "abreviatura", configuracion.moneda);
            return View(configuracion);
        }

        public ActionResult DetailsCorreo()
        {
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
                configuracion configuracionObj = db.configuracion.First(p => p.usuario == id);

                if (configuracionObj == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    if (configuracionObj.requerir.Equals("S"))
                    {
                        configuracionObj.requerir = "Si";

                    }
                    else
                    {
                        configuracionObj.requerir = "No";
                    }
                    return View(configuracionObj);
                }
            }
        }     

    }
}
