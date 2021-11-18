using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Venta
    {
        private int idCotizacion;
        private int idCliente;
        private string codigoPostal;
        private int idCiudad;
        private string calle;
        private string numExt;
        private string numInt;
        private string colonia;
        private string telefono;
        private byte[] archivo;
        private string archivoTemporal;
        private DateTime fecha;
        private string vendedor;
        //Para la consulta
        private string nombreCliente;
        private string total;
        private int idVenta;
        private int idEstado;
        private int idPais;
        private string email;
        public string ArchivoTemporal
        {
            get
            {
                return archivoTemporal;
            }

            set
            {
                archivoTemporal = value;
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
        public int IdCotizacion
        {
            get
            {
                return idCotizacion;
            }

            set
            {
                idCotizacion = value;
            }
        }
        public int IdVenta
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
        public int IdCliente
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
        public int IdCiudad
        {
            get
            {
                return idCiudad;
            }

            set
            {
                idCiudad = value;
            }
        }
        public string CP
        {
            get
            {
                return codigoPostal;
            }

            set
            {
                codigoPostal = value;
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
        public string NumExterior
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
        public string NumInterior
        {
            get
            {
                return numInt;
            }

            set
            {
                numInt = value;
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
        public byte[] Archivo
        {
            get
            {
                return archivo;
            }

            set
            {
                archivo = value;
            }
        }
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
        public string Vendedor
        {
            get
            {
                return vendedor;
            }

            set
            {
                vendedor = value;
            }
        }
        public string NombreCliente
        {
            get
            {
                return nombreCliente;
            }

            set
            {
                nombreCliente = value;
            }
        }
        public string Total
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
        public Venta()
        {

        }
        public Venta(int idCotizacion, int idCliente, string cp, int ciudad, string calle, string numExt, string numInt, string colonia, string telefono, byte[] archivo, DateTime fecha, string vendedor)
        {
            this.idCotizacion = idCotizacion;
            this.idCliente = idCliente;
            this.codigoPostal = cp;
            this.idCiudad = ciudad;
            this.calle = calle;
            this.numExt = numExt;
            this.numInt = numInt;
            this.colonia = colonia;
            this.telefono = telefono;
            this.archivo = archivo;
            this.fecha = fecha;
            this.vendedor = vendedor;
        }
        public Venta(int idVenta, int idCotizacion, string cp, int ciudad, string calle, string numExt, string numInt, string colonia, string telefono, byte[] archivo, DateTime fecha, string vendedor, string nombreCliente, string Total)
        {
            this.idVenta = idVenta;
            this.idCotizacion = idCotizacion;            
            this.codigoPostal = cp;
            this.idCiudad = ciudad;
            this.calle = calle;
            this.numExt = numExt;
            this.numInt = numInt;
            this.colonia = colonia;
            this.telefono = telefono;
            this.archivo = archivo;
            this.fecha = fecha;
            this.vendedor = vendedor;
            this.nombreCliente = nombreCliente;
            this.total = Total;
        }
    }
}
