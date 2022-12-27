using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Oportunidad
    {
        public int idOportunidad { get; set; }
        public Nullable<decimal> cotizacion { get; set; }
        public string notas { get; set; }
        public string acciones { get; set; }
        public Nullable<decimal> venta { get; set; }
        public Nullable<decimal> costo { get; set; }
        public string estatus { get; set; }
        public string idUsuario { get; set; }
        public Nullable<int> porcentaje { get; set; }
        public Oportunidad()
        {
        }
            public Oportunidad(int idOportunidad, decimal? cotizacion, string notas, string acciones, decimal? venta, decimal? costo, string estatus, string idUsuario, int? porcentaje)
        {
            this.idOportunidad = idOportunidad;
            this.cotizacion = cotizacion;
            this.notas = notas;
            this.acciones = acciones;
            this.venta = venta;
            this.costo = costo;
            this.estatus = estatus;
            this.idUsuario = idUsuario;
            this.porcentaje = porcentaje;
        }
    }
}
