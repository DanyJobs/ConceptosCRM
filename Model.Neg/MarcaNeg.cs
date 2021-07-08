using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;


namespace Model.Neg
{
    public class MarcaNeg
    {
        private MarcaDao objMarcaDao;
        private ProductoDao objProductoDao;

        public MarcaNeg()
        {
            objMarcaDao = new MarcaDao();
            objProductoDao = new ProductoDao();
        }

        public void create(Marca objMarca)
        {
            bool verificacion = true;

            //inicio verificacion codigo retorna estado=1
            string codigo = objMarca.IdMarca;
            if (codigo == null)
            {
                objMarca.Estado = 10;
                return;
            }
            else
            {
                codigo = objMarca.IdMarca.Trim();
                verificacion = codigo.Length > 0 && codigo.Length <= 5;
                if (!verificacion)
                {
                    objMarca.Estado = 1;
                    return;
                }
            }
      

            //inicio verificacion descripcion retorna estado=3
            string descripcion = objMarca.Descripcion;
            if (descripcion == null)
            {
                objMarca.Estado = 20;
                return;
            }
            else
            {
                descripcion = objMarca.Descripcion.Trim();
                verificacion = descripcion.Length > 0 && descripcion.Length <= 50;
                if (!verificacion)
                {
                    objMarca.Estado = 2;
                    return;
                }
            }
            //fin verificacion de descripcion

            //verificacion de duplicicdad
            Marca objMarcaAux = new Marca();
            objMarcaAux.IdMarca = objMarca.IdMarca;
            verificacion = !objMarcaDao.find(objMarcaAux);
            if (!verificacion)
            {
                objMarca.Estado = 4;
                return;
            }

            //todo bien
            objMarca.Estado = 99;
            objMarcaDao.create(objMarca);
            return;

        }

        public void update(Marca objMarca)
        {
            bool verificacion = true;
            //inicio verificacion descripcion retorna estado=3
            string descripcion = objMarca.Descripcion;
            if (descripcion == null)
            {
                objMarca.Estado = 20;
                return;
            }
            else
            {
                descripcion = objMarca.Descripcion.Trim();
                verificacion = descripcion.Length > 0 && descripcion.Length <= 50;
                if (!verificacion)
                {
                    objMarca.Estado = 2;
                    return;
                }
            }
            //fin verificacion de descripcion


            //todo bien
            objMarca.Estado = 99;
            objMarcaDao.update(objMarca);
            return;
        }

        public void delete(Marca objMarca)
        {
            bool verificacion = true;
            //verificacion de existencia
            Marca objMarcaAux = new Marca();
            objMarcaAux.IdMarca = objMarca.IdMarca;
            verificacion = objMarcaDao.find(objMarcaAux);
            if (!verificacion)
            {
                objMarca.Estado = 33;
                return;
            }

            //verificaicon de existencia de producto
            Producto objProducto = new Producto();
            objProducto.Marca = objMarca.IdMarca;
            verificacion = !objProductoDao.findProductoPorMarcaId(objProducto);
            if (!verificacion)
            {
                objMarca.Estado = 34;
                return;
            }

            //todo bien
            objMarca.Estado = 99;
            objMarcaDao.delete(objMarca);
            return;


        }

        public bool find(Marca objMarca)
        {
            return objMarcaDao.find(objMarca);
        }

        public List<Marca> findAll()
        {
            return objMarcaDao.findAll();
        }
    }
}
