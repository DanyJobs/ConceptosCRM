using Model.Dao;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Neg
{
    public class PlantillaCorreoNeg
    {
        private PlantillaCorreoDao objPlantillaCorreoDao;     
        public PlantillaCorreoNeg()
        {
            objPlantillaCorreoDao = new PlantillaCorreoDao();       
        }
        public List<plantillaCorreo> findAll()
        {
            return objPlantillaCorreoDao.findAll();
        }
    }
}
