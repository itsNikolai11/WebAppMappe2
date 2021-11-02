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
    public class OrdreController : ControllerBase
    {
        private readonly IOrdreRepository _db;

        private const string _loggetInn = "loggetInn";

        private ILogger<OrdreController> _log;
        public OrdreController(IOrdreRepository db, ILogger<OrdreController> log)
        {
            _db = db;
            _log = log;
        }

        [HttpGet]
        public async Task<ActionResult> HentOrdre()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                _log.LogInformation("Login ikke gyldig!");
                return Unauthorized();
            }

            List<Billett> billetter = await _db.hentAlle();
            _log.LogInformation("Henting av alle Ordre ble gjennomført suksessfullt");
            return Ok(billetter);
        }

        [HttpPost]
        public async Task<ActionResult> LagreOrdre(Billett b)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                _log.LogInformation("Login ikke gyldig!");
                return Unauthorized();
            }

            bool returOk = await _db.lagreBillett(b);
            if (!returOk)
            {
                _log.LogInformation("Lagring av ordre.id -> " + b.Id + " ble ikke gjennomført");
                return BadRequest();
            }
            _log.LogInformation("Lagring av ordre ble gjennomført suksessfullt");
            return Ok();
        }
    }
}
