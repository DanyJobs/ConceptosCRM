using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class RFQItemEliminado
    {
        public Nullable<int> idRFQItem { get; set; }
        public Nullable<int> idRfq { get; set; }

        public RFQItemEliminado(int? idRFQItem, int? idRfq)
        {
            this.idRFQItem = idRFQItem;
            this.idRfq = idRfq;
        }      
    }
}
