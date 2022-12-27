using Microsoft.AspNet.Identity;
using Model.Entity;
using Model.Neg;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WebFacturaMvc.Controllers
{
    [Authorize(Roles = "ADMIN,STANDARD")]
    public class InicioController : Controller
    {
        // GET: Inicio
        public ActionResult Index()
        {
            CotizacionNeg objVentaNeg = new CotizacionNeg();
            List<Cotizacion> lista = objVentaNeg.findAll(User.Identity.GetUserId());
            return View(lista);
        }
    }
}