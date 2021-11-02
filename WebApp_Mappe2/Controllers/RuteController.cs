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

                return Unauthorized();
            }
            //ID er id til fra-destinasjon
            List<Rute> alleRuter = await _db.HentRuter();
            return Ok(alleRuter);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> HentRute(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {

                return Unauthorized();
            }
            Rute rute = await _db.HentRute(id);
            if (rute== null)
            {
                _log.LogInformation("Fant ikke rute med id " + id);
                return NotFound();
            }
            return Ok(rute);
        }

        [HttpPost]
        public async Task<ActionResult> LagreRute(Rute innRute)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {

                return Unauthorized();
            }
            bool returOK = await _db.LagreRute(innRute);
            if (!returOK)
            {
                _log.LogInformation("Ruten kunne ikke lagres!");
                return BadRequest();
            }
           
            return Ok();
            
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> SlettRute(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {

                return Unauthorized();
            }

            bool returOK = await _db.SlettRute(id);
            if (!returOK)
            {
                _log.LogInformation("Sletting av ruten ble ikke utført");
                return NotFound();
            }
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> EndreRute(Rute endreRute)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {

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
                return Ok();
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest();
        }


    }
}
