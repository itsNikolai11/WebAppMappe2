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
    public class AvgangController : ControllerBase
    {
        private const string _loggetInn = "loggetInn";

        private IAvgangRepository _db;

        private ILogger<AvgangController> _log;
        public AvgangController(IAvgangRepository db, ILogger<AvgangController> log)
        {
            _db = db;
            _log = log;
        }

        [HttpGet]
        public async Task<ActionResult> HentAvganger(/*int RuteId, DateTime Tid*/)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                _log.LogInformation("Login ikke gyldig!");

                return Unauthorized("Login ikke gyldig!");
            }
            List<Avgang> alleAvganger = await _db.HentAvganger();
            if (alleAvganger == null)
            {
                _log.LogInformation("Fant ingen avganger");
                return NotFound("Fant ingen avganger");
            }
            return Ok(alleAvganger);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> HentAvgang(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                _log.LogInformation("Login ikke gyldig!");
                return Unauthorized("Login ikke gyldig!");
            }

            Avgang avgang = await _db.HentAvgang(id);
            if (avgang == null)
            {
                _log.LogInformation("Fant ikke avgang med id " + id);
                return NotFound("Fant ikke avgang");
            }
            _log.LogInformation("Henting av Avgang -> " + id + " ble gjennomført suksessfullt");
            return Ok(avgang);
        }
        [HttpPost]
        public async Task<ActionResult> LagreAvgang(Avgang r)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                _log.LogInformation("Login ikke gyldig!");
                return Unauthorized("Login ikke gyldig!");
            }
            bool returOK = await _db.LagreAvgang(r);
            if (!returOK)
            {
                _log.LogInformation("Destinasjonen kunne ikke lagres!");
                return BadRequest("Lagring feilet");
            }
            return Ok(true);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> SlettAvgang(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                _log.LogInformation("Login ikke gyldig!");
                return Unauthorized("Login ikke gyldig!");
            }
            bool returOK = await _db.SlettAvgang(id);
            if (!returOK)
            {
                _log.LogInformation("Sletting av Avgang ble ikke utført");
                return NotFound(false);
            }
            _log.LogInformation("Sletting av Avgang ble gjennomført suksessfullt");
            return Ok(true);
        }
        [HttpPut]
        public async Task<ActionResult> EndreAvgang(Avgang r)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                _log.LogInformation("Login ikke gyldig!");
                return Unauthorized("Login ikke gyldig!");
            }
            if (ModelState.IsValid)
            {
                bool returOK = await _db.EndreAvgang(r);
                if (!returOK)
                {
                    _log.LogInformation("Endringen kunne ikke utføres");
                    return NotFound(false);
                }
                _log.LogInformation("Endring av Avgang ble gjennomført suksessfullt");
                return Ok(true);
            }
            _log.LogInformation("Feil i inputvalidering ved endring av Avgang");
            return BadRequest("Feil i inputvalidering ved endring av Avgang");
        }
    }
}
