using Model.Entity;
using Model.Neg;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WebFacturaMvc.Controllers
{
    public class InicioController : Controller
    {
        // GET: Inicio
        public ActionResult Index()
        {
            CotizacionNeg objVentaNeg = new CotizacionNeg();
            List<Cotizacion> lista = objVentaNeg.findAll();
            return View(lista);
        }
    }
}