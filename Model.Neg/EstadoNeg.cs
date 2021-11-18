using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Dao;
using Model.Entity;

namespace Model.Neg
{
    public class EstadoNeg
    {
        //Carga los estados disponibles a partir del ID del pais
        public List<Estado> cargarEstados(int IdPais)
        {
            EstadoDao e = new EstadoDao();
            return e.cargarEstados(IdPais);
        }
        //Carga los estados disponibles a partir del ID del pais y el nombre del estado
        public List<Estado> cargarEstados(int IdPais, string txtparametro)
        {
            EstadoDao e = new EstadoDao();
            return e.cargarEstados(IdPais, txtparametro);
        }
        //Agrega un Estado nuevo
        public void agregarEstado(Estado e)
        {
            EstadoDao objetoE = new EstadoDao();
            objetoE.agregarEstado(e);
        }
        //Carga un estado a partir del ID del estado
        public Estado cargarEstado(int IdEstado)
        {
            EstadoDao objetoE = new EstadoDao();
            return objetoE.cargarEstado(IdEstado);
        }
        //Edita la información de un estado
        public void editarEstado(Estado e)
        {
            EstadoDao objetoE = new EstadoDao();
            objetoE.editarEstado(e);
        }
        //Comprueba si hay una venta con un estado activo
        public bool hayEstado(Estado e)
        {
            EstadoDao objetoE = new EstadoDao();
            return objetoE.hayEstado(e);
        }
        //Elimina la información de un estado
        public void eliminarEstado(Estado e)
        {
            EstadoDao objetoE = new EstadoDao();
            objetoE.eliminarEstado(e);
        }
        }
}
