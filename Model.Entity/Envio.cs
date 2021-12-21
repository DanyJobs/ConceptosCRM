using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Envio
    {
        private int idEnvio;
        private int idVenta;
        private DateTime fecha;
        private string estado;
        private string numeroGuia;
        private string paqueteria;
        private int idPaqueteria;
        //PARA LAS CONSULTAS
        private string nombreCliente;
        private string correo;
        private string usuarioEnvio;
        private string link;
        public int IdEnvio
        {
            get
            {
                return idEnvio;
            }

            set
            {
                idEnvio = value;
            }
        }
        public int IdPaqueteria
        {
            get
            {
                return idPaqueteria;
            }

            set
            {
                idPaqueteria = value;
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
        public string Estado
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
        public string Link
        {
            get
            {
                return link;
            }

            set
            {
                link = value;
            }
        }
        public string NumeroGuia
        {
            get
            {
                return numeroGuia;
            }

            set
            {
                numeroGuia = value;
            }
        }
        public string Paqueteria
        {
            get
            {
                return paqueteria;
            }

            set
            {
                paqueteria = value;
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
        public string Correo
        {
            get
            {
                return correo;
            }

            set
            {
                correo = value;
            }
        }
        public string UsuarioEnvio
        {
            get
            {
                return usuarioEnvio;
            }

            set
            {
                usuarioEnvio = value;
            }
        }
    }
}
