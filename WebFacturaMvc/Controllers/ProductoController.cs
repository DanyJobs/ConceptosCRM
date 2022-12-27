using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lenguaje;
using Model.Dao;
using Model.Entity;
using Model.Neg;

namespace WebFacturaMvc.Controllers
{
    [Authorize(Roles = "ADMIN,STANDARD")]
    public class ProductoController : Controller
    {
        private ProductoNeg objProductoNeg;
        private CategoriaNeg objCategoriaNeg;
        private MarcaNeg objMarcaNeg;
        private ProductoDao objProductoDao;
        private static string categoria;
        private static string marca;
        private static string unidadMedida;

        public ProductoController()
        {
            objProductoDao = new ProductoDao();
            objProductoNeg = new ProductoNeg();
            objMarcaNeg = new MarcaNeg();
            objCategoriaNeg = new CategoriaNeg();
        }
        // GET: Producto
        public ActionResult Index()
        {
            CategoriaNeg objCategoriaNeg = new CategoriaNeg();
            List<Categoria> data = objCategoriaNeg.findAll();
            SelectList lista = new SelectList(data, "idCategoria", "nombre");
            ViewBag.ListaCategorias = lista;

            Producto objProducto = new Producto();
            objProducto.IdProducto = null;
            objProducto.Nombre = null;
            objProducto.Categoria = "-1";
            objProducto.Marca = "-1";
            List<Producto> ListaProducto = objProductoNeg.findAllProductosCotizacion(objProducto);

            return View(ListaProducto);
        }
        [HttpGet]
        public ActionResult Create()
        {
            CategoriaNeg objCategoriaNeg = new CategoriaNeg();
            List<Categoria> data = objCategoriaNeg.findAll();
            SelectList lista = new SelectList(data, "idCategoria", "nombre");
            ViewBag.ListaCategorias = lista;

            MarcaNeg objMarcaNeg = new MarcaNeg();
            List<Marca> dataMarca = objMarcaNeg.findAll();
            SelectList ListaMarca = new SelectList(dataMarca, "idMarca", "descripcion");
            ViewBag.ListaMarcas = ListaMarca;

            LlenarUnidadMedida();


            mensajeInicioRegistrar();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Producto objProducto)
        {
            mensajeInicioRegistrar();      
            LlenarUnidadMedida();
            objProductoNeg.create(objProducto);
            MensajeErrorRegistrar(objProducto);

            CategoriaNeg objCategoriaNeg = new CategoriaNeg();
            List<Categoria> data = objCategoriaNeg.findAll();
            SelectList lista = new SelectList(data, "idCategoria", "nombre");
            ViewBag.ListaCategorias = lista;

            MarcaNeg objMarcaNeg = new MarcaNeg();
            List<Marca> dataMarca = objMarcaNeg.findAll();
            SelectList ListaMarca = new SelectList(dataMarca, "idMarca", "descripcion");
            ViewBag.ListaMarcas = ListaMarca;

            return View("Create");
        }
        public List<SelectListItem> LlenarUnidadMedida()
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem() { Text = Recurso.Pieza, Value = Recurso.PiezaAv });         
            lst.Add(new SelectListItem() { Text = Recurso.Litros, Value = Recurso.LitrosAv });
            lst.Add(new SelectListItem() { Text = Recurso.Kilogramos, Value = Recurso.KilogramosAv });
            lst.Add(new SelectListItem() { Text = Recurso.Metros, Value = Recurso.MetrosAv });
            lst.Add(new SelectListItem() { Text = Recurso.Galon, Value = Recurso.GalonAv });
            lst.Add(new SelectListItem() { Text = Recurso.Millas, Value = Recurso.MillasAv });
            lst.Add(new SelectListItem() { Text = Recurso.Yardas, Value = Recurso.YardasAv });
            lst.Add(new SelectListItem() { Text = Recurso.Pies, Value = Recurso.PiesAv });
            lst.Add(new SelectListItem() { Text = Recurso.Libras, Value = Recurso.LibrasAv });        
            ViewBag.UnidadMedida = lst;
            return lst;
        }

        //mensaje de error
        public void MensajeErrorRegistrar(Producto objProducto)
        {

            switch (objProducto.Estado)
            {
                case 1000://campo codigo vacio
                    ViewBag.MensajeError = "Error!!! Revise la instruccion Insertar";
                    break;
                case 10://campo codigo vacio
                    ViewBag.MensajeError = "Ingrese Código del Producto";
                    break;
                case 1://error campo cadigo
                    ViewBag.MensajeError = "No se permiten mas de 5 caracteres en al campo Codigo";
                    break;
                case 20://campo nombre vacio
                    ViewBag.MensajeError = "Ingrese Nombre del Producto";
                    break;

                case 2://error de nombre
                    ViewBag.MensajeError = "No se permiten mas de 30 caracteres en el campo Nombre";
                    break;

                case 30://campo orecio UNitario vacio
                    ViewBag.MensajeError = "Ingrese Precio Unitario del Producto";
                    break;

                case 3://error de precio Unitario
                    ViewBag.MensajeError = "Precio unitario no valido";
                    break;
                case 300://error de Apellido Paterno
                    ViewBag.MensajeError = "Ingrese un Precio Válido";
                    break;

                case 40://campo Apellido Paterno vacio
                    ViewBag.MensajeError = "Ingrese Categoria";
                    break;

                case 4://error de Apellido Paterno
                    ViewBag.MensajeError = "INgrese una categoria válida";
                    break;
                case 8://error de duplicidad
                    ViewBag.MensajeError = "Producto [" + objProducto.IdProducto + "] ya esta Registrado en el Sistema";
                    break;
                case 99://carrera registrada con exito
                    ViewBag.MensajeExito = "Producto [" + objProducto.IdProducto + "] fue Registrado en el Sistema";
                    break;

            }

        }

        public void mensajeInicioRegistrar()
        {
            ViewBag.MensajeInicio = "Ingrese Datos Del Producto y Click en Guardar";
        }

        public ActionResult Update(string id)
        {
            CategoriaNeg objCategoriaNeg = new CategoriaNeg();
            Producto objProducto = new Producto(id);
            objProductoNeg.findProductos(objProducto);

            marca = objProducto.Marca;
            categoria = objProducto.Categoria;
            unidadMedida = objProducto.unidadMedida;
           
            List <Categoria> data = objCategoriaNeg.findAll();     
            ViewBag.Categoria = new SelectList(data, "idCategoria", "nombre", categoria);
            
            MarcaNeg objMarcaNeg = new MarcaNeg();
            List<Marca> dataMarca = objMarcaNeg.findAll();
            SelectList ListaMarca = new SelectList(dataMarca, "idMarca", "descripcion", marca);
            ViewBag.Marca = ListaMarca;
            List<SelectListItem> ListaUnidades = LlenarUnidadMedida();
            SelectList ListaUnidadMedida = new SelectList(ListaUnidades, "Value", "Text", unidadMedida);
            ViewBag.unidadMedida = ListaUnidadMedida;
            //SelectList ListaUnidadMedida = new SelectList(dataMarca, "unidadMedida", "unidadMedida", marca);
            mensajeInicioActualizar();
            return View(objProducto);
        }
        [HttpPost]
        public ActionResult Update(Producto objProducto)
        {
            CategoriaNeg objCategoriaNeg = new CategoriaNeg();
            MarcaNeg objMarcaNeg = new MarcaNeg();
            List<Marca> dataMarca = objMarcaNeg.findAll();
            List<Categoria> data = objCategoriaNeg.findAll();
            SelectList ListaMarca = new SelectList(dataMarca, "idMarca", "descripcion", marca);
            SelectList lista = new SelectList(data, "idCategoria", "nombre", categoria);
            LlenarUnidadMedida();
            mensajeInicioActualizar();
            try
            {
                if (ModelState.IsValid)
                {                          
                    ViewBag.Categoria = lista;  
                    ViewBag.Marca = ListaMarca;
                    objProductoDao.update(objProducto);
                    MensajeErrorActualizar(objProducto);
                    return RedirectToAction("Index");
                }

                ViewBag.Categoria = lista;
                ViewBag.Marca = ListaMarca;
                return View();
            }
            catch (Exception)
            {

                throw;
            }   
        }
    

        //mensaje de error
        public void MensajeErrorActualizar(Producto objProducto)
        {

            switch (objProducto.Estado)
            {
                case 1000://campo codigo vacio
                    ViewBag.MensajeError = "Error!!! Revise la instruccion de actualizar";
                    break;
                case 10://campo codigo vacio
                    ViewBag.MensajeError = "Ingrese Código del Producto";
                    break;
                case 1://error campo cadigo
                    ViewBag.MensajeError = "No se permiten mas de 5 caracteres en al campo Codigo";
                    break;
                case 20://campo nombre vacio
                    ViewBag.MensajeError = "Ingrese Nombre del Producto";
                    break;

                case 2://error de nombre
                    ViewBag.MensajeError = "No se permiten mas de 30 caracteres en el campo Nombre";
                    break;

                case 30://campo orecio UNitario vacio
                    ViewBag.MensajeError = "Ingrese Precio Unitario del Producto";
                    break;

                case 3://error de precio Unitario
                    ViewBag.MensajeError = "Precio unitario no valido";
                    break;
                case 300://error de Apellido Paterno
                    ViewBag.MensajeError = "Ingrese un Precio Válido";
                    break;

                case 40://campo Apellido Paterno vacio
                    ViewBag.MensajeError = "Ingrese Categoria";
                    break;

                case 4://error de Apellido Paterno
                    ViewBag.MensajeError = "INgrese una categoria válida";
                    break;

                case 99://carrera registrada con exito
                    ViewBag.MensajeExito = "Datos del Producto [" + objProducto.IdProducto + "] Fueron Actualizados";
                    break;

            }

        }
        public void mensajeInicioActualizar()
        {
            ViewBag.MensajeInicio = "Ingrese Datos Del Producto y Click en Guardar";
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            mensajeInicialEliminar();
            Producto objProducto = new Producto(id);
            objProductoNeg.find(objProducto);
            return View(objProducto);
        }

        [HttpPost]
        public ActionResult Delete(Producto objProducto)
        {
            mensajeInicialEliminar();
            objProductoNeg.delete(objProducto);
            mostrarMensajeEliminar(objProducto);
            Producto objProducto2 = new Producto();
            return View(objProducto2);
            //return RedirectToAction("Index");
        }

        //mensaje de error al eliminar
        private void mostrarMensajeEliminar(Producto objProducto)
        {

            switch (objProducto.Estado)
            {
                case 1000://campo codigo vacio
                    ViewBag.MensajeError = "Error!!! Revise la instruccion de Eliminar";
                    break;
                case 1: //ERROR DE EXISTENCIA
                    ViewBag.MensajeError = "Producto [" + objProducto.IdProducto + "] No Esta Registrado en el Sistema ";
                    break;

                case 33://CLIENTE NO EXISTE
                    ViewBag.MensajeError = "El Producto: [" + objProducto.Nombre + "]Ya Fue Eliminado";
                    break;
                case 34:
                    ViewBag.MensajeError = "No se Puede Eliminar al Producto [" + objProducto.Nombre + "] Tiene  Ventas asignadas!!!";
                    break;
                case 99: //EXITO
                    ViewBag.MensajeExito = "Producto [" + objProducto.Nombre + "] Fue Eliminado!!!";
                    break;

                default:
                    ViewBag.MensajeError = "===???===";
                    break;
            }
        }
        public void mensajeInicialEliminar()
        {
            ViewBag.MensajeInicialEliminar = "Formulario de Eliminacion";
        }

        public ActionResult Find(string id)
        {
            Producto objProducto = new Producto(id);
            objProductoNeg.find(objProducto);
            //objClienteNeg.find2(objCliente);
            return View(objProducto);
        }
        public void cargarCategoria()
        {
            List<Categoria> data = objCategoriaNeg.findAll();
            SelectList lista = new SelectList(data, "idCategoria", "nombre");
            ViewBag.ListaCategoria = lista;
        }
        public void cargarMarca()
        {
            List<Marca> data = objMarcaNeg.findAll();
            SelectList lista = new SelectList(data, "idMarca", "descripcion");
            ViewBag.ListaMarca = lista;
        }

        [HttpGet]
        public ActionResult BuscarProductos()
        {
            Producto objProducto = new Producto();
            objProducto.IdProducto = null;
            objProducto.Nombre = null;
            objProducto.Categoria = "-1";
            objProducto.Marca = "-1";
            List<Producto> ListaProducto = objProductoNeg.findAllProductosCotizacion(objProducto);
            cargarCategoria();
            cargarMarca();
            return View(ListaProducto);
        }

        [HttpPost]//para buscar clientes
        public ActionResult BuscarProductos(string txtcodigo, string txtnombre, string txtCategoria, string txtMarca)
        {

            if (txtcodigo == "")
            {
                txtcodigo = null;
            }
            if (txtnombre == "")
            {
                txtnombre = null;
            }
            if (txtCategoria == "")
            {
                txtCategoria = "-1";
            }
            if (txtMarca == "")
            {
                txtMarca = "-1";
            }
            Producto objProducto = new Producto();
            objProducto.IdProducto = txtcodigo;
            objProducto.Nombre = txtnombre;
            objProducto.Categoria = txtCategoria;
            objProducto.Marca = txtMarca;
            List<Producto> ListaProducto = objProductoNeg.findAllProductosCotizacion(objProducto);
            cargarCategoria();
            cargarMarca();
            //System.Diagnostics.Debug.WriteLine(objProducto.IdProducto +"    " +objProducto.Nombre+"    " +objProducto.Categoria+ "     "+objProducto.Marca);
            return View(ListaProducto);
        }

        public void mensajeErrorServidor(Producto objProducto)
        {
            switch (objProducto.Estado)
            {
                case 1000:
                    ViewBag.MensajeError = "ERROR DE SERVIDOR DE SQL SERVER";
                    break;
            }
        }

        [HttpGet]
        public ActionResult BuscarProductosPorCategoria()
        {
            CategoriaNeg objCategoriaNeg = new CategoriaNeg();
            List<Categoria> data = objCategoriaNeg.findAll();
            SelectList lista = new SelectList(data, "idCategoria", "nombre");
            ViewBag.ListaCategorias = lista;
            List<Producto> Producto = objProductoNeg.findAll();
            return View(Producto);
        }

        [HttpPost]
        public ActionResult BuscarProductosPorCategoria(string ListaCategorias)
        {
            CategoriaNeg objCategoriaNeg = new CategoriaNeg();
            List<Categoria> data = objCategoriaNeg.findAll();
            SelectList lista = new SelectList(data, "idCategoria", "nombre");
            ViewBag.ListaCategorias = lista;

            Producto objProducto = new Producto();
            objProducto.Categoria = ListaCategorias;
            List<Producto> Producto = objProductoNeg.findAllProductosPorCategoria(objProducto);
            mensajeErrorServidor(objProducto);
            return View(Producto);
        }

    }
}