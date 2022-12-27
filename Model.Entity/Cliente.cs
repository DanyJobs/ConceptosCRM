using Lenguaje;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.Entity
{
    public class Cliente
    {
        private long idCliente;
        private string nombre;
        [Display(ResourceType = typeof(Recurso), Name = "Apellido_Materno")]
        private string apellido;
        private string email;
        private string direccion;
        private string telefono;
        private List<Cotizacion> ventas;
        public int Cuenta { get; set; }
        public string NombreCuenta { get; set; }
        private int estado;


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
        [Display(ResourceType = typeof(Recurso), Name = "Codigo")]
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
        //[Required(ErrorMessage ="Este Campo es Requerido")]
        [Required(ErrorMessageResourceType = typeof(Recurso), ErrorMessageResourceName = "Mensaje_requerido")]
        [Display(ResourceType = typeof(Recurso), Name = "Nombre")]
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
        [Required(ErrorMessageResourceType = typeof(Recurso), ErrorMessageResourceName = "Mensaje_requerido")]
        [Display(ResourceType = typeof(Recurso), Name = "OtroApellido")]
        public string Apellido
        {
            get
            {
                return apellido;
            }

            set
            {
                apellido = value;
            }
        }
        [Required(ErrorMessageResourceType = typeof(Recurso), ErrorMessageResourceName = "Mensaje_requerido")]
        [Display(ResourceType = typeof(Recurso), Name = "Email")]
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

        [Required(ErrorMessageResourceType = typeof(Recurso), ErrorMessageResourceName = "Mensaje_requerido")]
        [Display(ResourceType = typeof(Recurso), Name = "Dirección")]
        public string Direccion
        {
            get
            {
                return direccion;
            }

            set
            {
                direccion = value;
            }
        }
        [Required(ErrorMessageResourceType = typeof(Recurso), ErrorMessageResourceName = "Mensaje_requerido")]
        [Display(ResourceType = typeof(Recurso), Name = "Telefóno")]
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
        [Required(ErrorMessageResourceType = typeof(Recurso), ErrorMessageResourceName = "Mensaje_requerido")]
        public List<Cotizacion> Ventas
        {
            get
            {
                return ventas;
            }

            set
            {
                ventas = value;
            }
        }

        public Cliente()
        {

        }
        public Cliente(long idCliente)
        {
            this.idCliente = idCliente;
        }

        public Cliente(long idCliente, string nombre, string apellido, string email, string dni, string direccion, string telefono)
        {
            this.idCliente = idCliente;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Email = email;
            this.Direccion = direccion;
            this.Telefono = telefono;
        }

    }
}
