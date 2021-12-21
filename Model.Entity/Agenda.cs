using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Agenda
    {
        private int idEvento;
        private string idUsuario;
        private string titulo;
        private string descripcion;
        private DateTime fecha;
        private string hora;
        private string link;
        private string direccion;

        public int IdEvento
        {
            get
            {
                return idEvento;
            }
            set
            {
                idEvento = value;
            }
        }
        public string IdUsuario
        {
            get
            {
                return idUsuario;
            }
            set
            {
                idUsuario = value;
            }
        }
        public string Titulo
        {
            get
            {
                return titulo;
            }
            set
            {
                titulo = value;
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
        public string Hora
        {
            get
            {
                return hora;
            }
            set
            {
                hora = value;
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
        public Agenda(int idEvento, string idUsuario, string titulo, string descripcion, DateTime fecha, string hora, string link)
        {
            this.idEvento = idEvento;
            this.idUsuario = idUsuario;
            this.titulo = titulo;
            this.descripcion = descripcion;
            this.fecha = fecha;
            this.hora = hora;
            this.link = link;
        }
        public Agenda()
        {

        }
    }
}
