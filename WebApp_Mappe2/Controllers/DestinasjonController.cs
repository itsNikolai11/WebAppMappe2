﻿using Microsoft.AspNetCore.Http;
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
                return Unauthorized();
            }

            List<Destinasjon> alleDestinasjoner = await _db.HentAlleDestinasjoner();
            if(alleDestinasjoner == null)
            {
                _log.LogInformation("Henting av alle destinasjoner fungerte ikke");
                return NotFound("Henting av alle destinasjoner fungerte ikke");
            }
            _log.LogInformation("Henting av alle destinasjoner ble gjennomført suksessfullt");
            return Ok(alleDestinasjoner);
            
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> HentDestinasjon(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }

            Destinasjon destinasjon = await _db.HentDestinasjon(id);
            if(destinasjon == null)
            {
                _log.LogInformation("Fant ikke destinasjon med id " + id);
                return NotFound("Fant ikke avgang med id " + id);
            }
            _log.LogInformation("Henting av destinasjon -> " + id + " ble gjennomført suksessfullt");
            return Ok(destinasjon);
        }


        [HttpPost]
        public async Task<ActionResult> LagreDestinasjon(Destinasjon d)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }

            bool returOK = await _db.LagreDestinasjon(d);
            if (!returOK)
            {
                _log.LogInformation("Lagring av destinasjon.id -> " + d.Id + " ble ikke gjennomført");
                return BadRequest();
            }
            _log.LogInformation("Lagring av destinasjon ble gjennomført suksessfullt");
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> SlettDestinasjon(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }

            bool returOK = await _db.SlettDestinasjon(id);
            if (!returOK)
            {
                _log.LogInformation("Sletting av destinasjon.id -> " + id + " ble ikke gjennomført");
                return NotFound();
            }
            _log.LogInformation("Slett Destinasjon ble gjennomført suksessfullt");
            return Ok();

        }
        [HttpPut]
        public async Task<ActionResult> EndreDestinasjon(Destinasjon d)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }

            bool returOK = await _db.EndreDestinasjon(d);
            if (!returOK)
            {
                _log.LogInformation("Endringen ble ikke utført");
                return NotFound();
            }
            _log.LogInformation("Endre Destinasjon ble gjennomført suksessfullt");
            return Ok();
        }
    }
}
