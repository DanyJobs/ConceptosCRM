using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Lenguaje;

namespace Model.Entity
{
    public class ModoPago
    {
        private Int32 numPago;
        private string nombre;
        private string otros;
        private int estado;
        [Required(ErrorMessageResourceType = typeof(Recurso), ErrorMessageResourceName = "Mensaje_requerido")]
        [Display(ResourceType = typeof(Recurso), Name = "ModoPago_numero")]
        public Int32 NumPago
        {
            get
            {
                return numPago;
            }

            set
            {
                numPago = value;
            }
        }
        [Required(ErrorMessageResourceType = typeof(Recurso), ErrorMessageResourceName = "Mensaje_requerido")]
        [Display(ResourceType = typeof(Recurso), Name = "ModoPago_nombre")]
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
        [Display(ResourceType = typeof(Recurso), Name = "Otros_detalles")]
        public string Otros
        {
            get
            {
                return otros;
            }

            set
            {
                otros = value;
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
        public ModoPago()
        {

        }
        public ModoPago(Int32 numPago, string nombre, string otros)
        {
            this.NumPago = numPago;
            this.Nombre = nombre;
            this.Otros = otros;
        }
       
        public ModoPago(Int32 numPago)
        {
            this.NumPago = numPago;
           
        }
    }
}
