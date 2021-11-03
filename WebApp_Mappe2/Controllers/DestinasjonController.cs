using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Mappe2.DAL;
using WebApp_Mappe2.Models;

namespace WebApp_Mappe2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DestinasjonController : ControllerBase  
    {
        private const string _loggetInn = "loggetInn";

        private IDestinasjonRepository _db;

        private ILogger<DestinasjonController> _log;
        public DestinasjonController(IDestinasjonRepository db, ILogger<DestinasjonController> log)
        {
            _db = db;
            _log = log;
        }

        [HttpGet]
        public async Task<ActionResult> HentAlleDestinasjoner()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                _log.LogInformation("Login ikke gyldig!");
                return Unauthorized("Ikke logget inn");
            }

            List<Destinasjon> alleDestinasjoner = await _db.HentAlleDestinasjoner();
            if(alleDestinasjoner == null)
            {
                _log.LogInformation("Henting av alle destinasjoner fungerte ikke");
                return NotFound("Henting av alle destinasjoner fungerte ikke");
            }
            _log.LogInformation("Henting av alle Destinasjoner ble gjennomført suksessfullt");
            return Ok(alleDestinasjoner);
            
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> HentDestinasjon(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                _log.LogInformation("Login ikke gyldig!");
                return Unauthorized("Ikke logget inn");
            }

            Destinasjon destinasjon = await _db.HentDestinasjon(id);
            if(destinasjon == null)
            {
                _log.LogInformation("Fant ikke destinasjon med id " + id);
                return NotFound("Fant ikke destinasjonen");
            }
            _log.LogInformation("Henting av Destinasjon -> " + id + " ble gjennomført suksessfullt");
            return Ok(destinasjon);
        }


        [HttpPost]
        public async Task<ActionResult> LagreDestinasjon(Destinasjon d)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                _log.LogInformation("Login ikke gyldig!");
                return Unauthorized("Ikke logget inn");
            }

            bool returOK = await _db.LagreDestinasjon(d);
            if (!returOK)
            {
                _log.LogInformation("Lagring av destinasjon ble ikke gjennomført");
                return BadRequest("Destinasjon kunne ikke lagres");
            }
            _log.LogInformation("Lagring av Destinasjon ble gjennomført suksessfullt");
            return Ok("Destinasjon lagret");
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> SlettDestinasjon(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                _log.LogInformation("Login ikke gyldig!");
                return Unauthorized("Ikke logget inn");
            }

            bool returOK = await _db.SlettDestinasjon(id);
            if (!returOK)
            {
                _log.LogInformation("Sletting av destinasjon.id -> " + id + " ble ikke gjennomført");
                return NotFound("Sletting av destinasjon ble ikke utført");
            }
            _log.LogInformation("Sletting av Destinasjon ble gjennomført suksessfullt");
            return Ok("Destinasjon slettet");

        }
        [HttpPut]
        public async Task<ActionResult> EndreDestinasjon(Destinasjon d)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                _log.LogInformation("Login ikke gyldig!");
                return Unauthorized();
            }
            if (ModelState.IsValid)
            {
                bool returOK = await _db.EndreDestinasjon(d);
                if (!returOK)
                {
                    _log.LogInformation("Endringen kunne ikke utføres");
                    return NotFound();
                }
                _log.LogInformation("Endring av Destinasjon ble gjennomført suksessfullt");
                return Ok();
            }
            _log.LogInformation("Feil i inputvalidering ved endring av Destinasjon");
            return BadRequest();
        }
    }
}
