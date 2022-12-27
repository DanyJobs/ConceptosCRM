using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class VentaCarga
    {
        public List<DetalleCotizacion> ListaVenta { get; set; }
        public List<VentaDetalle> ListaCompra { get; set; }

        public VentaCarga(List<DetalleCotizacion> listaVenta, List<VentaDetalle> listaCompra)
        {
            ListaVenta = listaVenta;
            ListaCompra = listaCompra;
        }

        public VentaCarga()
        {
            ListaVenta = new List<DetalleCotizacion>();
            ListaCompra = new List<VentaDetalle>();
        }
    }
}
