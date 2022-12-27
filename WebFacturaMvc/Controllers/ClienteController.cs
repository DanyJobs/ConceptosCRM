using Model.Entity;
using Lenguaje;
using Model.Neg;
using System.Collections.Generic;
using System.Web.Mvc;
using WebFacturaMvc.Datos;
using System.Linq;
using System;
using Microsoft.Ajax.Utilities;

namespace WebFacturaMvc.Controllers
{
    [Authorize(Roles = "ADMIN,STANDARD")]
    public class ClienteController : Controller
    {
        private crmconceptoseEntities1 db = new crmconceptoseEntities1();
        ClienteNeg objClienteNeg;
        public ClienteController()
        {
            objClienteNeg = new ClienteNeg();
        }
        // GET: Cliente
        public ActionResult Index()
        {
            List<Cliente> lista = objClienteNeg.findAll();
            return View(lista);
        }

        [HttpGet]
        public ActionResult Create()
        {
            mensajeInicioRegistrar();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Cliente objCliente)
        {
            mensajeInicioRegistrar();
            if (String.IsNullOrWhiteSpace(objCliente.Cuenta.ToString())) {
                objCliente.Cuenta = 0;
            }
            objClienteNeg.create(objCliente);
            MensajeErrorRegistrar(objCliente);
            ModelState.Clear();
            return View("Create");
        }

        //mensaje de error
        public void MensajeErrorRegistrar(Cliente objCliente)
        {
            switch (objCliente.Estado)
            {
                //case 10://campo codigo vacio
                //    ViewBag.MensajeError = "Ingrese Código del Cliente";
                //    break;
                //case 1://error campo cadigo
                //    ViewBag.MensajeError = "No se permiten mas de 5 caracteres en al campo Codigo";
                //    break;
                case 1000://campo nombre vacio
                    ViewBag.MensajeError = Recurso.Mensaje_DNIE;
                    break;
                case 20://campo nombre vacio
                    ViewBag.MensajeError = Recurso.Mensaje_NC;
                    break;

                case 2://error de nombre
                    ViewBag.MensajeError = Recurso.Mensaje_NCE;
                    break;

                case 30://campo Apellido Paterno vacio
                    ViewBag.MensajeError = Recurso.Mensaje_AP;
                    break;

                case 3://error de Apellido Paterno
                    ViewBag.MensajeError = Recurso.Mensaje_APE;
                    break;

                case 40://campo Apellido Materno vacio
                    ViewBag.MensajeError = Recurso.Mensaje_AM;
                    break;

                case 4://error de Apellido Materno
                    ViewBag.MensajeError = Recurso.Mensaje_AME;
                    break;

                case 50://campo dni vacio
                    ViewBag.MensajeError = Recurso.Mensaje_DNI;
                    break;
                case 5://error de dni
                    ViewBag.MensajeError = Recurso.Mensaje_DNIE2;
                    break;
                case 60://campo de direccion vacio
                    ViewBag.MensajeError = Recurso.Mensaje_DC;
                    break;
                case 6://error de direccion
                    ViewBag.MensajeError = Recurso.Mensaje_DCE;
                    break;
                case 70://campo telefono vacio
                    ViewBag.MensajeError = Recurso.Mensaje_TC;
                    break;
                case 7://error de direccion
                    ViewBag.MensajeError = Recurso.Mensaje_TCE;
                    break;
                case 8://error de duplicidad
                    ViewBag.MensajeError = Recurso.Mensaje_dup1 + objCliente.IdCliente + Recurso.Mensaje_dup2;
                    break;
                case 9://error de duplicidad
                    ViewBag.MensajeError = Recurso.Mensaje_dup3 + objCliente.Email + Recurso.Mensaje_dup4;
                    break;
                case 99://carrera registrada con exito
                    ViewBag.MensajeExito = Recurso.Mensaje_dup5 + objCliente.Nombre + " " + objCliente.Apellido + Recurso.Mensaje_dup6;
                    break;

            }

        }

        public void mensajeInicioRegistrar()
        {
            ViewBag.MensajeInicio = Recurso.MensajeInicio_cliente;
        }


        [HttpGet]
        public ActionResult Update(long id)
        {
            mensajeInicialActualizar();
            Cliente objCliente = new Cliente(id);
            objClienteNeg.find(objCliente);
            return View(objCliente);
        }
        [HttpPost]
        public ActionResult Update(Cliente objCliente)
        {
            mensajeInicialActualizar();
            objClienteNeg.update(objCliente);
            MensajeErrorActualizar(objCliente);
            return View();
            //return Redirect("~/Cliente/Index/");
        }

