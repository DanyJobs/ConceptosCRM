using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;
using Model.Dao;

namespace Model.Neg
{
    public class PaqueteriaNeg
    {
        PaqueteriaDao paq = new PaqueteriaDao();
        //Carga las paqueterias disponibles
        public List<Paqueteria> cargarPaqueterias()
        {
            return paq.cargarPaqueterias();
        }

    }
}
