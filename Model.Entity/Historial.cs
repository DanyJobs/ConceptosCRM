using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Entity
{
    public class Historial
    {
        private string idProducto;
        private string cliente;
        private string numCotizacion;
        private string producto;
        private decimal precioUnitario;
        [DisplayFormat(DataFormatString = "{MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaCotizacion { get; set; }

        private int estado;


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

        public string Cliente
        {
            get
            {
                return cliente;
            }

            set
            {
                cliente = value;
            }
        }

        public string NumCotizacion
        {
            get
            {
                return numCotizacion;
            }

            set
            {
                numCotizacion = value;
            }
        }

        public string Producto
        {
            get
            {
                return producto;
            }

            set
            {
                producto = value;
            }
        }
        public decimal PrecioUnitario
        {
            get
            {
                return precioUnitario;
            }

            set
            {
                precioUnitario = value;
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
        public Historial()
        {

        }
        public Historial(string idProducto, string cliente, string numCotizacion, string producto, decimal precioUnitario, DateTime fechaCotizacion)
        {
            this.idProducto = idProducto;
            this.cliente = cliente;
            this.numCotizacion = numCotizacion;
            this.producto = producto;
            this.precioUnitario = precioUnitario;
            this.FechaCotizacion = fechaCotizacion;
        }
        public Historial(string producto)
        {
            this.producto = producto;
        }
    }
}
