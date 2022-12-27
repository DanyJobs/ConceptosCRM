using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class VentaDetalle
    {
        public int idDetalleVenta { get; set; }
		public int idVenta { get; set; }
        public int? idProveedor { get; set; }
        public string proveedor { get; set; }
        public string empresa { get; set; }
        public string idProducto { get; set; }
        public string nombreProducto { get; set; }
        public string marca { get; set; }
        public string unidadMedida { get; set; }
        public double precio { get; set; }
        public decimal cantidad { get; set; }
        public string notas { get; set; }
        public int idVentaNuevo { get; set; }

        public VentaDetalle(int idDetalleVenta, int idVenta, int? idProveedor, string idProducto, double precio, decimal cantidad, string notas,int idVentaNuevo)
        {
            this.idDetalleVenta = idDetalleVenta;
            this.idVenta = idVenta;
            this.idProveedor = idProveedor;
            this.idProducto = idProducto;
            this.precio = precio;
            this.cantidad = cantidad;
            this.notas = notas;
            this.idVentaNuevo = idVentaNuevo;
        }
        public VentaDetalle() { 
        
        }

    }
}
