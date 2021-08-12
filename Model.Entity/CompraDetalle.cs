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
        private int idCompra;
        private decimal precio;
        private int cantidad;
        private string idproducto;
        private int estado;
        [DisplayFormat(DataFormatString = "{MM-dd-yyyy}", ApplyFormatInEditMode = true)]
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

        public CompraDetalle()
        {

        }
        public CompraDetalle(int idcompra, string idproducto, int cantidad, decimal precio)
        {
            this.idCompra = idcompra;
            this.idproducto = idproducto;
            this.precio = precio;
            this.cantidad = cantidad;
        }
        public CompraDetalle(int idCompra)
        {
            this.idCompra = idCompra;
        }
    }
}
