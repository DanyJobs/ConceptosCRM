using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Sucursal
    {
        private int idSucursal;
        private string descripcion;
        private string calle;
        private string numExt;
        private string colonia;
        private string cp;
        private string email;
        private string telefono;


        public int IdSucursal
        {
            get
            {
                return idSucursal;
            }

            set
            {
                idSucursal = value;
            }
        }
        public string Descripcion
        {
            get
            {
                return descripcion;
            }

            set
            {
                descripcion = value;
            }
        }
        public string Calle
        {
            get
            {
                return calle;
            }

            set
            {
                calle = value;
            }
        }
        public string NumExt
        {
            get
            {
                return numExt;
            }

            set
            {
                numExt = value;
            }
        }
        public string Colonia
        {
            get
            {
                return colonia;
            }

            set
            {
                colonia = value;
            }
        }
        public string CP
        {
            get
            {
                return cp;
            }

            set
            {
                cp = value;
            }
        }
        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }
        public string Telefono
        {
            get
            {
                return telefono;
            }

            set
            {
                telefono = value;
            }
        }

        public Sucursal()
        {

        }
        public Sucursal(int idSucursal, string descripcion, string calle, string numExt, string colonia, string cp, string email, string telefono)
        {
            this.idSucursal = idSucursal;
            this.descripcion = descripcion;
            this.calle = calle;
            this.numExt = numExt;
            this.colonia = colonia;
            this.cp = cp;
            this.email = email;
            this.telefono = telefono;
        }

    }
}
