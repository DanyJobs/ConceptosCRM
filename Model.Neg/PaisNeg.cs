using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Dao;
using Model.Entity;

namespace Model.Neg
{
    public class PaisNeg
    {
        public List<Pais> cargarPaises()
        {
            PaisDao p = new PaisDao();
            return p.cargarPaises();
        }
        //Carga los paises disponibles FILTRADOS
        public List<Pais> cargarPaises(string parametro)
        {
            PaisDao p = new PaisDao();
            return p.cargarPaises(parametro);
        }
        //Agrega un pais nuevo
        public void agregarPais(Pais p)
        {
            PaisDao pa = new PaisDao();
            pa.agregarPais(p);
        }
        //Carga la información de un pais
        public Pais cargarPais(int IdPais)
        {
            PaisDao p = new PaisDao();
            return p.cargarPais(IdPais);
        }
        //Agrega un pais nuevo
        public void editarPais(Pais p)
        {
            PaisDao pa = new PaisDao();
            pa.editarPais(p);
        }
        //Eliminar un pais
        public void eliminarPais(Pais p)
        {
            PaisDao pa = new PaisDao();
            pa.eliminarPais(p);
        }
        //Comprueba si hay una venta con un pais activo
        public bool hayEstadoPais(Pais p)
        {
            PaisDao pa = new PaisDao();
            return pa.hayEstadoPais(p);
        }
        }
}
