using Lenguaje;
using System.ComponentModel.DataAnnotations;


namespace Model.Entity
{
    public class Categoria
    {
        private string idCategoria;
        private string nombre;
        private string descripcion;
        private int estado;

        [Display(ResourceType = typeof(Recurso), Name = "IdCategoria")]
        public string IdCategoria
        {
            get
            {
                return idCategoria;
            }

            set
            {
                idCategoria = value;
            }
        }
        [Display(ResourceType = typeof(Recurso), Name = "Nombre_Texto")]
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
        [Display(ResourceType = typeof(Recurso), Name = "Descripcion")]
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

        public Categoria()
        {

        }
        public Categoria(string idCategoria, string nombre, string descripcion)
        {
            this.idCategoria = idCategoria;
            this.Nombre = nombre;
            this.Descripcion = descripcion;
        }
        public Categoria(string idCategoria)
        {
            this.idCategoria = idCategoria;

        }
    }
}
