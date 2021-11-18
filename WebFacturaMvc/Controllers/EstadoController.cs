using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Neg;
using Model.Entity;

namespace WebFacturaMvc.Controllers
{
    public class EstadoController : Controller
    {
        // GET: Estado
        public ActionResult Index(string idPais)
        {
            EstadoNeg objEstado = new EstadoNeg();
            ViewData["Pais"] = idPais;
            return View(objEstado.cargarEstados(int.Parse(idPais)));
        }
        [HttpPost]
        public ActionResult Index(string txtPais, string txtParametro)
        {
            EstadoNeg objEstado = new EstadoNeg();
            return View(objEstado.cargarEstados(int.Parse(txtPais),txtParametro));
        }
        public ActionResult AgregarEstado(string idPais)
        {
            EstadoNeg objEstado = new EstadoNeg();
            ViewData["Pais2"] = idPais;
            return View();
        }
        [HttpPost]
        public ActionResult GuardarEstado(string Nombre, string IdPais)
        {
            string mensaje = "";
            Estado es = new Estado();

            EstadoNeg e = new EstadoNeg();
            if (Nombre == null || Nombre == "")
            {

                mensaje = "Debe introducir un nombre";
            }
            else
            {
                es.NombreEstado = Nombre;
                es.IdPais = int.Parse(IdPais);
                try
               {
                    e.agregarEstado(es);
                    mensaje = "Estado agregado correctamente";
                }
                catch (Exception ex)
               {
                   mensaje = "Error: " + ex.ToString();
               }
            }
            return Json(mensaje);
        }
        public ActionResult EditarEstado(int idEstado)
        {
            EstadoNeg objEstado = new EstadoNeg();
            Estado e = objEstado.cargarEstado(idEstado);
            ViewData["IdPais"] = e.IdPais;
            ViewData["Id"] = e.IdEstado;
            ViewData["Nombre"] = e.NombreEstado;
            return View();
        }
        [HttpPost]        
        public ActionResult Editar(string Nombre, string IdEstado)
        {
            string mensaje = "";
            Estado es = new Estado();

            EstadoNeg e = new EstadoNeg();
            if (Nombre == null || Nombre == "")
            {

                mensaje = "Debe introducir un nombre";
            }
            else
            {
                es.NombreEstado = Nombre;
                es.IdEstado = int.Parse(IdEstado);
                try
                {
                    e.editarEstado(es);
                    mensaje = "Estado editado correctamente";
                }
                catch (Exception ex)
                {
                    mensaje = "Error: " + ex.ToString();
                }
            }
            return Json(mensaje);
        }
        public ActionResult EliminarEstado(int idEstado)
        {
            EstadoNeg objEstado = new EstadoNeg();
            Estado e = objEstado.cargarEstado(idEstado);
            ViewData["IdPais"] = e.IdPais;
            ViewData["Id"] = e.IdEstado;
            ViewData["Nombre"] = e.NombreEstado;
            return View();
        }
        [HttpPost]
        public ActionResult Eliminar(string Nombre, string IdEstado)
        {
            string mensaje = "";
            Estado e = new Estado();            
            e.IdEstado = int.Parse(IdEstado);
            EstadoNeg objetoE = new EstadoNeg();
            //Para verificar si hay ventas registadas con esa ciudad
            bool respuesta = objetoE.hayEstado(e);
            if (respuesta == true)
            {
                mensaje = "Error, el estado no puede ser eliminado porque aún existen ventas con ciudades de este estado. Elimine todas las ventas referentes a las ciudades para poder eliminarlo.";
            }
            else
            {
                try
                {
                    objetoE.eliminarEstado(e);
                    mensaje = "Estado eliminado correctamente";                    
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