namespace Model.Entity
{
    public class Marca
    {
        private string idMarca;
        private string descripcion;
        private int estado;



        public string IdMarca
        {
            get
            {
                return idMarca;
            }

            set
            {
                idMarca = value;
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
        public Marca()
        {

        }
        public Marca(string idMarca, string descripcion)
        {
            this.idMarca = idMarca;
            this.descripcion = descripcion;
        }
        public Marca(string idMarca)
        {
            this.idMarca = idMarca;

        }
    }
}
