using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Paqueteria
    {
        private int idPaqueteria;
        private string nombre;
        public int IdPaqueteria
        {
            get
            {
                return idPaqueteria;
            }

            set
            {
                idPaqueteria = value;
            }
        }
        public string Nombre
        {
            get
            {
                return nombre;
            }

            set
            {
                nombre = value;
            }
        }
    }
}
