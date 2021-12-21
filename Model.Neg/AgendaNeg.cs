using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Dao;
using Model.Entity;

namespace Model.Neg
{
    public class AgendaNeg
    {
        AgendaDao agendaMetodos = new AgendaDao();
        //Carga los eventos de la agenda del día actual
        public List<Agenda> cargarAgendaHoy(string idUsuario, string fecha)
        {            
            return agendaMetodos.cargarAgendaHoy(idUsuario, fecha);
        }
        //Carga la información de un evento en particular
        public Agenda cargarEvento(int idEvento)
        {
            return agendaMetodos.cargarEvento(idEvento);
        }
        //Agrega la información de un evento a la tabla agenda
        public void agregarEvento(Agenda evento)
        {
            List<Agenda> lista = agendaMetodos.verificarEvento(evento.Fecha, evento.Hora, evento.IdUsuario);
            if (lista.Count > 0)
            {
                throw new Exception("Error, ya tiene un evento con la misma fecha y hora");
            }
            else
            {
                agendaMetodos.agregarEvento(evento);
            }
            
        }
        //Editar la información de un evento a la tabla agenda
        public void editarEvento(Agenda evento)
        {
            List<Agenda> lista = agendaMetodos.verificarEvento2(evento);
            if (lista.Count > 0)
            {
                throw new Exception("Error, ya tiene un evento con la misma fecha y hora");
            }
            else
            {
                agendaMetodos.editarEvento(evento);
            }
        }
        //Elimina un evento de la agenda
        public void eliminarEvento(Agenda evento)
        {
            agendaMetodos.eliminarEvento(evento);
        }
        //Carga los eventos según un periodo de fecha
        public List<Agenda> filtrarEventos(string Day, string Month, string Year, string Usuario)
        {
            return agendaMetodos.filtrarEventos(Day, Month, Year, Usuario);
        }

        }
    }
