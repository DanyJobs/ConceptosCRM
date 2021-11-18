using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Pais
    {
        private int idPais;
        private string nombrePais;

        public int IdPais
        {
            get
            {
                return idPais;
            }
            set
            {
                idPais = value;
            }
        }
        public string NombrePais
        {
            get
            {
                return nombrePais;
            }
            set
            {
                nombrePais = value;
            }
        }

        public Pais()
        {

        }
        public Pais(int idPais, string nombrePais)
        {
            this.idPais = idPais;
            this.nombrePais = nombrePais;
        }
    }
    
}
