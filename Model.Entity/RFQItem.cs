using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class RFQItem
    {
        public RFQItem()
        {
        }

        public RFQItem(int idRFQItem, int idRfq, int? idProveedor, string idProducto, decimal precio, int cantidad, string notas, string fecha)
        {
            this.idRFQItem = idRFQItem;
            this.idRfq = idRfq;
            this.idProveedor = idProveedor;
            this.idProducto = idProducto;
            this.nombre = nombre;
            this.precio = precio;
            this.cantidad = cantidad;
            this.notas = notas;     
            this.fecha = fecha;     
            
        }
        public RFQItem(int idRFQItem, int idRfq)
        {
            this.idRFQItem = idRFQItem;
            this.idRfq = idRfq;   
        }

        public int idRFQItem { get; set; }
        public Nullable<int> idRfq { get; set; }
        public Nullable<int> idProveedor { get; set; }
        public string idProducto { get; set; }
        public string nombre { get; set; }
        public decimal  precio { get; set; }
        public Nullable<int> cantidad { get; set; }
        public string notas { get; set; }
        public string fecha { get; set; }
        public string empresa { get; set; }
        public string empresaNombre { get; set; }
        public string marca { get; set; }

    }
}
