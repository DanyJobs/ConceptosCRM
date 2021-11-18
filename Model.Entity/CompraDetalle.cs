using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class CompraDetalle
    {
        //Normales
        private int idCompra;
        private decimal precio;
        private int cantidad;
        private string idproducto;
        private int estado;
        private string seccion;
        //JOIN
        private string nombreProducto;
        private int idSucursal;
        private string descripcion;
        private string nombreSucursal;

        
        public int IdCompra
        {
            get
            {
                return idCompra;
            }

            set
            {
                idCompra = value;
            }
        }
        public decimal Precio
        {
            get
            {
                return precio;
            }

            set
            {
                precio = value;
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

        public string IdProducto
        {
            get
            {
                return idproducto;
            }

            set
            {
                idproducto = value;
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
        public string NombreSucursal
        {
            get
            {
                return nombreSucursal;
            }

            set
            {
                nombreSucursal = value;
            }
        }
        public string NombreProducto
        {
            get
            {
                return nombreProducto;
            }

            set
            {
                nombreProducto = value;
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

        public CompraDetalle()
        {

        }
        public CompraDetalle(int idcompra, string idproducto, int cantidad, decimal precio, int idSucursal)
        {
            this.idCompra = idcompra;
            this.idproducto = idproducto;
            this.precio = precio;
            this.cantidad = cantidad;
            this.idSucursal = idSucursal;
            
        }
        public CompraDetalle(int idcompra, string idproducto, int cantidad, decimal precio, string seccion)
        {
            this.idCompra = idcompra;
            this.idproducto = idproducto;
            this.precio = precio;
            this.cantidad = cantidad;
            this.seccion = seccion;
        }
        public CompraDetalle(int idCompra)
        {
            this.idCompra = idCompra;
        }
    }
}
