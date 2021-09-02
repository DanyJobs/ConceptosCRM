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
        private CotizacionNeg objCotizacionNeg;
        public DetalleCotizacionController()
        {
            objCotizacionNeg = new CotizacionNeg();
            objDetalleVentaNeg = new DetalleCotizacionNeg();
            objFacturaNeg = new FacturaNeg();
            objVentaNeg = new CotizacionNeg();
        }
        // GET: DetalleVenta
        public ActionResult Index()
        {
            List<Cotizacion> lista = objCotizacionNeg.buscar();
            cargarFechas();
            return View(lista);
        }

        //Para ver los detalles de la cotizacion
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
        }
        
        public ActionResult DetalleCotizacion(int idVenta)
        {
            List<DetalleCotizacion> lista = objDetalleVentaNeg.VerDetalles(idVenta);
            return View(lista);
        }
        [HttpPost]
        public ActionResult Index(string txtMes, string txtyear)
        {
            string month = "", year = "";
            string vyear = "";
            int condicion = 0;
            if (txtMes == "")
            {
                txtMes = null;
            }
            if (txtyear == "")
            {
                txtyear = "-1";
            }
            //Validaciones
            if (txtyear == "-1")
            {
                vyear = null;
            }
            //Para meses 10-12
            if (txtMes != null)
            {
                condicion = int.Parse(txtMes);
                if (condicion >= 10)
                {
                    month = txtMes;
                }
                //Para meses 1-9
                else
                {
                    month = "0" + txtMes;
                }
            }
            //Para el año
            if (vyear != null)
            {
                year = txtyear;
            }
            cargarFechas();
            List<Cotizacion> lista = objCotizacionNeg.buscar(month, year);                        
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
        public ActionResult Editar()
        {           
            return View();
        }
    }
}