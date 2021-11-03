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
    [Route("api/[controller]")]
    [ApiController]
    public class RuteController : ControllerBase
    {
        private const string _loggetInn = "loggetInn";

        private IRuteRepository _db;

        private ILogger<RuteController> _log;
        public RuteController(IRuteRepository db, ILogger<RuteController> log)
        {
            _db = db;
            _log = log;
        }

        [HttpGet]
        public async Task<ActionResult> HentRuter()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                _log.LogInformation("Login ikke gyldig!");
                return Unauthorized("Ikke logget inn");
            }

            //ID er id til fra-destinasjon
            List<Rute> alleRuter = await _db.HentRuter();
            if(alleRuter == null)
            {
                _log.LogInformation("Henting av alle ruter fungerte ikke");
                return NotFound("Henting av alle ruter fungerte ikke");
            }
            _log.LogInformation("Henting av alle ruter ble gjennomført suksessfullt");
            return Ok(alleRuter);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> HentRute(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                _log.LogInformation("Login ikke gyldig!");
                return Unauthorized("Ikke logget inn");
            }

            Rute rute = await _db.HentRute(id);
            if (rute == null)
            {
                _log.LogInformation("Fant ikke rute med id " + id);
                return NotFound("Fant ingen matchende rute");
            }
            _log.LogInformation("Henting av rute -> " + id + " ble gjennomført suksessfullt");
            return Ok(rute);
        }

        [HttpPost]
        public async Task<ActionResult> LagreRute(Rute innRute)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                _log.LogInformation("Login ikke gyldig!");
                return Unauthorized("Ikke logget inn");
            }
            if (ModelState.IsValid)
            {
                bool returOK = await _db.LagreRute(innRute);
                if (!returOK)
                {
                    _log.LogInformation("Lagring av ruten ble ikke gjennomført");
                    return BadRequest("Ruten kunne ikke lagres");
                }
                _log.LogInformation("Lagring av rute ble gjennomført suksessfullt");
                return Ok("Rute lagret");
            }
            _log.LogInformation("Feil i inputvalidering ved lagring av rute");
            return BadRequest("Feil i inputvalidering ved lagring av rute");
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> SlettRute(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                _log.LogInformation("Login ikke gyldig!");
                return Unauthorized("Ikke logget inn");
            }

            bool returOK = await _db.SlettRute(id);
            if (!returOK)
            {
                _log.LogInformation("Sletting av rute.id -> " + id + " ble ikke gjennomført");
                return NotFound();
            }
            _log.LogInformation("Sletting av Rute ble gjennomført suksessfullt");
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> EndreRute(Rute endreRute)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                _log.LogInformation("Login ikke gyldig!");
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                bool returOK = await _db.EndreRute(endreRute);
                if (!returOK)
                {
                    _log.LogInformation("Endringen kunne ikke utføres");
                    return NotFound();
                }
                _log.LogInformation("Endring av Rute ble gjennomført suksessfullt");
                return Ok();
            }
            _log.LogInformation("Feil i inputvalidering ved endring av Rute");
            return BadRequest();
        }
    }
}
