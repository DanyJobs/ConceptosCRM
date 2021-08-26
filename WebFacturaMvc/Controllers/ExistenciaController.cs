using Model.Neg;
using Model.Dao;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using WebFacturaMvc.Datos;

namespace WebFacturaMvc.Controllers
{
    [Authorize(Roles = "ADMIN,STANDARD")]
    public class ExistenciaController : Controller
    {
        private crmconceptoseEntities1 db = new crmconceptoseEntities1();
      
        public void cargarSucursales()
        {
            //Consulta LINQ para obtener las sucursales
            var cSucursal = (from mProveedor in db.sucursal
                             select mProveedor);
            List<sucursal> sC = cSucursal.AsEnumerable<sucursal>().ToList();
            SelectList lista = new SelectList(sC, "idSucursal", "descripcion");
            ViewBag.ListaSucursal = lista;
        }
        // GET: Existencia
        public ActionResult Index()
        {
            //Trae los datos
            ExistenciaDao Existencias = new ExistenciaDao();
            DataTable dtExistencias = Existencias.consultar();
            List<ExistenciaT> lista = new List<ExistenciaT>();
            //Se llena la lista
            for(int i = 0; i < dtExistencias.Rows.Count; i++)
            {
                //Objeto existencias
                ExistenciaT objExistencia = new ExistenciaT();
                //Se llenan los datos
                objExistencia.Nombre = dtExistencias.Rows[i]["Nombre"].ToString();
                objExistencia.Sucursal = dtExistencias.Rows[i]["Descripcion"].ToString();
                objExistencia.Cantidad = int.Parse(dtExistencias.Rows[i]["Cantidad"].ToString());
                objExistencia.Seccion=  dtExistencias.Rows[i]["Seccion"].ToString();
                //Se agrega a la lista
                lista.Add(objExistencia);
            }
            //Se regresa la vista con la lista
            cargarSucursales();
            return View(lista);
        }

    

        [HttpGet]
        public ActionResult Filtrado()
        {
            //Trae los datos
            ExistenciaDao Existencias = new ExistenciaDao();
            DataTable dtExistencias = Existencias.consultar();
            List<ExistenciaT> lista = new List<ExistenciaT>();
            //Se llena la lista
            for (int i = 0; i < dtExistencias.Rows.Count; i++)
            {
                //Objeto existencias
                ExistenciaT objExistencia = new ExistenciaT();
                //Se llenan los datos
                objExistencia.Nombre = dtExistencias.Rows[i]["Nombre"].ToString();
                objExistencia.Sucursal = dtExistencias.Rows[i]["Descripcion"].ToString();
                objExistencia.Cantidad = int.Parse(dtExistencias.Rows[i]["Cantidad"].ToString());
                objExistencia.Seccion = dtExistencias.Rows[i]["Seccion"].ToString();
                //Se agrega a la lista
                lista.Add(objExistencia);
            }
            //Se regresa la vista con la lista
            cargarSucursales();
            return View(lista);
        }

        [HttpPost]//para buscar clientes
        public ActionResult Filtrado(string txtproducto,string txtsucursal)
        {
            if (txtproducto == "")
            {
                txtproducto = null;
            }
            if (txtsucursal =="")
            {
                txtsucursal = "-1";
            }
      
            int txtsucursalConvertido = Convert.ToInt32(txtsucursal);
         
            ExistenciaDao daoExistencia = new ExistenciaDao();
            List<ExistenciaT> listaexistencia = daoExistencia.findAllExistencia(txtproducto, txtsucursalConvertido);
            cargarSucursales();
            return View(listaexistencia);
        }
    }
}