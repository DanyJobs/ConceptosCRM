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
    [Authorize(Roles = "ADMIN,STANDARD")]
    public class MarcaController : Controller
    {
        private crmconceptoseEntities1 db = new crmconceptoseEntities1();

        // GET: Marca
        public ActionResult Index()
        {
            return View(db.marca.ToList());
        }

        // GET: Marca/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            marca marca = db.marca.Find(id);
            if (marca == null)
            {
                return HttpNotFound();
            }
            return View(marca);
        }

        // GET: Marca/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Marca/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idMarca,descripcion")] marca marca)
        {
            if (ModelState.IsValid)
            {
                db.marca.Add(marca);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(marca);
        }
        public ActionResult CreateProducto()
        {
            return View();
        }

        // POST: Marca/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProducto([Bind(Include = "idMarca,descripcion")] marca marca)
        {
            if (ModelState.IsValid)
            {
                db.marca.Add(marca);
                db.SaveChanges();
                return RedirectToAction("ObtenerMarca");
            }
            CategoriaNeg objCategoriaNeg = new CategoriaNeg();
            List<Categoria> data = objCategoriaNeg.findAll();
            SelectList lista = new SelectList(data, "idCategoria", "nombre");
            ViewBag.ListaCategorias = lista;

            MarcaNeg objMarcaNeg = new MarcaNeg();
            List<Marca> dataMarca = objMarcaNeg.findAll();
            SelectList ListaMarca = new SelectList(dataMarca, "idMarca", "descripcion");
            ViewBag.ListaMarcas = ListaMarca;

            return RedirectToAction("ObtenerMarca");
        }

        // GET: Marca/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            marca marca = db.marca.Find(id);
            if (marca == null)
            {
                return HttpNotFound();
            }
            return View(marca);
        }

        // POST: Marca/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idMarca,descripcion")] marca marca)
        {
            if (ModelState.IsValid)
            {
                db.Entry(marca).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(marca);
        }

        // GET: Marca/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            marca marca = db.marca.Find(id);
            if (marca == null)
            {
                return HttpNotFound();
            }
            return View(marca);
        }

        // POST: Marca/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            marca marca = db.marca.Find(id);
            db.marca.Remove(marca);
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

        [HttpGet]
        public ActionResult ObtenerMarca()
        {
            return View(db.marca.ToList());
        }
    }
}
