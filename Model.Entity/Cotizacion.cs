using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Entity
{
    public class Cotizacion
    {
        private long idVenta;
        private double total;
        private long idCliente;
        private string idVendedor;
        private string fecha;
        private double iva;
        private int estado;
        private string email;
        public string notas { get; set; } 
        public string notasCompras { get; set; }
        public string estatus { get; set; }

        [DisplayFormat(DataFormatString = "{MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaCotizacion { get; set; }
        public string NombreCliente { get; set; }
        //Para la parte de mostrar las cotizaciones y editar
        private string cliente;

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
        //Nombre del cliente
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
        //Para el Email del cliente
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



        public double Total
        {
            get
            {
                return total;
            }

            set
            {
                total = value;
            }
        }


        public long IdCliente
        {
            get
            {
                return idCliente;
            }

            set
            {
                idCliente = value;
            }
        }

        public string IdVendedor
        {
            get
            {
                return idVendedor;
            }

            set
            {
                idVendedor = value;
            }
        }


        public string Fecha
        {
            get
            {
                return fecha;
            }

            set
            {
                fecha = value;
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

        public double Iva
        {
            get
            {
                return iva;
            }

            set
            {
                iva = value;
            }
        }

        public Cotizacion()
        {

        }
        public Cotizacion(double total, long idCliente, string idVendedor, string fecha, double iva,string notas,string notasCompras,string estatus)
        {           
            this.total = total;
            this.idCliente = idCliente;
            this.idVendedor = idVendedor;
            this.iva = iva;
            this.fecha = fecha;
            this.notas = notas;
            this.notasCompras = notasCompras;
            this.estatus = estatus;
        }
        //Para la parte de mostrar las cotizaciones y editar
        public Cotizacion(double total, string Cliente, string idVendedor, string fecha, double iva)
        {
            this.total = total;
            this.cliente = Cliente;
            this.idVendedor = idVendedor;
            this.iva = iva;
            this.fecha = fecha;            
        }
        public Cotizacion(long idVenta)
        {
            this.idVenta = idVenta;
        }
    }
}
