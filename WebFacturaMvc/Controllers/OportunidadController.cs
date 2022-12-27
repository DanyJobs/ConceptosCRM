using Model.Entity;
using Model.Neg;
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
    public class OportunidadController : Controller
    {
        private crmconceptoseEntities1 db = new crmconceptoseEntities1();
        OportunidadNeg objOportunidadNeg;
        public OportunidadController()
        {
            objOportunidadNeg = new OportunidadNeg();           
        }


        // GET: Oportunidad
        public ActionResult Index()
        { 
            List<Oportunidad> ListaOportunidad = objOportunidadNeg.findAll(); 
            return View(ListaOportunidad);
        }

        // GET: Oportunidad/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oportunidad oportunidad = db.oportunidad.Find(id);
            if (oportunidad == null)
            {
                return HttpNotFound();
            }
            return View(oportunidad);
        }

        // GET: Oportunidad/Create
        public ActionResult Create()
        {
            ViewBag.idUsuario = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.cotizacion = new SelectList(db.cotizacion, "idVenta", "idVendedor");
            return View();
        }

        // POST: Oportunidad/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idOportunidad,cotizacion,notas,acciones,venta,costo,estatus,idUsuario,porcentaje")] oportunidad oportunidad)
        {
            if (ModelState.IsValid)
            {
                db.oportunidad.Add(oportunidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idUsuario = new SelectList(db.AspNetUsers, "Id", "Email", oportunidad.idUsuario);
            ViewBag.cotizacion = new SelectList(db.cotizacion, "idVenta", "idVendedor", oportunidad.cotizacion);
            return View(oportunidad);
        }

        // GET: Oportunidad/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oportunidad oportunidad = db.oportunidad.Find(id);
            if (oportunidad == null)
            {
                return HttpNotFound();
            }

            ViewBag.idUsuario = new SelectList(db.AspNetUsers, "Id", "UserName", oportunidad.idUsuario);
            ViewBag.cotizacion = new SelectList(db.cotizacion, "idVenta", "idVenta", oportunidad.cotizacion);
            return View(oportunidad);
        }

        // POST: Oportunidad/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idOportunidad,cotizacion,notas,acciones,venta,costo,estatus,idUsuario,porcentaje")] oportunidad oportunidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oportunidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idUsuario = new SelectList(db.AspNetUsers, "Id", "Email", oportunidad.idUsuario);
            ViewBag.cotizacion = new SelectList(db.cotizacion, "idVenta", "idVendedor", oportunidad.cotizacion);
            return View(oportunidad);
        }

        // GET: Oportunidad/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            oportunidad oportunidad = db.oportunidad.Find(id);
            if (oportunidad == null)
            {
                return HttpNotFound();
            }
            return View(oportunidad);
        }

        // POST: Oportunidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            oportunidad oportunidad = db.oportunidad.Find(id);
            db.oportunidad.Remove(oportunidad);
            db.SaveChanges();
            return RedirectToAction("Index");
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
