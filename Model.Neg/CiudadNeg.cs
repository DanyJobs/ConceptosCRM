using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;
using Model.Dao;

namespace Model.Neg
{
    public class CiudadNeg
    {
        //Carga las ciudades disponibles a partir del ID del estado
        public List<Ciudad> cargarCiudades(int IdEstado)
        {
            CiudadDao c = new CiudadDao();
            return c.cargarCiudades(IdEstado);
        }
        //Trae una sola ciudad para poder regresar el Estado Index con el IdPais
        public Ciudad cargarCiudadPais(int IdEstado)
        {
            CiudadDao c = new CiudadDao();
            return c.cargarCiudadPais(IdEstado);
        }
        //Carga las ciudades filtradas disponibles a partir del ID del estado
        public List<Ciudad> cargarCiudades(int IdEstado, string Parametro)
        {
            CiudadDao c = new CiudadDao();
            return c.cargarCiudades(IdEstado, Parametro);
        }
        //Agrega una Ciudad nueva
        public void agregarCiudad(Ciudad c)
        {
            CiudadDao ci = new CiudadDao();
            ci.agregarCiudad(c);
        }
        //Trae una sola ciudad a partir del IdCiudad
        public Ciudad cargarCiudad(int IdCiudad)
        {
            CiudadDao c = new CiudadDao();
            return c.cargarCiudad(IdCiudad);
        }
        //Edita una Ciudad
        public void editarCiudad(Ciudad c)
        {
            CiudadDao ci = new CiudadDao();
            ci.editarCiudad(c);
        }
        //Comprueba si hay una venta con una ciudad activa
        public bool hayCiudad(Ciudad c)
        {
            CiudadDao ci = new CiudadDao();
            return ci.hayCiudad(c);
        }
        //Elimina una ciudad
        public void eliminarCiudad(Ciudad c)
        {
            CiudadDao ci = new CiudadDao();
            ci.eliminarCiudad(c);
        }
        }
}
