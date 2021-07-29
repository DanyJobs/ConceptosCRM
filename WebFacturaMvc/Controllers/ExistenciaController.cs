using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebFacturaMvc.Datos;

namespace WebFacturaMvc.Controllers
{
    public class ExistenciaController : Controller
    {
        private crmconceptoseEntities1 db = new crmconceptoseEntities1();

        // GET: Existencia
        public ActionResult Index()
        {
            var existencia = db.existencia.Include(e => e.producto).Include(e => e.sucursal);
            return View(existencia.ToList());
        }

        // GET: Existencia/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            existencia existencia = db.existencia.Find(id);
            if (existencia == null)
            {
                return HttpNotFound();
            }
            return View(existencia);
        }

        // GET: Existencia/Create
        public ActionResult Create()
        {
            ViewBag.idProducto = new SelectList(db.producto, "idProducto", "nombre");
            ViewBag.idSucursal = new SelectList(db.sucursal, "idSucursal", "calle");
            return View();
        }

        // POST: Existencia/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idProducto,idSucursal,cantidad,seccion")] existencia existencia)
        {
            if (ModelState.IsValid)
            {
                db.existencia.Add(existencia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idProducto = new SelectList(db.producto, "idProducto", "nombre", existencia.idProducto);
            ViewBag.idSucursal = new SelectList(db.sucursal, "idSucursal", "calle", existencia.idSucursal);
            return View(existencia);
        }

        // GET: Existencia/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            existencia existencia = db.existencia.Find(id);
            if (existencia == null)
            {
                return HttpNotFound();
            }
            ViewBag.idProducto = new SelectList(db.producto, "idProducto", "nombre", existencia.idProducto);
            ViewBag.idSucursal = new SelectList(db.sucursal, "idSucursal", "calle", existencia.idSucursal);
            return View(existencia);
        }

        // POST: Existencia/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProducto,idSucursal,cantidad,seccion")] existencia existencia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(existencia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idProducto = new SelectList(db.producto, "idProducto", "nombre", existencia.idProducto);
            ViewBag.idSucursal = new SelectList(db.sucursal, "idSucursal", "calle", existencia.idSucursal);
            return View(existencia);
        }

        // GET: Existencia/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            existencia existencia = db.existencia.Find(id);
            if (existencia == null)
            {
                return HttpNotFound();
            }
            return View(existencia);
        }

        // POST: Existencia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            existencia existencia = db.existencia.Find(id);
            db.existencia.Remove(existencia);
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
