using Model.Entity;
using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Neg
{
    public class OportunidadNeg
    {
        private OportunidadDao objOportunidadDao;

        public OportunidadNeg()
        {
            objOportunidadDao = new OportunidadDao();
        }

        public List<Oportunidad> findAll()
        {
            return objOportunidadDao.findAll();
        }
    }
}
