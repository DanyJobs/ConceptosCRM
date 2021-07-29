using Model.Entity;
using Model.Neg;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WebFacturaMvc.Controllers
{
    [Authorize(Roles = "ADMIN,STANDARD")]
    public class DetalleCotizacionController : Controller
    {
        private DetalleCotizacionNeg objDetalleVentaNeg;
        private FacturaNeg objFacturaNeg;
        private CotizacionNeg objVentaNeg;
        public DetalleCotizacionController()
        {
            objDetalleVentaNeg = new DetalleCotizacionNeg();
            objFacturaNeg = new FacturaNeg();
            objVentaNeg = new CotizacionNeg();
        }
        // GET: DetalleVenta
        public ActionResult Index()
        {
            List<DetalleCotizacion> lista = objDetalleVentaNeg.findAll();
            return View(lista);
        }

        public ActionResult Eliminar(long idVenta, long idFactura)
        {
            DetalleCotizacion objDetalleVenta = new DetalleCotizacion();
            objDetalleVenta.IdVenta = idVenta;
            objDetalleVenta.NumFacura = idFactura;
            objDetalleVentaNeg.delete(objDetalleVenta);
            //eliminar venta
            Cotizacion objVenta = new Cotizacion(idVenta);
            objVentaNeg.delete(objVenta);

            //eliminar factura
            Factura objFactura = new Factura(idFactura);
            objFacturaNeg.delete(objFactura);

            return View();
        }
    }
}