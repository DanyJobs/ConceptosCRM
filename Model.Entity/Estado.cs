using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Estado
    {
        private int idPais;
        private int idEstado;
        private string nombreEstado;

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
        public int IdEstado
        {
            get
            {
                return idEstado;
            }
            set
            {
                idEstado = value;
            }
        }
        public string NombreEstado
        {
            get
            {
                return nombreEstado;
            }
            set
            {
                nombreEstado = value;
            }
        }

        public Estado()
        {

        }
        public Estado(int idPais, int idEstado, string nombreEstado)
        {
            this.idPais = idPais;
            this.idEstado = idEstado;
            this.nombreEstado = nombreEstado;
        }
    }
}
