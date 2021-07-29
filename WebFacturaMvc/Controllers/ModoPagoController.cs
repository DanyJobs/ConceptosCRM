using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Neg;
using Model.Entity;

namespace WebFacturaMvc.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class ModoPagoController : Controller
    {
        private ModoPagoNeg objModoPagoNeg;
        public ModoPagoController()
        {
            objModoPagoNeg = new ModoPagoNeg();
        }
        // GET: ModoPago
        public ActionResult Index()
        {
            List<ModoPago> lista = objModoPagoNeg.findAll();
            return View(lista);
        }

        [HttpGet]
        public ActionResult Create()
        {
            mensajeInicioRegistrar();
            return View();
        }

        [HttpPost]
        public ActionResult Create(ModoPago objModoPago)
        {
            mensajeInicioRegistrar();
            objModoPagoNeg.create(objModoPago);
            MensajeErrorRegistrar(objModoPago);
            return View("Create");
        }
        //mensaje de error
        public void MensajeErrorRegistrar(ModoPago objModoPago)
        {

            switch (objModoPago.Estado)
            {
                case 1000://campo codigo vacio
                    ViewBag.MensajeError = Lenguaje.Recurso.ProductoM_error1;
                    break;
                //case 10://campo codigo vacio
                //    ViewBag.MensajeError = "Ingrese Código del ModoPago";
                //    break;
                //case 100://campo codigo vacio
                //    ViewBag.MensajeError = "Ingrese solo numeros";
                //    break;
                //case 1://error campo cadigo
                    ViewBag.MensajeError = Lenguaje.Recurso.ProductoM_codigoError;
                    break;
                case 20://campo nombre vacio
                    ViewBag.MensajeError = Lenguaje.Recurso.ModoPago_ingrese;
                    break;

                case 2://error de nombre
                    ViewBag.MensajeError = Lenguaje.Recurso.ProductoM_nombreError;
                    break;

                case 30://campo descripcion vacio
                    ViewBag.MensajeError = Lenguaje.Recurso.ModoPago_detalles;
                    break;

                case 3://error de Apellido Paterno
                    ViewBag.MensajeError = Lenguaje.Recurso.ModoPago_detallesError;
                    break;

                case 8://error de duplicidad
                    ViewBag.MensajeError = Lenguaje.Recurso.ModoPago_m1 + objModoPago.NumPago + Lenguaje.Recurso.ModoPago_m2;
                    break;
                case 99://carrera registrada con exito
                    ViewBag.MensajeExito = Lenguaje.Recurso.ModoPago_m1 + objModoPago.Nombre + Lenguaje.Recurso.ModoPago_m3;
                    break;

            }

        }
        public void mensajeInicioRegistrar()
        {
            ViewBag.MensajeInicio = Lenguaje.Recurso.ModoPago_mensajeInicio;
        }

        public ActionResult Update(int id)
        {
            ModoPago objModoPago = new ModoPago(id);
            objModoPagoNeg.find(objModoPago);
            mensajeInicioActualizar();
            return View(objModoPago);
        }
        [HttpPost]
        public ActionResult Update(ModoPago objModoPago)
        {
            mensajeInicioActualizar();
            objModoPagoNeg.update(objModoPago);
            MensajeErrorActualizar(objModoPago);
            return View();
        }

        //mensaje de error
        public void MensajeErrorActualizar(ModoPago objModoPago)
        {

            switch (objModoPago.Estado)
            {
                case 1000://campo codigo vacio
                    ViewBag.MensajeError = Lenguaje.Recurso.ProductoM_error2;
                    break;
                case 10://campo codigo vacio
                    ViewBag.MensajeError = Lenguaje.Recurso.ModoPago_codigo;
                    break;
                case 1://error campo cadigo
                    ViewBag.MensajeError = Lenguaje.Recurso.ProductoM_codigoError;
                    break;
                case 20://campo nombre vacio
                    ViewBag.MensajeError = Lenguaje.Recurso.ModoPago_ingrese;
                    break;

                case 2://error de nombre
                    ViewBag.MensajeError = Lenguaje.Recurso.ProductoM_nombreError;
                    break;

                case 30://campo descripcion vacio
                    ViewBag.MensajeError = Lenguaje.Recurso.ModoPago_detalles;
                    break;

                case 3://error de precio Unitario
                    ViewBag.MensajeError = Lenguaje.Recurso.ModoPago_detallesError;
                    break;

                case 99://carrera registrada con exito
                    ViewBag.MensajeExito = Lenguaje.Recurso.ModoPago_m4 + objModoPago.NumPago + Lenguaje.Recurso.ModoPago_m5;
                    break;

            }

        }
        public void mensajeInicioActualizar()
        {
            ViewBag.MensajeInicio = Lenguaje.Recurso.ModoPago_mensajeInicio;
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            mensajeInicialEliminar();
            ModoPago objModoPago = new ModoPago(id);
            objModoPagoNeg.find(objModoPago);
            return View(objModoPago);
        }

        [HttpPost]
        public ActionResult Delete(ModoPago objModoPago)
        {
            mensajeInicialEliminar();
            objModoPagoNeg.delete(objModoPago);
            mostrarMensajeEliminar(objModoPago);
            return View();
            //return RedirectToAction("Index");
        }

        //mensaje de error al eliminar
        private void mostrarMensajeEliminar(ModoPago objModoPago)
        {

            switch (objModoPago.Estado)
            {
                case 1000://campo codigo vacio
                    ViewBag.MensajeError = Lenguaje.Recurso.ModoPago_eliminar;
                    break;
                case 1: //ERROR DE EXISTENCIA
                    ViewBag.MensajeError = Lenguaje.Recurso.ModoPago_m1 + objModoPago.NumPago + Lenguaje.Recurso.ProductoM_mensajeE1;
                    break;

                case 33://CLIENTE NO EXISTE
                    ViewBag.MensajeError = Lenguaje.Recurso.ModoPago_m1 + objModoPago.Nombre + Lenguaje.Recurso.ProductoM_mensajeE2;
                    break;
                case 34:
                    ViewBag.MensajeError = Lenguaje.Recurso.ModoPago_eliminarMensaje + objModoPago.Nombre + Lenguaje.Recurso.ModoPago_eliminarMensaje2;
                    break;
                case 99: //EXITO
                    ViewBag.MensajeExito = Lenguaje.Recurso.ModoPago_m1 + objModoPago.Nombre + Lenguaje.Recurso.ProductoM_mensajeE5;
                    break;

                default:
                    ViewBag.MensajeError = "===???===";
                    break;
            }
        }
        public void mensajeInicialEliminar()
        {
            ViewBag.MensajeInicialEliminar = Lenguaje.Recurso.MensajeE_inicial;
        }
        public ActionResult Find(int id)
        {
            ModoPago objModoPago = new ModoPago(id);
            objModoPagoNeg.find(objModoPago);
            //objClienteNeg.find2(objCliente);
            return View(objModoPago);
        }

    }
}