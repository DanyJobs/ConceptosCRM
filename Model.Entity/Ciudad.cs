using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Ciudad
    {
        private int idCiudad;
        private int idEstado;
        private string nombreCiudad;
        private int idPais;

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
        public string NombreCiudad
        {
            get
            {
                return nombreCiudad;
            }
            set
            {
                nombreCiudad = value;
            }
        }

        public Ciudad()
        {

        }
        public Ciudad(int idCiudad, int idEstado, string nombreCiudad)
        {
            this.idCiudad = idCiudad;
            this.idEstado = idEstado;
            this.nombreCiudad = nombreCiudad;
        }
    }
}
