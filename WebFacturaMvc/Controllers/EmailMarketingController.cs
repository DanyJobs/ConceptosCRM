using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebFacturaMvc.Datos;

namespace WebFacturaMvc.Controllers
{
    public class EmailMarketingController : Controller
    {
        private crmconceptoseEntities1 db = new crmconceptoseEntities1();

        // GET: EmailMarketing
        public ActionResult Index()
        {
            return View(db.EmailMarketing.ToList());
        }

        // GET: EmailMarketing/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailMarketing emailMarketing = db.EmailMarketing.Find(id);
            if (emailMarketing == null)
            {
                return HttpNotFound();
            }
            return View(emailMarketing);
        }

        // GET: EmailMarketing/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: EmailMarketing/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmailMarketing emailMarketing)
        {
            if (ModelState.IsValid)
            {
                emailMarketing.idUsuario = User.Identity.GetUserId();
                emailMarketing.fechaComienzo = DateTime.Now;
                emailMarketing.dias = 0;
                db.EmailMarketing.Add(emailMarketing);
                db.SaveChanges();
                return RedirectToAction("EnviarCorreosMarketing", "Cotizacion");
            }

            return View(emailMarketing);
        }

        // GET: EmailMarketing/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailMarketing emailMarketing = db.EmailMarketing.Find(id);
            if (emailMarketing == null)
            {
                return HttpNotFound();
            }
            return View(emailMarketing);
        }

        // POST: EmailMarketing/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmailMarketing emailMarketing)
        {
            if (ModelState.IsValid)
            {
                db.EmailMarketing.Attach(emailMarketing);
                db.Entry(emailMarketing).Property(x => x.nombre).IsModified = true;
                db.Entry(emailMarketing).Property(x => x.email).IsModified = true;        
                db.SaveChanges();
                return RedirectToAction("EnviarCorreosMarketing","Cotizacion");
            }
            return View(emailMarketing);
        }

        // GET: EmailMarketing/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailMarketing emailMarketing = db.EmailMarketing.Find(id);
            if (emailMarketing == null)
            {
                return HttpNotFound();
            }
            return View(emailMarketing);
        }

        // POST: EmailMarketing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmailMarketing emailMarketing = db.EmailMarketing.Find(id);
            db.EmailMarketing.Remove(emailMarketing);
            db.SaveChanges();
            return RedirectToAction("EnviarCorreosMarketing", "Cotizacion");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
