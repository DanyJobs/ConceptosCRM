using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class RFQHistorial
    {
        [DisplayFormat(DataFormatString = "{MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaRFQ { get; set; }
        public string Comprador { get; set; }
        public string Productos { get; set; }
        public decimal precio { get; set; }
        public string empresa { get; set; }
        public string nombreProveedor { get; set; }       
        public string idProducto { get; set; }

        public int Estado { get; set; }
    }
}
