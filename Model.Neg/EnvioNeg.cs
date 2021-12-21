using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Dao;
using Model.Entity;

namespace Model.Neg
{
    public class EnvioNeg
    {
        //Agrega un envio a la tabla cuando se hace la venta
        EnvioDao env = new EnvioDao();
        public void agregarGuia(Envio e)
        {
            env.agregarGuia(e);
        }
        //Carga los envios que no han enviado el número de guía
        public List<Envio> cargarEnviosN()
        {
            List<Envio> listEnvios = env.cargarEnviosN();
            return listEnvios;
        }
        public Envio cargarEnvio(int idEnvio)
        {
            return env.cargarEnvio(idEnvio);
        }
        //Trae la información del envio para el email
        public Envio datosEmail(int idEnvio)
        {
            return env.datosEmail(idEnvio);
        }
        //Carga los envios que se han enviado
        public List<Envio> cargarEnvios()
        {
            return env.cargarEnvios();
        }
        public Envio cargarEnvios(int IdEnvio)
        {
            return env.cargarEnvios(IdEnvio);
        }
        //Carga los envios que se han enviado filtrados por mes y año
        public List<Envio> cargarEnvios(string mes, string year)
        {
            return env.cargarEnvios(mes, year);
        }
            public void eliminarEnvio(Envio e)
        {
            env.eliminarEnvio(e);
        }
        //Se actualiza la información del envio
        public void editarEnvio(Envio e)
        {
            env.editarEnvio(e);
        }

        }

}
