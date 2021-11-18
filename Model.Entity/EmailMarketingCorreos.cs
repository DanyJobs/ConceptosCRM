using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public partial class EmailMarketingCorreos
    {
        public int idMarketing { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public Nullable<int> dias { get; set; }
        public DateTime fechaComienzo { get; set; }
        public string idUsuario { get; set; }      
    }
}
