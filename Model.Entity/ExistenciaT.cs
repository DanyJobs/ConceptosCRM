using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class ExistenciaT
    {
        private string nombre;
        private string sucursal;
        private int cantidad;
        private string seccion;
        private int estado;
        [DisplayFormat(DataFormatString = "{MM-dd-yyyy}", ApplyFormatInEditMode = true)]
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
        public string Sucursal
        {
            get
            {
                return sucursal;
            }

            set
            {
                sucursal = value;
            }
        }


        public int Cantidad
        {
            get
            {
                return cantidad;
            }

            set
            {
                cantidad = value;
            }
        }

        public string Seccion
        {
            get
            {
                return seccion;
            }

            set
            {
                seccion = value;
            }
        }
        public int Estado
        {
            get
            {
                return estado;
            }

            set
            {
                estado = value;
            }
        }

        public ExistenciaT()
        {

        }
        public ExistenciaT(string nombre, string Sucursal, int cantidad, string seccion)
        {
            this.nombre = nombre;
            this.sucursal = Sucursal;
            this.cantidad = cantidad;
            this.seccion = seccion;
        }
        public ExistenciaT(string nombre)
        {
            this.nombre = nombre;
        }
    }
}
