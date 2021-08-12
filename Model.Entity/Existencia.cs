using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Existencia
    {
        private string idProducto;
        private int idSucursal;
        private int cantidad;
        private string seccion;        
        private int estado;
        [DisplayFormat(DataFormatString = "{MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public string IdProducto
        {
            get
            {
                return idProducto;
            }

            set
            {
                idProducto = value;
            }
        }
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

        public Existencia()
        {

        }
        public Existencia(string idproducto,int idSucursal, int cantidad, string seccion)
        {
            this.idProducto = idproducto;
            this.idSucursal = idSucursal;
            this.cantidad = cantidad;
            this.seccion = seccion;
        }
        public Existencia(string idproducto)
        {
            this.idProducto = idproducto;
        }
    }
}