        //mensaje de error al actualizar
        public void MensajeErrorActualizar(Cliente objCliente)
        {

            switch (objCliente.Estado)
            {
                case 10://campo codigo vacio
                    ViewBag.MensajeError = Recurso.MensajeA_CC;
                    break;
                case 1://error campo cadigo
                    ViewBag.MensajeError = Recurso.MensajeA_CCE;
                    break;
                case 20://campo nombre vacio
                    ViewBag.MensajeError = Recurso.MensajeA_NC;
                    break;

                case 2://error de nombre
                    ViewBag.MensajeError = Recurso.MensajeA_NCE;
                    break;

                case 30://campo Apellido Paterno vacio
                    ViewBag.MensajeError = Recurso.Mensaje_AP;
                    break;

                case 3://error de Apellido Paterno
                    ViewBag.MensajeError = Recurso.Mensaje_APE;
                    break;

                case 40://campo Apellido Materno vacio
                    ViewBag.MensajeError = Recurso.Mensaje_AM;
                    break;

                case 4://error de Apellido Materno
                    ViewBag.MensajeError = Recurso.Mensaje_AME;
                    break;

                //case 50://campo dni vacio
                //    ViewBag.MensajeError = Recurso.Mensaje_DNI;
                //    break;
                //case 5://error de dni
                //    ViewBag.MensajeError = Recurso.Mensaje_DNIE2;
                //    break;
                case 60://campo de direccion vacio
                    ViewBag.MensajeError = Recurso.Mensaje_DC;
                    break;
                case 6://error de direccion
                    ViewBag.MensajeError = Recurso.Mensaje_DCE;
                    break;
                case 70://campo telefono vacio
                    ViewBag.MensajeError = Recurso.Mensaje_TC;
                    break;
                case 7://error de direccion
                    ViewBag.MensajeError = Recurso.Mensaje_TCE;
                    break;
                case 9://error de duplicidad
                    ViewBag.MensajeError = Recurso.Mensaje_dup3 + objCliente.Email + Recurso.Mensaje_dup4;
                    break;

                case 99://carrera registrada con exito
                    ViewBag.MensajeExito = Recurso.MensajeA_dup1 + objCliente.IdCliente + Recurso.MensajeA_dup2;
                    break;

            }

        }
        //mensjae inicial actualizar
        public void mensajeInicialActualizar()
        {
            ViewBag.MensajeInicialActualizar = Recurso.MensajeA_inicial;
        }

        [HttpGet]
        public ActionResult Delete(long id)
        {
            mensajeInicialEliminar();
            Cliente objCliente = new Cliente(id);
            objClienteNeg.find(objCliente);
            return View(objCliente);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            mensajeInicialEliminar();
            Cliente objCliente = new Cliente(id);
            objClienteNeg.delete(objCliente);
            mostrarMensajeEliminar(objCliente);
            return Redirect("~/Cliente/Index/");
        }

        [HttpGet]
        public ActionResult Eliminar(long id)
        {
            mensajeInicialEliminar();
            Cliente objCliente = new Cliente(id);
            objClienteNeg.find(objCliente);
            return View(objCliente);
        }

        [HttpPost]
        public ActionResult Eliminar(Cliente objCliente)
        {
            mensajeInicialEliminar();
            objClienteNeg.delete(objCliente);
            mostrarMensajeEliminar(objCliente);
            Cliente objCLiente2 = new Cliente();
            return View(objCLiente2);
            //return RedirectToAction("Index");
        }

        //mensaje de error al eliminar
        private void mostrarMensajeEliminar(Cliente objCliente)
        {

            switch (objCliente.Estado)
            {
                case 1: //ERROR DE EXISTENCIA
                    ViewBag.MensajeError = Recurso.MensajeE_1 + objCliente.IdCliente + Recurso.MensajeE_2;
                    break;

                case 33://CLIENTE NO EXISTE
                    ViewBag.MensajeError = Recurso.MensajeE_1 + objCliente.Apellido + ", " + objCliente.Nombre + Recurso.MensajeE_3;
                    break;
                case 34:
                    ViewBag.MensajeError = Recurso.MensajeE_4 + objCliente.Apellido + ", " + objCliente.Nombre + Recurso.MensajeE_5;
                    break;
                case 99: //EXITO
                    ViewBag.MensajeExito = Recurso.MensajeE_1 + objCliente.Apellido + ", " + objCliente.Nombre + Recurso.MensajeE_6;
                    break;

                default:
                    ViewBag.MensajeError = "===???===";
                    break;
            }
        }
        public void mensajeInicialEliminar()
        {
            ViewBag.MensajeInicialEliminar = Recurso.MensajeE_inicial;
        }

        public ActionResult Find(long id)
        {
            Cliente objCliente = new Cliente(id);
            objClienteNeg.find(objCliente);
            //objClienteNeg.find2(objCliente);
            return View(objCliente);
        }

        [HttpGet]
        public ActionResult BuscarClientes()
        {
            List<Cliente> lista = objClienteNeg.findAll();
            return View(lista);
        }
        [HttpPost]
        public ActionResult BuscarClientes(string txtnombre, string txtappaterno, string txtemail, long txtcliente = -1)
        {

            if (txtnombre == "")
            {
                txtnombre = "-1";
            }
            if (txtappaterno == "")
            {
                txtappaterno = "-1";
            }
            if (txtemail == "")
            {
                txtemail = "-1";
            }
            Cliente objCliente = new Cliente();
            objCliente.Nombre = txtnombre;
            objCliente.IdCliente = txtcliente;
            objCliente.Apellido = txtappaterno;
            objCliente.Email = txtemail;

            List<Cliente> cliente = objClienteNeg.findAllClientes(objCliente);
            return View(cliente);
        }

        [HttpGet]
        public ActionResult ObtenerCuenta()
        {
            return View(db.cuenta.ToList());
        }
    }
}