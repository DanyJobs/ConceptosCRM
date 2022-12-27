using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class VentaItemEliminado
    {
        public VentaItemEliminado(int? idDetalleVenta, int? idVenta)
        {
            this.idDetalleVenta = idDetalleVenta;
            this.idVenta = idVenta;
        }

        public Nullable<int> idDetalleVenta { get; set; }
        public Nullable<int>  idVenta { get; set; }
    }
}
