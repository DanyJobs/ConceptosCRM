using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Model.Entity
{
    public class Compra
    {
        //Información limpia
        private int idCompra;        
        private decimal total;
        private int idSucursal;
        private int idProveedor;
        private DateTime fecha;        
        private int estado;
        //Para JOIN
        private string nombreSucursal;
        private string nombreProveedor;
        //Para el Editar
        private string fechaCompra;
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
        public decimal Total
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
        public string FechaCompra
        {
            get
            {
                return fechaCompra;
            }

            set
            {
                fechaCompra = value;
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

        public int IdProveedor
        {
            get
            {
                return idProveedor;
            }

            set
            {
                idProveedor = value;
            }
        }
        [DisplayFormat(DataFormatString = "{MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha
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
        public string NombreProveedor
        {
            get
            {
                return nombreProveedor;
            }

            set
            {
                nombreProveedor = value;
            }
        }

        public Compra()
        {

        }
        public Compra(decimal total, int idProveedor, int idSucursal, DateTime fecha)
        {
            this.total = total;
            this.idProveedor = idProveedor;
            this.idSucursal = idSucursal;            
            this.fecha = fecha;
        }
        public Compra(int idCompra)
        {
            this.idCompra = idCompra;
        }
        public Compra(decimal total, string Proveedor, string Sucursal, DateTime fecha)
        {
            this.total = total;
            this.nombreProveedor = Proveedor;
            this.nombreSucursal = Sucursal;
            this.fecha = fecha;
        }
    }
}
