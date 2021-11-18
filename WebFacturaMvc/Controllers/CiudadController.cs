using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Neg;
using Model.Entity;


namespace WebFacturaMvc.Controllers
{
    public class CiudadController : Controller
    {
        // GET: Ciudad
        public ActionResult Index(int idEstado)
        {
            CiudadNeg objCiudad = new CiudadNeg();
            Ciudad c = new Ciudad();
            c = objCiudad.cargarCiudadPais(idEstado);
            ViewData["Estado"] = c.IdPais;
            ViewData["EstadoPrincipal"] = idEstado;                        
            return View(objCiudad.cargarCiudades(idEstado));
        }
        [HttpPost]
        public ActionResult Index(string txtEstado, string txtParametro)
        {
            CiudadNeg objCiudad = new CiudadNeg();
            Ciudad c = new Ciudad();            
            c = objCiudad.cargarCiudadPais(int.Parse(txtEstado));
            ViewData["Estado"] = c.IdPais;
            ViewData["EstadoId"] = c.IdEstado;
            return View(objCiudad.cargarCiudades(int.Parse(txtEstado), txtParametro));
        }
        public ActionResult AgregarCiudad(string idEstado)
        {                                                
            ViewData["EstadoR"] = idEstado;
            return View();
        }
        [HttpPost]
        public ActionResult GuardarCiudad(string Nombre, string IdEstado)
        {
            string mensaje = "";
            Ciudad c = new Ciudad();

            CiudadNeg objC = new CiudadNeg();
            if (Nombre == null || Nombre == "")
            {

                mensaje = "Debe introducir un nombre";
            }
            else
            {
                c.NombreCiudad = Nombre;
                c.IdEstado = int.Parse(IdEstado);
                try
                {
                    objC.agregarCiudad(c);
                    mensaje = "Ciudad agregada correctamente";
                }
                catch (Exception ex)
                {
                    mensaje = "Error: " + ex.ToString();
                }
            }
            return Json(mensaje);
        }
        public ActionResult EditarCiudad(string idCiudad)
        {
            ViewData["CiudadId"] = idCiudad;
            CiudadNeg objC = new CiudadNeg();
            Ciudad c = objC.cargarCiudad(int.Parse(idCiudad));
            ViewData["NombreCiudad"] = c.NombreCiudad;
            ViewData["EstadoRe"] = c.IdEstado;
            return View();
        }
        [HttpPost]
        public ActionResult Editar(string Nombre, string IdCiudad)
        {
            string mensaje = "";
            Ciudad c = new Ciudad();

            CiudadNeg objC = new CiudadNeg();
            if (Nombre == null || Nombre == "")
            {

                mensaje = "Debe introducir un nombre";
            }
            else
            {
                c.NombreCiudad = Nombre;
                c.IdCiudad = int.Parse(IdCiudad);
                try
                {
                    objC.editarCiudad(c);
                    mensaje = "Ciudad editada correctamente";
                }
                catch (Exception ex)
                {
                    mensaje = "Error: " + ex.ToString();
                }
            }
            return Json(mensaje);
        }
        public ActionResult EliminarCiudad(int idCiudad)
        {
            ViewData["CiudadId"] = idCiudad;
            CiudadNeg objC = new CiudadNeg();
            Ciudad c = objC.cargarCiudad(idCiudad);
            ViewData["NombreCiudad"] = c.NombreCiudad;
            ViewData["EstadoRe"] = c.IdEstado;
            return View();
        }
        [HttpPost]
        public ActionResult Eliminar(string Nombre, string IdCiudad)
        {
            string mensaje = "";
            Ciudad c = new Ciudad();
            c.NombreCiudad = Nombre;
            c.IdCiudad = int.Parse(IdCiudad);
            CiudadNeg objC = new CiudadNeg();
            //Para verificar si hay ventas registadas con esa ciudad
            bool respuesta = objC.hayCiudad(c);
            if (respuesta==true)
            {
                mensaje = "Error, la ciudad no puede ser eliminada porque aún existen ventas con esta ciudad. Elimine todas las ventas referentes a esta ciudad para poder eliminarla.";
            }
            else
            {
                try
                {
                    objC.eliminarCiudad(c);
                    mensaje = "Ciudad eliminada correctamente";
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
