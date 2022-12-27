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
    public class PlantillaCorreoController : Controller
    {
        private crmconceptoseEntities1 db = new crmconceptoseEntities1();

        // GET: PlantillaCorreo
        public ActionResult Index()
        {
            return View(db.PlantillaCorreo.ToList());
        }

        // GET: PlantillaCorreo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlantillaCorreo plantillaCorreo = db.PlantillaCorreo.Find(id);
            if (plantillaCorreo == null)
            {
                return HttpNotFound();
            }
            return View(plantillaCorreo);
        }

        // GET: PlantillaCorreo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlantillaCorreo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "idPlantilla,DescpPlantilla,plantilla")] PlantillaCorreo plantillaCorreo)
        {
            if (ModelState.IsValid)
            {
                db.PlantillaCorreo.Add(plantillaCorreo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(plantillaCorreo);
        }

        // GET: PlantillaCorreo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlantillaCorreo plantillaCorreo = db.PlantillaCorreo.Find(id);
            if (plantillaCorreo == null)
            {
                return HttpNotFound();
            }
            return View(plantillaCorreo);
        }

        // POST: PlantillaCorreo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPlantilla,DescpPlantilla,plantilla")] PlantillaCorreo plantillaCorreo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plantillaCorreo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(plantillaCorreo);
        }

        // GET: PlantillaCorreo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlantillaCorreo plantillaCorreo = db.PlantillaCorreo.Find(id);
            if (plantillaCorreo == null)
            {
                return HttpNotFound();
            }
            return View(plantillaCorreo);
        }

        // POST: PlantillaCorreo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlantillaCorreo plantillaCorreo = db.PlantillaCorreo.Find(id);
            db.PlantillaCorreo.Remove(plantillaCorreo);
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
