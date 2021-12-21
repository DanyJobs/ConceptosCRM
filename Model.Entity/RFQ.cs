using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class RFQ
    {
        public RFQ(int idRFQ, string idVendedor, DateTime fecha, string estatus)
        {
            this.idRFQ = idRFQ;
            this.idVendedor = idVendedor;
            this.fecha = fecha;
            this.estatus = estatus;
        }

        public RFQ()
        {
        }

        public RFQ(int idRFQ, string estatus)
        {
            this.idRFQ = idRFQ;
            this.estatus = estatus;
        }

        public int idRFQ { get; set; }
        public string idVendedor { get; set; }
        public DateTime fecha { get; set; }
        public string estatus { get; set; }
    }
}
