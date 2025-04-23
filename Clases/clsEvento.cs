using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Natillera_Parcial.Models;

namespace Natillera_Parcial.Clases
{
    public class clsEvento
    {
        private DBNatilleraEntities DBNati = new DBNatilleraEntities();
        public Evento evento { get; set; }
        public string Insertar()
        {
            try
            {
                DBNati.Eventos.Add(evento);
                DBNati.SaveChanges();
                return "Evento insertado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al insertar evento: " + ex.Message;
            }
        }
        public string Actualizar()
        {
            Evento eve = ConsultarxNombre(evento.NombreEvento);
            if (eve == null)
            {
                return "El nombre del evento no es válido";
            }
            DBNati.Eventos.AddOrUpdate(evento);
            DBNati.SaveChanges();
            return "Se actualizó el evento correctamente";
        }
        public Evento ConsultarxNombre(string NombreEvento)
        {
            Evento eve = DBNati.Eventos.FirstOrDefault(e => e.NombreEvento == NombreEvento);
            return eve;
        }

        public Evento ConsultarxFecha(DateTime Fecha)
        {
            Evento eve = DBNati.Eventos.FirstOrDefault(e => e.FechaEvento == Fecha);
            return eve; 
        }

        public Evento ConsultarxTipo(string tipo)
        {       
            Evento eve = DBNati.Eventos.FirstOrDefault(e => e.TipoEvento == tipo);
            return eve; 
        }
        public string Eliminar()
        {
            try
            {
                Evento even = ConsultarxNombre(evento.NombreEvento); 
                if (even == null)
                {
                    return "El nombre del Evento no es válido";
                }
                DBNati.Eventos.Remove(even);
                DBNati.SaveChanges();
                return "Se eliminó el Evento correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
