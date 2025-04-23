using Natillera_Parcial.Clases;
using Natillera_Parcial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Natillera_Parcial.Controllers
{
    [RoutePrefix("api/Eventos")]
    public class EventosController : ApiController
    {
        [HttpGet]
        [Route("ConsultarxTipo")]
        public Evento ConsultarxTipo(string TipoEvento)
        {
            clsEvento even = new clsEvento();
            return even.ConsultarxTipo(TipoEvento);
        }

        [HttpGet]
        [Route("ConsultarxFecha")]
        public Evento ConsultarxFecha(DateTime Fecha)
        {
            clsEvento even = new clsEvento();
            return even.ConsultarxFecha(Fecha);
        }

        [HttpGet]
        [Route("ConsultarxNombre")]
        public Evento ConsultarxNombre(string NombreEvento)
        {
            clsEvento eve = new clsEvento();
            return eve.ConsultarxNombre(NombreEvento);
        }

        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] Evento even)
        {
            clsEvento Evento = new clsEvento();
             Evento.evento = even;
            return Evento.Insertar();
        }
        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] Evento even)
        {
            clsEvento Evento = new clsEvento();
            Evento.evento = even;
            return Evento.Actualizar();
        }
        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar([FromBody] Evento even)
        {
            clsEvento Evento = new clsEvento();
            Evento.evento = even;
            return Evento.Eliminar();
        }
    }
}