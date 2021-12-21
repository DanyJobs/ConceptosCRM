namespace Model.Entity
{
    public class DetalleCotizacion
    {
        private long idDetalleVenta;
        private long numFacura;
        private long idVenta;
        private string idProducto;

        private double subTotal;
        private double descuento;
        private int cantidad;
        private int estado;
        public string notas { get; set; }     
        public long NumFacura
        {
            get
            {
                return numFacura;
            }

            set
            {
                numFacura = value;
            }
        }

        public long IdVenta
        {
            get
            {
                return idVenta;
            }

            set
            {
                idVenta = value;
            }
        }
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
        public double SubTotal
        {
            get
            {
                return subTotal;
            }

            set
            {
                subTotal = value;
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

        public double Descuento
        {
            get
            {
                return descuento;
            }

            set
            {
                descuento = value;
            }
        }

        public long IdDetalleVenta
        {
            get
            {
                return idDetalleVenta;
            }

            set
            {
                idDetalleVenta = value;
            }
        }

        public DetalleCotizacion()
        {

        }
        public DetalleCotizacion(long idDetalleVenta)
        {
            this.idDetalleVenta = idDetalleVenta;

        }
        public DetalleCotizacion(int numFacura, int idVenta, string idProducto, double subTotal, double descuento, int cantidad,string notas)
        {
            this.numFacura = numFacura;
            this.idVenta = idVenta;
            this.idProducto = idProducto;
            this.subTotal = subTotal;
            this.descuento = descuento;
            this.cantidad = cantidad;
            this.notas = notas;
        }
    }
}
