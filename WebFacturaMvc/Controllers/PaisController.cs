using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Entity;
using Model.Neg;

namespace WebFacturaMvc.Controllers
{
    public class PaisController : Controller
    {
        // GET: Pais
        public ActionResult Index()
        {
            PaisNeg objPais = new PaisNeg();
            return View(objPais.cargarPaises());
        }
        [HttpPost]
        public ActionResult Index(string txtParametro)
        {
            PaisNeg objPais = new PaisNeg();
            return View(objPais.cargarPaises(txtParametro));
        }
        public ActionResult AgregarPais()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GuardarPais(string Nombre)
        {
            string mensaje = "";
            Pais pa = new Pais();

            PaisNeg p = new PaisNeg();
            if (Nombre == null || Nombre == "")
            {

                mensaje = "Debe introducir un nombre";
            }
            else
            {
                pa.NombrePais = Nombre;
                try
                {
                    p.agregarPais(pa);
                    mensaje = "País agregado correctamente";
                }
                catch (Exception e)
                {
                    mensaje = "Error: " + e.ToString();
                }
            }
            return Json(mensaje);
        }
        public ActionResult EditarPais(int IdPais)
        {
            PaisNeg objetoPais = new PaisNeg();
            Pais p = new Pais();
            p = objetoPais.cargarPais(IdPais);
            ViewData["Nombre"] = p.NombrePais;
            ViewData["Id"] = p.IdPais;
            return View();
        }
        [HttpPost]
        public ActionResult Editar(string Nombre, string IdPais)
        {
            string mensaje = "";
            Pais pa = new Pais();

            PaisNeg p = new PaisNeg();
            if (Nombre == null || Nombre == "")
            {

                mensaje = "Debe introducir un nombre";
            }
            else
            {
                pa.NombrePais = Nombre;
                pa.IdPais = int.Parse(IdPais);
                try
                {
                    p.editarPais(pa);
                    mensaje = "País editado correctamente";
                }
                catch (Exception e)
                {
                    mensaje = "Error: " + e.ToString();
                }
            }
            return Json(mensaje);
        }
        public ActionResult EliminarPais(int IdPais)
        {
            PaisNeg objetoPais = new PaisNeg();
            Pais p = new Pais();
            p = objetoPais.cargarPais(IdPais);
            ViewData["Nombre"] = p.NombrePais;
            ViewData["Id"] = p.IdPais;
            return View();
        }
        [HttpPost]
        public ActionResult Eliminar(string Nombre, string IdPais)
        {
            string mensaje = "";
            Pais p = new Pais();
            p.IdPais = int.Parse(IdPais);
            PaisNeg objetoE = new PaisNeg();
            //Para verificar si hay ventas registadas con esa ciudad
            bool respuesta = objetoE.hayEstadoPais(p);
            if (respuesta == true)
            {
                mensaje = "Error, el país no puede ser eliminado porque contiene estados en él. Elimine todos los estados referente al país antes de eliminarlo.";
            }
            else
            {
                try
                {
                    objetoE.eliminarPais(p);
                    mensaje = "Pais eliminado correctamente";
                }
                catch (Exception ex)
                {
                    mensaje = "Error: " + ex.ToString();
                }

            }
            return Json(mensaje);
        }
    }
}